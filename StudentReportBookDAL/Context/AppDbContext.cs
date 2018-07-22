using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentReportBookDAL.Entities;


namespace StudentReportBookDAL.Context
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<Person> People { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Mark> Marks { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<TeachersWorkload> TeachersWorkloads { get; set; }
        public DbSet<Faculty> Faculties { get; set; }

        public AppDbContext(DbContextOptions options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PersonConfiguration());
            modelBuilder.ApplyConfiguration(new SubjectConfiguration());
            modelBuilder.ApplyConfiguration(new StudentConfiguration());
            //modelBuilder.ApplyConfiguration(new PersonSubjectConfiguration());
            modelBuilder.ApplyConfiguration(new TeacherConfiguration());
            modelBuilder.ApplyConfiguration(new GroupConfiguration());
            modelBuilder.ApplyConfiguration(new FacultyConfiguration());
            modelBuilder.ApplyConfiguration(new MarkConfiguration());
        }
        public class PersonConfiguration : IEntityTypeConfiguration<Person>
        {
            public void Configure(EntityTypeBuilder<Person> builder)
            {
                builder.ToTable("People").HasKey(p => p.Id);

                builder.ToTable("People").HasDiscriminator<int>("Position")
                    .HasValue<Student>(1)
                    .HasValue<Teacher>(2);
                    
                builder.Property(p => p.FirstName).IsRequired().HasMaxLength(30);
                builder.Property(p => p.LastName).IsRequired().HasMaxLength(30);
                builder.Property(p => p.Name).HasComputedColumnSql("[FirstName] + ' ' + [LastName]");
                builder.Property(p => p.Identity).IsRequired();
            }
        }

        public class StudentConfiguration : IEntityTypeConfiguration<Student>
        {
            public void Configure(EntityTypeBuilder<Student> builder)
            {
                builder.HasOne(s => s.Group).WithMany(g => g.Students);
            }
        }
        public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
        {
            public void Configure(EntityTypeBuilder<Subject> builder)
            {
                builder.ToTable("Subjects").HasKey(p => p.Id);
                builder.Property(p => p.Name).IsRequired().HasMaxLength(50);

            }
        }
        //public class PersonSubjectConfiguration : IEntityTypeConfiguration<PersonSubject>
        //{
        //    public void Configure(EntityTypeBuilder<PersonSubject> builder)
        //    {
        //        builder.ToTable("PersonSubjects").HasKey(t => new { t.PersonId, t.SubjectId });

        //        builder//.ToTable("PersonSubjects")
        //             .HasOne(ps => ps.Person)
        //             .WithMany(p => p.PersonSubjects)
        //             .HasForeignKey(ps => ps.PersonId);

        //        builder//.ToTable("PersonSubjects")
        //            .HasOne(ps => ps.Subject)
        //            .WithMany(s => s.PersonSubjects)
        //            .HasForeignKey(ps => ps.SubjectId);
        //    }
        //}
        public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
        {
            public void Configure(EntityTypeBuilder<Teacher> builder)
            {
               //?
            }
        }
        public class TeachersWorkloadConfiguration : IEntityTypeConfiguration<TeachersWorkload>
        {
            public void Configure(EntityTypeBuilder<TeachersWorkload> builder)
            {
                builder.ToTable("TeachersWorkloads").HasKey(p => p.Id);
                builder.HasOne(tw => tw.Group).WithMany(g => g.TeachersWorkloads);
                builder.HasOne(tw => tw.Subject).WithMany(s => s.TeachersWorkloads);
                builder.HasOne(tw => tw.Teacher).WithMany(t => t.TeachersWorkloads);
                builder.Property(tw => tw.Term).IsRequired();
            }
        }
        public class GroupConfiguration : IEntityTypeConfiguration<Group>
        {
            public void Configure(EntityTypeBuilder<Group> builder)
            {
                builder.ToTable("Groups").HasKey(g => g.Id);
                builder.Property(p => p.Name).IsRequired().HasMaxLength(30);
                builder.HasOne(g => g.Faculty).WithMany(f => f.Groups);
            }
        }
        public class FacultyConfiguration : IEntityTypeConfiguration<Faculty>
        {
            public void Configure(EntityTypeBuilder<Faculty> builder)
            {
                builder.ToTable("Faculties").HasKey(g => g.Id);
                builder.Property(p => p.Name).IsRequired().HasMaxLength(30);
            }
        }
        public class MarkConfiguration : IEntityTypeConfiguration<Mark>
        {
            public void Configure(EntityTypeBuilder<Mark> builder)
            {
                builder.ToTable("Marks").HasKey(m => m.Id);
                builder.Property(m => m.Student).IsRequired();
                builder.Property(m => m.TeachersWorkload).IsRequired();
            }
        }
    }
}
