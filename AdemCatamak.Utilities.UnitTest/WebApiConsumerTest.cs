using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdemCatamak.Utilities.UnitTest
{
    [TestClass]
    public class WebApiConsumerTest
    {
        [TestMethod, Ignore]
        public void AdemCatamak_Utilities_UnitTest__WebApiConsumerTest__CheckNet()
        {
            // internet bağlantısı varsa true, yoksa false beklenmeli
            const bool expected = true;


            bool actual = WebApiConsumer.CheckNet();


            Assert.AreEqual(expected, actual, $"{actual} is not expected");
        }

        [TestMethod, Ignore]
        public void AdemCatamak_Utilities_UnitTest__WebApiConsumerTest__CheckConnectionAndSetAddress()
        {
            // internet bağlantısı varsa => var olan bir site için true, yoksa false beklenmeli

            WebApiConsumer consumer = new WebApiConsumer("https://google.com.tr");
            bool actualSuccessfullConnection = consumer.CheckConnection();
            consumer.SetBaseUrl("https://notexistsiteaddress.com");
            bool actualFailureConnection = consumer.CheckConnection();


            Assert.IsTrue(actualSuccessfullConnection, $"{actualSuccessfullConnection} is not expected");
            Assert.IsFalse(actualFailureConnection, $"{actualSuccessfullConnection} is not expected");
        }

        [TestMethod, Ignore]
        public void AdemCatamak_Utilities_UnitTest__WebApiConsumerTest__Get()
        {
            // Note: ileride kişisel websitesi üzerinden test edilebilir.
            string expected = string.Empty;


            WebApiConsumer consumer = new WebApiConsumer("http://127.0.0.1");
            string actualResponse = consumer.Get(string.Empty);


            Assert.AreEqual(expected, actualResponse, $"{actualResponse} is not expected");
        }

        [TestMethod, Ignore]
        [ExpectedException(typeof(FriendlyException), "Response is not null")]
        public void AdemCatamak_Utilities_UnitTest__WebApiConsumerTest__Get_ResponseWillBeNull()
        {
            WebApiConsumer consumer = new WebApiConsumer("http://notexistwebsite.com");
            consumer.Get(string.Empty);
        }
    }
}