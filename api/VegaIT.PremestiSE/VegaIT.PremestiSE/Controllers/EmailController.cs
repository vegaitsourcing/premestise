using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces.Intefaces;
using Microsoft.AspNetCore.Mvc;
using Util;

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

        [HttpPost]
        
        [Route("verify")]
        public IActionResult Verify([FromBody] string id)
        {
            int idAsInt = EncodeDecode.Decode(id);
            _matchService.TryMatch(idAsInt);
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