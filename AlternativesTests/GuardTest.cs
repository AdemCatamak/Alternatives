using System;
using Alternatives;
using Xunit;

namespace AlternativesTests
{
    public class GuardTest
    {
        [Fact]
        public void IsNull_IfObjectIsNull_ActionWillBeExecuted()
        {
            int value = 0;
            object obj = null;

            // ReSharper disable once ExpressionIsAlwaysNull
            Guard.IsNull(obj, () => value++);

            Assert.Equal(1, value);
        }

        [Fact]
        public void IsNull_IfObjectIsNotNull_ActionWillNotBeExecuted()
        {
            int value = 0;

            Guard.IsNull(42, () => value++);

            Assert.Equal(0, value);
        }

        [Fact]
        public void IsNull_IfObjectIsNull_ExceptionIsThrown()
        {
            object obj = null;

            // ReSharper disable once ExpressionIsAlwaysNull
            Assert.Throws<TimeoutException>(() => Guard.IsNull(obj, new TimeoutException("Test")));
        }

        [Fact]
        public void IsNull_IfObjectIsNotNull_ExceptionIsNotThrown()
        {
            Guard.IsNull(42, new TimeoutException("Test"));
        }


        [Fact]
        public void IsNotNull_IfObjectIsNull_ActionWillNotBeExecuted()
        {
            int value = 0;
            object obj = null;

            // ReSharper disable once ExpressionIsAlwaysNull
            Guard.IsNotNull(obj, () => value++);

            Assert.Equal(0, value);
        }

        [Fact]
        public void IsNotNull_IfObjectIsNotNull_ActionWillBeExecuted()
        {
            int value = 0;

            Guard.IsNotNull(42, () => value++);

            Assert.Equal(1, value);
        }

        [Fact]
        public void IsNotNull_IfObjectIsNull_ExceptionIsNotThrown()
        {
            object obj = null;

            // ReSharper disable once ExpressionIsAlwaysNull
            Guard.IsNotNull(obj, new TimeoutException("Test"));
        }

        [Fact]
        public void IsNotNull_IfObjectIsNotNull_ExceptionIsThrown()
        {
            Assert.Throws<TimeoutException>(() => Guard.IsNotNull(42, new TimeoutException("Test")));
        }

        [Fact]
        public void IsTrue_IfConditionIsTrue_ActionWillBeWork()
        {
            const bool condition = true;
            bool operationGetHit = false;

            Guard.IsTrue(condition, () => operationGetHit = true);

            Assert.True(operationGetHit);
        }

        [Fact]
        public void IsTrue_IfConditionIsFalse_ActionWillNotBeWork()
        {
            const bool condition = false;
            bool operationGetHit = false;

            Guard.IsTrue(condition, () => operationGetHit = true);

            Assert.False(operationGetHit);
        }

        [Fact]
        public void IsTrue_IfConditionIsTrue_ExceptionIsThrown()
        {
            const bool condition = true;

            Assert.Throws<TimeoutException>(() => Guard.IsTrue(condition, new TimeoutException()));
        }

        [Fact]
        public void IsTrue_IfConditionIsFalse_ExceptionIsNotThrown()
        {
            const bool condition = false;

            Guard.IsTrue(condition, new ArgumentException());
        }


        [Fact]
        public void IsFalse_IfConditionIsTrue_ActionWillNotBeWork()
        {
            const bool valid = true;
            bool operationGetHit = false;

            Guard.IsFalse(valid, () => operationGetHit = true);

            Assert.False(operationGetHit);
        }

        [Fact]
        public void IsFalse_IfConditionIsFalse_ActionWillBeWork()
        {
            const bool valid = false;
            bool operationGetHit = false;

            Guard.IsFalse(valid, () => operationGetHit = true);

            Assert.True(operationGetHit);
        }

        [Fact]
        public void IsFalse_IfConditionIsTrue_ExceptionIsNotThrown()
        {
            const bool valid = true;

            Guard.IsFalse(valid, new ArgumentException());
        }

        [Fact]
        public void IsFalse_IfConditionIsFalse_ExceptionIsThrown()
        {
            const bool valid = false;

            Assert.Throws<AccessViolationException>(() => Guard.IsFalse(valid, new AccessViolationException()));
        }


        [Fact]
        public void IfConditionMeet_IfConditionIsTrue_ActionWillBeWork()
        {
            const int value = 5;
            bool operationGetHit = false;

            Guard.IfConditionMeet(() => value > 0, () => operationGetHit = true);

            Assert.True(operationGetHit);
        }

        [Fact]
        public void IfConditionMeet_IfConditionIsFalse_ActionWillNotBeWork()
        {
            const int value = 5;
            bool operationGetHit = false;

            Guard.IfConditionMeet(() => value < 0, () => operationGetHit = true);

            Assert.False(operationGetHit);
        }
    }
}
