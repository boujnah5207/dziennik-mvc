using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dziennik_MVC.Models.Entities;


namespace Dziennik_MVC.Models.Data.Abstract
{
    public interface IUsersRepository
    {
      
        IQueryable<Users> GetAllUsers{ get; }
        IQueryable<Roles> GetAllRoles{ get; }

        Users GetUser(int id);
        Users GetUser(string userName);

        Roles GetRole(int id);
        Roles GetRole(string name);

        IQueryable<Users> GetUsersForRole(string roleName);
        IQueryable<Users> GetUsersForRole(int id);
        IQueryable<Users> GetUsersForRole(Roles role);

        Roles GetRoleForUser(string userName);
        Roles GetRoleForUser(int id);
        Roles GetRoleForUser(Users user);

        void AddUser(Users user);
        void EditUser(Users user);
        void DeleteUser(Users user);
        void DeleteUser(string userName);

        void AddRole(Roles role);
        void AddRole(string roleName);
        void DeleteRole(Roles role);
        void DeleteRole(string roleName);

        bool UserExists(Users user);
        bool RoleExists(Roles role);

        bool IsActive(string user);

        void Save();
        void Dispose();
    }
}
