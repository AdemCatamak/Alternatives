using System;

namespace Alternatives
{
    public static class Guard
    {
        public static void IsNull<T>(object instance, T exception) where T : Exception
        {
            IsNull(instance, () => throw exception);
        }
        public static void IsNull(object instance, Action action)
        {
            IfConditionMeet(instance == null, action);
        }

        public static void IsTrue<T>(bool condition, T exception) where T : Exception
        {
            IsTrue(condition, () => throw exception);
        }
        public static void IsTrue(bool condition, Action action)
        {
            IfConditionMeet(condition, action);
        }

        public static void IsFalse<T>(bool condition, T exception) where T : Exception
        {
            IsFalse(condition, () => throw exception);
        }
        public static void IsFalse(bool condition, Action action)
        {
            IfConditionMeet(!condition, action);
        }

        public static void IfConditionMeet(Func<bool> conditionFunc, Action action)
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
