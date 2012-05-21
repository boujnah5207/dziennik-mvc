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
    public class UzytkownicyRepository : IUzytkownicyRepository
    {
        private const string MissingRole = "Uprawnienie nie istnieje";
        private const string MissingUser = "Użytkownik nie istnieje";
        private const string TooManyUser = "Taki użytkownik już istnieje";
        private const string TooManyRole = "Uprawnienie już istnieje";
        private const string AssignedRole = "Nie można usunąć uprawnienia przypisanego do innych użytkowników:";

        private EFContext entities;

        public UzytkownicyRepository( )
        {
            entities = new EFContext();
        }
 
        public IQueryable<Uzytkownicy> GetAllUsers
        {
            get { return from user in entities.Uzytkownicy select user; }
        }
 
        public Uzytkownicy GetUser(int id)
        {
            return entities.Uzytkownicy.SingleOrDefault(user => user.id_uzytkownika == id);
        }
 
        public Uzytkownicy GetUser(string userName)
        {
            return entities.Uzytkownicy.SingleOrDefault(user => user.login == userName);
        }
 
        public IQueryable<Uzytkownicy> GetUsersForRole(string roleName)
        {
            return GetUsersForRole(GetRole(roleName));
        }
 
        public IQueryable<Uzytkownicy> GetUsersForRole(int id)
        {
            return GetUsersForRole(GetRole(id));
        }
 
        public IQueryable<Uzytkownicy> GetUsersForRole(Dziennik_MVC.Models.Entities.Uprawnienia role)
        {
            if (!RoleExists(role))
                throw new ArgumentException(MissingRole);

            return from user in entities.Uzytkownicy
                   where user.Uprawnienia.nazwa_uprawnienia == role.nazwa_uprawnienia
                   orderby user.login
                   select user;
        }

        public IQueryable<Entities.Uprawnienia> GetAllRoles
        {
            get {   return from role in entities.Uprawnienia orderby role.nazwa_uprawnienia select role; }
        }

        public Dziennik_MVC.Models.Entities.Uprawnienia GetRole(int id)
        {
            return entities.Uprawnienia.SingleOrDefault(role => role.id_uprawnienia == id);
        }

        public Dziennik_MVC.Models.Entities.Uprawnienia GetRole(string name)
        {
            return entities.Uprawnienia.SingleOrDefault(role => role.nazwa_uprawnienia == name);
        }

        public Dziennik_MVC.Models.Entities.Uprawnienia GetRoleForUser(string userName)
        {
            return GetRoleForUser(GetUser(userName));
        }

        public Dziennik_MVC.Models.Entities.Uprawnienia GetRoleForUser(int id)
        {
            return GetRoleForUser(GetUser(id));
        }

        public Dziennik_MVC.Models.Entities.Uprawnienia GetRoleForUser(Uzytkownicy user)
        {
            if (!UserExists(user))
                throw new ArgumentException(MissingUser);
 
            return user.Uprawnienia;
        }
 
        public void AddUser(Uzytkownicy user)
        {
            if (UserExists(user))
                throw new ArgumentException(TooManyUser);
 
            entities.Uzytkownicy.Add(user);
        }

        public void EditUser(Uzytkownicy user)
        {
            entities.Entry(user).State = EntityState.Modified;
        }

        public bool IsActive(string user) 
        {
            return entities.Uzytkownicy.SingleOrDefault(x => x.login == user).aktywny;
        }        
 
        public void DeleteUser(Uzytkownicy user)
        {
            if (!UserExists(user))
                throw new ArgumentException(MissingUser);
 
            entities.Uzytkownicy.Remove(user);
        }
 
        public void DeleteUser(string userName)
        {
            DeleteUser(GetUser(userName));
        }

        public void AddRole(Dziennik_MVC.Models.Entities.Uprawnienia role)
        {
            if (RoleExists(role))
                throw new ArgumentException(TooManyRole);
 
            entities.Uprawnienia.Add(role);
        }
 
        public void AddRole(string roleName)
        {
            Dziennik_MVC.Models.Entities.Uprawnienia role = new Dziennik_MVC.Models.Entities.Uprawnienia()
            {
                nazwa_uprawnienia = roleName
            };
 
            AddRole(role);
        }

        public void DeleteRole(Dziennik_MVC.Models.Entities.Uprawnienia role)
        {
            if (!RoleExists(role))
                throw new ArgumentException(MissingRole);
 
            if (GetUsersForRole(role).Count() > 0)
                throw new ArgumentException(AssignedRole);
 
            entities.Uprawnienia.Remove(role);
        }
 
        public void DeleteRole(string roleName)
        {
            DeleteRole(GetRole(roleName));
        }
 
        public void Save()
        {
            entities.SaveChanges();
        }

        public bool UserExists(Uzytkownicy user)
        {
            if (user == null)
                return false;

            return (entities.Uzytkownicy.SingleOrDefault(u => u.id_uzytkownika == user.id_uzytkownika || u.login == user.login || u.email == user.email) != null);
        }

        public bool RoleExists(Dziennik_MVC.Models.Entities.Uprawnienia role)
        {
            if (role == null)
                return false;
 
            return (entities.Uprawnienia.SingleOrDefault(r => r.id_uprawnienia == role.id_uprawnienia || r.nazwa_uprawnienia == role.nazwa_uprawnienia) != null);
        }

        public void Dispose() {
            entities.Dispose();
        }
    }
}

