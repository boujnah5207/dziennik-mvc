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
        
        public int NumberOfUsers
        {
            get { return this.entities.Uzytkownicy.Count(); }
        }

        public int NumberOfRoles
        {
            get { return this.entities.Uprawnienia.Count(); }
        }

        public int GetNewID() {
            return entities.Uzytkownicy.Max(m => m.ID_uzytkownika); 
        }
 
        public IQueryable<Uzytkownicy> GetAllUsers
        {
            get { return from user in entities.Uzytkownicy select user; }
        }
 
        public Uzytkownicy GetUser(int id)
        {
            return entities.Uzytkownicy.SingleOrDefault(user => user.ID_uzytkownika == id);
        }
 
        public Uzytkownicy GetUser(string userName)
        {
            return entities.Uzytkownicy.SingleOrDefault(user => user.Login == userName);
        }
 
        public IQueryable<Uzytkownicy> GetUsersForRole(string roleName)
        {
            return GetUsersForRole(GetRole(roleName));
        }
 
        public IQueryable<Uzytkownicy> GetUsersForRole(int id)
        {
            return GetUsersForRole(GetRole(id));
        }
 
        public IQueryable<Uzytkownicy> GetUsersForRole(Uprawnienia role)
        {
            if (!RoleExists(role))
                throw new ArgumentException(MissingRole);
 
            return from user in entities.Uzytkownicy
                   where user.Uprawnienia.ID_uprawnienia == role.ID_uprawnienia
                   orderby user.Login
                   select user;
        }
 
        public IQueryable<Uprawnienia> GetAllRoles()
        {
            return from role in entities.Uprawnienia
                   orderby role.Nazwa_uprawnienia
                   select role;
        }
 
        public Uprawnienia GetRole(int id)
        {
            return entities.Uprawnienia.SingleOrDefault(role => role.ID_uprawnienia == id);
        }
 
        public Uprawnienia GetRole(string name)
        {
            return entities.Uprawnienia.SingleOrDefault(role => role.Nazwa_uprawnienia == name);
        }
 
        public Uprawnienia GetRoleForUser(string userName)
        {
            return GetRoleForUser(GetUser(userName));
        }
 
        public Uprawnienia GetRoleForUser(int id)
        {
            return GetRoleForUser(GetUser(id));
        }
 
        public Uprawnienia GetRoleForUser(Uzytkownicy user)
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
            return entities.Uzytkownicy.SingleOrDefault(x => x.Login == user).isActive;
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
 
        public void AddRole(Uprawnienia role)
        {
            if (RoleExists(role))
                throw new ArgumentException(TooManyRole);
 
            entities.Uprawnienia.Add(role);
        }
 
        public void AddRole(string roleName)
        {
            Uprawnienia role = new Uprawnienia()
            {
                Nazwa_uprawnienia = roleName
            };
 
            AddRole(role);
        }
 
        public void DeleteRole(Uprawnienia role)
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
 
            return (entities.Uzytkownicy.SingleOrDefault(u => u.ID_uzytkownika == user.ID_uzytkownika || u.Login == user.Login) != null);
        }
 
        public bool RoleExists(Uprawnienia role)
        {
            if (role == null)
                return false;
 
            return (entities.Uprawnienia.SingleOrDefault(r => r.ID_uprawnienia == role.ID_uprawnienia || r.Nazwa_uprawnienia == role.Nazwa_uprawnienia) != null);
        }

        public void Dispose() {
            entities.Dispose();
        }
    }
}

