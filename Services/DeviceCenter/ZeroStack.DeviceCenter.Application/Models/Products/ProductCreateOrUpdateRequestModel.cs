using System;

namespace ZeroStack.DeviceCenter.Application.Models.Products
{
    public class ProductCreateOrUpdateRequestModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public DateTimeOffset CreationTime { get; set; }
    }
}
