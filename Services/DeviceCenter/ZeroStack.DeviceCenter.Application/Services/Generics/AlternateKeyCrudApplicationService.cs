using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZeroStack.DeviceCenter.Application.Models.Generics;
using ZeroStack.DeviceCenter.Domain.Entities;
using ZeroStack.DeviceCenter.Domain.Repositories;

namespace ZeroStack.DeviceCenter.Application.Services.Generics
{
    public abstract class AlternateKeyCrudApplicationService<TEntity, TKey, TGetResponseModel, TGetListRequestModel, TGetListResponseModel, TCreateRequestModel, TUpdateRequestModel> : ICrudApplicationService<TKey, TGetResponseModel, TGetListRequestModel, TGetListResponseModel, TCreateRequestModel, TUpdateRequestModel> where TEntity : BaseEntity
    {
        protected IRepository<TEntity> Repository { get; }

        private readonly IMapper _mapper;

        public AlternateKeyCrudApplicationService(IRepository<TEntity> repository, IMapper mapper)
        {
            Repository = repository;
            _mapper = mapper;
        }

        public virtual async Task<TGetResponseModel> CreateAsync(TCreateRequestModel requestModel)
        {
            TEntity entity = _mapper.Map<TEntity>(requestModel);
            await Repository.InsertAsync(entity, true);
            return _mapper.Map<TGetResponseModel>(entity);
        }

        public virtual async Task DeleteAsync(TKey id) => await DeleteByIdAsync(id);

        protected abstract Task DeleteByIdAsync(TKey id);

        public virtual async Task<TGetResponseModel> GetAsync(TKey id) => _mapper.Map<TGetResponseModel>(await GetEntityByIdAsync(id));

        protected abstract Task<TEntity> GetEntityByIdAsync(TKey id);

        public virtual async Task<PagedResponseModel<TGetListResponseModel>> GetListAsync(TGetListRequestModel requestModel)
        {
            IQueryable<TEntity> query = CreateFilteredQuery(requestModel);

            int totalCount = await Repository.AsyncExecuter.CountAsync(query);

            query = ApplySorting(query, requestModel);
            query = ApplyPaging(query, requestModel);

            var entities = await Repository.AsyncExecuter.ToListAsync(query);
            var entityDtos = _mapper.Map<List<TGetListResponseModel>>(entities);

            return new PagedResponseModel<TGetListResponseModel>(entityDtos, totalCount);
        }

        protected virtual IQueryable<TEntity> CreateFilteredQuery(TGetListRequestModel requestModel) => Repository.Query;

        protected virtual IQueryable<TEntity> ApplySorting(IQueryable<TEntity> query, TGetListRequestModel requestModel)
        {
            if (requestModel is PagedRequestModel model && !string.IsNullOrWhiteSpace(model.Sorting))
            {
                return query.OrderBy(model.Sorting);
            }

            return query;
        }

        protected virtual IQueryable<TEntity> ApplyPaging(IQueryable<TEntity> query, TGetListRequestModel requestModel)
        {
            if (requestModel is PagedRequestModel model)
            {
                return query.Skip((model.PageNumber - 1) * model.PageSize).Take(model.PageSize);
            }

            return query;
        }

        public async Task<TGetResponseModel> UpdateAsync(TUpdateRequestModel requestModel)
        {
            TEntity entity = _mapper.Map<TEntity>(requestModel);
            await Repository.UpdateAsync(entity, true);
            return _mapper.Map<TGetResponseModel>(entity);
        }
    }
}
