using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dziennik_MVC.Models.Entities;

namespace Dziennik_MVC.Models.Data.Abstract
{
    public interface ISemestryRepository
    {
        IQueryable<Semestry> GetAllSemestry { get; }

        Semestry GetSemestrByType(string typ);
        Semestry GetSemestrByYear(string rok);
        Semestry GetSemestrByID(int id);

        void AddSemestr(Semestry semestr);
        void EditSemestr(Semestry semestr);
        void DeleteSemestr(Semestry semestr);
        void DeleteSemestr(int id);

        void Save();
        void Dispose();
    }
}