using System;
using System.Collections.Generic;

namespace itHostingWebServer.Entities;

public partial class File
{
    public int Id { get; set; }

    public string Text { get; set; } = null!;

    public int BranchId { get; set; }

    public string Title { get; set; } = null!;

    public virtual Branch Branch { get; set; } = null!;

    public virtual ICollection<Commit> Commits { get; set; } = new List<Commit>();
}
