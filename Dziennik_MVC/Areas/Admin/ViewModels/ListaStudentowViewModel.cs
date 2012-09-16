using System;
using System.Linq;
using Dziennik_MVC.Models.Entities;

namespace Dziennik_MVC.Areas.Admin.ViewModels
{
    public class ListaStudentowViewModel
    {
        public Grupy grupa { get; set; }
        public IQueryable<Studenci> studenci { get; set; }
        public String nazwaKontrolera;
    }
}