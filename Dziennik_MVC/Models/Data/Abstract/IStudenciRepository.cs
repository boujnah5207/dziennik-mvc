using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dziennik_MVC.Models.Entities;

namespace Dziennik_MVC.Models.Data.Abstract
{
    public interface IStudenciRepository
    {
        IQueryable<Studenci> GetAllStudents { get; }

        Studenci GetStudentByName(string name);
        Studenci GetStudentByID(int id);

        bool StudentExists(Studenci student);
        bool IndeksExists(Studenci student);
        void AddStudent(Studenci student);
        void EditStudent(Studenci student);
        void DeleteStudent(Studenci student);
        void DeleteStudent(int id);

        void Save();
        void Dispose();
    }
}
