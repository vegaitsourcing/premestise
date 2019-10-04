using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Contracts.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VegaIT.PremestiSE.Controllers
{
    [Produces("application/json")]
    [Route("api/Requests")]
    public class RequestsController : Controller
    {

        //private IRequestService _requestService;
     

        //public RequestsController(IRequestService requestService)
        //{
        //    _requestService = requestService;

        //}
        // GET: api/Requests
        [HttpGet]
        public IEnumerable<Request> GetAll()
        {
            return null;
            //return _requestService.GetAll();
        }

        // GET: api/Requests/5
        [HttpGet("{id}", Name = "Get")]
        public Request Get(int id)
        {
            return null;
        }
        
        // POST: api/Requests
        [HttpPost]
        public Request Post([FromBody]Request value)
        {
            return null;
        }
        
        // PUT: api/Requests/5
        [HttpPut("{id}")]
        public Request Put(int id, [FromBody]Request value)
        {
            return null;
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
