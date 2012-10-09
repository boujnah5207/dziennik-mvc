using System.Data;
using System.Linq;
using Dziennik_MVC.Models.Data.Abstract;
using Dziennik_MVC.Models.Entities;

namespace Dziennik_MVC.Models.Data.Concrete
{
    public class PrzedmiotyRepository : IPrzedmiotyRepository
    {
        private readonly EFContext entities;

        public PrzedmiotyRepository(IUnitOfWork unitOfWork)
        {
            entities = unitOfWork as EFContext;
        }

        public IQueryable<Przedmioty> GetAllPrzedmioty
        {
            get { return from g in entities.Przedmioty select g; }
        }

        public IQueryable<Przedmioty> GetPrzedmiotyNiePrzypisane
        {
            get { return from g in entities.Przedmioty where g.Prowadzacy.id_prowadzacego ==null select g; }
        }

        public Przedmioty GetPrzedmiotByName(string name)
        {
            return (entities.Przedmioty.FirstOrDefault(g => g.nazwa_przedmiotu == name));
        }

        public Przedmioty GetPrzedmiotByID(int id)
        {
            return entities.Przedmioty.FirstOrDefault(g => g.id_przedmiotu == id);
        }

        public bool PrzedmiotExists(Przedmioty przedmiot) {
            return entities.Przedmioty.Any(x => x.nazwa_przedmiotu == przedmiot.nazwa_przedmiotu && x.id_przedmiotu != przedmiot.id_przedmiotu);
        }

        public void AddPrzedmiot(Przedmioty przedmiot)
        {
            entities.Przedmioty.Add(przedmiot);
        }

        public void EditPrzedmiot(Przedmioty przedmiot)
        {
            entities.Entry(przedmiot).State = EntityState.Modified;
        }

        public void DeletePrzedmiot(Przedmioty przedmiot)
        {
            entities.Przedmioty.Remove(przedmiot);
        }

        public void DeletePrzedmiot(int id)
        {
            DeletePrzedmiot(GetPrzedmiotByID(id));
        }

        public void Save()
        {
            entities.SaveChanges();
        }

        public void Dispose()
        {
           entities.Dispose();
        }
    }
}