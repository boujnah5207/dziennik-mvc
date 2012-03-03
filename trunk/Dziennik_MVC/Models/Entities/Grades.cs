using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Dziennik_MVC.Models.Entities
{
    public class Grades
    {
        [Key]
        public int GradeID { get; set; }
        public decimal Grade { get; set; }
        public DateTime Date { get; set; }
        public string Comments { get; set; }

        public virtual ICollection<Projects> Projects { get; set; }
        public virtual Courses Courses { get; set; }
        public virtual Students Students { get; set; }
    }
}
