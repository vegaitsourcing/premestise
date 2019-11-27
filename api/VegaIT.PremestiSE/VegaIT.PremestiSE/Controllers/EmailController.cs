using Core.Interfaces.Intefaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;
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

        [HttpGet]
        [Route("verify")]
        public IActionResult Verify([FromQuery] string id)
        {
            int decodedId = HashId.Decode(id);
            _matchService.TryMatch(decodedId);

            // radi testiranja zakucano, nisam siguran kako ce rutiranje funkcionisati
            // Dns.GetHostEntry(Dns.GetHostName()).AddressList[0].ToString();
            return Redirect($"http://localhost:50800/verify.html");
        }

        [HttpPost]
        [Route("confirm")]
        public IActionResult Confirm([FromBody] string id)
        {
            int decodedId = HashId.Decode(id);
            _matchService.ConfirmMatch(decodedId);
            return Ok();
        }

        [HttpPost]
        [Route("recover")]
        public IActionResult Recover([FromBody] string id)
        {
            int decodedId = HashId.Decode(id);
            int newId =_matchService.Unmatch(decodedId);
            _matchService.TryMatch(newId);

            return Ok();
        }

    }
}
