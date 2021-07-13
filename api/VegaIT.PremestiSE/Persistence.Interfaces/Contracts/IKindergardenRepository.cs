using Persistence.Interfaces.Entites;
using System.Collections.Generic;

namespace Persistence.Interfaces.Contracts
{
    public interface IKindergardenRepository
    {
        Kindergarden GetById(int id);

        KindergardenEmailInfo GetForEmailById(int id);

        List<Kindergarden> GetAll();
        List<Kindergarden> GetToByRequestId(int id);
        List<string> GetAllCities();

        IEnumerable<string> GetAllActiveCities();
        List<Kindergarden> GetKindergardensByCity(string city);
    }
}
