using Aprenda.Backend.Models;

namespace Aprenda.Backend.Repositories.Archive;

public interface IArchiveRepository
{
    Task<IEnumerable<Models.Archive>> GetAllAsync();
    Task<Models.Archive> GetByIdAsync(long id);

    Task<Models.Archive> GetByStoredNameAsync(string storedName);
    Task<Models.Archive> AddAsync(Models.Archive Archive);
    Task UpdateAsync(Models.Archive Archive);
    Task DeleteAsync(long id);
}
