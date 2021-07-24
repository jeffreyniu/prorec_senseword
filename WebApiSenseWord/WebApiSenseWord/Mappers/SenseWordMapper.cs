using Infrastructure.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiSenseWord.Mappers.Interfaces;
using WebApiSenseWord.Models;

namespace WebApiSenseWord.Mappers
{
    public class SenseWordMapper : ISenseWordMapper
    {
        public SenseWordModel Map(SenseWordEntity entity)
        {
            return new SenseWordModel
            {
                ID = entity.ID,
                Word = entity.Word,
                Enabled = entity.Enabled,
                CreatedDate = entity.CreatedDate,
                LastUpdatedDate = entity.LastUpdatedDate
            };
        }
    }
}
