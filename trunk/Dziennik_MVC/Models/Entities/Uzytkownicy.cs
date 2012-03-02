using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;

namespace Dziennik_MVC.Models.Entities
{
    public abstract class Uzytkownicy
    {
        public Uzytkownicy() { 
        }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID_uzytkownika { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "{0} musi być co najmniej {2} znaków długości.", MinimumLength = 4)]
        [RegularExpression(@"^[a-zA-Z0-9_.]*$", ErrorMessage = "Błędny login. Login powinien mieć maksymalnie 50 znaków (male i duże litery oraz cyfry) ")]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "{0} musi być co najmniej {2} znaków długości.", MinimumLength = 6)]
        public string Haslo { get; set; }

        [Display(Name = "Aktywny")]
        public bool isActive { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "{0} musi być co najmniej {2} znaków długości.", MinimumLength = 3)]
        public string Imie { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "{0} musi być co najmniej {2} znaków długości.", MinimumLength = 2)]
        public string Nazwisko { get; set; }

        [Required]
        [Email(ErrorMessage = "To nie jest poprawny adres email!")]
        public string Email { get; set; }

        public int ID_uprawnienia { get; set; }

        public virtual Uprawnienia Uprawnienia { get; set; }
    }
}
