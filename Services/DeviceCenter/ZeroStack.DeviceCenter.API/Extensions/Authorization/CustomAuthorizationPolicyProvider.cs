using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using ZeroStack.DeviceCenter.Application.Services.Permissions;

namespace ZeroStack.DeviceCenter.API.Extensions.Authorization
{
    /// <summary>
    /// 自定义授权策略提供者,重写asp.net core identity 中的 DefaultAuthorizationPolicyProvider
    /// 将自己的PermissionDefinitionContext中的所有权限都当以policy的形式作为策略授权
    /// 当然也支持框架原生的AddPolicy的方法
    /// </summary>
    public class CustomAuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
    {
        private readonly IPermissionDefinitionManager _permissionDefinitionManager;

        public CustomAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options, IPermissionDefinitionManager permissionDefinitionManager) : base(options)
        {
            _permissionDefinitionManager = permissionDefinitionManager;
        }

        public async override Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
            AuthorizationPolicy? policy = await base.GetPolicyAsync(policyName);

            if (policy is not null)
            {
                return policy;
            }

            var permission = _permissionDefinitionManager.GetOrNull(policyName);

            if (permission is not null)
            {
                var policyBuilder = new AuthorizationPolicyBuilder(Array.Empty<string>());
                policyBuilder.Requirements.Add(new PermissionRequirement(policyName));

                return policyBuilder.Build();
            }

            return null;
        }
    }
}
