using Microsoft.EntityFrameworkCore;
using PIMTool.Core.Domain.Entities;

namespace PIMTool.Database
{
    public class PimContext : DbContext
    {
        // TODO: Define your models
        public DbSet<Project> Projects { get; set; } = null!;

        public DbSet<Employee> Employees { get; set; } = null!;

        public PimContext(DbContextOptions options) : base(options)
        {
        }

        public PimContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(
                entity =>
                {
                    entity.ToTable("EMPLOYEE");
                    entity.HasKey(e => e.Id);
                    entity.Property(e => e.Id)
                        .HasColumnName("ID");
                    entity.Property(e => e.Visa)
                        .HasColumnName("VISA")
                        .HasMaxLength(3);
                    entity.Property(e => e.FirstName)
                        .HasColumnName("FIRST_NAME")
                        .HasMaxLength(50);
                    entity.Property(e => e.LastName)
                        .HasColumnName("LAST_NAME")
                        .HasMaxLength(50);
                    entity.Property(e => e.Birthday)
                        .HasColumnName("BIRTH_DATE");
                    entity.Property(e => e.Version)
                        .HasColumnName("VERSION")
                        .IsRowVersion();
                }
            );

            modelBuilder.Entity<Group>(
                entity =>
                {
                    entity.ToTable("GROUP");
                    entity.HasKey(e => e.Id);
                    entity.Property(e => e.Id)
                        .HasColumnName("ID");
                    entity.Property(e => e.GroupLeaderId)
                        .HasColumnName("GROUP_LEADER_ID");
                    entity.HasOne(g => g.Employee)
                        .WithOne(e => e.Group)
                        .HasForeignKey<Group>(e => e.GroupLeaderId);
                    entity.Property(e => e.Version)
                        .HasColumnName("VERSION")
                        .IsRowVersion();
                }
                );

            modelBuilder.Entity<Project>(
                entity =>
                {
                    entity.ToTable("PROJECT")
                        .HasKey(e => e.Id);
                    entity.Property(e => e.Id)
                        .HasColumnName("ID");
                    entity.Property(e => e.ProjectNumber)
                        .HasColumnName("PROJECT_NUMBER");
                    entity.Property(e => e.Name)
                        .HasColumnName("NAME")
                        .HasMaxLength(50);
                    entity.Property(e => e.Customer)
                        .HasColumnName("CUSTOMER")
                        .HasMaxLength(50);
                    entity.Property(e => e.Status)
                        .HasColumnName("STATUS")
                        .HasMaxLength(3);
                    entity.Property(e => e.StartDate)
                        .HasColumnName("START_DATE");
                    entity.Property(e => e.EndDate)
                        .HasColumnName("END_DATE");
                    entity.HasOne<Group>(e => e.Group)
                        .WithMany(p => p.Projects)
                        .HasForeignKey(p => p.GroupId);
                    entity.Property(e => e.Version)
                        .HasColumnName("VERSION")
                        .IsRowVersion();
                }
                );

            modelBuilder.Entity<ProjectEmployee>(
                entity =>
                {
                    entity.ToTable("PROJECT_EMPLOYEE");
                    entity.Property(e => e.ProjectId)
                        .HasColumnName("PROJECT_ID");
                    entity.Property(e => e.EmployeeId)
                        .HasColumnName("EMPLOYEE_ID");
                    entity.Ignore(e => e.Id);
                    entity.HasKey(e => new { e.ProjectId, e.EmployeeId });
                    entity.HasOne(e => e.Employee)
                        .WithMany(p => p.ProjectEmployees)
                        .HasForeignKey(p => p.EmployeeId);
                    entity.HasOne(e => e.Project)
                        .WithMany(p => p.projectEmployees)
                        .HasForeignKey(p => p.ProjectId);

                }
                );
        }
    }
}