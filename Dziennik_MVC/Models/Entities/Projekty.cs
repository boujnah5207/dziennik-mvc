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
        public int ID_projektu { get; set; }
        public decimal ocena { get; set; }
        public DateTime Data { get; set; }
        public string Nazwa_projektu { get; set; }

        public virtual Oceny Oceny { get; set; }
        public virtual Wykladowcy Wykladowcy { get; set; }
        public virtual ICollection<Studenci> Studenci { get; set; }
    }
}
