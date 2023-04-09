using Microsoft.EntityFrameworkCore;
using University.Models;

namespace University.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Coursework> Courseworks { get; set; }
        public DbSet<Submission> Submissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>()
                .HasMany(c => c.Assignments)
                .WithOne(a => a.Course)
                .HasForeignKey(a => a.CourseId);

            modelBuilder.Entity<Course>()
                .HasMany(c => c.Courseworks)
                .WithOne(cw => cw.Course)
                .HasForeignKey(cw => cw.CourseId);

            modelBuilder.Entity<Student>()
                .HasMany(s => s.Assignments)
                .WithOne(a => a.Student)
                .HasForeignKey(a => a.StudentId);

            modelBuilder.Entity<Student>()
                .HasMany(s => s.Courseworks)
                .WithOne(cw => cw.Student)
                .HasForeignKey(cw => cw.StudentId);

            modelBuilder.Entity<Coursework>()
                .HasOne(cw => cw.Submission)
                .WithOne(s => s.Coursework)
                .HasForeignKey<Submission>(s => s.CourseworkId);
        }
    }
}
