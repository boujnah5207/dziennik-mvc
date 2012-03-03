using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Dziennik_MVC.Models.Entities
{
    public class Projects
    {
        [Key]
        public int ProjectID { get; set; }
        public decimal Grade { get; set; }
        public DateTime Date { get; set; }
        public string ProjectName { get; set; }

        public virtual Grades Grades { get; set; }
        public virtual Teachers Teachers { get; set; }
        public virtual ICollection<Students> Students { get; set; }
    }
}
