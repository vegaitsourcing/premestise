using Core.Clients;
using Microsoft.AspNetCore.Mvc;
using Core.Interfaces.Models;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using Microsoft.Extensions.Logging;

namespace VegaIT.PremestiSE.Controllers
{
    [ApiController]
    [Route("api/contact")]
    public class ContactController : Controller
    {
        private readonly IMailClient _mailClient;
        private readonly IConfiguration _config;
        private readonly ILogger<ContactController> _logger;
        public ContactController(IMailClient mailClient, IConfiguration config, ILogger<ContactController> logger)
        {
            _mailClient = mailClient;
            _config = config;
            _logger = logger;
        }


        [HttpPost]
        public IActionResult Create([FromBody] ContactFormDto form)
        {
            _logger.LogTrace("Contact");
            if (!ModelState.IsValid)
            {
                _logger.LogError("Model invalid");
                return BadRequest();
            }

            try
            {
                _mailClient.Send(form.Email, "Hvala sto ste nas kontaktirali, odgovoricemo Vam sto pre!");
                _mailClient.Send(_config.GetValue<string>("SecondEmail"), form.Message);
            }
            catch (SmtpException ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest();
            }

           
            return Ok();
        }
    }
}