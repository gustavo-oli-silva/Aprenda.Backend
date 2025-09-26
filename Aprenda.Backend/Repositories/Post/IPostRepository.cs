using Aprenda.Backend.Models;

namespace Aprenda.Backend.Repositories.Post;

public interface IPostRepository
{
    Task<IEnumerable<Models.Post>> GetAllAsync();
    Task<Models.Post> GetByIdAsync(long id);

    Task<IEnumerable<Models.Post>> GetPostsByClassroomIdAsync(long idClassroom);

    Task AddAsync(Models.Post Post);
    Task UpdateAsync(Models.Post Post);
    Task DeleteAsync(long id);
}
