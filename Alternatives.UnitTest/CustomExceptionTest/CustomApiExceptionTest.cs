using System;
using System.Net;
using Alternatives.CustomExceptions;
using Xunit;

namespace Alternatives.UnitTest.CustomExceptionTest
{
    public class CustomApiExceptionTest
    {
        [Fact]
        public void CustomApiExceptionTest_ExceptionMessageEqualsToExceptionMessage()
        {
            const string exceptionMessage = "message";
            const HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest;
            CustomApiException customApiException = new CustomApiException(exceptionMessage, httpStatusCode);

            Assert.Equal(exceptionMessage, customApiException.FriendlyMessage);
            Assert.Equal(exceptionMessage, customApiException.Message);

            Assert.Equal(httpStatusCode, customApiException.ReturnHttpStatusCode);

            Assert.Null(customApiException.InnerException);

            Assert.Null(customApiException.Tags);
        }

        [Fact]
        public void CustomApiExceptionTest_IfExceptionObjectsIsGivenFromConstructor_InnerExceptionMustBeNotNull()
        {
            const string exceptionMessage = "message";
            Exception innerException = new Exception("inner ex");
            const HttpStatusCode httpStatusCode = HttpStatusCode.BadGateway;
            CustomApiException customApiException = new CustomApiException(exceptionMessage, httpStatusCode, innerException);

            Assert.Equal(exceptionMessage, customApiException.FriendlyMessage);
            Assert.Equal(exceptionMessage, customApiException.Message);

            Assert.Equal(httpStatusCode, customApiException.ReturnHttpStatusCode);

            Assert.NotNull(customApiException.InnerException);
            Assert.Equal(innerException.Message, customApiException.InnerException.Message);

            Assert.Null(customApiException.Tags);
        }

        [Fact]
        public void CustomApiExceptionTest_IfTagIsGivenFromConstructor_TagsMustBeNotNull()
        {
            const string exceptionMessage = "message";
            const HttpStatusCode httpStatusCode = HttpStatusCode.BadGateway;
            CustomApiException customApiException = new CustomApiException(exceptionMessage, httpStatusCode, ExceptionTags.Internal.ToString());

            Assert.Equal(exceptionMessage, customApiException.FriendlyMessage);
            Assert.Equal(exceptionMessage, customApiException.Message);

            Assert.Equal(httpStatusCode, customApiException.ReturnHttpStatusCode);

            Assert.Null(customApiException.InnerException);

            Assert.NotNull(customApiException.Tags);
            Assert.Equal(1, customApiException.Tags.Length);
        }

        [Fact]
        public void CustomApiExceptionTest_IfTagsAreGivenFromConstructor_TagsMustBeNotNull()
        {
            const string exceptionMessage = "message";
            const HttpStatusCode httpStatusCode = HttpStatusCode.Conflict;
            CustomApiException customApiException = new CustomApiException(exceptionMessage, httpStatusCode, ExceptionTags.Internal.ToString(), ExceptionTags.Persistent.ToString());

            Assert.Equal(exceptionMessage, customApiException.FriendlyMessage);
            Assert.Equal(exceptionMessage, customApiException.Message);

            Assert.Equal(httpStatusCode, customApiException.ReturnHttpStatusCode);

            Assert.Null(customApiException.InnerException);

            Assert.NotNull(customApiException.Tags);
            Assert.Equal(2, customApiException.Tags.Length);
            Assert.Contains(ExceptionTags.Persistent.ToString(), customApiException.Tags);
            Assert.Contains(ExceptionTags.Internal.ToString(), customApiException.Tags);
        }

        [Fact]
        public void CustomApiExceptionTest_IfTagsAndInnerExceptionAreGivenFromConstructor_TagsAndInnerExMustBeNotNull()
        {
            const string exceptionMessage = "message";
            Exception innerException = new Exception("inner ex");
            const HttpStatusCode httpStatusCode = HttpStatusCode.Conflict;
            CustomApiException customApiException = new CustomApiException(exceptionMessage, httpStatusCode, innerException, ExceptionTags.Internal.ToString(), ExceptionTags.Persistent.ToString());

            Assert.Equal(exceptionMessage, customApiException.FriendlyMessage);
            Assert.Equal(exceptionMessage, customApiException.Message);

            Assert.Equal(httpStatusCode, customApiException.ReturnHttpStatusCode);

            Assert.NotNull(customApiException.InnerException);
            Assert.Equal(innerException.Message, innerException.Message);

            Assert.NotNull(customApiException.Tags);
            Assert.Equal(2, customApiException.Tags.Length);
            Assert.Contains(ExceptionTags.Persistent.ToString(), customApiException.Tags);
            Assert.Contains(ExceptionTags.Internal.ToString(), customApiException.Tags);
        }
    }
}