using System;

namespace ZeroStack.DeviceCenter.Domain.Aggregates.TenantAggregate
{
    public interface ICurrentTenant
    {
        Guid? Id { get; }
        /// <summary>
        /// 切换当前租户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IDisposable Change(Guid? id);
    }
}
