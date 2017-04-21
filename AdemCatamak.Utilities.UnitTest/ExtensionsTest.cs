using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using AdemCatamak.Utilities.UnitTest.ExtensionsTestClass;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdemCatamak.Utilities.UnitTest
{
    [TestClass]
    public class ExtensionsTest
    {
        #region IsValid

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest__ExtensionsTest__IsValid_NullTest()
        {
            bool isValid = ((object) null).IsValid();


            Assert.IsFalse(isValid, "Item is valid");
        }

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest__ExtensionsTest__IsValid_EmailFormat()
        {
            bool isValid = SetEmailAndTest("ademcatamak@gmail.com");
            Assert.IsTrue(isValid, "Email-1 format must be valid");


            isValid = SetEmailAndTest("catamak@itu.edu.tr");
            Assert.IsTrue(isValid, "Email-2 format must be valid");


            isValid = SetEmailAndTest("x@x.com");
            Assert.IsTrue(isValid, "Email-3 format must be valid");


            isValid = SetEmailAndTest("x@");
            Assert.IsFalse(isValid, "Email-4 format has not be valid");


            isValid = SetEmailAndTest("22343");
            Assert.IsFalse(isValid, "Email-5 format has not be valid");
        }

        private static bool SetEmailAndTest(string emailAddress)
        {
            IsValidTestClass item = new IsValidTestClass()
                                    {
                                        Phone = "555 555 55 55",
                                        RequiredPhone = "123",
                                        Username = "ademcatamak",
                                        Email = emailAddress
                                    };

            return item.IsValid();
        }

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest__ExtensionsTest__IsValid_Required()
        {
            bool isValid = SetUsernameAndTest("ademcatamak@gmail.com");
            Assert.IsTrue(isValid, "Username-1 format must be valid");


            isValid = SetUsernameAndTest(null);
            Assert.IsFalse(isValid, "Username-2 format has not be valid");
        }

        private static bool SetUsernameAndTest(string username)
        {
            IsValidTestClass item = new IsValidTestClass
                                    {
                                        Phone = "555-555-55-55",
                                        RequiredPhone = "123",
                                        Email = "ademcatamak@gmail.com",
                                        Username = username
                                    };

            return item.IsValid();
        }


        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest__ExtensionsTest__IsValid_TurkeyPhoneFormat()
        {
            bool isValid = SetPhoneNumberAndTest("+90 555 555 55 55");
            Assert.IsTrue(isValid, "Phone-1 format is not valid");

            isValid = SetPhoneNumberAndTest("+90-555-555-55-55");
            Assert.IsTrue(isValid, "Phone-2 format is not valid");

            isValid = SetPhoneNumberAndTest("0555-555-55-55");
            Assert.IsTrue(isValid, "Phone-3 format is not valid");

            isValid = SetPhoneNumberAndTest("555-555-55-55");
            Assert.IsTrue(isValid, "Phone-4 format is not valid");

            isValid = SetPhoneNumberAndTest("+905555555555");
            Assert.IsTrue(isValid, "Phone-5 format is not valid");

            isValid = SetPhoneNumberAndTest("+90 555 555 55 5");
            Assert.IsFalse(isValid, "Phone-6 format is valid");

            isValid = SetPhoneNumberAndTest("+90 555 555 55 5a");
            Assert.IsFalse(isValid, "Phone-7 format is valid");

            isValid = SetPhoneNumberAndTest("+90 555 5?5 55 55");
            Assert.IsFalse(isValid, "Phone-8 format is valid");
        }

        private static bool SetPhoneNumberAndTest(string phoneNumber)
        {
            IsValidTestClass item = new IsValidTestClass()
                                    {
                                        RequiredPhone = "123",
                                        Username = "ademcatamak",
                                        Email = "ademcatamak@gmail.com",
                                        Phone = phoneNumber
                                    };

            return item.IsValid();
        }

        #endregion

        #region ToInt

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Argument not null")]
        public void AdemCatamak_Utilities_UnitTest__ExtensionsTest__ToInt_Null()
        {
            ((object) null).ToInt();
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException), "Value cannot be converted")]
        public void AdemCatamak_Utilities_UnitTest__ExtensionsTest__ToInt_Alphabet()
        {
            const string data = "123a123";


            data.ToInt();
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException), "Value cannot be converted")]
        public void AdemCatamak_Utilities_UnitTest__ExtensionsTest__ToInt_WithComma()
        {
            const string data = "12,3";


            data.ToInt();
        }

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest__ExtensionsTest__ToInt()
        {
            const int expected = 123;
            const string data = "123";


            int actual = data.ToInt();


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }

        #endregion

        #region TryToInt

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest__ExtensionsTest__TryToInt_Null()
        {
            const int expected = default(int);


            int actual = ((object) null).TryToInt();


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest__ExtensionsTest__TryToInt_NullWithDefault()
        {
            const int expected = 5;


            int actual = ((object) null).TryToInt(expected);


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest__ExtensionsTest__TryToInt_WithComma()
        {
            const int expected = default(int);
            object data = "12,3";


            int actual = data.TryToInt();


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest__ExtensionsTest__TryToInt_WithCommaAndDefault()
        {
            const int expected = 5;
            object data = "12,3";


            int actual = data.TryToInt(expected);


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest__ExtensionsTest__TryToInt()
        {
            const int expected = 123;
            object data = "123";


            int actual = data.TryToInt();


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest__ExtensionsTest__TryToInt_WithDefault()
        {
            const int expected = 111,
                      defaultValue = 15;
            object data = "111";


            int actual = data.TryToInt(defaultValue);


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }

        #endregion

        #region ToDouble

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Argument not null")]
        public void AdemCatamak_Utilities_UnitTest__ExtensionsTest__ToDouble_Null()
        {
            ((object) null).ToDouble();
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException), "Value cannot be converted")]
        public void AdemCatamak_Utilities_UnitTest__ExtensionsTest__ToDouble_Alphabet()
        {
            const string data = "123a123.24";


            data.ToDouble();
        }

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest__ExtensionsTest__ToDouble_WithComma()
        {
            const double expected = 12.5;
            const string data = "12,5";


            double actual = data.ToDouble();


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest__ExtensionsTest__ToDouble_WithDot()
        {
            const double expected = 1215;
            const string data = "12.15";


            double actual = data.ToDouble();


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }

        #endregion

        #region TryToDouble

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest__ExtensionsTest__TryToDouble_Null()
        {
            const double expected = default(double);


            double actual = ((object) null).TryToDouble();


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest__ExtensionsTest__TryToDouble_NullWithDefault()
        {
            const double expected = 5.5;


            double actual = ((object) null).TryToDouble(expected);


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest__ExtensionsTest__TryToDouble_WithComma()
        {
            const double expected = 12.3;
            object data = "12,3";


            double actual = data.TryToDouble();


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest__ExtensionsTest__TryToDouble_WithDotAndComma()
        {
            const double expected = 15412.3;
            object data = "15.412,3";


            double actual = data.TryToDouble();


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest__ExtensionsTest__TryToDouble_WithCommaAndDefault()
        {
            const double expected = 12.3,
                         defaultValue = 8;
            object data = "12,3";


            double actual = data.TryToDouble(defaultValue);


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest__ExtensionsTest__TryToDouble()
        {
            const double expected = 1237;
            object data = "123.7";


            double actual = data.TryToDouble();


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest__ExtensionsTest__TryToDouble_WithDefault()
        {
            const double expected = 1118,
                         defaultValue = 15;
            object data = "111.8";


            double actual = data.TryToDouble(defaultValue);


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }

        #endregion

        #region ToLong

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Argument not null")]
        public void AdemCatamak_Utilities_UnitTest__ExtensionsTest__ToLong_Null()
        {
            ((object) null).ToLong();
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException), "Value cannot be converted")]
        public void AdemCatamak_Utilities_UnitTest__ExtensionsTest__ToLong_Alphabet()
        {
            const string data = "123a123.24";


            data.ToLong();
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException), "Value cannot be converted")]
        public void AdemCatamak_Utilities_UnitTest__ExtensionsTest__ToLong_DotSeperator()
        {
            const string data = "12.15";


            data.ToLong();
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException), "Value cannot be converted")]
        public void AdemCatamak_Utilities_UnitTest__ExtensionsTest__ToLong_CommaSeperator()
        {
            const string data = "12,15";


            data.ToLong();
        }

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest__ExtensionsTest__ToLong()
        {
            const long expected = 123123123;
            const string data = "123123123";


            long actual = data.ToLong();


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }

        #endregion

        #region TryToLong

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest__ExtensionsTest__TryToLong_Null()
        {
            const long expected = default(long);


            long actual = ((object) null).TryToLong();


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest__ExtensionsTest__TryToLong_NullWithDefault()
        {
            const long expected = 5;


            long actual = ((object) null).TryToLong(expected);


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest__ExtensionsTest__TryToLong_WithComma()
        {
            const long expected = default(long);
            object data = "12,3";


            long actual = data.TryToLong();


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest__ExtensionsTest__TryToLong_WithDotAndComma()
        {
            const long expected = default(long);
            object data = "15.412,3";


            long actual = data.TryToLong();


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest__ExtensionsTest__TryToLong_WithCommaAndDefault()
        {
            const long expected = 8,
                       defaultValue = expected;
            object data = "12,3";


            long actual = data.TryToLong(defaultValue);


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest__ExtensionsTest__TryToLong()
        {
            const long expected = default(long);
            object data = "123.7";


            long actual = data.TryToLong();


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest__ExtensionsTest__TryToLong_WithDefault()
        {
            const double expected = 1118,
                         defaultValue = 15;
            object data = "111.8";


            double actual = data.TryToDouble(defaultValue);


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }

        #endregion

        #region GetDbValue

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest__ExtensionsTest__GetDbValue_Null()
        {
            DBNull expected = DBNull.Value;


            object actual = ((object) null).GetDbValue();


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest__ExtensionsTest__GetDbValue_NullInt()
        {
            DBNull expected = DBNull.Value;
            int? data = null;


            // ReSharper disable once ExpressionIsAlwaysNull
            object actual = data.GetDbValue();


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest__ExtensionsTest__GetDbValue_EmptyString()
        {
            string expected = string.Empty;
            string data = string.Empty;


            object actual = data.GetDbValue();


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest__ExtensionsTest__GetDbValue_Int()
        {
            const int expected = 5;
            const int data = 5;


            object actual = data.GetDbValue();


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }

        #endregion

        #region FirstLetterUpper

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest__ExtensionsTest__FirstLetterUpper_Null()
        {
            string expected = string.Empty;


            string actual = ((string) null).FirstLetterToUpper();


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest__ExtensionsTest__FirstLetterUpper_Empty()
        {
            string expected = string.Empty;
            string data = string.Empty;

            string actual = data.FirstLetterToUpper();


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest__ExtensionsTest__FirstLetterUpper_OneCharacter()
        {
            const string expected = "A",
                         data = "a";

            string actual = data.FirstLetterToUpper();


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest__ExtensionsTest__FirstLetterUpper_Word()
        {
            const string expected = "Adem",
                         data = "adem";

            string actual = data.FirstLetterToUpper();


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest__ExtensionsTest__FirstLetterUpper_WordWithSpace()
        {
            const string expected = "Adem",
                         data = "adem ";

            string actual = data.FirstLetterToUpper();


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest__ExtensionsTest__FirstLetterUpper_Words()
        {
            const string expected = "Adem catamak",
                         data = "adem catamak";

            string actual = data.FirstLetterToUpper();


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }

        #endregion

        #region FirstLetterUpperAll

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest__ExtensionsTest__FirstLetterUpperAll_Null()
        {
            string expected = string.Empty;


            string actual = ((string) null).FirstLetterToUpperAll();


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest__ExtensionsTest__FirstLetterUpperAll_Empty()
        {
            string expected = string.Empty;


            string actual = string.Empty.FirstLetterToUpperAll();


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest__ExtensionsTest__FirstLetterUpperAll_Word()
        {
            const string expected = "Adem",
                         data = "adem";

            string actual = data.FirstLetterToUpperAll();


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest__ExtensionsTest__FirstLetterUpperAll_WordWithSpace()
        {
            const string expected = "Adem",
                         data = "adem ";

            string actual = data.FirstLetterToUpperAll();


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest__ExtensionsTest__FirstLetterUpperAll_Words()
        {
            const string expected = "Adem Catamak",
                         data = "adem catamak ";

            string actual = data.FirstLetterToUpperAll();


            Assert.AreEqual(expected, actual, $"{actual} value is not expected");
        }

        #endregion

        #region Map

        //NOTE : Parametresiz constructor sahibi olmayan sınıflar için kullanılamaz

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest__ExtensionsTest__Map_Null()
        {
            object actual = ((object) null).Map<object, object>();


            Assert.IsNull(actual, "Expected value is null");
        }

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest__ExtensionsTest__Map_ClassItemMap()
        {
            IsValidTestClassPartial expected = new IsValidTestClassPartial
                                               {
                                                   Id = 5
                                               };
            IsValidTestClass data = new IsValidTestClass
                                    {
                                        Id = 5,
                                        Username = "ademcatamak"
                                    };


            IsValidTestClassPartial actual = data.Map<IsValidTestClass, IsValidTestClassPartial>();


            Assert.AreEqual(expected.Id, actual.Id, $"{actual} is not expected");
        }

        #endregion

        #region Copy

        //NOTE: Parametresiz constructor sahibi olmayan sınıflar için kullanılamaz

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest__ExtensionsTest__Copy_Null()
        {
            object actual = ((object) null).Copy();


            Assert.IsNull(actual, "Expected value is null");
        }

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest__ExtensionsTest__Copy_ClassItemCopy()
        {
            IsValidTestClass expected = new IsValidTestClass()
                                        {
                                            Username = "ademcatamak",
                                            Email = "ademcatamak@gmail.com",
                                            Id = 5
                                        };


            IsValidTestClass actual = expected.Copy();


            Assert.AreEqual(expected.Serialize(), actual.Serialize(), $"{actual} is not expected");
            Assert.AreNotSame(expected, actual, $"{actual} is the same expected");
        }

        #endregion

        #region Serialize

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest__ExtensionsTest__Serialize_Null()
        {
            const string expected = @"null";


            string actual = ((IsValidTestClass) null).Serialize();


            Assert.AreEqual(expected, actual, $"{actual} is not expected");
        }

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest__ExtensionsTest__Serialize()
        {
            string expected = @"
{""Phone"":null,
""Email"":""ademcatamak@gmail.com"",
""Username"":""ademcatamak"",
""RequiredPhone"":null,
""Id"":3,""ExtraData"":null}"
                .Replace(" ", string.Empty)
                .Replace(Environment.NewLine, string.Empty);

            IsValidTestClass item = new IsValidTestClass
                                    {
                                        Id = 3,
                                        Username = "ademcatamak",
                                        Email = "ademcatamak@gmail.com"
                                    };


            string actual = item.Serialize();


            Assert.AreEqual(expected, actual, $"{actual} is not expected");
        }

        #endregion

        #region Deserialize

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest__ExtensionsTest__Deserialize_Null()
        {
            IsValidTestClass actual = @"null".Deserialize<IsValidTestClass>();


            Assert.AreEqual(null, actual, $"{actual} is not expected");
        }

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest__ExtensionsTest__Deserialize()
        {
            IsValidTestClass expected = new IsValidTestClass
                                        {
                                            Id = 3,
                                            Username = "ademcatamak",
                                            Email = "ademcatamak@gmail.com"
                                        };
            string item = @"
{""Phone"":null,
""Email"":""ademcatamak@gmail.com"",
""Username"":""ademcatamak"",
""RequiredPhone"":null,
""Id"":3,""ExtraData"":null}"
                .Replace(" ", string.Empty)
                .Replace(Environment.NewLine, string.Empty);


            IsValidTestClass actual = item.Deserialize<IsValidTestClass>();


            Assert.AreEqual(expected.Id, actual.Id, $"{actual.Id} is not expected");
            Assert.AreEqual(expected.Username, actual.Username, $"{actual.Username} is not expected");
            Assert.AreEqual(expected.Email, actual.Email, $"{actual.Email} is not expected");
            Assert.AreEqual(expected.ExtraData, actual.ExtraData, $"{actual.ExtraData} is not expected");
            Assert.AreEqual(expected.Phone, actual.Phone, $"{actual.Phone} is not expected");
            Assert.AreEqual(expected.RequiredPhone, actual.RequiredPhone, $"{actual.RequiredPhone} is not expected");
        }

        #endregion

        #region EnumToDictionary

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest__ExtensionsTest__EnumToDictionary_WithNegativeValue()
        {
            Dictionary<int, string> expected = new Dictionary<int, string>()
                                               {
                                                   {0, "TestValue0"},
                                                   {1, "TestValue1"}
                                               };


            Dictionary<int, string> actual = Extensions.EnumToDictionary(typeof(TestEnumWithNegative));

            for (int i = 0; i < actual.Count; i++)
            {
                Assert.AreEqual(expected.ElementAt(i), actual.ElementAt(i), $"Actual[{i}] value is not expected");
            }
        }

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest__ExtensionsTest__EnumToDictionary()
        {
            Dictionary<int, string> expected = new Dictionary<int, string>()
                                               {
                                                   {1, "TestValue1"},
                                                   {2, "TestValue2"}
                                               };


            Dictionary<int, string> actual = Extensions.EnumToDictionary(typeof(TestEnum));

            for (int i = 0; i < actual.Count; i++)
            {
                Assert.AreEqual(expected.ElementAt(i), actual.ElementAt(i), $"Actual[{i}] value is not expected");
            }
        }

        #endregion

        #region CreateInstance

        //NOTE : Parametresiz constructor sahibi olmayan sınıflarda kullanılamaz

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest__ExtensionsTest__CreateInstance_InSameAssembly()
        {
            string fullName = typeof(IsValidTestClass).AssemblyQualifiedName;


            object actual = Extensions.CreateInstance(fullName);


            Assert.IsNotNull(actual, "Actual is null");
            Assert.IsNotNull(actual as IsValidTestClass,
                             "Cast operation show that IsValidClass instance cannot be created");
        }

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest__ExtensionsTest__CreateInstance_InDifferentAssembly()
        {
            string fullName = typeof(AdemCatamak.Utilities.CryptographyEngine).FullName;


            object actual = Extensions.CreateInstance(fullName);


            Assert.IsNotNull(actual, "Actual is null");
            Assert.IsNotNull(actual as AdemCatamak.Utilities.CryptographyEngine,
                             "Cast operation show that IsValidClass instance cannot be created");
        }

        #endregion

        #region ConvertToDataTable

        [TestMethod]
        public void AdemCatamak_Utilities_UnitTest__ExtensionsTest__ToDataTable()
        {
            DataTable expected = new DataTable()
                                 {
                                     Columns =
                                     {
                                         new DataColumn
                                         {
                                             DataType = typeof(int),
                                             ColumnName = "Id"
                                         },
                                         new DataColumn
                                         {
                                             DataType = typeof(string),
                                             ColumnName = "Username"
                                         },
                                         new DataColumn
                                         {
                                             DataType = typeof(string),
                                             ColumnName = "Email"
                                         },
                                         new DataColumn
                                         {
                                             DataType = typeof(string),
                                             ColumnName = "Phone"
                                         },
                                         new DataColumn
                                         {
                                             DataType = typeof(string),
                                             ColumnName = "RequiredPhone"
                                         },
                                         new DataColumn
                                         {
                                             DataType = typeof(int),
                                             ColumnName = "ExtraData"
                                         }
                                     }
                                 };
            DataRow dataRow0 = expected.NewRow();

            dataRow0["Id"] = 1;
            dataRow0["Username"] = "catamak";
            dataRow0["Email"] = "adem.catamak@gmail.com";
            dataRow0["Phone"] = "666 666 66 66";
            dataRow0["ExtraData"] = DBNull.Value;
            expected.Rows.Add(dataRow0);

            DataRow dataRow1 = expected.NewRow();

            dataRow1["Id"] = 5;
            dataRow1["Username"] = "ademcatamak";
            dataRow1["Email"] = "ademcatamak@gmail.com";
            dataRow1["Phone"] = "555 555 55 55";
            dataRow1["ExtraData"] = 12;
            expected.Rows.Add(dataRow1);

            List<DataTableTestClass> dataList = new List<DataTableTestClass>
                                                {
                                                    new DataTableTestClass
                                                    {
                                                        Id = 1,
                                                        Username = "catamak",
                                                        Email = "adem.catamak@gmail.com",
                                                        Phone = "666 666 66 66",
                                                        ExtraData = null
                                                    },
                                                    new DataTableTestClass
                                                    {
                                                        Id = 5,
                                                        Username = "ademcatamak",
                                                        Email = "ademcatamak@gmail.com",
                                                        Phone = "555 555 55 55",
                                                        ExtraData = 12
                                                    }
                                                };


            DataTable actual = dataList.ToDataTable();

            for (int i = 0; i < actual.Rows.Count; i++)
            {
                Assert.AreEqual(expected.Rows[i]["Id"], actual.Rows[i]["Id"], "Actual.Id is not expected");
                Assert.AreEqual(expected.Rows[i]["Username"], actual.Rows[i]["Username"], "Actual.Username is not expected");
                Assert.AreEqual(expected.Rows[i]["Email"], actual.Rows[i]["Email"], "Actual.Email is not expected");
                Assert.AreEqual(expected.Rows[i]["Phone"], actual.Rows[i]["Phone"], "Actual.Phone is not expected");
                Assert.AreEqual(expected.Rows[i]["ExtraData"], actual.Rows[i]["ExtraData"], "Actual.ExtraData is not expected");
            }
        }

        #endregion
    }
}