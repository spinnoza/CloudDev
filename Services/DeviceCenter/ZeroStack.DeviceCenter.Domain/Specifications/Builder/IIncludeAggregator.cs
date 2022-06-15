namespace ZeroStack.DeviceCenter.Domain.Specifications
{
    public interface IIncludeAggregator
    {
        void AddNavigationPropertyName(string? navigationPropertyName);

        string IncludeString { get; }
    }
}