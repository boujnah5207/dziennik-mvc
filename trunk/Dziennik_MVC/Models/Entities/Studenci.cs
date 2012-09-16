using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dziennik_MVC.Models.Entities
{
    public class Studenci 
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_studenta { get; set; }
        public int id_grupy { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "{0} musi być co najmniej {2} znaków długości.", MinimumLength = 4)]
        [RegularExpression(@"^[a-zA-Z0-9_.]*$", ErrorMessage = "Błędny login. Login powinien mieć maksymalnie 50 znaków (male i duże litery oraz cyfry) ")]
        [Display(Name = "Login")]
        public string login { get; set; }

        [Required]
        [Display(Name = "Hasło")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "{0} musi być co najmniej {2} znaków długości.", MinimumLength = 4)]
        public string haslo { get; set; }

        [Required]
        [Display(Name = "Imię")]
        [StringLength(50, ErrorMessage = "{0} musi być co najmniej {2} znaków długości.", MinimumLength = 3)]
        public string imie { get; set; }

        [Required]
        [Display(Name = "Nazwisko")]
        [StringLength(50, ErrorMessage = "{0} musi być co najmniej {2} znaków długości.", MinimumLength = 2)]
        public string nazwisko { get; set; }

        [Required]
        [Display(Name="Numer Indeksu")]
        public int nr_indeksu { get; set; }

        public virtual ICollection<Projekty> Projekty { get; set; }
        public virtual ICollection<Oceny> Oceny { get; set; }
        public virtual Grupy Grupy { get; set; }
    }
}
