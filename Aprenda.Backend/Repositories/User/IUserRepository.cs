using Aprenda.Backend.Models;

namespace Aprenda.Backend.Repositories.User;

public interface IUserRepository
{
    Task<IEnumerable<Models.User>> GetAllAsync();
    Task<Models.User> GetByIdAsync(long id);
    Task AddAsync(Models.User user);
    Task UpdateAsync(Models.User user);
    Task DeleteAsync(long id);
}
