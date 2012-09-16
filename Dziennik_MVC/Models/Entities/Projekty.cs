using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dziennik_MVC.Models.Entities
{
    public class Projekty
    {
        [Key]
        public int id_projektu { get; set; }
        public decimal ocena { get; set; }
        public DateTime data { get; set; }
        public string nazwa_projektu { get; set; }
        public int ilosc_osob { get; set; }
        public bool status { get; set; }

        public virtual Prowadzacy Prowadzacy { get; set; }
        public virtual ICollection<Studenci> Studenci { get; set; }
    }
}
