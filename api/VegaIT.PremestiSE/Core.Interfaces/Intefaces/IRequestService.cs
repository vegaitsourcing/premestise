using System;
using System.Collections.Generic;
using System.Text;
using Core.Interfaces.Models;

namespace Core.Interfaces.Intefaces
{
    public interface IRequestService
    {
        RequestDto Create(RequestDto newRequest);
        IEnumerable<RequestDto> GetAll();
        void Delete(int id);
    }
}
