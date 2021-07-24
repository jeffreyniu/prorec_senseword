using Application.Adapter.SenseWord.Criteria;
using Infrastructure.Entity;
using Infrastructure.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Adapter.SenseWord.Interfaces
{
    public interface ISenseWordAdapter
    {
        SenseWordEntity Get(GetCriterion criterion);
    }
}
