using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeroStack.DeviceCenter.Infrastructure.EntityConfigurations.Tenants
{
    /// <summary>
    /// 当前租户访问器
    /// </summary>
    public interface ICurrentTenantAccessor
    {
        Guid? TenantId { get; set; }
    }
}
