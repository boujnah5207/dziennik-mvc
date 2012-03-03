using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Dziennik_MVC.Models.Entities
{
    public class Semesters
    {
        [Key]
        public int SemesterID { get; set; }
        public string Type { get; set; }
        public string Year { get; set; }

        public virtual ICollection<Groups> Groups { get; set; }
    }
}
