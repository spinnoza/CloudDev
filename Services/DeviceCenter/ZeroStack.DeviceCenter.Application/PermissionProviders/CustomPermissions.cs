namespace ZeroStack.DeviceCenter.Application.PermissionProviders
{
    public static class CustomPermissions
    {
        public const string GroupName = "ProductStore";

        public static class Products
        {
            public const string Default = GroupName + ".Products";
            public const string Create = Default + ".Create";
            public const string Edit = Default + ".Edit";
            public const string Delete = Default + ".Delete";
        }
    }
}
