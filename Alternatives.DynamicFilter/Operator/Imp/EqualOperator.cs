using System;
using System.Linq;
using System.Linq.Expressions;

namespace Alternatives.DynamicFilter.Operator.Imp
{
    internal class EqualOperator : Operator
    {
        public override IQueryable<T> Apply<T>(IQueryable<T> collection, string propertyName, object value)
        {
            ParameterExpression parameterExpression = Expression.Parameter(typeof(T));
            ConstantExpression constantExpression = Expression.Constant(value, value.GetType());
            BinaryExpression binaryExpression = Expression.Equal(Expression.Property(parameterExpression, propertyName),
                                                                 constantExpression);

            Func<T, bool> comparison = Expression.Lambda<Func<T, bool>>(binaryExpression, parameterExpression)
                                                 .Compile();

            return collection.AsEnumerable()
                             .Where(comparison)
                             .AsQueryable();
        }

        public override bool DoesOperatorCanApplied(Type propertyType)
        {
            if (!(propertyType.IsInstanceOfType(typeof(IComparable)) || propertyType.IsPrimitive))
            {
                return false;
            }

            return true;
        }
    }
}