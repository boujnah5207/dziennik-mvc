using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Dziennik_MVC.Models.Entities
{
    public class Courses
    {
        [Key]
        public int CourseID { get; set; }
        public string CourseName { get; set; }

        public int Type { get; set; }
        public decimal Hours { get; set; }
        public decimal Ects { get; set; }
        public string Description { get; set; }
        public string Comments { get; set; }

        public virtual Teachers Teachers { get; set;}
        public virtual ICollection<Grades> Grades { get; set; }
        public virtual ICollection<Groups> Groups { get; set; }
    }
}
