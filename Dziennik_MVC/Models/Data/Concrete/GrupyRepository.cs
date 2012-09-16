using System.Data;
using System.Linq;
using Dziennik_MVC.Models.Data.Abstract;

namespace Dziennik_MVC.Models.Data.Concrete
{
    public class GrupyRepository : IGrupyRepository
    {
        private readonly EFContext entities;

        public GrupyRepository(IUnitOfWork unitOfWork)
        {
            entities = unitOfWork as EFContext;
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