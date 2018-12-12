using System;
using System.Linq;
using System.Linq.Expressions;

namespace Alternatives.DynamicFilter.Operator.Imp
{
    internal class GreaterThanOperator : Operator
    {
        public override IQueryable<T> Apply<T>(IQueryable<T> collection, string propertyName, object value)
        {
            ParameterExpression parameterExpression = Expression.Parameter(typeof(T));
            ConstantExpression constantExpression = Expression.Constant(value, value.GetType());
            BinaryExpression binaryExpression = Expression.GreaterThan(Expression.Property(parameterExpression, propertyName),
                                                                       constantExpression);

            Func<T, bool> comparison = Expression.Lambda<Func<T, bool>>(binaryExpression, parameterExpression)
                                                 .Compile();

            return collection.AsEnumerable()
                             .Where(comparison)
                             .AsQueryable();
        }

        public override bool DoesOperatorCanApplied(Type propertyType)
        {
            if (!propertyType.IsInstanceOfType(typeof(IComparable)))
            {
                return false;
                //throw new InvalidOperationException($"{Op} does not accept {propertyType}");
            }

            return true;
        }
    }
}