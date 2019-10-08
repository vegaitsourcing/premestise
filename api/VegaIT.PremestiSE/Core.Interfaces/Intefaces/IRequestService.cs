using System;
using System.Collections.Generic;
using System.Text;
using Core.Interfaces.Models;
using Persistence.Interfaces.Entites;

namespace Core.Interfaces.Intefaces
{
    public interface IRequestService
    {
        RequestDto CreatePending(RequestDto newRequest);
        WishDto GetLatest();
        IEnumerable<RequestDto> GetAllPending();
        void DeletePending(int id);
        IEnumerable<RequestDto> GetAllMatched();
        void DeleteMatched(int id);
    }
}
