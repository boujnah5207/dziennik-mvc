using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dziennik_MVC.Models.Entities;

namespace Dziennik_MVC.Models.Data.Abstract
{
    public interface ISemestersRepository
    {
        IQueryable<Semesters> GetAllSemesters { get; }

        Semesters GetSemesterByType(string type);
        Semesters GetSemesterByYear(string year);
        Semesters GetSemesterByID(int id);

        void AddSemester(Semesters semester);
        void EditSemester(Semesters semester);
        void DeleteSemester(Semesters semester);
        void DeleteSemester(int id);

        void Save();
        void Dispose();
    }
}
