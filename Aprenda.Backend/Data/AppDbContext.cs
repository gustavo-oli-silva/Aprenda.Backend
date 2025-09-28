using Aprenda.Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Aprenda.Backend.Data;

public class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    public DbSet<Classroom> Classrooms { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Homework> Homeworks { get; set; }
    public DbSet<Submission> Submissions { get; set; }
    public DbSet<Grade> Grades { get; set; }
    public DbSet<Archive> Archives { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuração da hierarquia TPH (Table Per Hierarchy) para Post e Homework
        modelBuilder.Entity<Post>()
            .HasDiscriminator<string>("PostType")
            .HasValue<Post>("Post")
            .HasValue<Homework>("Homework");

        // Relacionamento User -> Archive (Avatar) - 1:1 opcional
        modelBuilder.Entity<User>()
            .HasOne(u => u.Avatar)
            .WithMany()
            .HasForeignKey(u => u.AvatarId)
            .OnDelete(DeleteBehavior.SetNull);

        // Relacionamento User -> Submissions - 1:N
        modelBuilder.Entity<User>()
            .HasMany(u => u.Submissions)
            .WithOne(s => s.User)
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // Relacionamento User -> Posts - 1:N
        modelBuilder.Entity<User>()
            .HasMany(u => u.Posts)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Relacionamento User <-> Classroom - N:N
        modelBuilder.Entity<User>()
            .HasMany(u => u.Classrooms)
            .WithMany(c => c.Users)
            .UsingEntity(j => j.ToTable("UserClassrooms"));

        // Relacionamento Classroom -> Archive (Banner) - 1:1 opcional
        modelBuilder.Entity<Classroom>()
            .HasOne(c => c.Banner)
            .WithMany()
            .HasForeignKey(c => c.BannerId)
            .OnDelete(DeleteBehavior.Restrict);

        // Relacionamento Classroom -> Archive (Icon) - 1:1 opcional
        modelBuilder.Entity<Classroom>()
            .HasOne(c => c.Icon)
            .WithMany()
            .HasForeignKey(c => c.IconId)
            .OnDelete(DeleteBehavior.Restrict);

        // Relacionamento Classroom -> Posts - 1:N
        modelBuilder.Entity<Classroom>()
            .HasMany(c => c.Posts)
            .WithOne(p => p.Classroom)
            .HasForeignKey(p => p.ClassroomId)
            .OnDelete(DeleteBehavior.Cascade);

        // Relacionamento Post -> Archives - 1:N
        modelBuilder.Entity<Post>()
            .HasMany(p => p.Archives)
            .WithOne()
            .OnDelete(DeleteBehavior.NoAction);

        // Relacionamento Homework -> Submissions - 1:N
        modelBuilder.Entity<Homework>()
            .HasMany(h => h.Submissions)
            .WithOne(s => s.Homework)
            .HasForeignKey(s => s.HomeworkId)
            .OnDelete(DeleteBehavior.Cascade);

        // Relacionamento Submission -> Grade - 1:1
        modelBuilder.Entity<Submission>()
         .HasOne(s => s.Grade)
         .WithOne(g => g.Submission)
         .HasForeignKey<Grade>(g => g.SubmissionId)
         .OnDelete(DeleteBehavior.Cascade);

        // Relacionamento Submission -> Archives - 1:N
        modelBuilder.Entity<Submission>()
            .HasMany(s => s.Archives)
            .WithOne()
            .OnDelete(DeleteBehavior.NoAction);

        // Configurações de propriedades
        modelBuilder.Entity<User>()
            .Property(u => u.Profile)
            .HasConversion<int>();

        modelBuilder.Entity<Submission>()
            .Property(s => s.Status)
            .HasConversion<int>();

        // Configurações de índices únicos
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        // Configurações de campos obrigatórios
        modelBuilder.Entity<User>()
            .Property(u => u.Name)
            .IsRequired()
            .HasMaxLength(200);

        modelBuilder.Entity<User>()
            .Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(300);

        modelBuilder.Entity<User>()
            .Property(u => u.Password)
            .IsRequired();

        modelBuilder.Entity<Classroom>()
            .Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(200);

        modelBuilder.Entity<Post>()
            .Property(p => p.Title)
            .IsRequired()
            .HasMaxLength(300);

        modelBuilder.Entity<Post>()
            .Property(p => p.Content)
            .IsRequired();

        modelBuilder.Entity<Archive>()
            .Property(a => a.OriginalName)
            .IsRequired()
        .HasMaxLength(255);


        modelBuilder.Entity<Archive>()
            .Property(a => a.StoredName)
            .IsRequired()
            .HasMaxLength(255);

        modelBuilder.Entity<Archive>()
            .Property(a => a.ContentType)
            .IsRequired()
            .HasMaxLength(100);

        modelBuilder.Entity<Archive>()
            .HasIndex(a => a.StoredName)
            .IsUnique();

        modelBuilder.Entity<Grade>()
            .Property(g => g.Value)
            .HasPrecision(5, 2);

        modelBuilder.Entity<Classroom>()
            .HasIndex(c => c.InviteCode)
            .IsUnique();

    }
}