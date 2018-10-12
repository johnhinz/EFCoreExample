using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace EFCoreExample
{
    class Program
    {
        static void Main(string[] args)
        {
            MyContext context = new MyContext();
            context.Students.Add(
                new StudentPoco()
                {
                    Id = 1,
                    Name = "Sally Jones"
                  
                });

            context.SaveChanges();
        }
    }

    public class MyContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentPoco> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=School;Integrated Security=True;Connect Timeout=30;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Teacher)
                .WithMany(t => t.Courses);

            modelBuilder.Entity<Course>()
                .HasMany(c => c.Students);

        }

    }

    public class StudentPoco
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public virtual ICollection<CourseToStudent> Courses { get; set; }
    }

    public class Course
    {
        public int Id { get; set; }
        public virtual ICollection<StudentPoco> Students {get;set;}
        public Teacher Teacher { get; set; }
    }

    //public class CourseToStudent
    //{
    //    public int CourseId { get; set; }
    //    public Course Course { get; set; }
    //    public int StudentId { get; set; }
    //    public StudentPoco Student { get; set; }
    //}

    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}
