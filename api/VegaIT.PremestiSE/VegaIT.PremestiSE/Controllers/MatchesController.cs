using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VegaIT.PremestiSE.Controllers
{
    [Produces("application/json")]
    [Route("api/Matches")]
    public class MatchesController : Controller
    {
        //private IMatchesService _matchesService;

        //public MatchesController(IMatchesService matchesService)
        //{
        //    _matchesService = matchesService;
        //}

        // GET: api/Matches
        [HttpGet]
        public IEnumerable<string> GetAll()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{/count}", Name = "GetCount")]
        public int GetCount()
        {
            return 0;
        }

        // GET: api/Matches/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Matches
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Matches/5
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
