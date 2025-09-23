using System;
using System.Net.Mail;

namespace Aprenda.Backend.Models;

public class Classroom
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    public long? BannerId { get; set; }
    public virtual Archive Banner { get; set; }

    public long? IconId { get; set; }
    public virtual Archive Icon { get; set; }

}
