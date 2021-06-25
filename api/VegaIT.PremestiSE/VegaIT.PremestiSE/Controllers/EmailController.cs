using Core.Interfaces.Intefaces;
using Microsoft.AspNetCore.Mvc;
using Util;
using System;
using System.Net.Mail;
using Microsoft.Extensions.Logging;

namespace VegaIT.PremestiSE.Controllers
{
    [ApiController]
    [Route("api/email")]
    public class EmailController : Controller
    {
        private readonly IMatchService _matchService;
        private readonly ILogger<EmailController> _logger;

        public EmailController(IMatchService service, ILogger<EmailController> logger)
        {
            _matchService = service;
            _logger = logger;
        }

        [HttpGet]
        [Route("verify")]
        public IActionResult Verify([FromQuery] string id)
        {
            int decodedId = HashId.Decode(id);

            try
            {
               _matchService.TryMatch(decodedId);
            }
            catch (SmtpException e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest();
            }
            
            return Ok();
        }

        [HttpGet]
        [Route("confirm")]
        public IActionResult Confirm([FromQuery] string id)
        {
            int decodedId = HashId.Decode(id);
            try
            {
                _matchService.ConfirmMatch(decodedId);
            }
            catch(SmtpException e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest();
            }
            
            return Ok();
        }

        [HttpGet]
        [Route("recover")]
        public IActionResult Recover([FromQuery] string id)
        {
            int decodedId = HashId.Decode(id);
            try
            {
                int newId = _matchService.Unmatch(decodedId);
                _matchService.TryMatch(newId);
            }
            catch(SmtpException e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest();
            }
            

            return Ok();
        }

    }
}
