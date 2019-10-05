using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces.Intefaces;
using Microsoft.AspNetCore.Mvc;

namespace VegaIT.PremestiSE.Controllers
{
    public class EmailController : Controller
    {
        private IMatchService _matchService;

        public EmailController(IMatchService service)
        {
            _matchService = service;
        }

        [HttpPost]
        public IActionResult Verify([FromBody]int id)
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