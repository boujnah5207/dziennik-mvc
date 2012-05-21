using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Dziennik_MVC.Models.Entities
{
    public class Semestry
    {

        [Key]
        public int id_semestru { get; set; }

        [Display(Name = "Semestr")]
        [Required(ErrorMessage = "Pole wymagane!")]
        public string typ { get; set; }

        [Display(Name = "Rok akademicki")]
        [Required(ErrorMessage = "Pole wymagane!")]
        [RegularExpression(@"^(19|20)\d\d[- /.](19|20)\d\d$", ErrorMessage = "Poprawny format to YYYY/YYYY")]
        public string rok { get; set; }

        public virtual ICollection<Grupy> Grupy { get; set; }
    }
}