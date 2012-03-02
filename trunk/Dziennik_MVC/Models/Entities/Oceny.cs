using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Dziennik_MVC.Models.Entities
{
    public class Oceny
    {
        [Key]
        public int ID_oceny { get; set; }
        public decimal Ocena { get; set; }
        public DateTime Data { get; set; }
        public string Uwagi { get; set; }

        public virtual ICollection<Projekty> Projekty { get; set; }
        public virtual Przedmioty Przedmioty { get; set; }
        public virtual Studenci Studenci { get; set; }
    }
}
