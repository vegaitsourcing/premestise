﻿using Core.Interfaces.Intefaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Util;
using VegaIT.PremestiSE.Controllers;
using Xunit;

namespace UnitTest
{
    public class EmailControllerTest
    {
        private readonly EmailController _emailController;
        private readonly Mock<IMatchService> _matchService;

        public EmailControllerTest()
        {
            _matchService = new Mock<IMatchService>();
            _emailController = new EmailController(_matchService.Object);
        }

        [Fact]
        public void GivenId_WhenVerifyCalled_ShouldReturnOk()
        {
            string id = HashId.Encode(int.MaxValue);
            int decodedId = HashId.Decode(id);

            var result = _emailController.Verify(id.ToString());

            _matchService.Verify(service => service.TryMatch(decodedId));
            Assert.Equal(typeof(OkResult), result.GetType());
        }

        [Fact]
        public void GivenId_WhenRecoverCalled_ShouldReturnOk()
        {
            string id = HashId.Encode(int.MaxValue);
            int decodedId = HashId.Decode(id);

            var result = _emailController.Recover(id);

            _matchService.Verify(service => service.Unmatch(decodedId));
            Assert.Equal(typeof(OkResult), result.GetType());
        }
    }
}
