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



}
