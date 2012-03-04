using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dziennik_MVC.Models.Data.Abstract;
using System.Data;

namespace Dziennik_MVC.Models.Data.Concrete
{
    public class SemestersRepository : ISemestersRepository
    {
        private EFContext entities; 

        public SemestersRepository() {
            entities = new EFContext();
        }

        public IQueryable<Entities.Semesters> GetAllSemesters
        {
            get { return from semesters in entities.Semesters select semesters; }
        }

        public Entities.Semesters GetSemesterByType(string type)
        {
            return entities.Semesters.Single(s => s.Type == type);
        }

        public Entities.Semesters GetSemesterByYear(string year)
        {
            return entities.Semesters.Single(s => s.Year == year);
        }

        public Entities.Semesters GetSemesterByID(int id)
        {
            return entities.Semesters.Single(s => s.SemesterID == id);
        }

        public void AddSemester(Entities.Semesters semester)
        {
            entities.Semesters.Add(semester);
        }

        public void EditSemester(Entities.Semesters semester)
        {
            entities.Entry(semester).State = EntityState.Modified;
        }

        public void DeleteSemester(Entities.Semesters semester)
        {
            entities.Semesters.Remove(semester);
        }

        public void DeleteSemester(int id)
        {
            DeleteSemester(GetSemesterByID(id));
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