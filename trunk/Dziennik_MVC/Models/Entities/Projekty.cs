using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Dziennik_MVC.Models.Entities
{
    public class Projekty
    {
        [Key]
        public int id_projektu { get; set; }
        public DateTime data { get; set; }
        public string nazwa_projektu { get; set; }
        public int ilosc_osob { get; set; }

        public virtual Oceny Oceny { get; set; }
        public virtual Wykladowcy Wykladowcy { get; set; }
        public virtual ICollection<Studenci> Studenci { get; set; }
    }
}
