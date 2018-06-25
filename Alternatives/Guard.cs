using System;

namespace Alternatives
{
    public static class Guard
    {
        public static void IsNull(this object instance, Action action)
        {
            IfConditionMeet(instance == null, action);
        }

        public static void IsTrue(this bool condition, Action action)
        {
            IfConditionMeet(condition, action);
        }

        public static void IsFalse(this bool condition, Action action)
        {
            IfConditionMeet(!condition, action);
        }

        public static void IsConditionMeet(Func<bool> conditionFunc, Action action)
        {
            bool result = conditionFunc();
            IfConditionMeet(result, action);
        }

        public static void IfConditionMeet(bool condition, Action action)
        {
            if (condition)
            {
                action();
            }
        }
    }
}
