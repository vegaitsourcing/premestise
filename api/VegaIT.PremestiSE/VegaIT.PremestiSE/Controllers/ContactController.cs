using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Clients;
using Microsoft.AspNetCore.Mvc;
using Core.Interfaces.Models;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;

namespace VegaIT.PremestiSE.Controllers
{
    [ApiController]
    [Route("api/contact")]
    public class ContactController : Controller
    {
        private readonly IMailClient _mailClient;
        private readonly IConfiguration _config;
        public ContactController(IMailClient mailClient, IConfiguration config)
        {
            _mailClient = mailClient;
            _config = config;
        }
        

        [HttpPost]
        public IActionResult Create([FromBody] ContactFormDto form)
        {
            if (!ModelState.IsValid) return BadRequest();


            try
            {
                _mailClient.Send(form.Email, "Hvala sto ste nas kontaktirali, odgovoricemo Vam sto pre!");
                _mailClient.Send(_config.GetValue<string>("SecondEmail"), form.Message);
            }
            catch (SmtpException)
            {
                return BadRequest();
            }

           
            return Ok();
        }
    }
}