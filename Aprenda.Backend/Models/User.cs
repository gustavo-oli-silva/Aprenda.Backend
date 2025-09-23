using System;

namespace Aprenda.Backend.Models;

public class User
{

    public long Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime CreatedAt { get; set; }

    // public File Files { get; set; }



    public EProfile Profile { get; set; }

    public long? AvatarId { get; set; }
    public virtual Archive Avatar { get; set; }

    public virtual ICollection<Submission> Submissions { get; set; } = new List<Submission>();
    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    public virtual ICollection<Classroom> Classrooms { get; set; } = new List<Classroom>();
}
