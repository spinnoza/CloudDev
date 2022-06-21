using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZeroStack.DeviceCenter.Application.Models.Generics;
using ZeroStack.DeviceCenter.Application.Models.Products;
using ZeroStack.DeviceCenter.Domain.Aggregates.ProductAggregate;
using ZeroStack.DeviceCenter.Domain.Repositories;

namespace ZeroStack.DeviceCenter.Application.Services.Products
{
    public class ProductApplicationService : IProductApplicationService
    {
        private readonly IRepository<Product, Guid> _repository;

        public ProductApplicationService(IRepository<Product, Guid> repository) => _repository = repository;

        public async Task<ProductGetResponseModel> CreateAsync(ProductCreateOrUpdateRequestModel requestModel)
        {
            Product product = new Product
            {
                Id = requestModel.Id,
                Name = requestModel.Name,
            };

            product = await _repository.InsertAsync(product, true);

            ProductGetResponseModel responseModel = new ProductGetResponseModel
            {
                Id = product.Id,
                Name = product.Name
            };

            return responseModel;
        }

        public async Task DeleteAsync(Guid id) => await _repository.DeleteAsync(id, true);

        public async Task<ProductGetResponseModel> GetAsync(Guid id)
        {
            Product product = await _repository.GetAsync(id);

            ProductGetResponseModel responseModel = new ProductGetResponseModel
            {
                Id = product.Id,
                Name = product.Name
            };

            return responseModel;
        }

        public async Task<PagedResponseModel<ProductGetResponseModel>> GetListAsync(PagedRequestModel requestModel)
        {
            IQueryable<Product> query = _repository.Query;

            if (requestModel.Sorting is not null)
            {
                query = query.OrderBy(requestModel.Sorting);
            }

            int totalCount = await _repository.AsyncExecuter.CountAsync(_repository.Query);
            IQueryable<Product> products = query.Skip((requestModel.PageNumber - 1) * requestModel.PageSize).Take(requestModel.PageSize);

            List<ProductGetResponseModel> items = products.Select(product => new ProductGetResponseModel
            {
                Id = product.Id,
                Name = product.Name
            }).ToList();

            var responseModel = new PagedResponseModel<ProductGetResponseModel>(items, totalCount);

            return responseModel;
        }

        public async Task<ProductGetResponseModel> UpdateAsync(ProductCreateOrUpdateRequestModel requestModel)
        {
            Product product = new Product
            {
                Id = requestModel.Id,
                Name = requestModel.Name,
            };

            product = await _repository.UpdateAsync(product, true);

            ProductGetResponseModel responseModel = new ProductGetResponseModel
            {
                Id = product.Id,
                Name = product.Name
            };

            return responseModel;
        }
    }
}
