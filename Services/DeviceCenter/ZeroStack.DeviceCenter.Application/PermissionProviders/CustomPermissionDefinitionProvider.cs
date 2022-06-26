using Microsoft.Extensions.Localization;
using System.Reflection;
using ZeroStack.DeviceCenter.Application.Services.Permissions;

namespace ZeroStack.DeviceCenter.Application.PermissionProviders
{
    public class CustomPermissionDefinitionProvider : IPermissionDefinitionProvider
    {
        private readonly IStringLocalizer _localizer;

        public CustomPermissionDefinitionProvider(IStringLocalizerFactory factory)
        {
            _localizer = factory.Create("Permissions.CustomPermission", Assembly.GetExecutingAssembly().FullName!);
        }

        public void Define(PermissionDefinitionContext context)
        {
            var productGroup = context.AddGroup(CustomPermissions.GroupName, _localizer["Welcome"]);

            var productManagement = productGroup.AddPermission(CustomPermissions.Products.Default, _localizer["Permission:ProductStore.Products"]);

            productManagement.AddChild(CustomPermissions.Products.Create, _localizer["Permission:ProductStore.Products.Creeate"]);
            productManagement.AddChild(CustomPermissions.Products.Edit, _localizer["Permission:ProductStore.Products.Edit"]);
            productManagement.AddChild(CustomPermissions.Products.Delete, _localizer["Permission:ProductStore.Products.Delete"]);
        }
    }
}
