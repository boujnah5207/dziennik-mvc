using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Dziennik_MVC.Models.Entities
{
    public class Uprawnienia
    {
        [Key]
        [Required]
        public int id_uprawnienia { get; set; }

        [Required]
        public string nazwa_uprawnienia { get; set; }

        public virtual ICollection<Uzytkownicy> Uzytkownicy { get; set; }
    }
}
