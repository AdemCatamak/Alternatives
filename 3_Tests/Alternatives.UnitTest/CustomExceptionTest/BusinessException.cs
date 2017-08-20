using System;
using Alternatives.CustomExceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Alternatives.UnitTest.CustomExceptionTest
{
    [TestClass]
    public class BusinessExceptionTest
    {
        [TestMethod]
        public void Alternatives_UnitTest_CustomExceptionTest__BusinessExceptionCreation_WithMessage()
        {
            BusinessException businessException = new BusinessException("Business Message");

            Assert.IsNull(businessException.InnerException);
            Assert.IsNotNull(businessException.ErrorMessage);
        }

        [TestMethod]
        public void Alternatives_UnitTest_CustomExceptionTest__BusinessExceptionCreation_WithMessageAndException()
        {
            Exception ex = new Exception("Common Exception");
            BusinessException businessException = new BusinessException("Business Message", ex);

            Assert.IsNotNull(businessException.InnerException , "BusinessException's inner exception is null");
            Assert.IsFalse(string.IsNullOrEmpty(businessException.ErrorMessage), "BusinessException's error message is null or empty");
            Assert.AreEqual(ex.Message, businessException.InnerException.Message , "Business ex's inner exception message and ex's message are not equal");
        }
    }
}
