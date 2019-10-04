using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VegaIT.PremestiSE.Controllers
{
    [Produces("application/json")]
    [Route("api/Kindergardens")]
    public class KindergardensController : Controller
    {
        //private IKindergardenService _kindergardenService;

        //public KindergardensController(IKindergardenService kindergardenService)
        //{
        //    _kindergardenService = kindergardenService;
        //}

        // GET: api/Kindergardens
        [HttpGet]
        public IEnumerable<string> GetAll()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Kindergardens/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Kindergardens
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Kindergardens/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
