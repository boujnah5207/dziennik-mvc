using System.Linq;
using Dziennik_MVC.Models.Entities;


namespace Dziennik_MVC.Models.Data.Abstract
{
    public interface IUsersRepository
    {
        IQueryable<Prowadzacy> GetAllProwadzacy{ get; }
        IQueryable<Studenci> GetAllStudents { get; }

        Prowadzacy GetProwadzacyById(int id);
        Prowadzacy GetProwadzacyByName(string userName);

        Studenci GetStudentByName(string name);
        Studenci GetStudentByID(int id);

        IQueryable<Studenci> getStudentsInGroup(int id);

        void AddProwadzacy(Prowadzacy user);
        void EditProwadzacy(Prowadzacy user);
        void DeleteProwadzacy(Prowadzacy user);
        void DeleteProwadzacy(string userName);

        void AddStudent(Studenci student);
        void EditStudent(Studenci student);
        void DeleteStudent(Studenci student);
        void DeleteStudent(int id);

        bool ProwadzacyExists(Prowadzacy user);

        bool StudentExists(Studenci student);
        bool IndeksExists(Studenci student);

        bool IsAdmin(string user);

        void Save();
        void Dispose();
    }
}
