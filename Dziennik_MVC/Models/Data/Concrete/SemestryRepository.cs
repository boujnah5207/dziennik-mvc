using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dziennik_MVC.Models.Data.Abstract;
using System.Data;

namespace Dziennik_MVC.Models.Data.Concrete
{
    public class SemestryRepository : ISemestryRepository
    {
        private EFContext entities;

        public SemestryRepository()
        {
            entities = new EFContext();
        }

        public IQueryable<Entities.Semestry> GetAllSemestry
        {
            get { return from Semestry in entities.Semestry select Semestry; }
        }

        public Entities.Semestry GetSemestrByType(string type)
        {
            return entities.Semestry.Single(s => s.typ == type);
        }

        public Entities.Semestry GetSemestrByYear(string year)
        {
            return entities.Semestry.Single(s => s.rok == year);
        }

        public Entities.Semestry GetSemestrByID(int id)
        {
            return entities.Semestry.Single(s => s.id_semestru == id);
        }

        public void AddSemestr(Entities.Semestry semester)
        {
            entities.Semestry.Add(semester);
        }

        public void EditSemestr(Entities.Semestry semester)
        {
            entities.Entry(semester).State = EntityState.Modified;
        }

        public void DeleteSemestr(Entities.Semestry semester)
        {
            entities.Semestry.Remove(semester);
        }

        public void DeleteSemestr(int id)
        {
            DeleteSemestr(GetSemestrByID(id));
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