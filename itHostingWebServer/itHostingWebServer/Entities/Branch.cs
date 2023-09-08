using System;
using System.Collections.Generic;

namespace itHostingWebServer.Entities;

public partial class Branch
{
    public int Id { get; set; }

    public int RepositoryId { get; set; }

    public string Title { get; set; } = null!;

    public bool? IsMain { get; set; }

    public virtual ICollection<File> Files { get; set; } = new List<File>();

    public virtual ICollection<PullRequest> PullRequestFromBranches { get; set; } = new List<PullRequest>();

    public virtual ICollection<PullRequest> PullRequestToBranches { get; set; } = new List<PullRequest>();

    public virtual Repository Repository { get; set; } = null!;
}
