using Core.Interfaces.Intefaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
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
            var id = int.MaxValue;
            
            var result = _emailController.Verify(id.ToString());
            
            _matchService.Verify(service => service.TryMatch(id));
            Assert.Equal(typeof(OkResult), result.GetType());
        }

        [Fact]
        public void GivenId_WhenRecoverCalled_ShouldReturnOk()
        {
            var id = int.MaxValue;
            
            var result = _emailController.Recover(id);
            
            _matchService.Verify(service => service.Unmatch(id));
            Assert.Equal(typeof(OkResult), result.GetType());
        }
    }
}