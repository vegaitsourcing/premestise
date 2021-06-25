using Core.Interfaces.Intefaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace VegaIT.PremestiSE.Controllers
{
    [ApiController]
    [Route("api/kindergarden")]
    public class KindergardenController : Controller
    {
        private readonly IKindergardenService _kindergardenService;
        private readonly ILogger<KindergardenController> _logger;

        public KindergardenController(IKindergardenService kindergardenService, ILogger<KindergardenController> logger)
        {
            _kindergardenService = kindergardenService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogInformation("Everything");
            try
            {
                return Ok(_kindergardenService.GetAll());
            }catch(Exception e)
            {
                _logger.LogError(e, e.Message);
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("cities")]
        public IActionResult GetKindergardenCities()
        {
            _logger.LogError("Cities");
            try
            {
                return Ok(_kindergardenService.GetAllKindergardenCities());
            } catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("bycity")]
        public IActionResult GetKindergardenByCity([FromQuery] string city)
        {
            try
            {
                return Ok(_kindergardenService.GetKindergardensByCity(city));
            } catch(Exception e)
            {
                _logger.LogError(e, e.Message);
            }
            return BadRequest();
        }

    }
}