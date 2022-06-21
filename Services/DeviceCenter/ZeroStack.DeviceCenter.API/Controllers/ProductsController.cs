using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ZeroStack.DeviceCenter.Application.Models.Generics;
using ZeroStack.DeviceCenter.Application.Models.Products;
using ZeroStack.DeviceCenter.Application.Services.Products;

namespace ZeroStack.DeviceCenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductApplicationService _productService;

        public ProductsController(IProductApplicationService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<PagedResponseModel<ProductGetResponseModel>> Get([FromQuery] PagedRequestModel model)
        {
            return await _productService.GetListAsync(model);
        }

        [HttpGet("{id}")]
        public async Task<ProductGetResponseModel> Get(Guid id)
        {
            return await _productService.GetAsync(id);
        }

        [HttpPost]
        public async Task<ProductGetResponseModel> Post([FromBody] ProductCreateOrUpdateRequestModel value)
        {
            return await _productService.CreateAsync(value);
        }

        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            await _productService.DeleteAsync(id);
        }
    }
}
