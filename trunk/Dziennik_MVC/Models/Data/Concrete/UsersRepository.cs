using System;
using System.Data;
using System.Linq;
using Dziennik_MVC.Models.Data.Abstract;
using Dziennik_MVC.Models.Entities;

namespace Dziennik_MVC.Models.Data.Concrete
{
    public class UsersRepository : IUsersRepository
    {
        private const string MissingUser = "Użytkownik nie istnieje";
        private const string TooManyUser = "Taki użytkownik już istnieje";
        private const string AssignedRole = "Nie można usunąć uprawnienia przypisanego do innych użytkowników:";

        private readonly EFContext entities;

        public UsersRepository(IUnitOfWork unitOfWork)
        {
            entities = unitOfWork as EFContext;
        }

        public IQueryable<Prowadzacy> GetAllProwadzacy
        {
            get { return from user in entities.Prowadzacy select user; }
        }

        public IQueryable<Studenci> GetAllStudents
        {
            get { return from g in entities.Studenci select g; }
        }

        public Prowadzacy GetProwadzacyById(int id)
        {
            return entities.Prowadzacy.SingleOrDefault(user => user.id_prowadzacego == id);
        }

        public Prowadzacy GetProwadzacyByName(string userName)
        {
            return entities.Prowadzacy.SingleOrDefault(user => user.login == userName);
        }

        public Studenci GetStudentByName(string name)
        {
            return (entities.Studenci.SingleOrDefault(g => g.login == name));
        }

        public Studenci GetStudentByID(int id)
        {
            return entities.Studenci.Single(g => g.id_studenta == id);
        }


        public IQueryable<Studenci> getStudentsInGroup(int id){
            return entities.Studenci.Where(user => user.id_grupy == id);
        }

        public void AddProwadzacy(Prowadzacy user)
        {
            if (ProwadzacyExists(user))
                throw new ArgumentException(TooManyUser);

            entities.Prowadzacy.Add(user);
        }

        public void EditProwadzacy(Prowadzacy user)
        {
            entities.Entry(user).State = EntityState.Modified;
        }

        public void DeleteProwadzacy(Prowadzacy user)
        {
            if (!ProwadzacyExists(user))
                throw new ArgumentException(MissingUser);

            entities.Prowadzacy.Remove(user);
        }

        public void DeleteProwadzacy(string userName)
        {
            DeleteProwadzacy(GetProwadzacyByName(userName));
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

        public bool ProwadzacyExists(Prowadzacy user)
        {
            if (user == null)
                return false;

            return (entities.Prowadzacy.SingleOrDefault(u => u.id_prowadzacego == user.id_prowadzacego || u.login == user.login) != null);
        }

        public bool StudentExists(Studenci student)
        {
            return entities.Studenci.Any(m => m.login == student.login);
        }

        public bool IndeksExists(Studenci student)
        {
            return entities.Studenci.Any(m => m.nr_indeksu == student.nr_indeksu);
        }

        public bool IsAdmin(string user)
        {
            Prowadzacy prow = entities.Prowadzacy.SingleOrDefault(x => x.login == user);
            if (prow != null)
                if (entities.Prowadzacy.SingleOrDefault(x => x.login == user).admin)
                    return true;
                else
                    return false;
            else
                return false;
        }
                 
        public void Save()
        {
            entities.SaveChanges();
        }
              
        public void Dispose() {
            entities.Dispose();
        }
    }
}

