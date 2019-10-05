using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces.Intefaces
{
    public interface IMapper<TEntity, TDto>
    {
        TDto DtoFromEntity(TEntity entity);
        TEntity DtoToEntity(TDto dto);
    }
}
