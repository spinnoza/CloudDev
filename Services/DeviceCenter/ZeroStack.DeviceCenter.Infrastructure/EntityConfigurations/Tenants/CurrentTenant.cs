using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroStack.DeviceCenter.Infrastructure.EntityConfigurations.Tenants;

namespace ZeroStack.DeviceCenter.Infrastructure.EntityConfigurations.Tenants
{
    public class CurrentTenant : ICurrentTenant
    {
        private readonly ICurrentTenantAccessor _currentTenantAccessor;

        public CurrentTenant(ICurrentTenantAccessor currentTenantAccessor) => _currentTenantAccessor = currentTenantAccessor;

        public Guid? Id => _currentTenantAccessor.TenantId;
        /// <summary>
        /// 切换当前租户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IDisposable Change(Guid? id)
        {
            var parentScope = _currentTenantAccessor.TenantId;

            _currentTenantAccessor.TenantId = id;

            return new DisposeAction(() => _currentTenantAccessor.TenantId = parentScope);
        }

        public class DisposeAction : IDisposable
        {
            private readonly Action _action;

            public DisposeAction([NotNull] Action action) => _action = action;

            void IDisposable.Dispose()
            {
                _action();
                GC.SuppressFinalize(this);
            }
        }
    }
}
