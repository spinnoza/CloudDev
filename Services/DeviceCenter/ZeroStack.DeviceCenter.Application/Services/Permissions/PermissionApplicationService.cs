﻿using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ZeroStack.DeviceCenter.Application.Models.Permissions;
using ZeroStack.DeviceCenter.Domain.Aggregates.PermissionAggregate;

namespace ZeroStack.DeviceCenter.Application.Services.Permissions
{
    public class PermissionApplicationService : IPermissionApplicationService
    {
        private readonly IPermissionDefinitionManager _permissionDefinitionManager;

        private readonly IPermissionGrantRepository _permissionGrantRepository;

        private readonly IStringLocalizer _localizer;

        public PermissionApplicationService(IPermissionDefinitionManager permissionDefinitionManager, IPermissionGrantRepository permissionGrantRepository, IStringLocalizerFactory factory)
        {
            _localizer = factory.Create("Permissions.CustomPermission", Assembly.GetExecutingAssembly().FullName!);
            _permissionGrantRepository = permissionGrantRepository;
            _permissionDefinitionManager = permissionDefinitionManager;
        }

        public async Task<PermissionListResponseModel> GetAsync([NotNull] string providerName, [NotNull] string providerKey)
        {
            var result = new PermissionListResponseModel { EntityDisplayName = providerKey, Groups = new List<PermissionGroupModel>() };

            foreach (var group in _permissionDefinitionManager.GetGroups())
            {
                PermissionGroupModel permissionGroupModel = new PermissionGroupModel
                {
                    DisplayName = (_localizer[group.Name] ?? group.Name)!,
                    Name = group.Name,
                    Permissions = new List<PermissionGrantModel>()
                };

                foreach (PermissionDefinition? permission in group.GetPermissionsWithChildren())
                {
                    if (permission.IsEnabled && (!permission.AllowedProviders.Any() || permission.AllowedProviders.Contains(providerName)))
                    {
                        PermissionGrantModel permissionGrantModel = new PermissionGrantModel
                        {
                            Name = permission.Name,
                            DisplayName = (_localizer[permission.DisplayName ?? permission.Name] ?? permission.Name)!,
                            ParentName = permission.Parent?.Name!,
                            AllowedProviders = permission.AllowedProviders
                        };

                        if (permission.AllowedProviders.Any() && !permission.AllowedProviders.Contains(providerName))
                        {
                            throw new ApplicationException($"The permission named {permission.Name} has not compatible with the provider named {providerName}");
                        }

                        if (!permission.IsEnabled)
                        {
                            throw new ApplicationException($"The permission named {permission.Name} is disabled");
                        }

                        PermissionGrant permissionGrant = await _permissionGrantRepository.FindAsync(permission.Name, providerName, providerKey);

                        permissionGrantModel.IsGranted = permissionGrant != null;

                        permissionGroupModel.Permissions.Add(permissionGrantModel);
                    }
                }

                if (permissionGroupModel.Permissions.Any())
                {
                    result.Groups.Add(permissionGroupModel);
                }
            }

            return result;
        }

        public async Task UpdateAsync([NotNull] string providerName, [NotNull] string providerKey, IEnumerable<PermissionUpdateRequestModel> requestModels)
        {
            foreach (PermissionUpdateRequestModel requestModel in requestModels)
            {
                var permission = _permissionDefinitionManager.Get(requestModel.Name);

                if (permission.AllowedProviders.Any() && !permission.AllowedProviders.Contains(providerName))
                {
                    throw new ApplicationException($"The permission named {permission.Name} has not compatible with the provider named {providerName}");
                }

                if (!permission.IsEnabled)
                {
                    throw new ApplicationException($"The permission named {permission.Name} is disabled");
                }

                PermissionGrant permissionGrant = await _permissionGrantRepository.FindAsync(requestModel.Name, providerName, providerKey);

                if (requestModel.IsGranted && permissionGrant is null)
                {
                    await _permissionGrantRepository.InsertAsync(new PermissionGrant { Name = requestModel.Name, ProviderName = providerName, ProviderKey = providerKey });
                }

                if (!requestModel.IsGranted && permissionGrant is not null)
                {
                    await _permissionGrantRepository.DeleteAsync(permissionGrant);
                }
            }

            await _permissionGrantRepository.UnitOfWork.SaveChangesAsync();
        }
    }
}
