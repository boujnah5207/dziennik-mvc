using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Dziennik_MVC.Models.Entities
{
    public class Roles
    {
        [Key]
        [Required]
        public int RoleID { get; set; }

        [Required]
        public string RoleName { get; set; }

        public ICollection<Users> Users { get; set; }
    }
}
