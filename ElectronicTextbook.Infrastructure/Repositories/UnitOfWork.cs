using ElectronicTextbook.Core.Interfaces;
using ElectronicTextbook.Infrastructure.Data;
using System.Threading.Tasks;

namespace ElectronicTextbook.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public ILectureRepository Lectures => new LectureRepository(_context);
        public IUserRepository Users => new UserRepository(_context);

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}