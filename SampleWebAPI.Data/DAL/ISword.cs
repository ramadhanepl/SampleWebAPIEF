﻿using SampleWebAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleWebAPI.Data.DAL
{
    public interface ISword : ICrud<Sword>
    {
        Task<IEnumerable<Sword>> GetBySwordName(string swordName);
        Task<IEnumerable<Sword>> GetSwordWithElement();
        Task<SwordType> AddSwordWithType(SwordType obj);
        Task<Element> AddSwordToExistingElement(Sword obj);


    }
}