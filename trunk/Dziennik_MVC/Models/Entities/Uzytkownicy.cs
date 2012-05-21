using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;
using Resources;

namespace Dziennik_MVC.Models.Entities
{
    public abstract class Uzytkownicy
    {
        public Uzytkownicy() { 
        }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_uzytkownika { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "{0} musi być co najmniej {2} znaków długości.", MinimumLength = 4)]
        [RegularExpression(@"^[a-zA-Z0-9_.]*$", ErrorMessage = "Błędny login. Login powinien mieć maksymalnie 50 znaków (male i duże litery oraz cyfry) ")]
        [Display(Name = "Login")]
        public string login { get; set; }

        [Required]
        [Display(Name = "Hasło")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "{0} musi być co najmniej {2} znaków długości.", MinimumLength = 6)]
        public string haslo { get; set; }

        [Display(Name = "Aktywny")]
        public bool aktywny { get; set; }

        [Required]
        [Display(Name= "Imię")]
        [StringLength(50, ErrorMessage = "{0} musi być co najmniej {2} znaków długości.", MinimumLength = 3)]
        public string imie { get; set; }

        [Required]
        [Display(Name = "Nazwisko")]
        [StringLength(50, ErrorMessage = "{0} musi być co najmniej {2} znaków długości.", MinimumLength = 2)]
        public string nazwisko { get; set; }

        [Required]
        [Email(ErrorMessage = "To nie jest poprawny adres email!")]
        [Display(Name = "Email")]
        public string email { get; set; }

        public int id_uprawnienia { get; set; }

        public virtual Uprawnienia Uprawnienia { get; set; }
    }
}
