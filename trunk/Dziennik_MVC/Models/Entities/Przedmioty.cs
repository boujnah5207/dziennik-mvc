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
        public int ID_przedmiotu { get; set; }
        public string Nazwa_przedmiotu { get; set; }

        public int Typ { get; set; }
        public decimal Ilosc_godzin { get; set; }
        public decimal Ects { get; set; }
        public string Opis { get; set; }
        public string Uwagi { get; set; }

        public virtual Wykladowcy Wykladowcy { get; set;}
        public virtual ICollection<Oceny> Oceny { get; set; }
        public virtual ICollection<Grupy> Grupy { get; set; }
    }
}
