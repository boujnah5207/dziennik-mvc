﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

using Ninject;
using Dziennik_MVC.Models.Data.Abstract;
using Dziennik_MVC.Models.Entities;
using Dziennik_MVC.Models.Data.Concrete;

namespace Dziennik_MVC.Helpers
{
    public class DziennikRoleProvider : RoleProvider
    {
        private UzytkownicyRepository repository { get; set; }

        public DziennikRoleProvider()
        {
            repository = new UzytkownicyRepository();
        }
 
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username){
              Uprawnienia role = this.repository.GetRoleForUser(username);
            if (!this.repository.RoleExists(role))
                return new string[] { string.Empty };

            return new string[] { role.Nazwa_uprawnienia };
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string rolename)
        {
            Uzytkownicy user = repository.GetUser(username);
            Uprawnienia role = repository.GetRole(rolename);

            if (!repository.UserExists(user))
                return false;
            if (!repository.RoleExists(role))
                return false;

            return user.Uprawnienia.Nazwa_uprawnienia == role.Nazwa_uprawnienia; ;
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}