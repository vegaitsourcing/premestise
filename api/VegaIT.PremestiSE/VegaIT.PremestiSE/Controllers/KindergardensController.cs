using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces.Intefaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VegaIT.PremestiSE.Controllers
{
    [ApiController]
    [Route("api/kindergarden")]
    public class KindergardenController : Controller
    {
        private readonly IKindergardenService _kindergardenService;

        public KindergardenController(IKindergardenService kindergardenService)
        {
            _kindergardenService = kindergardenService;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_kindergardenService.GetAll());
        }

    }
}