using System;
using System.Collections.Generic;

namespace itHostingWebServer.Entities;

public partial class Repository
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsPrivate { get; set; }

    public virtual ICollection<Branch> Branches { get; set; } = new List<Branch>();

    public virtual ICollection<Collaborator> Collaborators { get; set; } = new List<Collaborator>();

    public virtual User User { get; set; } = null!;
}
