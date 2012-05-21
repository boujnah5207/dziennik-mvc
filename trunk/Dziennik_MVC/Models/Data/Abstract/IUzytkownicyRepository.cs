using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dziennik_MVC.Models.Entities;


namespace Dziennik_MVC.Models.Data.Abstract
{
    public interface IUzytkownicyRepository
    {
      
        IQueryable<Uzytkownicy> GetAllUsers{ get; }
        IQueryable<Uprawnienia> GetAllRoles{ get; }

        Uzytkownicy GetUser(int id);
        Uzytkownicy GetUser(string userName);

        Uprawnienia GetRole(int id);
        Uprawnienia GetRole(string name);

        IQueryable<Uzytkownicy> GetUsersForRole(string roleName);
        IQueryable<Uzytkownicy> GetUsersForRole(int id);
        IQueryable<Uzytkownicy> GetUsersForRole(Uprawnienia role);

        Uprawnienia GetRoleForUser(string userName);
        Uprawnienia GetRoleForUser(int id);
        Uprawnienia GetRoleForUser(Uzytkownicy user);

        void AddUser(Uzytkownicy user);
        void EditUser(Uzytkownicy user);
        void DeleteUser(Uzytkownicy user);
        void DeleteUser(string userName);

        void AddRole(Uprawnienia role);
        void AddRole(string roleName);
        void DeleteRole(Uprawnienia role);
        void DeleteRole(string roleName);

        bool UserExists(Uzytkownicy user);
        bool RoleExists(Uprawnienia role);

        bool IsActive(string user);

        void Save();
        void Dispose();
    }
}
