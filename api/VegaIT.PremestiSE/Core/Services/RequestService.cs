using Core.Interfaces.Intefaces;
using Core.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services
{
    public class RequestService : IRequestService
    {
        public RequestService()
        {
        }

        public RequestDto Create(RequestDto newRequest)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RequestDto> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
