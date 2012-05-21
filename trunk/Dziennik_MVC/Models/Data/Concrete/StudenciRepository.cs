using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dziennik_MVC.Models.Data.Abstract;
using System.Data;
using Dziennik_MVC.Models.Entities;

namespace Dziennik_MVC.Models.Data.Concrete
{
    public class StudenciRepository : IStudenciRepository
    {
        private EFContext entities;

        public StudenciRepository()
        {
            entities = new EFContext();
        }

        public bool StudentExists(Entities.Studenci student)
        {
            return entities.Uzytkownicy.Any(m => m.login == student.login || m.email == student.email);
        }

        public bool IndeksExists(Entities.Studenci student)
        {
            return entities.Studenci.Any(m => m.nr_indeksu == student.nr_indeksu);
        }

        public IQueryable<Studenci> GetAllStudents
        {
            get { return from g in entities.Studenci select g; }
        }

        public Studenci GetStudentByName(string name)
        {
            return (entities.Studenci.Single(g => g.nazwisko == name));
        }

        public Studenci GetStudentByID(int id)
        {
            return entities.Studenci.Single(g => g.id_uzytkownika == id);
        }

        public void AddStudent(Studenci student)
        {
            entities.Studenci.Add(student);
        }

        public void EditStudent(Studenci student)
        {
            entities.Entry(student).State = EntityState.Modified;
        }

        public void DeleteStudent(Studenci student)
        {
            entities.Studenci.Remove(student);
        }

        public void DeleteStudent(int id)
        {
            DeleteStudent(GetStudentByID(id));
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