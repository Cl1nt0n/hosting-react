using System;
using System.Collections.Generic;

namespace itHostingWebServer.Entities;

public partial class User
{
    public int Id { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Nickname { get; set; } = null!;

    public string? Image { get; set; }

    public virtual ICollection<Collaborator> Collaborators { get; set; } = new List<Collaborator>();

    public virtual ICollection<Repository> Repositories { get; set; } = new List<Repository>();
}
