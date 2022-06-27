using System;
using ZeroStack.DeviceCenter.Domain.Entities;

namespace ZeroStack.DeviceCenter.Domain.Aggregates.PermissionAggregate
{
    /// <summary>
    /// 授权决定
    /// 1.角色为admin的用户可以修改产品
    /// 2.id为003 的用户可以创建项目
    /// 3.部门为销售的用户可以浏览产品列表
    /// .....
    /// </summary>
    public class PermissionGrant : BaseAggregateRoot<Guid>, IMultiTenant
    {
        public Guid? TenantId { get; set; }

        public string Name { get; set; } = null!;

        public string ProviderName { get; set; } = null!;

        public string ProviderKey { get; set; } = null!;
    }
}
