using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dziennik_MVC.Models.Entities;

namespace Dziennik_MVC.Models.Data.Abstract
{
    public interface IGroupsRepository
    {
        IQueryable<Groups> GetAllGroups { get; }

        Groups GetGroupByName(string name);
        Groups GetGroupByID(int id);

        void AddGroup(Groups group);
        void EditGroup(Groups group);
        void DeleteGroup(Groups group);
        void DeleteGroup(int id);

        void Save();
        void Dispose();
    }
}
