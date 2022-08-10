  using Microsoft.EntityFrameworkCore;
using SampleWebAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleWebAPI.Data.DAL
{
    public class SwordDAL : ISword
    {
        private readonly SamuraiContext _context;

        public SwordDAL(SamuraiContext context)
        {
            _context = context;
        }

        public async Task<Sword> AddElementToExistingSword(Sword obj)
        {
            try
            {
                var updateSword = await _context.Swords.Include(s => s.Elements).FirstOrDefaultAsync(s => s.Id == obj.Id);
                if (updateSword == null)
                    throw new Exception($"Data samurai dengan id {obj.Id} tidak ditemukan");

                updateSword.Elements = obj.Elements;
                await _context.SaveChangesAsync();
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<Sword> AddSwordWithType(Sword obj)
        {
            try
            {
                _context.Swords.Add(obj);
                await _context.SaveChangesAsync();
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                var deleteSword = await _context.Swords.FirstOrDefaultAsync(s => s.Id == id);
                if (deleteSword == null)
                    throw new Exception($"Data samurai dengan id {id} tidak ditemukan");
                _context.Swords.Remove(deleteSword);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<Sword> DeleteElementInSword(int id)
        {
            var result = await _context.Swords.Include(s => s.Elements).FirstOrDefaultAsync(s => s.Id == id);
            if (result == null) throw new Exception($"Data sword dengan id {id} tidak ditemukan");

            result.Elements = null;
            await _context.SaveChangesAsync();
            return result;
        }

        public async Task<IEnumerable<Sword>> GetAll()
        {
            var results = await _context.Swords.Include(t => t.SwordType).OrderBy(s => s.Weight).ToListAsync();
            return results;
        }

        public async Task<Sword> GetById(int id)
        {
            var result = await _context.Swords.FirstOrDefaultAsync(s => s.Id == id);
            if (result == null) throw new Exception($"Data sword dengan id {id} tidak ditemukan");
            return result;
        }

        public async Task<IEnumerable<Sword>> GetBySwordName(string swordName)
        {
            var swords = await _context.Swords.Where(s => s.SwordName.Contains(swordName))
                 .OrderBy(s => s.SwordName).ToListAsync();
            return swords;
        }

        public async Task<IEnumerable<Sword>> GetSwordWithElement()
        {
            var swords = await _context.Swords.Include(s => s.Elements).OrderBy(s => s.SwordName).ToListAsync();
            
            foreach (var sword in swords)
            {
                await _context.Swords.Include(t => t.SwordType).OrderBy(s => s.SwordName).ToListAsync();
            }
            return swords;
        }

        public async Task<IEnumerable<Sword>> GetSwordWithType(int page)
        {
            var pageResults = 10f;
            var pageCount = Math.Ceiling(_context.Swords.Count() / pageResults);

            var swords = await _context.Swords.Include(s => s.SwordType).OrderBy(s => s.SwordName)
                .Skip((page - 1) * (int)pageResults)
                .Take((int)pageResults)
                .ToListAsync();

            
            return swords;
        }

        public async Task<Sword> Insert(Sword obj)
        {
            try
            {
                _context.Swords.Add(obj);
                await _context.SaveChangesAsync();
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<Sword> Update(Sword obj)
        {
            try
            {
                var updateSword = await _context.Swords.FirstOrDefaultAsync(s => s.Id == obj.Id);
                if (updateSword == null)
                    throw new Exception($"Data samurai dengan id {obj.Id} tidak ditemukan");

                updateSword.SwordName = obj.SwordName;
                await _context.SaveChangesAsync();
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
