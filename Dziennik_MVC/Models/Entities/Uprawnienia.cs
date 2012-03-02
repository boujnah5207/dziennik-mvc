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
        public int ID_uprawnienia { get; set; }

        [Required]
        public string Nazwa_uprawnienia { get; set; }

        public ICollection<Uzytkownicy> Uzytkownicy { get; set; }
    }
}
