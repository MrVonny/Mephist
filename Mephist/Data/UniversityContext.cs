using Mephist.Extensions;
using Mephist.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
    public class UniversityContext : IdentityDbContext<User>
    {
        public UniversityContext(DbContextOptions<UniversityContext> options) : base(options)
        {
            
            //Database.EnsureCreated();

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
        }
        public DbSet<EducationalMaterial> EducationalMaterials { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Media> Medias { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Review> Reviews { get; set; }
        //public DbSet<User> Users { get; set; }
        public DbSet<LaboratoryJournal> LaboratoryJournals { get; set; }
    }
}
