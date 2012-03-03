using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Dziennik_MVC.Models.Entities
{
    public class Groups
    {
        [Key]
        public int GroupID { get; set; }
        public string GroupName { get; set; }

        public Semesters Semesters { get; set; }
        public ICollection<Courses> Courses { get; set; }
    }
}
