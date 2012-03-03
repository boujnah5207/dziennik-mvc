using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Dziennik_MVC.Models.Entities
{
    [Table("Students")]
    public class Students : Users
    {
        public int SchoolBook_Number { get; set; }

        public virtual ICollection<Projects> Projects { get; set; }
        public virtual ICollection<Grades> Grades { get; set; }
        public virtual Groups Groups { get; set; }
    }
}
