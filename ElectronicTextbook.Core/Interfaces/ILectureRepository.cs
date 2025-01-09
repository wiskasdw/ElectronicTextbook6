using System.Collections.Generic;
using System.Threading.Tasks;
using ElectronicTextbook.Core.Models;

namespace ElectronicTextbook.Core.Interfaces
{
    public interface ILectureRepository
    {
        Task<IEnumerable<Lecture>> GetAllAsync();
        Task<Lecture> GetByIdAsync(int id);
        Task AddAsync(Lecture lecture);
        Task UpdateAsync(Lecture lecture);
        Task DeleteAsync(int id);
        Task<IEnumerable<Lecture>> SearchAsync(string searchTerm);
    }
}
