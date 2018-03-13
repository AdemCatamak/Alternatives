using System;
using Alternatives.CustomExceptions;
using Xunit;

namespace Alternatives.UnitTest.CustomExceptionTest
{
    public class CustomFriendlyExceptionTest
    {
        [Fact]
        public void CustomFriendlyExceptionTest_ExceptionMessageEqualsToExceptionMessage()
        {
            const string exceptionMessage = "message";
            CustomFriendlyException customFriendlyException = new CustomFriendlyException(exceptionMessage);

            Assert.Equal(exceptionMessage, customFriendlyException.FriendlyMessage);
            Assert.Equal(exceptionMessage, customFriendlyException.Message);

            Assert.Null(customFriendlyException.InnerException);

            Assert.Null(customFriendlyException.Tags);
        }

        [Fact]
        public void CustomFriendlyExceptionTest_IfExceptionObjectsIsGivenFromConstructor_InnerExceptionMustBeNotNull()
        {
            const string exceptionMessage = "message";
            Exception innerException = new Exception("inner ex");
            CustomFriendlyException customFriendlyException = new CustomFriendlyException(exceptionMessage, innerException);

            Assert.Equal(exceptionMessage, customFriendlyException.FriendlyMessage);
            Assert.Equal(exceptionMessage, customFriendlyException.Message);

            Assert.NotNull(customFriendlyException.InnerException);
            Assert.Equal(innerException.Message, customFriendlyException.InnerException.Message);

            Assert.Null(customFriendlyException.Tags);
        }

        [Fact]
        public void CustomFriendlyExceptionTest_IfTagIsGivenFromConstructor_TagsMustBeNotNull()
        {
            const string exceptionMessage = "message";
            CustomFriendlyException customFriendlyException = new CustomFriendlyException(exceptionMessage, ExceptionTags.External.ToString());

            Assert.Equal(exceptionMessage, customFriendlyException.FriendlyMessage);
            Assert.Equal(exceptionMessage, customFriendlyException.Message);

            Assert.Null(customFriendlyException.InnerException);

            Assert.NotNull(customFriendlyException.Tags);
            Assert.Equal(1, customFriendlyException.Tags.Length);
        }

        [Fact]
        public void CustomFriendlyExceptionTest_IfTagsAreGivenFromConstructor_TagsMustBeNotNull()
        {
            const string exceptionMessage = "message";
            CustomFriendlyException customFriendlyException = new CustomFriendlyException(exceptionMessage, ExceptionTags.External.ToString(), ExceptionTags.Transient.ToString());

            Assert.Equal(exceptionMessage, customFriendlyException.FriendlyMessage);
            Assert.Equal(exceptionMessage, customFriendlyException.Message);

            Assert.Null(customFriendlyException.InnerException);

            Assert.NotNull(customFriendlyException.Tags);
            Assert.Equal(2, customFriendlyException.Tags.Length);
            Assert.Contains(ExceptionTags.Transient.ToString(), customFriendlyException.Tags);
            Assert.Contains(ExceptionTags.External.ToString(), customFriendlyException.Tags);
        }

        [Fact]
        public void CustomFriendlyExceptionTest_IfTagsAndInnerExceptionAreGivenFromConstructor_TagsAndInnerExMustBeNotNull()
        {
            const string exceptionMessage = "message";
            Exception innerException = new Exception("inner ex");
            CustomFriendlyException customFriendlyException = new CustomFriendlyException(exceptionMessage, innerException, ExceptionTags.External.ToString(), ExceptionTags.Transient.ToString());

            Assert.Equal(exceptionMessage, customFriendlyException.FriendlyMessage);
            Assert.Equal(exceptionMessage, customFriendlyException.Message);

            Assert.NotNull(customFriendlyException.InnerException);
            Assert.Equal(innerException.Message, innerException.Message);

            Assert.NotNull(customFriendlyException.Tags);
            Assert.Equal(2, customFriendlyException.Tags.Length);
            Assert.Contains(ExceptionTags.Transient.ToString(), customFriendlyException.Tags);
            Assert.Contains(ExceptionTags.External.ToString(), customFriendlyException.Tags);
        }
    }
}