using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ZeroStack.DeviceCenter.Domain.Aggregates.PermissionAggregate;

namespace ZeroStack.DeviceCenter.Application.Services.Permissions
{
    public class PermissionStore : IPermissionStore
    {
        private readonly IPermissionGrantRepository _repository;

        private readonly IPermissionDefinitionManager _permissionDefinitionManager;

        private readonly IDistributedCache _distributedCache;

        private readonly ILogger<PermissionStore> _logger;

        private const string CacheKeyFormat = "pn:{0},pk:{1},n:{2}"; //<object-type>:<id>:<field1>.<field2> Or <object-type>:<id>:<field1>-<field2>

        public PermissionStore(IPermissionGrantRepository repository, IPermissionDefinitionManager permissionDefinitionManager, IDistributedCache distributedCache, ILogger<PermissionStore> logger)
        {
            _repository = repository;
            _permissionDefinitionManager = permissionDefinitionManager;
            _distributedCache = distributedCache;
            _logger = logger ?? NullLogger<PermissionStore>.Instance;
        }

        public async Task<bool> IsGrantedAsync([NotNull] string name, [MaybeNull] string providerName, [MaybeNull] string providerKey)
        {
            return (await GetCacheItemAsync(name, providerName, providerKey)).IsGranted;
        }

        public async Task<MultiplePermissionGrantResult> IsGrantedAsync([NotNull] string[] names, [MaybeNull] string providerName, [MaybeNull] string providerKey)
        {
            if (names is null)
            {
                throw new ArgumentNullException(nameof(names));
            }

            MultiplePermissionGrantResult result = new MultiplePermissionGrantResult();

            if (names.Length == 1)
            {
                var name = names.First();
                result.Result.Add(name, await IsGrantedAsync(names.First(), providerName, providerKey) ? PermissionGrantResult.Granted : PermissionGrantResult.Undefined);
                return result;
            }

            var cacheItems = await GetCacheItemsAsync(names, providerName, providerKey);

            foreach (var (Key, IsGranted) in cacheItems)
            {
                result.Result.Add(GetPermissionInfoFormCacheKey(Key).Name, IsGranted ? PermissionGrantResult.Granted : PermissionGrantResult.Undefined);
            }

            return result;
        }

        protected virtual async Task<(string Key, bool IsGranted)> GetCacheItemAsync(string name, string providerName, string providerKey)
        {
            var cacheKey = string.Format(CacheKeyFormat, providerName, providerKey, name);

            _logger.LogDebug($"PermissionStore.GetCacheItemAsync: {cacheKey}");

            string cacheItem = await _distributedCache.GetStringAsync(cacheKey);

            if (cacheItem is not null)
            {
                _logger.LogDebug($"Found in the cache: {cacheKey}");
                return (cacheKey, Convert.ToBoolean(cacheItem));
            }

            _logger.LogDebug($"Not found in the cache: {cacheKey}");

            return await SetCacheItemsAsync(providerName, providerKey, name);
        }

        protected virtual async Task<(string Key, bool IsGranted)> SetCacheItemsAsync(string providerName, string providerKey, string currentName)
        {
            var permissions = _permissionDefinitionManager.GetPermissions();

            _logger.LogDebug($"Getting all granted permissions from the repository for this provider name,key: {providerName},{providerKey}");

            var grantedPermissionsHashSet = new HashSet<string>((await _repository.GetListAsync(providerName, providerKey)).Select(p => p.Name));

            _logger.LogDebug($"Setting the cache items. Count: {permissions.Count}");

            var cacheItems = new List<(string Key, bool IsGranted)>();

            bool currentResult = false;

            foreach (var permission in permissions)
            {
                var isGranted = grantedPermissionsHashSet.Contains(permission.Name);

                cacheItems.Add((string.Format(CacheKeyFormat, providerName, providerKey, permission.Name), isGranted));

                if (permission.Name == currentName)
                {
                    currentResult = isGranted;
                }
            }

            List<Task> setCacheItemTasks = new List<Task>();

            foreach ((string Key, bool IsGranted) in cacheItems)
            {
                setCacheItemTasks.Add(_distributedCache.SetStringAsync(Key, IsGranted.ToString()));
            }

            await Task.WhenAll(setCacheItemTasks);

            _logger.LogDebug($"Finished setting the cache items. Count: {permissions.Count}");

            return (string.Format(CacheKeyFormat, providerName, providerKey, currentName), currentResult);
        }

        protected virtual async Task<List<(string Key, bool IsGranted)>> GetCacheItemsAsync(string[] names, string providerName, string providerKey)
        {
            var cacheKeys = names.Select(x => string.Format(CacheKeyFormat, x, providerName, providerKey)).ToList();

            _logger.LogDebug($"PermissionStore.GetCacheItemAsync: {string.Join(",", cacheKeys)}");

            List<Task<(string key, string value)>> getCacheItemTasks = new List<Task<(string, string)>>();

            foreach (string cacheKey in cacheKeys)
            {
                getCacheItemTasks.Add(Task.Run(() => (cacheKey, _distributedCache.GetStringAsync(cacheKey).Result)));
            }

            var cacheItems = await Task.WhenAll(getCacheItemTasks);

            if (cacheItems.All(x => x.value is not null))
            {
                _logger.LogDebug($"Found in the cache: {string.Join(",", cacheKeys)}");
                return Array.ConvertAll(cacheItems, i => (i.key, Convert.ToBoolean(i.value))).ToList();
            }

            var notCacheKeys = cacheItems.Where(x => x.value is null).Select(x => x.key).ToList();

            _logger.LogDebug($"Not found in the cache: {string.Join(",", notCacheKeys)}");

            return await SetCacheItemsAsync(providerName, providerKey, notCacheKeys);
        }

        protected virtual async Task<List<(string Key, bool IsGranted)>> SetCacheItemsAsync(string providerName, string providerKey, List<string> notCacheKeys)
        {
            var permissions = _permissionDefinitionManager.GetPermissions().Where(x => notCacheKeys.Any(k => GetPermissionInfoFormCacheKey(k).Name == x.Name)).ToList();

            _logger.LogDebug($"Getting not cache granted permissions from the repository for this provider name,key: {providerName},{providerKey}");

            var grantedPermissionsHashSet = new HashSet<string>((await _repository.GetListAsync(notCacheKeys.Select(k => GetPermissionInfoFormCacheKey(k).Name).ToArray(), providerName, providerKey)).Select(p => p.Name));

            _logger.LogDebug($"Setting the cache items. Count: {permissions.Count}");

            var cacheItems = new List<(string Key, bool IsGranted)>();

            foreach (PermissionDefinition? permission in permissions)
            {
                var isGranted = grantedPermissionsHashSet.Contains(permission.Name);
                cacheItems.Add((string.Format(CacheKeyFormat, providerName, providerKey, permission.Name), isGranted));
            }

            List<Task> setCacheItemTasks = new List<Task>();

            foreach ((string Key, bool IsGranted) in cacheItems)
            {
                setCacheItemTasks.Add(_distributedCache.SetStringAsync(Key, IsGranted.ToString()));
            }

            await Task.WhenAll(setCacheItemTasks);

            _logger.LogDebug($"Finished setting the cache items. Count: {permissions.Count}");

            return cacheItems;
        }

        protected virtual (string ProviderName, string ProviderKey, string Name) GetPermissionInfoFormCacheKey(string key)
        {
            string pattern = @"^pn:(?<providerName>.+),pk:(?<providerKey>.+),n:(?<name>.+)$";

            Match match = Regex.Match(key, pattern, RegexOptions.IgnoreCase);

            string providerName = match.Groups["providerName"].Value;
            string providerKey = match.Groups["providerKey"].Value;
            string name = match.Groups["name"].Value;

            return (providerName, providerKey, name);
        }
    }
}
