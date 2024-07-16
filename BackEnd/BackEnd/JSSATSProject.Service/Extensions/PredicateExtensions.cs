using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JSSATSProject.Service.Extensions
{
    public static class PredicateExtensions
    {
        public static Expression<Func<T, bool>> AndAlso<T>(
            this Expression<Func<T, bool>> first,
            Expression<Func<T, bool>> second)
        {
            var invokedExpr = Expression.Invoke(second, first.Parameters);
            var combinedExpr = Expression.Lambda<Func<T, bool>>(
                Expression.AndAlso(first.Body, invokedExpr), first.Parameters);
            return combinedExpr;
        }
    }
}
