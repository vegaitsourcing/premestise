using Core.Interfaces.Intefaces;
using Core.Interfaces.Models;
using Microsoft.AspNetCore.Mvc;

namespace VegaIT.PremestiSE.Controllers
{
    [ApiController]
    [Route("api/request")]
    public class RequestController : Controller
    {
        private readonly IRequestService _requestService;
        public RequestController(IRequestService service)
        {
            _requestService = service;
        }

        [HttpPost]
        public IActionResult Create([FromBody] RequestDto newRequest)
        {
            RequestDto result = _requestService.Create(newRequest);
            return Created(Request.Host + Request.Path + result.Id, result);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_requestService.GetAll());
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            _requestService.Delete(id);
            return NoContent();
        }
    }
}