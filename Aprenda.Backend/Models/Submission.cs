using System;

namespace Aprenda.Backend.Models;

public class Submission
{
    public long Id { get; set; }

    public DateTime SubmittedAt { get; set; }

    public ESubmissionStatus Status { get; set; }

    public long GradeId { get; set; }
      public virtual Grade Grade { get; set; }

    // Relacionamento N-para-1: Aluno que fez a entrega.
    public long UserId { get; set; }
    public virtual User User { get; set; }

    // Relacionamento N-para-1: Atividade a que se refere.
    public long HomeworkId { get; set; }
    public virtual Homework Homework { get; set; }


    // Relacionamento 1-para-N: Arquivos anexados à entrega.
    public virtual ICollection<Archive> Archives { get; set; } = new List<Archive>();
}
