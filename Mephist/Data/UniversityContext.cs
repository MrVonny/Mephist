using Mephist.Extensions;
using Mephist.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Newtonsoft.Json;
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
            modelBuilder.Entity<Employee>().Property(p => p.Subjects)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<List<string>>(v),
                     new ValueComparer<List<string>>(
            (c1, c2) => c1.SequenceEqual(c2),
            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
            c => c.ToList()));

            modelBuilder.Entity<Employee>().Property(p => p.Positions)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<List<string>>(v),
                     new ValueComparer<List<string>>(
            (c1, c2) => c1.SequenceEqual(c2),
            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
            c => c.ToList()));

            modelBuilder.Entity<Employee>().Property(p => p.Departments)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<List<string>>(v),
                     new ValueComparer<List<string>>(
            (c1, c2) => c1.SequenceEqual(c2),
            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
            c => c.ToList()));
        }
        public DbSet<Institute> Institutions { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<EducationalMaterial> EducationalMaterials { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Media> Medias { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<LaboratoryJournal> LaboratoryJournals { get; set; }
    }
}
