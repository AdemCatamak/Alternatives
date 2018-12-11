using System;
using System.Linq;
using System.Linq.Expressions;

namespace Alternatives.DynamicFilter.Operator.Imp
{
    internal class LessThanOperator : Operator
    {
        public override IQueryable<T> Apply<T>(IQueryable<T> queryable, string propertyName, object value)
        {
            ParameterExpression parameterExpression = Expression.Parameter(typeof(T));
            ConstantExpression constantExpression = Expression.Constant(value, value.GetType());
            BinaryExpression binaryExpression = Expression.LessThan(Expression.Property(parameterExpression, propertyName),
                                                                    constantExpression);

            Func<T, bool> comparison = Expression.Lambda<Func<T, bool>>(binaryExpression, parameterExpression)
                                                 .Compile();

            return queryable.AsEnumerable().
                             Where(comparison)
                            .AsQueryable();
        }
    }
}