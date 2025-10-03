using System;
using Aprenda.Backend.Models;
namespace Aprenda.Backend.Services.Grade;

public interface IGradeService
{

    Task<Dtos.Grade.GradeDto> GetGradeBySubmissionIdAsync(long idSubmission);
    Task<Dtos.Grade.GradeDto> CreateGradeAsync(long idSubmission, Dtos.Grade.CreateGradeDto Grade);

}
