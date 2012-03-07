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

        [Display(Name="Semestr")]
        [Required(ErrorMessage="Pole wymagane!")]
        public string Type { get; set; }

        [Display(Name = "Rok akademicki")]
        [Required(ErrorMessage = "Pole wymagane!")]
        [RegularExpression(@"^(19|20)\d\d[- /.](19|20)\d\d$", ErrorMessage = "Poprawny format to YYYY/YYYY")]
        public string Year { get; set; }

        public virtual ICollection<Groups> Groups { get; set; }
    }
}
