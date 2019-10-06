using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Clients;
using Microsoft.AspNetCore.Mvc;

namespace VegaIT.PremestiSE.Controllers
{
    [ApiController]
    [Route("api/contact")]
    public class ContactController : Controller
    {
        private readonly IMailClient _mailClient;

        public ContactController(IMailClient mailClient)
        {
            _mailClient = mailClient;
        }

        [HttpPost]
        public IActionResult Index( )
        {
            string email = Request.Form["email"];
            string message = Request.Form["message"];
            Console.WriteLine(email);
            Console.WriteLine(message);
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(message))
                return BadRequest();

            _mailClient.Send(email, message);
            
            return Ok();
        }
    }
}