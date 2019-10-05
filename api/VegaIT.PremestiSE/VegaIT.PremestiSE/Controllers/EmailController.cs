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
    public class EmailController : Controller
    {
        private readonly IMatchService _matchService;
        private readonly IRequestService _requestService;

        public EmailController(IMatchService service, IRequestService requestService)
        {
            _matchService = service;
            _requestService = requestService;
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