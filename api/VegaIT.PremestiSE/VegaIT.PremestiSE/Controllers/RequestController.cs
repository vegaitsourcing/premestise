using System;
using System.Net.Mail;
using Core.Interfaces.Intefaces;
using Core.Interfaces.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Util;

namespace VegaIT.PremestiSE.Controllers
{
    [ApiController]
    [Route("api/request")]
    public class RequestController : Controller
    {
        private readonly IRequestService _requestService;
        private readonly IMatchService _matchService;
        private readonly ILogger<RequestController> _logger;

        public RequestController(IRequestService service, IMatchService matchService, ILogger<RequestController> logger)
        {
            _requestService = service;
            _matchService = matchService;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Create([FromBody] RequestDto newRequest)
        {
            _logger.LogTrace("Create request");
            if (!ModelState.IsValid)
            {
                _logger.LogError("Model Invalid");
                return BadRequest();
            }

            RequestDto result;
            try
            {
                result = _requestService.CreatePending(newRequest);
            } catch(SmtpException e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest();
            }

            return Created(Request.Host + Request.Path + result.Id, result);
        }

        [HttpGet]
        [Route("pending")]
        public IActionResult GetAllPending()
        {
            _logger.LogTrace("Get all pending requests");
            try
            {
                return Ok(_requestService.GetAllPending());
            } catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }

            return BadRequest();
        }

        [HttpGet]
        [Route("matched")]
        public IActionResult GetAllMatched()
        {
            _logger.LogTrace("Get all matched requests");
            try
            {
                return Ok(_requestService.GetAllMatched());
            } catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }

            return BadRequest();
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
            _logger.LogTrace("get latest pending request");
            try
            {
                return Ok(_requestService.GetLatest());
            } catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return BadRequest();
        }

        [HttpGet]
        [Route("allWishes")]
        public IActionResult GetAllWishes()
        {
            _logger.LogTrace("Get all wishes");
            try
            {
                return Ok(_requestService.GetAllPendingWishes());
            }catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }
            return BadRequest();
        }
        /*
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _requestService.DeletePending(id);
            return NoContent();
        }*/

        [HttpGet]
        [Route("delete")]
        public IActionResult Delete([FromQuery]string id)
        {
            int decodedId = HashId.Decode(id);
            _requestService.DeletePending(decodedId);
            return Ok();
        }
    }
}