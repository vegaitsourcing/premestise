using Core.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces.Intefaces
{
    public interface IKindergardenService
    {
        IEnumerable<KindergardenDto> GetAll();
        IEnumerable<KindergardenDto> GetToKindergardens(int requestId);
    }
}
