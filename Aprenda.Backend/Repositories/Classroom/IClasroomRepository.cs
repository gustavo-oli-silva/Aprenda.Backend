using Aprenda.Backend.Models;

namespace Aprenda.Backend.Repositories.Classroom;

public interface IClassroomRepository
{
    Task<IEnumerable<Models.Classroom>> GetAllAsync();
    Task<Models.Classroom> GetByIdAsync(long id);
    Task AddAsync(Models.Classroom classroom);
    Task UpdateAsync(Models.Classroom classroom);
    Task DeleteAsync(long id);

    Task AssignUserToClassroom(Models.Classroom classroom, Models.User user);

    Task<Models.Classroom> GetClassroomWithUsersAsync(long id);

    Task<bool> InviteCodeExistsAsync(string code);

    Task<Models.Classroom> GetClassroomByInviteCodeAsync(string inviteCode);
}
