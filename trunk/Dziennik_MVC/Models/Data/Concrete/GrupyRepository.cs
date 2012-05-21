using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dziennik_MVC.Models.Data.Abstract;
using System.Data;

namespace Dziennik_MVC.Models.Data.Concrete
{
    public class GrupyRepository : IGrupyRepository
    {
        private EFContext entities; 

        public GrupyRepository(){
            entities = new EFContext();
        }

        public bool GrupaExists(Entities.Grupy grupa) 
        {
            var query =
                from gr in entities.Grupy
                join sem in entities.Semestry on gr.id_semestru equals sem.id_semestru
                where (gr.nazwa_grupy == grupa.nazwa_grupy && sem.id_semestru == grupa.id_semestru)
                select  sem;


            if (query.Count() == 1)
                return true;
            else
                return false;
           // return entities.Semestry.Any(m => m.Grupy.Contains(grupa));
        }

        public IQueryable<Entities.Grupy> GetAllGroups
        {
            get { return from g in entities.Grupy select g; }
        }

        public Entities.Grupy GetGroupByName(string name)
        {
            return (entities.Grupy.Single(g => g.nazwa_grupy == name));
        }

        public Entities.Grupy GetGroupByID(int id)
        {
            return entities.Grupy.Single(g => g.id_grupy == id);
        }

        public void AddGroup(Entities.Grupy group)
        {
            entities.Grupy.Add(group);
        }

        public void EditGroup(Entities.Grupy group)
        {
           entities.Entry(group).State = EntityState.Modified;
        }

        public void DeleteGroup(Entities.Grupy group)
        {
            entities.Grupy.Remove(group);
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