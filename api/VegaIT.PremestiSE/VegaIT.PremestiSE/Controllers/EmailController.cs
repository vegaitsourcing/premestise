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
        public IActionResult Confirm([FromBody] string id)
        {
            int idAsInt = EncodeDecode.Decode(id);
            _matchService.ConfirmMatch(idAsInt);
            return Ok();
        }

        [HttpPost]
        public IActionResult Recover([FromBody] string id)
        {
            int idAsInt = EncodeDecode.Decode(id);
            _matchService.Unmatch(idAsInt);
            return Ok();
        }

    }
}