using Microsoft.EntityFrameworkCore;
using SampleWebAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleWebAPI.Data.DAL
{
    public class ElementDAL : IElement
    {
        private readonly SamuraiContext _context;

        public ElementDAL(SamuraiContext context)
        {
            _context = context;
        }


        public async Task Delete(int id)
        {
            try
            {
                var deleteElement = await _context.Elements.FirstOrDefaultAsync(s => s.Id == id);
                if (deleteElement == null)
                    throw new Exception($"Data Element dengan id {id} tidak ditemukan");
                _context.Elements.Remove(deleteElement);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<IEnumerable<Element>> GetAll()
        {
            var results = await _context.Elements.OrderBy(s => s.ElementType).ToListAsync();
            return results;
        }

        public async Task<Element> GetById(int id)
        {
            var result = await _context.Elements.FirstOrDefaultAsync(s => s.Id == id);
            if (result == null) throw new Exception($"Data Element dengan id {id} tidak ditemukan");
            return result;
        }

        public async Task<IEnumerable<Element>> GetByName(string name)
        {
            var elements = await _context.Elements.Where(s => s.ElementType.Contains(name))
                .OrderBy(s => s.ElementType).ToListAsync();
            return elements;
        }

        public async Task<Element> Insert(Element obj)
        {
            try
            {
                _context.Elements.Add(obj);
                await _context.SaveChangesAsync();
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<Element> Update(Element obj)
        {
            try
            {
                var updateElement = await _context.Elements.FirstOrDefaultAsync(s => s.Id == obj.Id);
                if (updateElement == null)
                    throw new Exception($"Data samurai dengan id {obj.Id} tidak ditemukan");

                updateElement.ElementType = obj.ElementType;
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
