using System;
using System.Collections.Generic;
using System.Text;
using Core.Interfaces.Models;

namespace Core.Interfaces.Intefaces
{
    public interface IRequestService
    {
        RequestDto CreatePending(RequestDto newRequest);
        //void OnRequestVerified(int pendingRequestId);

        IEnumerable<RequestDto> GetAllPending();
        void DeletePending(int id);

        

        RequestDto CreateMatched(RequestDto newRequest);
        IEnumerable<RequestDto> GetAllMatched();
        void DeleteMatched(int id);
    }
}
