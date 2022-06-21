using System;

namespace ZeroStack.DeviceCenter.Domain.Entities
{
    public interface IMultiTenant
    {
        Guid? TenantId { get; }
    }
}
