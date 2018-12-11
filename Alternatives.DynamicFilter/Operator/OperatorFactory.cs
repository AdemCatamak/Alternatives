using System;
using Alternatives.DynamicFilter.Operator.Imp;

namespace Alternatives.DynamicFilter.Operator
{
    public class OperatorFactory
    {
        public Operator Resolve(Operators o)
        {
            switch (o)
            {
                case Operators.GreaterThan:
                    break;
                case Operators.LessThan:
                    return new LessThanOperator();
            }
            throw new ArgumentOutOfRangeException(nameof(o), o, null);
        }
    }
}
