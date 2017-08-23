using System;
using NUnit.Framework;

namespace Alternatives.UnitTest
{
    public class CryptographyEngineTest
    {
        [Test]
        public void Alternatives_UnitTest__CryptographyEngineTest_Encrypte_Null()
        {
            CryptographyEngine engine = new CryptographyEngine();
            Assert.Throws<ArgumentNullException>(() =>
                                                          {
                                                              engine.Encrypt(null);
                                                          });
        }

        [Test]
        public void Alternatives_UnitTest__CryptographyEngineTest_Encrypte()
        {
            const string expected = "wKxS9gMmJL5ali85zkBcsA==";
            const string plainText = "adem";


            CryptographyEngine engine = new CryptographyEngine();
            string actual = engine.Encrypt(plainText);


            Assert.AreEqual(expected, actual, $"{actual} is not expected");
        }

        [Test]
        public void Alternatives_UnitTest__CryptographyEngineTest_Encrypte_DifferentKey()
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

        [Test]
        public void Alternatives_UnitTest__CryptographyEngineTest_Encrypte_DifferentKey_KeyIsEmpty()
        {
            const string plainText = "adem";


            CryptographyEngine engine = new CryptographyEngine(string.Empty);
            string actual = engine.Encrypt(plainText);

            Assert.IsNotNull(actual);
            Assert.AreNotEqual(actual, string.Empty);
            Assert.AreNotEqual(plainText, actual);
        }



        [Test]
        public void Alternatives_UnitTest__CryptographyEngineTest_Decrypte_Null()
        {
            CryptographyEngine engine = new CryptographyEngine();

            Assert.Throws<ArgumentNullException>(() =>
                                                          {
                                                              engine.Decrypt(null);
                                                          });
        }

        [Test]
        public void Alternatives_UnitTest__CryptographyEngineTest_Decrypte()
        {
            const string expected = "adem";
            const string cipherText = "wKxS9gMmJL5ali85zkBcsA==";


            CryptographyEngine engine = new CryptographyEngine();
            string actual = engine.Decrypt(cipherText);


            Assert.AreEqual(expected, actual, $"{actual} is not expected");
        }

        [Test]
        public void Alternatives_UnitTest__CryptographyEngineTest_Decrypte_DifferentKey()
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



        [Test]
        public void Alternatives_UnitTest__CryptographyEngineTest_Hash_Null()
        {
            CryptographyEngine engine = new CryptographyEngine();

            Assert.Throws<ArgumentNullException>(() =>
                                                          {
                                                              engine.Hashing(null);
                                                          });
        }

        [Test]
        public void Alternatives_UnitTest__CryptographyEngineTest_Hash_Empty()
        {
            const string expected = "DA-39-A3-EE-5E-6B-4B-0D-32-55-BF-EF-95-60-18-90-AF-D8-07-09";
            const string data = "";

            CryptographyEngine engine = new CryptographyEngine();
            string actual = engine.Hashing(data);


            Assert.AreEqual(expected, actual, $"{actual} is not expected");
        }

        [Test]
        public void Alternatives_UnitTest__CryptographyEngineTest_Hash()
        {
            const string expected = "40-BD-00-15-63-08-5F-C3-51-65-32-9E-A1-FF-5C-5E-CB-DB-BE-EF";
            const string data = "123";

            CryptographyEngine engine = new CryptographyEngine();
            string actual = engine.Hashing(data);


            Assert.AreEqual(expected, actual, $"{actual} is not expected");
        }
    }
}