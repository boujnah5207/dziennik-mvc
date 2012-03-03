using System;
using System.Web.Security;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Dziennik_MVC.Models.Data.Abstract;
using Dziennik_MVC.Models.Entities;

namespace Dziennik_MVC.Models.Data.Concrete
{
    public class EFContext : DbContext
    {
        public EFContext() : base("Dziennik_WWW")
        { 
        
        }

        public DbSet<Dziennik_MVC.Models.Entities.Roles> Roles { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Grades> Grades { get; set; }
        public DbSet<Groups> Groups { get; set; }
        public DbSet<Projects> Projects { get; set; }
        public DbSet<Courses> Courses { get; set; }
        public DbSet<Semesters> Semesters { get; set; }
        public DbSet<Students> Students { get; set; }
        public DbSet<Teachers> Teachers { get; set; }

        public void Save()
        {
            SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public class EFContextInitializer : DropCreateDatabaseIfModelChanges<EFContext>
        {
            protected override void Seed(EFContext context)
            {

                var role = new Dziennik_MVC.Models.Entities.Roles
                {
                    RoleName = "Admin",                    
                };

                var role1 = new Dziennik_MVC.Models.Entities.Roles
                {
                    RoleName = "Wykladowca",
                };
                var role2 = new Dziennik_MVC.Models.Entities.Roles
                {
                    RoleName = "Student",
                };

                context.Roles.Add(role);
                context.Roles.Add(role1);
                context.Roles.Add(role2);

                context.Users.Add(
                    new Admins
                    {
                        Login = "Admin",
                        Password = FormsAuthentication.HashPasswordForStoringInConfigFile("wildchild", "md5"),
                        CreationDate = DateTime.Now,
                        FirstName = "Adam",
                        LastName = "Skubicha",
                        BirthDay = new DateTime(1987,1,25),
                        isActive = true,
                        Email = "mvaddib@gmail.com",
                        RoleID = 1                        

                    });
            }
        }
    }
}
