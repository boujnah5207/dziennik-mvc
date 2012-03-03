using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Dziennik_MVC.Models.Entities
{
    [Table("Teachers")]
    public class Teachers : Users
    {
        public string Titles { get; set; }
        public string Consultations { get; set; }

        public virtual ICollection<Courses> Courses { get; set; }
        public virtual ICollection<Projects> Projects { get; set; }
    }
}
