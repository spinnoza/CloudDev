namespace ZeroStack.DeviceCenter.Application.Models.Generics
{
    public class PagedRequestModel
    {
        public virtual string? Sorting { get; set; }

        public virtual int PageNumber { get; set; }

        public virtual int PageSize { get; set; }
    }
}
