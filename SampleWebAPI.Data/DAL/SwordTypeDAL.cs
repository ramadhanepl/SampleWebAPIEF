using Microsoft.EntityFrameworkCore;
using SampleWebAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleWebAPI.Data.DAL
{
    public class SwordTypeDAL : ISwordType
    {
        private readonly SamuraiContext _context;

        public SwordTypeDAL(SamuraiContext context)
        {
            _context = context;
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<SwordType>> GetAll()
        {
            var results = await _context.SwordType.OrderBy(s => s.Style).ToListAsync();
            return results;
        }

        public Task<SwordType> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<SwordType> Insert(SwordType obj)
        {
            try
            {
                _context.SwordType.Add(obj);
                await _context.SaveChangesAsync();
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public Task<SwordType> Update(SwordType obj)
        {
            throw new NotImplementedException();
        }
    }
}
