using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Dziennik_MVC.Models.Entities
{
    [Table("Wykladowcy")]
    public class Wykladowcy : Uzytkownicy
    {
        public string tytuly { get; set; }
        public string konsultacje { get; set; }

        public virtual ICollection<Przedmioty> Przedmioty { get; set; }
        public virtual ICollection<Projekty> Projekty { get; set; }
    }
}
