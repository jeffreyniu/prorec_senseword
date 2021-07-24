using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiSenseWord.Mappers.Interfaces
{
    public interface IMapper<TSourceEntity, TTargetModel>
    {
        TTargetModel Map(TSourceEntity entity);
    }
}
