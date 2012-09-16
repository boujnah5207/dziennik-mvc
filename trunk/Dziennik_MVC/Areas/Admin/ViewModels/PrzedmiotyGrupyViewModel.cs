using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dziennik_MVC.Models.Entities;

namespace Dziennik_MVC.Areas.Admin.ViewModels
{
    public class PrzedmiotyGrupyViewModel
    {
        public Grupy grupa { get; set; }
        public List<Przedmioty> AvailablePrzedmioty { get; set; }
        public List<Przedmioty> RequestedPrzedmioty { get; set; }
                
        public int[] AvailableSelected { get; set; }
        public int[] RequestedSelected { get; set; }
        public string SavedRequested { get; set; }
    }
}