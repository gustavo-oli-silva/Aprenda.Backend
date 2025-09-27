using Aprenda.Backend.Models;

namespace Aprenda.Backend.Repositories.Homework;

public interface IHomeworkRepository
{
    Task<IEnumerable<Models.Homework>> GetAllAsync();
    Task<Models.Homework> GetByIdAsync(long id);

    Task<IEnumerable<Models.Homework>> GetHomeworksByClassroomIdAsync(long idClassroom);

    Task AddAsync(Models.Homework Homework);
    Task UpdateAsync(Models.Homework Homework);
    Task DeleteAsync(long id);
}
