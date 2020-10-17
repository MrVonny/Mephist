using Mephist.Extensions;
using Mephist.Models;
using Microsoft.EntityFrameworkCore;
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
            
            
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {/*
            User user1 = new User()
            {
                NickName = "MEPhI Killer"
            };

            Media media1 = new Media()
            {
                 
                PartialMediaPath = Path.Combine(@"\Employees\",
                                   String.Concat("Самедов Виктор Витальевич".Transliterate().Split(' '))),
                MediaName = "1131_cps.jpg"
            };



            Rating rating1 = new Rating()
            {
                EmployeeId = 1,
                CharacterScore = 5,
                CharacterVotes = 4,
                ExamsScore = -10,
                ExamsVotes = 5,
                TeachingScore = 28,
                TeachingVotes = 15
            };
            modelBuilder.Entity<Employee>().HasData(
                     new Employee("Самедов Виктор Витальевич")
                     {
                         Id = 1,
                         Department = "40",
                         Institutions = "ИЯФиТ",
                         Subjects = "Общая физика, Тензорный анализ",
                         Photos = new List<Media>
                         {
                             
                         },
                         Reviews = new List<Review>()
                         {

                         },
                         



                     }
                 );*/
        }
        public DbSet<EducationalMaterial> EducationalMaterials { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Media> Medias { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
