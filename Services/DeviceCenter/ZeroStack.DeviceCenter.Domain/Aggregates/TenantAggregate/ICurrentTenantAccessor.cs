using System;

namespace ZeroStack.DeviceCenter.Domain.Aggregates.TenantAggregate
{
    /// <summary>
    /// 当前租户访问器
    /// </summary>
    public interface ICurrentTenantAccessor
    {
        Guid? TenantId { get; set; }
    }
}
