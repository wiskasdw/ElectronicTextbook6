using ElectronicTextbook.Core.Interfaces;
using ElectronicTextbook.Core.Models;
using ElectronicTextbook.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectronicTextbook.Infrastructure.Repositories
{
    public class LectureRepository : ILectureRepository
    {
        private readonly AppDbContext _context;

        public LectureRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Lecture> GetByIdAsync(int id)
        {
            return await _context.Lectures.FindAsync(id);
        }

        public async Task<IEnumerable<Lecture>> GetAllAsync()
        {
            return await _context.Lectures.ToListAsync();
        }

        public async Task AddAsync(Lecture lecture)
        {
            await _context.Lectures.AddAsync(lecture);
        }

        public async Task UpdateAsync(Lecture lecture)
        {
            _context.Lectures.Update(lecture);
        }

        public async Task DeleteAsync(int id)
        {
            var lecture = await _context.Lectures.FindAsync(id);
            if (lecture != null)
            {
                _context.Lectures.Remove(lecture);
            }
        }
        public async Task<IEnumerable<Lecture>> SearchAsync(string searchTerm)
        {
            return await _context.Lectures.Where(l =>
                l.Title.Contains(searchTerm) ||
                l.Description.Contains(searchTerm)
           ).ToListAsync();
        }
    }
}