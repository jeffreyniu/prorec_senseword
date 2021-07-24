using Infrastructure.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiSenseWord.Models;

namespace WebApiSenseWord.Mappers.Interfaces
{
    public interface ISenseWordMapper : IMapper<SenseWordEntity, SenseWordModel>
    {
    }
}
