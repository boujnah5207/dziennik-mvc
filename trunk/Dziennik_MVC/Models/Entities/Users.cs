using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;

namespace Dziennik_MVC.Models.Entities
{
    public abstract class Users
    {
        public Users() { 
        }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "{0} musi być co najmniej {2} znaków długości.", MinimumLength = 4)]
        [RegularExpression(@"^[a-zA-Z0-9_.]*$", ErrorMessage = "Błędny login. Login powinien mieć maksymalnie 50 znaków (male i duże litery oraz cyfry) ")]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "{0} musi być co najmniej {2} znaków długości.", MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime CreationDate { get; set; }

        [Required(ErrorMessage="Data urodzenia jest wymagana")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime BirthDay { get; set; }

        [Display(Name = "Aktywny")]
        public bool isActive { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "{0} musi być co najmniej {2} znaków długości.", MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "{0} musi być co najmniej {2} znaków długości.", MinimumLength = 2)]
        public string LastName { get; set; }

        [Required]
        [Email(ErrorMessage = "To nie jest poprawny adres email!")]
        public string Email { get; set; }

        public int RoleID { get; set; }

        public virtual Roles Roles { get; set; }
    }
}
