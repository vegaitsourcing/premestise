using Core.Interfaces.Intefaces;
using Microsoft.AspNetCore.Mvc;
using Util;
using System;
using System.Net.Mail;

namespace VegaIT.PremestiSE.Controllers
{
    [ApiController]
    [Route("api/email")]
    public class EmailController : Controller
    {
        private readonly IMatchService _matchService;

        public EmailController(IMatchService service)
        {
            _matchService = service;
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
            catch(SmtpException)
            {

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
            catch(SmtpException)
            {
                return BadRequest();
            }
            

            return Ok();
        }

    }
}
