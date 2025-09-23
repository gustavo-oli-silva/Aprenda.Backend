using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aprenda.Backend.Models;

public class Post
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }

    public bool IsFixed { get; set; }

    public long UserId { get; set; }
    public virtual User User { get; set; }


    public long ClassroomId { get; set; }
    public virtual Classroom Classroom { get; set; }

    public virtual ICollection<Archive> Archives { get; set; } = new List<Archive>();
}
