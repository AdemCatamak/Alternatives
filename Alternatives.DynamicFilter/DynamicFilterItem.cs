using System;
using System.Linq;
using System.Reflection;
using Alternatives.DynamicFilter.Operator;

namespace Alternatives.DynamicFilter
{
    public class DynamicFilterItem
    {
        public string PropertyName { get; }
        public object Value { get; }
        public Operators Op { get; }

        public DynamicFilterItem(string propertyName, Operators op, object value)
        {
            PropertyName = propertyName ?? throw new ArgumentNullException($"{nameof(propertyName)} should not be null");
            Value = value ?? throw new ArgumentNullException($"{nameof(value)} should not be null");
            if (!Enum.IsDefined(typeof(Operators), op))
                throw new ArgumentException($"{nameof(op)} should not be null");
            Op = op;
        }

        public IQueryable<T> Apply<T>(IQueryable<T> queryable)
        {
            CheckProperty(typeof(T));
            Type propertyType = GetPropertyType(typeof(T));
            DoesMatchPropertyTypeAndObjectType(propertyType, Value.GetType());

            var operationFactory = new OperatorFactory();
            Operator.Operator operation = operationFactory.Resolve(Op);

            if (!operation.DoesOperatorCanApplied(propertyType))
            {
                throw new InvalidOperationException($"{Op} operation could not applied this type -{propertyType.FullName}-");
            }

            return operation.Apply<T>(queryable, PropertyName, Value);
        }



        private void DoesMatchPropertyTypeAndObjectType(Type propertyType, Type objectType)
        {
            if (propertyType != objectType)
            {
                throw new ArgumentException($"Supplied type -{objectType.FullName}- does not match property type -{propertyType.FullName}-");
            }
        }

        private Type GetPropertyType(Type type)
        {
            PropertyInfo propertyInfo = type.GetProperty(PropertyName);
            return propertyInfo.PropertyType;
        }

        private void CheckProperty(Type type)
        {
            PropertyInfo propertyInfo = type.GetProperty(PropertyName);
            if (propertyInfo == null)
            {
                throw new ArgumentException($"{PropertyName} is not exist for type {type.FullName}");
            }
        }
    }
}