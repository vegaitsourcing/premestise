using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces.Intefaces;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Verify([FromBody] int id)
        {
            _matchService.TryMatch(id);
            return Ok();
        }

        [HttpPost]
        public IActionResult Recover([FromBody] int id)
        {
            _matchService.Unmatch(id);
            return Ok();
        }

    }
}