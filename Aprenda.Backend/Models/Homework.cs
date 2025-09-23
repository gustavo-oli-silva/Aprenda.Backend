using System;

namespace Aprenda.Backend.Models;

public class Homework : Post
{
    public DateTime DueDate { get; set; }

    public ICollection<Submission> Submissions { get; set; } = new List<Submission>();

    
}
