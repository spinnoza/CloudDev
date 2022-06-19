using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeroStack.DeviceCenter.Infrastructure.EntityConfigurations.Tenants
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
