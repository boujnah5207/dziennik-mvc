using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dziennik_MVC.Models.Data.Abstract;
using System.Data;

namespace Dziennik_MVC.Models.Data.Concrete
{
    public class GroupsRepository : IGroupsRepository
    {
        private EFContext entities; 

        public GroupsRepository(){
            entities = new EFContext();
        }

        public IQueryable<Entities.Groups> GetAllGroups
        {
            get { return from g in entities.Groups select g; }
        }

        public Entities.Groups GetGroupByName(string name)
        {
            return (entities.Groups.Single(g => g.GroupName == name));
        }

        public Entities.Groups GetGroupByID(int id)
        {
            return entities.Groups.Single(g => g.GroupID == id);
        }

        public void AddGroup(Entities.Groups group)
        {
            entities.Groups.Add(group);
        }

        public void EditGroup(Entities.Groups group)
        {
           entities.Entry(group).State = EntityState.Modified;
        }

        public void DeleteGroup(Entities.Groups group)
        {
            entities.Groups.Remove(group);
        }

        public void DeleteGroup(int id)
        {
             DeleteGroup(GetGroupByID(id));
        }

        public void Save()
        {
            entities.SaveChanges();
        }

        public void Dispose()
        {
           entities.Dispose();
        }
    }
}