using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Dziennik_MVC.Models.Entities
{
    public class Grupy
    {
        [Key]
        public int ID_grupy { get; set; }
        public string Nazwa_grupy { get; set; }

        public Semestry Semestry { get; set; }
        public ICollection<Przedmioty> Przedmioty { get; set; }
    }
}
