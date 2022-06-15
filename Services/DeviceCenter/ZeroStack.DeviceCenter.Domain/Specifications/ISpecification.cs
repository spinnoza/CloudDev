using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ZeroStack.DeviceCenter.Domain.Specifications
{
    public interface ISpecification<T, TResult> : ISpecification<T>
    {
        Expression<Func<T, TResult>>? Selector { get; }
    }

    public interface ISpecification<T>
    {
        IEnumerable<Expression<Func<T, bool>>> WhereExpressions { get; }

        IEnumerable<(Expression<Func<T, object>> KeySelector, OrderTypeEnum OrderType)> OrderExpressions { get; }

        IEnumerable<IIncludeAggregator> IncludeAggregators { get; }

        IEnumerable<string> IncludeStrings { get; }

        IEnumerable<(Expression<Func<T, string>> selector, string searchTerm, int searchGroup)> SearchCriterias { get; }

        int? Take { get; }

        int? Skip { get; }

        bool CacheEnabled { get; }

        string? CacheKey { get; }
    }
}