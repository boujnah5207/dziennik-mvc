using System;
using System.Web.Security;
using Dziennik_MVC.Models.Data.Concrete;
using Dziennik_MVC.Models.Entities;

namespace Dziennik_MVC.Helpers
{
    public class DziennikRoleProvider : RoleProvider
    {
        private UsersRepository repository { get; set; }

        public DziennikRoleProvider()
        {
            repository = new UsersRepository(new EFContext());
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
            
            if (repository.IsAdmin(username))
            {
                return new String[]{"Admin"};
            }
            else
            {
                return new String[] { "" };
            }
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string rolename)
        {
            Prowadzacy user = repository.GetProwadzacyByName(username);
            Studenci student = null;

            if(user == null)
                student=repository.GetStudentByName(username);

            if (!repository.ProwadzacyExists(user))
                return false;
            if ( student != null && !repository.StudentExists(student))
                return false;

            return user.admin;
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