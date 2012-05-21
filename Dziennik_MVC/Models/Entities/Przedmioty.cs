using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Dziennik_MVC.Models.Entities
{
    public class Przedmioty
    {
        [Key]
        public int id_przedmiotu { get; set; }
        public string nazwa_przedmiotu { get; set; }

        public int typ { get; set; }
        public decimal ilosc_godzin { get; set; }
        public decimal ects { get; set; }
        public string opis { get; set; }
        public string uwagi { get; set; }

        public virtual Wykladowcy Wykladowcy { get; set; }
        public virtual ICollection<Oceny> Oceny { get; set; }
        public virtual ICollection<Grupy> Grupy { get; set; }
    }
}
