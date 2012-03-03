using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;

using Dziennik_MVC.Models.Data.Abstract;
using Dziennik_MVC.Models.Data.Concrete;
using Dziennik_MVC.Models.Entities;
using System.Data;

namespace Dziennik_MVC.Models.Data.Concrete
{
    public class UsersRepository : IUsersRepository
    {
        private const string MissingRole = "Uprawnienie nie istnieje";
        private const string MissingUser = "Użytkownik nie istnieje";
        private const string TooManyUser = "Taki użytkownik już istnieje";
        private const string TooManyRole = "Uprawnienie już istnieje";
        private const string AssignedRole = "Nie można usunąć uprawnienia przypisanego do innych użytkowników:";

        private EFContext entities;

        public UsersRepository( )
        {
            entities = new EFContext();
        }
 
        public IQueryable<Users> GetAllUsers
        {
            get { return from user in entities.Users select user; }
        }
 
        public Users GetUser(int id)
        {
            return entities.Users.SingleOrDefault(user => user.UserID == id);
        }
 
        public Users GetUser(string userName)
        {
            return entities.Users.SingleOrDefault(user => user.Login == userName);
        }
 
        public IQueryable<Users> GetUsersForRole(string roleName)
        {
            return GetUsersForRole(GetRole(roleName));
        }
 
        public IQueryable<Users> GetUsersForRole(int id)
        {
            return GetUsersForRole(GetRole(id));
        }
 
        public IQueryable<Users> GetUsersForRole(Dziennik_MVC.Models.Entities.Roles role)
        {
            if (!RoleExists(role))
                throw new ArgumentException(MissingRole);

            return from user in entities.Users
                   where user.Roles.RoleName == role.RoleName
                   orderby user.Login
                   select user;
        }

        public IQueryable<Entities.Roles> GetAllRoles
        {
            get {   return from role in entities.Roles orderby role.RoleName select role; }
        }

        public Dziennik_MVC.Models.Entities.Roles GetRole(int id)
        {
            return entities.Roles.SingleOrDefault(role => role.RoleID == id);
        }

        public Dziennik_MVC.Models.Entities.Roles GetRole(string name)
        {
            return entities.Roles.SingleOrDefault(role => role.RoleName == name);
        }

        public Dziennik_MVC.Models.Entities.Roles GetRoleForUser(string userName)
        {
            return GetRoleForUser(GetUser(userName));
        }

        public Dziennik_MVC.Models.Entities.Roles GetRoleForUser(int id)
        {
            return GetRoleForUser(GetUser(id));
        }

        public Dziennik_MVC.Models.Entities.Roles GetRoleForUser(Users user)
        {
            if (!UserExists(user))
                throw new ArgumentException(MissingUser);
 
            return user.Roles;
        }
 
        public void AddUser(Users user)
        {
            if (UserExists(user))
                throw new ArgumentException(TooManyUser);
 
            entities.Users.Add(user);
        }

        public void EditUser(Users user)
        {
            entities.Entry(user).State = EntityState.Modified;
        }

        public bool IsActive(string user) 
        {
            return entities.Users.SingleOrDefault(x => x.Login == user).isActive;
        }        
 
        public void DeleteUser(Users user)
        {
            if (!UserExists(user))
                throw new ArgumentException(MissingUser);
 
            entities.Users.Remove(user);
        }
 
        public void DeleteUser(string userName)
        {
            DeleteUser(GetUser(userName));
        }

        public void AddRole(Dziennik_MVC.Models.Entities.Roles role)
        {
            if (RoleExists(role))
                throw new ArgumentException(TooManyRole);
 
            entities.Roles.Add(role);
        }
 
        public void AddRole(string roleName)
        {
            Dziennik_MVC.Models.Entities.Roles role = new Dziennik_MVC.Models.Entities.Roles()
            {
                RoleName = roleName
            };
 
            AddRole(role);
        }

        public void DeleteRole(Dziennik_MVC.Models.Entities.Roles role)
        {
            if (!RoleExists(role))
                throw new ArgumentException(MissingRole);
 
            if (GetUsersForRole(role).Count() > 0)
                throw new ArgumentException(AssignedRole);
 
            entities.Roles.Remove(role);
        }
 
        public void DeleteRole(string roleName)
        {
            DeleteRole(GetRole(roleName));
        }
 
        public void Save()
        {
            entities.SaveChanges();
        }

        public bool UserExists(Users user)
        {
            if (user == null)
                return false;
 
            return (entities.Users.SingleOrDefault(u => u.UserID == user.UserID || u.Login == user.Login) != null);
        }

        public bool RoleExists(Dziennik_MVC.Models.Entities.Roles role)
        {
            if (role == null)
                return false;
 
            return (entities.Roles.SingleOrDefault(r => r.RoleID == role.RoleID || r.RoleName == role.RoleName) != null);
        }

        public void Dispose() {
            entities.Dispose();
        }
    }
}

