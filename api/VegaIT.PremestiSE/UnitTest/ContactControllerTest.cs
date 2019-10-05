using System;
using System.Collections.Generic;
using Core.Clients;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Moq;
using VegaIT.PremestiSE.Controllers;
using Xunit;

namespace UnitTest
{
    public class ContactControllerTest : IDisposable
    {
        private readonly ContactController _controller;
        private readonly Mock<IMailClient> _mailClient;
        
        public ContactControllerTest()
        {
            _mailClient = new Mock<IMailClient>();
            _controller = new ContactController(_mailClient.Object);
        }

        public void Dispose()
        {
            _controller.Dispose();   
        }

        [Fact]
        public void GivenValidRequest_WhenIndexCalled_ShouldReturnOk()
        {
            var httpContext = new DefaultHttpContext();
            const string testEMail = "test@e.mail";
            const string testMessage = "test_message";
            httpContext.Request.Form = new FormCollection(new Dictionary<string, StringValues>
            {
                {"email", testEMail},
                {"message", testMessage}
            });
            _controller.ControllerContext = new ControllerContext{HttpContext = httpContext};

            var result = _controller.Index();
            
            Assert.Equal(typeof(OkResult), result.GetType());
            _mailClient.VerifyAll();
        }

        [Fact]
        public void GivenNoEmailOrMessage_WhenIndexCalled_ShouldReturnBadRequest()
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Form = new FormCollection(new Dictionary<string, StringValues>());
            _controller.ControllerContext = new ControllerContext{HttpContext = httpContext};

            var result = _controller.Index();
            
            Assert.Equal(typeof(BadRequestResult), result.GetType());
            _mailClient.Verify(mock => mock.Send(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void GivenValidEmailAndNoMessage_WhenIndexCalled_ShouldReturnBadRequest()
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Form = new FormCollection(new Dictionary<string, StringValues>
            {
                {"email", "test@e.mail"}
            });
            _controller.ControllerContext = new ControllerContext{HttpContext = httpContext};
            
            var result = _controller.Index();
            
            Assert.Equal(typeof(BadRequestResult), result.GetType());
            _mailClient.Verify(mock => mock.Send(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void GivenNoEmailAndValidMessage_WhenIndexCalled_ShouldReturnBadRequest()
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Form = new FormCollection(new Dictionary<string, StringValues>
            {
                {"message", "test_message"}
            });
            _controller.ControllerContext = new ControllerContext{HttpContext = httpContext};
            
            var result = _controller.Index();
            
            Assert.Equal(typeof(BadRequestResult), result.GetType());
            _mailClient.Verify(mock => mock.Send(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }
    }
}