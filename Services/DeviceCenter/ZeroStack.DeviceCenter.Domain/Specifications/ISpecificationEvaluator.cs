using System.Linq;

namespace ZeroStack.DeviceCenter.Domain.Specifications
{
    public interface ISpecificationEvaluator<T> where T : class
    {
        IQueryable<TResult> GetQuery<TResult>(IQueryable<T> inputQuery, ISpecification<T, TResult> specification);

        IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> specification);
    }
}