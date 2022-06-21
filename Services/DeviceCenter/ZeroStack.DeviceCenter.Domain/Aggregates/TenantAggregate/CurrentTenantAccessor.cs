using System;
using System.Threading;

namespace ZeroStack.DeviceCenter.Domain.Aggregates.TenantAggregate
{
    public class CurrentTenantAccessor : ICurrentTenantAccessor
    {
        private readonly AsyncLocal<Guid?> _currentScope = new AsyncLocal<Guid?>();

        public Guid? TenantId { get => _currentScope.Value; set => _currentScope.Value = value; }
    }
}
