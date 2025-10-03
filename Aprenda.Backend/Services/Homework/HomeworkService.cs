using System;
using Aprenda.Backend.Dtos.Homework;
using Aprenda.Backend.Dtos.User;
using Aprenda.Backend.Mappers.Homework;
using Aprenda.Backend.Mappers.User;
using Aprenda.Backend.Models;
using Aprenda.Backend.Repositories.Archive;
using Aprenda.Backend.Repositories.Classroom;
using Aprenda.Backend.Repositories.Homework;
using Aprenda.Backend.Repositories.User;
using Aprenda.Backend.Services.User;
namespace Aprenda.Backend.Services.Homework;

public class HomeworkService : IHomeworkService
{
    private readonly IHomeworkRepository _HomeworkRepository;
    private readonly IUserRepository _UserRepository;

    private readonly IClassroomRepository _ClassroomRepository;

    private readonly IArchiveRepository _ArchiveRepository;

    private readonly IHttpContextAccessor _httpContextAccessor;

    public HomeworkService(IHomeworkRepository HomeworkRepository, IUserRepository userRepository, IClassroomRepository classroomRepository, IArchiveRepository archiveRepository)
    {
        _HomeworkRepository = HomeworkRepository;
        _UserRepository = userRepository;
        _ClassroomRepository = classroomRepository;
        _ArchiveRepository = archiveRepository;
    }

    public async Task<HomeworkDto> CreateHomeworkAsync(long userId, long classroomId, CreateHomeworkDto Homework)
    {
        var professor = await _UserRepository.GetByIdAsync(userId);
        if (professor == null || professor.Profile != EProfile.Professor)
        {
            throw new KeyNotFoundException("User not found or is not a professor");
        }

        var classroom = await _ClassroomRepository.GetByIdAsync(classroomId);
        if (classroom == null)
        {
            throw new KeyNotFoundException("Classroom not found");
        }

        if (!classroom.Users.Any(u => u.Id == professor.Id))
        {
            throw new InvalidOperationException("This professor is not part of the classroom");
        }


        var HomeworkEntity = HomeworkMapper.ToDomain(Homework);

        if (Homework.AttachmentIds != null && Homework.AttachmentIds.Any())
        {
            var archives = await _ArchiveRepository.GetByIdsAsync(Homework.AttachmentIds);

            if (archives.Count() != Homework.AttachmentIds.Count())
            {
                throw new KeyNotFoundException("One or more attached files were not found.");
            }

            foreach (var archive in archives)
            {
                HomeworkEntity.Archives.Add(archive);
            }

        }
        HomeworkEntity.UserId = professor.Id;
        HomeworkEntity.ClassroomId = classroom.Id;
        HomeworkEntity.CreatedAt = DateTime.UtcNow;
        await _HomeworkRepository.AddAsync(HomeworkEntity);
        return HomeworkEntity.ToDto(_httpContextAccessor);
    }

    public async Task DeleteHomeworkAsync(long id)
    {
        await _HomeworkRepository.DeleteAsync(id);
    }

    public async Task DeleteHomeworkAsync(long userId, long idHomework)
    {
        var Homework = await _HomeworkRepository.GetByIdAsync(idHomework);
        if (Homework == null)
        {
            throw new KeyNotFoundException("Homework not found");
        }

        if (Homework.UserId != userId)
        {
            throw new UnauthorizedAccessException("You are not authorized to delete this Homework");
        }

        await _HomeworkRepository.DeleteAsync(idHomework);
    }

    public async Task<IEnumerable<HomeworkDto>> GetAllHomeworksAsync()
    {
        var Homeworks = await _HomeworkRepository.GetAllAsync();
        return Homeworks.Select(c => c.ToDto(_httpContextAccessor));
    }

    public async Task<IEnumerable<HomeworkDto>> GetAllHomeworksByClassroomIdAsync(long classroomId)
    {
        var Homeworks = await _HomeworkRepository.GetHomeworksByClassroomIdAsync(classroomId);
        return Homeworks.Select(c => c.ToDto(_httpContextAccessor));
    }

    public async Task<HomeworkDto> GetHomeworkByIdAsync(long id)
    {
        return await _HomeworkRepository.GetByIdAsync(id) is Models.Homework Homework ? Homework.ToDto(_httpContextAccessor) : throw new KeyNotFoundException("Homework not found");
    }

    public async Task UpdateHomeworkAsync(long userId, long idHomework, CreateHomeworkDto Homework)
    {
        var HomeworkEntity = await _HomeworkRepository.GetByIdAsync(idHomework) ?? throw new KeyNotFoundException("Homework not found");
        if (HomeworkEntity.UserId != userId)
        {
            throw new UnauthorizedAccessException("You are not authorized to update this Homework");
        }
        HomeworkEntity.Title = Homework.Title;
        HomeworkEntity.Content = Homework.Content;
        HomeworkEntity.IsFixed = Homework.IsFixed;
        HomeworkEntity.CreatedAt = DateTime.UtcNow;

        await _HomeworkRepository.UpdateAsync(HomeworkEntity);
    }



}
