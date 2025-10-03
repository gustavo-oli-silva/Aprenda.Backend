using System;
using Aprenda.Backend.Dtos.Submission;
using Aprenda.Backend.Dtos.User;
using Aprenda.Backend.Mappers.Submission;
using Aprenda.Backend.Mappers.User;
using Aprenda.Backend.Models;
using Aprenda.Backend.Repositories.Archive;
using Aprenda.Backend.Repositories.Homework;
using Aprenda.Backend.Repositories.Submission;
using Aprenda.Backend.Repositories.User;
using Aprenda.Backend.Services.User;
namespace Aprenda.Backend.Services.Submission;

public class SubmissionService : ISubmissionService
{
    private readonly ISubmissionRepository _SubmissionRepository;
    private readonly IUserRepository _UserRepository;

    private readonly IHomeworkRepository _HomeworkRepository;

    private readonly IArchiveRepository _ArchiveRepository;

    private readonly IHttpContextAccessor _httpContextAccessor;

    public SubmissionService(ISubmissionRepository SubmissionRepository, IUserRepository userRepository, IHomeworkRepository HomeworkRepository, IArchiveRepository archiveRepository, IHttpContextAccessor httpContextAccessor)
    {
        _SubmissionRepository = SubmissionRepository;
        _UserRepository = userRepository;
        _HomeworkRepository = HomeworkRepository;
        _ArchiveRepository = archiveRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<SubmissionDto> CreateSubmissionAsync(long userId, long homeworkId, CreateSubmissionDto Submission)
    {
        var student = await _UserRepository.GetByIdAsync(userId);
        if (student == null || student.Profile != EProfile.Student)
        {
            throw new KeyNotFoundException("User not found or is not a student");
        }

        var homework = await _HomeworkRepository.GetByIdAsync(homeworkId);
        if (homework == null)
        {
            throw new KeyNotFoundException("Homework not found");
        }

       

        var SubmissionEntity = SubmissionMapper.ToDomain(Submission);

         var dueDateUtc = DateTime.SpecifyKind(homework.DueDate.Value, DateTimeKind.Utc);
        if (DateTime.UtcNow > dueDateUtc)
        {
            SubmissionEntity.Status = ESubmissionStatus.Overdue;
        }


        if (Submission.AttachmentIds != null && Submission.AttachmentIds.Any())
        {
            var archives = await _ArchiveRepository.GetByIdsAsync(Submission.AttachmentIds);

            if (archives.Count() != Submission.AttachmentIds.Count())
            {
                throw new KeyNotFoundException("One or more attached files were not found.");
            }

            foreach (var archive in archives)
            {
                SubmissionEntity.Archives.Add(archive);
            }

        }
        SubmissionEntity.UserId = student.Id;
        SubmissionEntity.HomeworkId = homework.Id;
        SubmissionEntity.SubmittedAt = DateTime.UtcNow;
        await _SubmissionRepository.AddAsync(SubmissionEntity);
        return SubmissionEntity.ToDto(_httpContextAccessor);
    }



    public async Task DeleteSubmissionAsync(long userId, long idSubmission)
    {
        var Submission = await _SubmissionRepository.GetByIdAsync(idSubmission);
        if (Submission == null)
        {
            throw new KeyNotFoundException("Submission not found");
        }

        if (Submission.UserId != userId)
        {
            throw new UnauthorizedAccessException("You are not authorized to delete this Submission");
        }

        await _SubmissionRepository.DeleteAsync(idSubmission);
    }

    public async Task<IEnumerable<SubmissionDto>> GetAllSubmissionsAsync()
    {
        var Submissions = await _SubmissionRepository.GetAllAsync();
        return Submissions.Select(c => c.ToDto(_httpContextAccessor));
    }


    public async Task<IEnumerable<SubmissionDto>> GetAllSubmissionsByHomeworkIdAsync(long userId, long homeworkId)
    {
        var student = await _UserRepository.GetByIdAsync(userId);
        if (student == null || student.Profile != EProfile.Student)
        {
            throw new KeyNotFoundException("User not found or is not a student");
        }
        var submissions = await _SubmissionRepository.GetAllSubmissionsByHomeworkIdAsync(userId, homeworkId);
        return submissions.Select(c => c.ToDto(_httpContextAccessor)).OrderByDescending(s => s.SubmittedAt);
    }

    public async Task<IEnumerable<SubmissionDto>> GetAllSubmissionsByHomeworkIdAsync(long homeworkId)
    {
        var submissions = await _SubmissionRepository.GetAllSubmissionsByHomeworkIdAsync(homeworkId);
        return submissions.Select(c => c.ToDto(_httpContextAccessor)).OrderByDescending(s => s.SubmittedAt);
    }

    public async Task<SubmissionDto> GetSubmissionByIdAsync(long id)
    {
        return await _SubmissionRepository.GetByIdAsync(id) is Models.Submission submission ? submission.ToDto(_httpContextAccessor) : throw new KeyNotFoundException("Submission not found");
    }

    public async Task UpdateSubmissionAsync(long userId, long idSubmission, CreateSubmissionDto submission)
    {
        var submissionEntity = await _SubmissionRepository.GetByIdAsync(idSubmission) ?? throw new KeyNotFoundException("Submission not found");
        if (submissionEntity.UserId != userId)
        {
            throw new UnauthorizedAccessException("You are not authorized to update this Submission");
        }
        // submissionEntity.Title = submission.Title;
        // submissionEntity.Content = submission.Content;
        // submissionEntity.IsFixed = submission.IsFixed;
        // submissionEntity.CreatedAt = DateTime.UtcNow;

        await _SubmissionRepository.UpdateAsync(submissionEntity);
    }



}
