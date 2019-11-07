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
        private readonly IMatchService _matchService;

        public RequestController(IRequestService service, IMatchService matchService)
        {
            _requestService = service;
            _matchService = matchService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] RequestDto newRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            RequestDto result = _requestService.CreatePending(newRequest);

            return Created(Request.Host + Request.Path + result.Id, result);
        }

        [HttpGet]
        [Route("pending")]
        public IActionResult GetAllPending()
        {
            return Ok(_requestService.GetAllPending());
        }

        [HttpGet]
        [Route("matched")]
        public IActionResult GetAllMatched()
        {
            return Ok(_requestService.GetAllMatched());
        }

        [HttpGet]
        [Route("matched/count")]
        public IActionResult GetSuccessfulMatchesCount()
        {
            int successfulMatchesCount = _matchService.GetTotalCount();
            return Ok(successfulMatchesCount);
        }

        [HttpGet]
        [Route("latest")]
        public IActionResult GetLatest()
        {
            return Ok(_requestService.GetLatest());
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _requestService.DeletePending(id);
            return NoContent();
        }
    }
}