using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace itHostingWebServer.Entities;

public partial class ItHostingContext : DbContext
{
    public ItHostingContext()
    {
    }

    public ItHostingContext(DbContextOptions<ItHostingContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Branch> Branches { get; set; }

    public virtual DbSet<Collaborator> Collaborators { get; set; }

    public virtual DbSet<Commit> Commits { get; set; }

    public virtual DbSet<File> Files { get; set; }

    public virtual DbSet<PullRequest> PullRequests { get; set; }

    public virtual DbSet<Repository> Repositories { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;user=root;password=1111;database=it-hosting;charset=utf8", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.32-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Branch>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("branches");

            entity.HasIndex(e => e.RepositoryId, "FK_branches_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IsMain).HasColumnName("isMain");
            entity.Property(e => e.RepositoryId).HasColumnName("repository_id");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");

            entity.HasOne(d => d.Repository).WithMany(p => p.Branches)
                .HasForeignKey(d => d.RepositoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_branches_id");
        });

        modelBuilder.Entity<Collaborator>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("collaborators");

            entity.HasIndex(e => e.RepositoryId, "FK_collaborators_repository_id");

            entity.HasIndex(e => e.UserId, "FK_collaborators_user_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.RepositoryId).HasColumnName("repository_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Repository).WithMany(p => p.Collaborators)
                .HasForeignKey(d => d.RepositoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_collaborators_repository_id");

            entity.HasOne(d => d.User).WithMany(p => p.Collaborators)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_collaborators_user_id");
        });

        modelBuilder.Entity<Commit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("commits");

            entity.HasIndex(e => e.FileId, "FK_commits_file_id");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatingDate)
                .HasColumnType("datetime")
                .HasColumnName("creating_date");
            entity.Property(e => e.FileId).HasColumnName("file_id");
            entity.Property(e => e.Text)
                .HasMaxLength(255)
                .HasColumnName("text");

            entity.HasOne(d => d.File).WithMany(p => p.Commits)
                .HasForeignKey(d => d.FileId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_commits_file_id");
        });

        modelBuilder.Entity<File>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("files");

            entity.HasIndex(e => e.BranchId, "FK_files_branch_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BranchId).HasColumnName("branch_id");
            entity.Property(e => e.Text)
                .HasColumnType("text")
                .HasColumnName("text");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");

            entity.HasOne(d => d.Branch).WithMany(p => p.Files)
                .HasForeignKey(d => d.BranchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_files_branch_id");
        });

        modelBuilder.Entity<PullRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("pull requests");

            entity.HasIndex(e => e.FromBranchId, "FK_pull requests_FirstBranchID");

            entity.HasIndex(e => e.ToBranchId, "FK_pull requests_SecondBranchID");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FromBranchId).HasColumnName("from_branch_id");
            entity.Property(e => e.ToBranchId).HasColumnName("to_branch_id");

            entity.HasOne(d => d.FromBranch).WithMany(p => p.PullRequestFromBranches)
                .HasForeignKey(d => d.FromBranchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_pull requests_FirstBranchID");

            entity.HasOne(d => d.ToBranch).WithMany(p => p.PullRequestToBranches)
                .HasForeignKey(d => d.ToBranchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_pull requests_SecondBranchID");
        });

        modelBuilder.Entity<Repository>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("repositories");

            entity.HasIndex(e => e.UserId, "FK_repositories_user_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasDefaultValueSql("' '")
                .HasColumnName("description");
            entity.Property(e => e.IsPrivate).HasColumnName("isPrivate");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Repositories)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_repositories_user_id");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("users");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Image)
                .HasMaxLength(255)
                .HasDefaultValueSql("'avatarka-pustaya-vk_0.jpg'")
                .HasColumnName("image");
            entity.Property(e => e.Login)
                .HasMaxLength(255)
                .HasColumnName("login");
            entity.Property(e => e.Nickname)
                .HasMaxLength(255)
                .HasColumnName("nickname");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
