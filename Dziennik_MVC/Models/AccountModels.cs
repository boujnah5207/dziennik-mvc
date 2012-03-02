using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using DataAnnotationsExtensions;

namespace Dziennik_MVC.Models
{

    public class ChangePasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Aktualne hasło")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} musi być co najmniej {2} znaków długości.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nowe hasło")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź nowe hasło")]
        [Compare("NewPassword", ErrorMessage = "Hasła się nie zgadzają")]
        public string ConfirmPassword { get; set; }
    }

    public class LogOnModel
    {
        [Required]
        [Display(Name = "Login")]
        public string UserName { get; set; }

        public bool isAcvite { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "{0} musi być co najmniej {2} znaków długości.", MinimumLength = 6)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [Display(Name = "Pamiętaj mnie?")]
        public bool RememberMe { get; set; }
    }
}
