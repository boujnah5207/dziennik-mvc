using System.Linq;
using Dziennik_MVC.Models.Entities;

namespace Dziennik_MVC.Models.Data.Abstract
{
    public interface IGrupyRepository
    {
        IQueryable<Grupy> GetAllGroups { get; }

        Grupy GetGroupByName(string name);
        Grupy GetGroupByID(int id);
        
        void AddGroup(Grupy grupa);
        void EditGroup(Grupy grupa);
        void DeleteGroup(Grupy grupa);
        void DeleteGroup(int id);

        void Save();
        void Dispose();
    }
}
