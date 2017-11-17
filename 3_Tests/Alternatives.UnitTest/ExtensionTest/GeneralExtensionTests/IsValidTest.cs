using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Alternatives.CustomDataAnnotations;
using Alternatives.Extensions;
using NUnit.Framework;

namespace Alternatives.UnitTest.ExtensionTest.GeneralExtensionTests
{
    public class IsValidTest
    {
        #region TestModel

        private class IsValidTestClass
        {
            [TurkeyPhone]
            public string Phone { get; set; }

            [EmailAddress]
            public string Email { get; set; }

            [Required]
            public string Username { get; set; }

            [Phone, Required]
            public string RequiredPhone { get; set; }
        }

        private class IsValidTestClassWithCustomIsValid
        {
            [TurkeyPhone]
            public string Phone { get; set; }

            public string Email { get; set; }

            public string Username { get; set; }

            [Phone, Required]
            public string RequiredPhone { get; set; }

            public bool IsValid()
            {
                return IsValid(out string _);
            }

            public bool IsValid(out string message)
            {
                bool result = GeneralExtensions.IsValid(this, out message);

                List<string> messageList = new List<string>();
                if (Email == null)
                {
                    if (Username == null)
                    {
                        result = false;
                        messageList.Add("Username cannot be null when email does not supplied");
                    }
                }

                if (Email != null)
                {
                    EmailAddressAttribute addressAttribute = new EmailAddressAttribute();
                    bool emailValidation = addressAttribute.IsValid(Email);
                    if (!emailValidation)
                    {
                        result = false;
                        messageList.Add("Email format is not valid");
                    }
                }

                if (messageList.Any())
                {
                    message += Environment.NewLine
                               + string.Join(Environment.NewLine, messageList);
                }

                return result;
            }
        }

        #endregion

        [Test]
        public void IsValid_WhenIsValidFuctionExecuteForNull_ResponseMustBeFalse()
        {
            bool isValid = ((object) null).IsValid();
            Assert.IsFalse(isValid, "Item is valid");
        }


        [Test]
        public void IsValid_ExecuteIsValidFunctionForDifferentKindOfEmailFormat()
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
            IsValidTestClass item = new IsValidTestClass
                                    {
                                        Phone = "555 555 55 55",
                                        RequiredPhone = "123",
                                        Username = "ademcatamak",
                                        Email = emailAddress
                                    };
            return item.IsValid();
        }


        [Test]
        public void IsValid_ExecuteIsValidFunctionForRequiredField()
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


        [Test]
        public void IsValid_ExecuteIsValidFunctionForDifferentKindOfTurkeyPhoneFormat()
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
            IsValidTestClass item = new IsValidTestClass
                                    {
                                        RequiredPhone = "123",
                                        Username = "ademcatamak",
                                        Email = "ademcatamak@gmail.com",
                                        Phone = phoneNumber
                                    };
            return item.IsValid();
        }


        [Test]
        public void IsValid_WhenClassHasValidator_IsValidFunctionTakeNoticeBothValidatorClassAndDataAnnotaions()
        {
            IsValidTestClassWithCustomIsValid testClassWithValidator = new IsValidTestClassWithCustomIsValid();
            bool isValid = testClassWithValidator.IsValid(out string errorMessage);
            Assert.IsFalse(isValid);
            Assert.IsTrue(errorMessage.Contains("Phone"));
            Assert.IsTrue(errorMessage.Contains("RequiredPhone"));
            Assert.IsTrue(errorMessage.Contains("Username"));
            testClassWithValidator = new IsValidTestClassWithCustomIsValid
            {
                                         Email = "adm"
                                     };
            isValid = testClassWithValidator.IsValid(out errorMessage);
            Assert.IsFalse(isValid);
            Assert.IsTrue(errorMessage.Contains("Phone"));
            Assert.IsTrue(errorMessage.Contains("RequiredPhone"));
            Assert.IsTrue(errorMessage.Contains("Email"));
            testClassWithValidator = new IsValidTestClassWithCustomIsValid
            {
                                         Email = "a@a"
                                     };
            isValid = testClassWithValidator.IsValid(out errorMessage);
            Assert.IsFalse(isValid);
            Assert.IsTrue(errorMessage.Contains("Phone"));
            Assert.IsTrue(errorMessage.Contains("RequiredPhone"));
            Assert.IsTrue(errorMessage.Contains("Email"));
            testClassWithValidator = new IsValidTestClassWithCustomIsValid
            {
                                         Email = "a@a.com"
                                     };
            isValid = testClassWithValidator.IsValid(out errorMessage);
            Assert.IsFalse(isValid);
            Assert.IsTrue(errorMessage.Contains("Phone"));
            Assert.IsTrue(errorMessage.Contains("RequiredPhone"));
        }
    }
}