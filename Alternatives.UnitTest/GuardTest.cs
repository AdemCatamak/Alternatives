using Xunit;

namespace Alternatives.UnitTest
{
    public class GuardTest
    {
        [Fact]
        public void IfItIsNull_IfObjectIsNull_ActionWillBeExecuted()
        {
            int value = 0;
            object obj = null;

            obj.IsNull(() => value++);

            Assert.Equal(1, value);
        }

        [Fact]
        public void IfItIsNull_IfObjectIsNotNull_ActionWillNotBeExecuted()
        {
            int value = 0;

            5.IsNull(() => value++);

            Assert.Equal(0, value);
        }

        [Fact]
        public void IsTrue_IfConditionIsTrue_ActionWillBeWork()
        {
            const bool condition = true;
            bool operationGetHit = false;

            condition.IsTrue(() => operationGetHit = true);

            Assert.True(operationGetHit);
        }

        [Fact]
        public void IsTrue_IfConditionIsFalse_ActionWillNotBeWork()
        {
            const bool condition = false;
            bool operationGetHit = false;

            condition.IsTrue(() => operationGetHit = true);

            Assert.False(operationGetHit);
        }

        [Fact]
        public void IsFalse_IfConditionIsTrue_ActionWillNotBeWork()
        {
            const bool valid = true;
            bool operationGetHit = false;

            valid.IsFalse(() => operationGetHit = true);

            Assert.False(operationGetHit);
        }

        [Fact]
        public void IsFalse_IfConditionIsFalse_ActionWillBeWork()
        {
            const bool valid = false;
            bool operationGetHit = false;

            valid.IsFalse(() => operationGetHit = true);

            Assert.True(operationGetHit);
        }

        [Fact]
        public void IsConditionMeet_IfConditionIsTrue_ActionWillBeWork()
        {
            const int value = 5;
            bool operationGetHit = false;

            Guard.IsConditionMeet(() => value > 0, () => operationGetHit = true);

            Assert.True(operationGetHit);
        }

        [Fact]
        public void IsConditionMeet_IfConditionIsFalse_ActionWillNotBeWork()
        {
            const int value = 5;
            bool operationGetHit = false;

            Guard.IsConditionMeet(() => value < 0, () => operationGetHit = true);

            Assert.False(operationGetHit);
        }
    }
}
