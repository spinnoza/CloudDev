using System;
using System.Threading.Tasks;

namespace ZeroStack.DeviceCenter.Domain.Repositories
{
    /// <summary>
    /// 数据初始化提供者,实现该接口的类都会实现数据初始化功能
    /// </summary>
    public interface IDataSeedProvider
    {
        Task SeedAsync(IServiceProvider serviceProvider);
    }
}
