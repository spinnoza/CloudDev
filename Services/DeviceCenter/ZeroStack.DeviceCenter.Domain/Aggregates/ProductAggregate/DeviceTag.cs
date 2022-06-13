using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroStack.DeviceCenter.Domain.Entities;

namespace ZeroStack.DeviceCenter.Domain.Aggregates.ProductAggregate
{
    public class DeviceTag : BaseEntity<int>
    {
        public string Name { get; set; } = null!;
    }
}
