using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dziennik_MVC.Models.Entities
{
    public class Przedmioty
    {
        [Key]
        public int id_przedmiotu { get; set; }


        [Display(Name = "Nazwa przedmiotu")]
        [Required(ErrorMessage = "Pole wymagane!")]
        [StringLength(100)]
        public string nazwa_przedmiotu { get; set; }

        public virtual Prowadzacy Prowadzacy { get; set; }
        public virtual ICollection<Oceny> Oceny { get; set; }
        public virtual ICollection<Grupy> Grupy { get; set; }
    }
}
