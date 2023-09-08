using System;
using System.Collections.Generic;

namespace itHostingWebServer.Entities;

public partial class Collaborator
{
    public int Id { get; set; }

    public int RepositoryId { get; set; }

    public int UserId { get; set; }

    public virtual Repository Repository { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
