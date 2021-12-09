using System;
using System.Reflection;
using eogrenci.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace eogrenci.Dal.Concrete.Context
{
    public class StudentDbContext : DbContext
    {

        public StudentDbContext(DbContextOptions<StudentDbContext> context) : base(context)
        {

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseMySQL("Server=localhost; Port=3306; Database=EStudent;User=root;Password=123456789; ConvertZeroDateTime=True;");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Konfigürasyonları tek tek eklemek istersek:
            //modelBuilder.ApplyConfiguration(new QuestionConfiguration)
            //QuestionConfiguration buarada konfigürasyon dosyamızın ismi.

            //Konfigürasyonları tek tek eklemek yerine, hepsini dahil etmek için yazılır.
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Department> Departments { get; set; }

    }
}
