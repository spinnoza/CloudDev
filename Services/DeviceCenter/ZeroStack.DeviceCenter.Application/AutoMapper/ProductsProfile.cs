using AutoMapper;
using ZeroStack.DeviceCenter.Application.Models.Products;
using ZeroStack.DeviceCenter.Domain.Aggregates.ProductAggregate;

namespace ZeroStack.DeviceCenter.Application.AutoMapper
{
    public class ProductsProfile : Profile
    {
        public ProductsProfile()
        {
            CreateMap<Product, ProductGetResponseModel>();
            CreateMap<ProductCreateOrUpdateRequestModel, Product>();
        }
    }
}
