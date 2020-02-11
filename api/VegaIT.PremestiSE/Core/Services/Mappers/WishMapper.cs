using Util;
using System.Linq;
using Core.Interfaces.Models;
using Core.Interfaces.Intefaces;
using Persistence.Interfaces.Entites;

namespace Core.Services.Mappers
{
    public class WishMapper : IMapper<Request, WishDto>
    {
        public WishDto DtoFromEntity(Request entity)
        {
            throw new System.NotImplementedException();
        }

        public Request DtoToEntity(WishDto dto)
        {
            throw new System.NotImplementedException();
        }
    }
}
