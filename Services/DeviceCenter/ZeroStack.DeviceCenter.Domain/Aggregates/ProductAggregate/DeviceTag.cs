using ZeroStack.DeviceCenter.Domain.Entities;

namespace ZeroStack.DeviceCenter.Domain.Aggregates.ProductAggregate
{
    public class DeviceTag : BaseEntity<int>
    {
        public string Name { get; set; } = null!;
    }
}
