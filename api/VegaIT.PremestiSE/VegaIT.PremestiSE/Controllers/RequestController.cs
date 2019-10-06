using System;
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
            Console.WriteLine(newRequest.FromKindergardenId);
            Console.WriteLine(newRequest.ToKindergardenIds);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            RequestDto result = _requestService.CreatePending(newRequest);

            return Created(Request.Host + Request.Path + result.Id, result);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_requestService.GetAllPending());
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _requestService.DeletePending(id);
            return NoContent();
        }
    }
}