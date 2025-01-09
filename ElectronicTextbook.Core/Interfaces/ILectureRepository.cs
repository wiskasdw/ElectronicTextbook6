using ElectronicTextbook.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElectronicTextbook.Core.Interfaces
{
    public interface ILectureRepository
    {
        Task<Lecture> GetByIdAsync(int id);
        Task<IEnumerable<Lecture>> GetAllAsync();
        Task AddAsync(Lecture lecture);
        Task UpdateAsync(Lecture lecture);
        Task DeleteAsync(int id);
        Task<IEnumerable<Lecture>> SearchAsync(string searchTerm);
    }
}