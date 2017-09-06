using NUnit.Framework;

namespace Alternatives.UnitTest.ExtensionTest.GeneralExtensionTests
{
    public class GetSolutionNameTest
    {
        [Test]
        public void Alternatives_UnitTest_ExtensionsTest__GetSolutionName()
        {
            string expectedSolutionName = "Alternatives";
            string actualSolutionName = Extensions.GeneralExtensions.SolutionName;

            Assert.AreEqual(expectedSolutionName, actualSolutionName);
        }
    }
}