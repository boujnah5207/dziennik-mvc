using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Dziennik_MVC.Models.Entities
{
    public class Semestry
    {
        [Key]
        public int ID_semestru { get; set; }
        public string Typ { get; set; }
        public string Rok { get; set; }

        public virtual ICollection<Grupy> Grupy { get; set; }
    }
}
