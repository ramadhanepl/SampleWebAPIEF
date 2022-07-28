using SampleWebAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleWebAPI.Data.DAL
{
    public interface ISamurai : ICrud<Samurai>
    {
        Task<IEnumerable<Samurai>> GetByName(string name);
        Task<IEnumerable<Samurai>> GetSamuraiWithQuote();
        Task<IEnumerable<Samurai>> GetSamuraiWithSwordElement();
        Task<Samurai> AddSamuraiWithSword(Samurai obj);
    }
}
