using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Dziennik_MVC.Models.Entities
{
    [Table("Administrator")]
    public class Administrator : Uzytkownicy
    {
        public Administrator() { }
    }
}