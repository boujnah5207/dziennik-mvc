﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Dziennik_MVC.Models.Entities
{
    [Table("Studenci")]
    public class Studenci : Uzytkownicy
    {
        public int Nr_indeksu { get; set; }

        public virtual ICollection<Projekty> Projekty { get; set; }
        public virtual ICollection<Oceny> Oceny { get; set; }
        public virtual Grupy Grupy { get; set; }
    }
}
