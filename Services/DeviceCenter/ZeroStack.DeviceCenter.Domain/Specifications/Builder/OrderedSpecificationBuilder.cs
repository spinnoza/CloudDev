namespace ZeroStack.DeviceCenter.Domain.Specifications
{
    public class OrderedSpecificationBuilder<T> : IOrderedSpecificationBuilder<T>
    {
        public Specification<T> Specification { get; }

        public OrderedSpecificationBuilder(Specification<T> specification) => Specification = specification;
    }
}
