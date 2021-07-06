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
        IEnumerable<string> GetAllKindergardenCities();

        IEnumerable<string> GetAllActiveCities();
        IEnumerable<KindergardenDto> GetKindergardensByCity(string city);
    }
}
