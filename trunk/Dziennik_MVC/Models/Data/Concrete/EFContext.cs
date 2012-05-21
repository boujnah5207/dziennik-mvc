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

        public DbSet<Uprawnienia> Uprawnienia { get; set; }
        public DbSet<Uzytkownicy> Uzytkownicy { get; set; }
        public DbSet<Oceny> Oceny { get; set; }
        public DbSet<Grupy> Grupy { get; set; }
        public DbSet<Projekty> Projekty { get; set; }
        public DbSet<Przedmioty> Przedmioty { get; set; }
        public DbSet<Studenci> Studenci { get; set; }
        public DbSet<Semestry> Semestry { get; set; }
        public DbSet<Wykladowcy> Wykladowcy { get; set; }

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

                var role = new Uprawnienia
                {
                    nazwa_uprawnienia = "Admin",                    
                };

                var role1 = new Uprawnienia
                {
                    nazwa_uprawnienia = "Wykladowca",
                };
                var role2 = new Uprawnienia
                {
                    nazwa_uprawnienia = "Student",
                };

                context.Uprawnienia.Add(role);
                context.Uprawnienia.Add(role1);
                context.Uprawnienia.Add(role2);

                context.Uzytkownicy.Add(
                    new Administrator
                    {
                        login = "Admin",
                        haslo = FormsAuthentication.HashPasswordForStoringInConfigFile("wildchild", "md5"),                        
                        imie = "Adam",
                        nazwisko = "Skubicha",
                        aktywny = true,
                        email = "mvaddib@gmail.com",
                        id_uprawnienia = 1                        

                    });
            }
        }
    }
}
