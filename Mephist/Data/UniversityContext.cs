using Mephist.Extensions;
using Mephist.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Mephist.Data
{
    public class UniversityContext : DbContext
    {
        public UniversityContext(DbContextOptions<UniversityContext> options) : base(options)
        {
            Database.EnsureCreated();

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*

            Employee samedov = new Employee()
            {
                Id = 1,
                FullName = "Самедов Виктор Витальевич",
                Department = "40",
                Institutions = "ИЯФиТ;ИИКС",
                Subjects = "Предмет 1;Предмет 2"

            };

            User user1 = new User()
            {
                Id=1,
                NickName = "MEPhI Killer"
            };

            Media mediaSamedov = new Media()
            {
                Id=1,
                PartialMediaPath = Path.Combine(@"\Employees\",
                                   String.Concat("Самедов Виктор Витальевич".Transliterate().Split(' '))),
                MediaName = "1131_cps.jpg",
                EmployeeId=1
            };

            EducationalMaterial em1 = new EducationalMaterial
            {
                Id=1,
                Type = EducationMaterialType.ExamTickets,
                Name = "Билеты за 3 14888 год",
                EmployeeId=1
            };


            Media mediaBilets = new Media()
            {
                Id=2,
                CreatedDate = DateTime.Now,
                PartialMediaPath = @"\EducationMaterials\",
                EducationalMaterialId=1

            };

            Rating rating1 = new Rating()
            {
                Id=1,
                CharacterScore = 5,
                CharacterVotes = 4,
                ExamsScore = -10,
                ExamsVotes = 5,
                TeachingScore = 28,
                TeachingVotes = 15,
                EmployeeId=1
                
            };

            Review reviewSame = new Review()
            {
                Id=1,
                Anonymously = false,
                User = user1,
                Text = "АААААА ПРЕПОООООД ТОПППППППП 11/10",
                CreatedDate = DateTime.Now,
                EmployeeId=1

            };
            modelBuilder.Entity<Employee>().HasData(samedov);
            modelBuilder.Entity<User>().HasData(user1);
            modelBuilder.Entity<EducationalMaterial>().HasData(em1);
            //modelBuilder.Entity<Rating>().HasData(rating1);
            modelBuilder.Entity<Review>().HasData(reviewSame);
            modelBuilder.Entity<Media>().HasData(mediaBilets,mediaSamedov);
            */
        }
        public DbSet<EducationalMaterial> EducationalMaterials { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Media> Medias { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<LaboratoryJournal> LaboratoryJournals { get; set; }
    }
}
