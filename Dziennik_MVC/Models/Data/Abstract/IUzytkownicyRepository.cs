using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dziennik_MVC.Models.Entities;


namespace Dziennik_MVC.Models.Data.Abstract
{
    public interface IUzytkownicyRepository
    {
        int NumberOfUsers { get; }
        int NumberOfRoles { get; }
        IQueryable<Uzytkownicy> GetAllUsers{ get; }
        Uzytkownicy GetUser(int id);
        Uzytkownicy GetUser(string userName);
        IQueryable<Uzytkownicy> GetUsersForRole(string roleName);
        IQueryable<Uzytkownicy> GetUsersForRole(int id);
        IQueryable<Uzytkownicy> GetUsersForRole(Uprawnienia role);
        IQueryable<Uprawnienia> GetAllRoles();
        Uprawnienia GetRole(int id);
        Uprawnienia GetRole(string name);
        Uprawnienia GetRoleForUser(string userName);
        Uprawnienia GetRoleForUser(int id);
        Uprawnienia GetRoleForUser(Uzytkownicy user);
        bool IsActive(string user);

        void AddUser(Uzytkownicy user);
       
        void EditUser(Uzytkownicy user);
        void DeleteUser(Uzytkownicy user);
        void DeleteUser(string userName);
        void AddRole(Uprawnienia role);
        void AddRole(string roleName);
        void DeleteRole(Uprawnienia role);
        void DeleteRole(string roleName);
        void Save();
        bool UserExists(Uzytkownicy user);
        bool RoleExists(Uprawnienia role);
        void Dispose();
    }
}
