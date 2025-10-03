using System;
using Aprenda.Backend.Dtos.Grade;
using Aprenda.Backend.Dtos.User;
using Aprenda.Backend.Mappers.Grade;
using Aprenda.Backend.Mappers.User;
using Aprenda.Backend.Models;
using Aprenda.Backend.Repositories.Archive;
using Aprenda.Backend.Repositories.Classroom;
using Aprenda.Backend.Repositories.Grade;
using Aprenda.Backend.Repositories.Submission;
using Aprenda.Backend.Repositories.User;
using Aprenda.Backend.Services.User;
namespace Aprenda.Backend.Services.Grade;

public class GradeService : IGradeService
{
    private readonly IGradeRepository _GradeRepository;
    private readonly IUserRepository _UserRepository;

    private readonly IClassroomRepository _ClassroomRepository;

    private readonly ISubmissionRepository _SubmissionRepository;

    public GradeService(IGradeRepository GradeRepository, IUserRepository userRepository, IClassroomRepository classroomRepository, ISubmissionRepository submissionRepository)
    {
        _GradeRepository = GradeRepository;
        _UserRepository = userRepository;
        _ClassroomRepository = classroomRepository;
        _SubmissionRepository = submissionRepository;
    }

    public async Task<GradeDto> CreateGradeAsync(long idSubmission, CreateGradeDto Grade)
    {
        var submission = await _SubmissionRepository.GetByIdAsync(idSubmission);
        if (submission == null)
        {
            throw new Exception("Submission not found");
        }
        var gradeDomain = GradeMapper.ToDomain(Grade);
        gradeDomain.SubmissionId = submission.Id;
        submission.Status = ESubmissionStatus.Graded;
        await _SubmissionRepository.UpdateAsync(submission); 
        await _GradeRepository.AddAsync(gradeDomain);
        return gradeDomain.ToDto();
    }

    public Task<GradeDto> GetGradeBySubmissionIdAsync(long idSubmission)
    {
        throw new NotImplementedException();
    }
}
