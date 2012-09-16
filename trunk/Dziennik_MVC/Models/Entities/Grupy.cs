using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dziennik_MVC.Models.Entities
{
    public class Grupy
    {
        [Key]
        public int id_grupy { get; set; }

        [Display(Name="Grupa zajęciowa")]
        [Required(ErrorMessage = "Pole wymagane!")]
        public string nazwa_grupy { get; set; }

        public virtual ICollection<Przedmioty> Przedmioty { get; set; }
        public virtual ICollection<Studenci> Studenci { get; set; }
    }
}
