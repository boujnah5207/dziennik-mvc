using System;
using System.ComponentModel.DataAnnotations;

namespace Dziennik_MVC.Models.Entities
{
    public class Oceny
    {
        [Key]
        public int id_oceny { get; set; }
        public decimal ocena { get; set; }
        public DateTime data { get; set; }
        public string uwagi { get; set; }

        public virtual Przedmioty Przedmioty { get; set; }
        public virtual Studenci Studenci { get; set; }
    }
}
