using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdemCatamak.Utilities.UnitTest
{
    [TestClass]
    public class CryptographyEngineTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Argument is not null")]
        public void AdemCatamak_Utilities_UnitTest__CryptographyEngineTest_Encrypte_Null()
        {
            CryptographyEngine engine = new CryptographyEngine();
            engine.Encrypt(null);
        }

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest__CryptographyEngineTest_Encrypte()
        {
            const string expected = "wKxS9gMmJL5ali85zkBcsA==";
            const string plainText = "adem";


            CryptographyEngine engine = new CryptographyEngine();
            string actual = engine.Encrypt(plainText);


            Assert.AreEqual(expected, actual, $"{actual} is not expected");
        }

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest__CryptographyEngineTest_Encrypte_DifferentKey()
        {
            const string expected = "21LHLh9bVDumyVE/A8RAzw==";
            const string notExpected = "wKxS9gMmJL5ali85zkBcsA==";
            const string plainText = "adem";


            CryptographyEngine engine = new CryptographyEngine();
            engine.SetKeyCode("newKey");
            string actual = engine.Encrypt(plainText);


            Assert.AreEqual(expected, actual, $"{actual} is not expected");
            Assert.AreNotEqual(notExpected, actual, $"{actual} is not expected");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Argument is not null")]
        public void AdemCatamak_Utilities_UnitTest__CryptographyEngineTest_Decrypte_Null()
        {
            CryptographyEngine engine = new CryptographyEngine();
            engine.Decrypt(null);
        }

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest__CryptographyEngineTest_Decrypte()
        {
            const string expected = "adem";
            const string cipherText = "wKxS9gMmJL5ali85zkBcsA==";


            CryptographyEngine engine = new CryptographyEngine();
            string actual = engine.Decrypt(cipherText);


            Assert.AreEqual(expected, actual, $"{actual} is not expected");
        }

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest__CryptographyEngineTest_Decrypte_DifferentKey()
        {
            const string cipherText = "21LHLh9bVDumyVE/A8RAzw==";
            const string defaultCipherText = "wKxS9gMmJL5ali85zkBcsA==";
            const string expected = "adem";


            CryptographyEngine engine = new CryptographyEngine();
            string defaultActual = engine.Decrypt(defaultCipherText);
            engine.SetKeyCode("newKey");
            string actual = engine.Decrypt(cipherText);


            Assert.AreEqual(expected, actual, $"{actual} is not expected");
            Assert.AreEqual(expected, defaultActual, $"{defaultActual} is not expected");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Argument is not null")]
        public void AdemCatamak_Utilities_UnitTest__CryptographyEngineTest_Hash_Null()
        {
            CryptographyEngine engine = new CryptographyEngine();
            engine.Hashing(null);
        }

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest__CryptographyEngineTest_Hash()
        {
            const string expected = "40-BD-00-15-63-08-5F-C3-51-65-32-9E-A1-FF-5C-5E-CB-DB-BE-EF";
            const string data = "123";


            CryptographyEngine engine = new CryptographyEngine();
            string actual = engine.Hashing(data);


            Assert.AreEqual(expected, actual, $"{actual} is not expected");
        }
    }
}