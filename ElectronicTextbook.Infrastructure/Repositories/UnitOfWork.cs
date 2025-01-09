using System.Threading.Tasks;
using ElectronicTextbook.Core.Interfaces;
using ElectronicTextbook.Infrastructure.Data;

namespace ElectronicTextbook.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public ILectureRepository Lectures { get; }

        public UnitOfWork(AppDbContext context, ILectureRepository lectureRepository)
        {
            _context = context;
            Lectures = lectureRepository;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
