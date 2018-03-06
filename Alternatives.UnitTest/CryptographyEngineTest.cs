using System;
using Xunit;

namespace Alternatives.UnitTest
{
    public class CryptographyEngineTest
    {
        [Fact]
        public void CryptographyEngineTest_WhenEncrypteNullAsObject_ThrowsArgumentNullException()
        {
            CryptographyEngine engine = new CryptographyEngine();
            Assert.Throws<ArgumentNullException>(() => { engine.Encrypt(null); });
        }

        [Fact]
        public void CryptographyEngineTest_WhenEncrypteValidStringAsObject_ResponseMustBeSuccessfull()
        {
            const string expected = "wKxS9gMmJL5ali85zkBcsA==";
            const string plainText = "adem";


            CryptographyEngine engine = new CryptographyEngine();
            string actual = engine.Encrypt(plainText);


            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CryptographyEngineTest_WhenEncrypteWithDifferentKey_ResultMustBeDifferentFromDefaultResult()
        {
            const string expected = "21LHLh9bVDumyVE/A8RAzw==";
            const string notExpected = "wKxS9gMmJL5ali85zkBcsA==";
            const string plainText = "adem";


            CryptographyEngine engine = new CryptographyEngine();
            engine.SetKeyCode("newKey");
            string actual = engine.Encrypt(plainText);


            Assert.Equal(expected, actual);
            Assert.NotEqual(notExpected, actual);
        }

        [Fact]
        public void CryptographyEngineTest_WhenEncrypteWithEmptyStringAsKey_ResponseMustBeEncrypteWithLastValidKey()
        {
            const string plainText = "adem";


            CryptographyEngine engine = new CryptographyEngine(string.Empty);
            string actual = engine.Encrypt(plainText);

            Assert.NotNull(actual);
            Assert.NotEqual(actual, string.Empty);
            Assert.NotEqual(plainText, actual);
        }


        [Fact]
        public void CryptographyEngineTest_WhenDecrypteNullAsObject_ThrowsArgumentNullException()
        {
            CryptographyEngine engine = new CryptographyEngine();

            Assert.Throws<ArgumentNullException>(() => { engine.Decrypt(null); });
        }

        [Fact]
        public void CryptographyEngineTest_WhenDecrypteStringWithValidForm_ResponseMustBeSuccessfull()
        {
            const string expected = "adem";
            const string cipherText = "wKxS9gMmJL5ali85zkBcsA==";


            CryptographyEngine engine = new CryptographyEngine();
            string actual = engine.Decrypt(cipherText);


            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CryptographyEngineTest_WhenDecrypteWithDifferentKey_ResponseMustBeDifferentFromDefaultResponse()
        {
            const string cipherText = "21LHLh9bVDumyVE/A8RAzw==";
            const string defaultCipherText = "wKxS9gMmJL5ali85zkBcsA==";
            const string expected = "adem";


            CryptographyEngine engine = new CryptographyEngine();
            string defaultActual = engine.Decrypt(defaultCipherText);
            engine.SetKeyCode("newKey");
            string actual = engine.Decrypt(cipherText);


            Assert.Equal(expected, actual);
            Assert.Equal(expected, defaultActual);
        }


        [Fact]
        public void CryptographyEngineTest_WhenHashNullAsObject_ThrowsArgumentNullException()
        {
            CryptographyEngine engine = new CryptographyEngine();

            Assert.Throws<ArgumentNullException>(() => { engine.Hashing(null); });
        }

        [Fact]
        public void CryptographyEngineTest_WhenHashEmptyStringAsObject_ResponseMustBeSuccessfullAndDifferentFromEmptyString()
        {
            const string expected = "DA-39-A3-EE-5E-6B-4B-0D-32-55-BF-EF-95-60-18-90-AF-D8-07-09";
            const string data = "";

            CryptographyEngine engine = new CryptographyEngine();
            string actual = engine.Hashing(data);


            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CryptographyEngineTest_WhenHashStringAsObject_ResponseMustBeSuccessfull()
        {
            const string expected = "40-BD-00-15-63-08-5F-C3-51-65-32-9E-A1-FF-5C-5E-CB-DB-BE-EF";
            const string data = "123";

            CryptographyEngine engine = new CryptographyEngine();
            string actual = engine.Hashing(data);


            Assert.Equal(expected, actual);
        }
    }
}