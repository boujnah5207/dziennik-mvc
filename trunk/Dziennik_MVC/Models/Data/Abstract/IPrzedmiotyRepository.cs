using System.Linq;
using Dziennik_MVC.Models.Entities;

namespace Dziennik_MVC.Models.Data.Abstract
{
    public interface IPrzedmiotyRepository
    {
        IQueryable<Przedmioty> GetAllPrzedmioty { get; }

        Przedmioty GetPrzedmiotByName(string name);
        Przedmioty GetPrzedmiotByID(int id);

        bool PrzedmiotExists(Przedmioty przedmiot);

        void AddPrzedmiot(Przedmioty przedmiot);
        void EditPrzedmiot(Przedmioty przedmiot);
        void DeletePrzedmiot(Przedmioty przedmiot);
        void DeletePrzedmiot(int id);

        void Save();
        void Dispose();
    }
}
