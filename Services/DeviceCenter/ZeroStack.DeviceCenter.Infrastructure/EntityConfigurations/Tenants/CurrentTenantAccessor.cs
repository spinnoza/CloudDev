using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ZeroStack.DeviceCenter.Infrastructure.EntityConfigurations.Tenants
{
    public class CurrentTenantAccessor : ICurrentTenantAccessor
    {
        private readonly AsyncLocal<Guid?> _currentScope = new AsyncLocal<Guid?>();

        public Guid? TenantId { get => _currentScope.Value; set => _currentScope.Value = value; }
    }
}
