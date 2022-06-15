namespace ZeroStack.DeviceCenter.Domain.Specifications
{
    public interface IIncludableSpecificationBuilder<T, out TProperty> : ISpecificationBuilder<T>
    {
        IIncludeAggregator Aggregator { get; }
    }
}