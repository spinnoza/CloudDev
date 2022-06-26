namespace ZeroStack.DeviceCenter.Application.Services.Permissions
{
    /// <summary>
    /// 权限定义提供者(实现该接口的类都能向PermissionDefinitionContext 添加权限的定义)
    /// </summary>
    public interface IPermissionDefinitionProvider
    {
        void Define(PermissionDefinitionContext context);
    }
}
