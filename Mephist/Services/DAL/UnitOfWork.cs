using Mephist.Data;
using Mephist.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mephist.Services.DAL
{
    public class UnitOfWork : IDisposable
    {
        //UserManager<User> _userManager;
        //RoleManager<IdentityRole> _roleManager;
        public UnitOfWork(UniversityContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.context = context;
            /*
            lock (context)
            {
                string adminEmail = "borafon@gmail.com";
                string password = "MrBen228";
                if (roleManager.FindByNameAsync("Admin").Result == null)
                {
                    roleManager.CreateAsync(new IdentityRole("admin"));
                }
                if (userManager.FindByEmailAsync(adminEmail).Result == null)
                {
                    User admin = new User { Email = adminEmail, UserName = "Admin", EmailConfirmed = true };

                    IdentityResult result = userManager.CreateAsync(admin, password).Result;
                    if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(admin, "admin");
                    }
                }
            }
            
            */
        }

        private readonly UniversityContext context;

        private EmployeeRepository employeeRepository;
        private GenericRepository<Media> mediaRepository;
        private EducationalMaterialRepository educationalMaterialRepository;
        private GenericRepository<LaboratoryJournal> laboratoryJournalRepository;
        private GenericRepository<Review> reviewRepository;
        private GenericRepository<User> userRepository;
        private UniversityStaticData universityStaticDataRepository;

        public EmployeeRepository Employees
        {
            get
            {
                if (this.employeeRepository == null)
                {
                    this.employeeRepository = new EmployeeRepository(context);
                }
                return employeeRepository;
            }
        }
        public GenericRepository<Media> Medias
        {
            get
            {
                if (this.mediaRepository == null)
                {
                    this.mediaRepository = new GenericRepository<Media>(context);
                }
                return mediaRepository;
            }
        }
        public EducationalMaterialRepository EducationalMaterials
        {
            get
            {
                if (this.educationalMaterialRepository == null)
                {
                    this.educationalMaterialRepository = new EducationalMaterialRepository(context);
                }
                return educationalMaterialRepository;
            }
        }
        public GenericRepository<LaboratoryJournal> LaboratoryJournals
        {
            get
            {
                if (this.laboratoryJournalRepository == null)
                {
                    this.laboratoryJournalRepository = new GenericRepository<LaboratoryJournal>(context);
                }
                return laboratoryJournalRepository;
            }
        }
        public GenericRepository<Review> Reviews
        {
            get
            {
                if (this.reviewRepository == null)
                {
                    this.reviewRepository = new GenericRepository<Review>(context);
                }
                return reviewRepository;
            }
        }
        public GenericRepository<User> Users
        {
            get
            {
                if (this.userRepository == null)
                {
                    this.userRepository = new GenericRepository<User>(context);
                }
                return userRepository;
            }
        }
        public UniversityStaticData StaticData
        {
            get
            {
                if (this.universityStaticDataRepository == null)
                {
                    this.universityStaticDataRepository = new UniversityStaticData(context);
                }
                return universityStaticDataRepository;
            }
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
