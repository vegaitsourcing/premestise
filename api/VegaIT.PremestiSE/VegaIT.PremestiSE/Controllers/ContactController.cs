using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace VegaIT.PremestiSE.Controllers
{
    [ApiController]
    public class ContactController : Controller
    {
        [HttpPost]
        public IActionResult Index( )
        {
            string email = Request.Form["email"];
            string message = Request.Form["message"];

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(message))
                return BadRequest();

        
          // TODO: use EmailClient

          return Ok();
        }
    }
}