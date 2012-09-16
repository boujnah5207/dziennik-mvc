using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Web.Security;
using Dziennik_MVC.Models.Entities;
using Dziennik_MVC.Models.Data.Abstract;

namespace Dziennik_MVC.Models.Data.Concrete
{
    public class EFContext : DbContext, IUnitOfWork
    {
        public EFContext() : base("Dziennik_WWW")
        { 
        
        }

        public DbSet<Prowadzacy> Prowadzacy { get; set; }
        public DbSet<Oceny> Oceny { get; set; }
        public DbSet<Grupy> Grupy { get; set; }
        public DbSet<Projekty> Projekty { get; set; }
        public DbSet<Przedmioty> Przedmioty { get; set; }
        public DbSet<Studenci> Studenci { get; set; }

        public void Save()
        {
            SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public class EFContextInitializer : DropCreateDatabaseIfModelChanges<EFContext>
        {
            protected override void Seed(EFContext context)
            {
                context.Prowadzacy.Add(
                    new Prowadzacy
                    {
                        login = "Admin",
                        haslo = FormsAuthentication.HashPasswordForStoringInConfigFile("admin", "md5"),                        
                        imie = "Adam",
                        nazwisko = "Skubicha",
                        admin = true
                    });
                context.Prowadzacy.Add(
                   new Prowadzacy
                   {
                       login = "NoAdmin",
                       haslo = FormsAuthentication.HashPasswordForStoringInConfigFile("admin", "md5"),
                       imie = "Adam",
                       nazwisko = "Skubicha",
                       admin = false
                   });
            }
        }
    }
}
