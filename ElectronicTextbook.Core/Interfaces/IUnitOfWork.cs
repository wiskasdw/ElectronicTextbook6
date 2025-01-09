using System.Threading.Tasks;

namespace ElectronicTextbook.Core.Interfaces
{
    public interface IUnitOfWork
    {
        ILectureRepository Lectures { get; }
        Task SaveAsync();
    }
}
