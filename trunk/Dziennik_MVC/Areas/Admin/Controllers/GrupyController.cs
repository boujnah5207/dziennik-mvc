using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dziennik_MVC.Models.Entities;
using Dziennik_MVC.Models.Data.Concrete;
using Dziennik_MVC.Models.Data.Abstract;
using PagedList;
using System.Data.Objects.SqlClient;
using Dziennik_MVC.Infrastructure.Logging;

namespace Dziennik_MVC.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class GrupyController : Controller
    {
        private IGrupyRepository _repo;
        private ISemestryRepository _repo1;
        private readonly ILogger _logger;

        public GrupyController(IGrupyRepository repo, ISemestryRepository repo1, ILogger logger) 
        {
            _repo = repo;
            _repo1 = repo1;
            _logger = logger;
        }

        public ViewResult List(string sortOrder, int? page)
        {
            _logger.Info("GrupyController.List => HTTP POST");
            ViewBag.Current = "Grupy";    // Aktualne zaznaczenie zakladki Profil w Menu 
            ViewBag.CurrentSort = sortOrder;    // Zachowanie sortowania pomiędzy przejściami stron

            ViewBag.IDGrupySortParm = sortOrder == "ID Grupy asc" ? "ID Grupy desc" : "ID Grupy asc";
            ViewBag.IDSemestruSortParm = sortOrder == "ID Semestru asc" ? "ID Semestru desc" : "ID Semestru asc";
            ViewBag.NameSortParm = sortOrder == "Name asc" ? "Name desc" : "Name asc";
            ViewBag.SemesterSortParm = sortOrder == "Semester asc" ? "Semester desc" : "Semester asc";
            ViewBag.TypSortParm = sortOrder == "Typ asc" ? "Typ desc" : "Typ asc";

            var grupy = _repo.GetAllGroups;

            switch (sortOrder)
            {
                case "ID Grupy desc":
                    grupy = grupy.OrderByDescending(s => s.nazwa_grupy);
                    break;
                case "ID Grupy asc":
                    grupy = grupy.OrderBy(s => s.nazwa_grupy);
                    break;
                case "ID Semestru desc":
                    grupy = grupy.OrderByDescending(s => s.nazwa_grupy);
                    break;
                case "ID Semestru asc":
                    grupy = grupy.OrderBy(s => s.nazwa_grupy);
                    break;
                case "Name desc":
                    grupy = grupy.OrderByDescending(s => s.nazwa_grupy);
                    break;
                case "Name asc":
                    grupy = grupy.OrderBy(s => s.nazwa_grupy);
                    break;
                case "Semester desc":
                    grupy = grupy.OrderByDescending(s => s.Semestry.rok);
                    break;
                case "Semester asc":
                    grupy = grupy.OrderBy(s => s.Semestry.rok);
                    break;
                case "Typ desc":
                    grupy = grupy.OrderByDescending(s => s.Semestry.typ);
                    break;
                case "Typ asc":
                    grupy = grupy.OrderBy(s => s.Semestry.typ);
                    break;
                default:
                    grupy = grupy.OrderBy(s => s.id_grupy);
                    break;
            }

            int pageSize = 10; 
            int pageNumber = (page ?? 1); // Jeśli page == null to page = 1

            return View(grupy.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Add()
        {
            _logger.Info("GrupyController.Add => HTTP GET");
            ViewBag.Current = "Grupy";
            ViewBag.ListaSem = _repo1.GetAllSemestry;
            return View();
        } 

        [HttpPost]
        public ActionResult Add(Grupy grupa)
        {
            _logger.Info("GrupyController.Add => Entering | HTTP POST");
            if(!_repo.GrupaExists(grupa))
                if (ModelState.IsValid)
                {
                    _repo.AddGroup(grupa);
                    _repo.Save();
                    _logger.Info("GrupyController.Add => SUCCES = Add Grupy| HTTP POST");
                    TempData["message"] = "Pomyślnie dodano nową grupę!";
                    TempData["status"] = "valid";
                    return RedirectToAction("List");
                }
                _logger.Info("GrupyController.Add => FAILED = Add Grupy | HTTP POST");
                TempData["message"] = "Nie udało się dodać grupy! Taka grupa istnieje!";
                TempData["status"] = "invalid";
                ViewBag.ListaSem = _repo1.GetAllSemestry;
                ViewBag.Current = "Grupy";    
        
            return View(grupa);
        }
 
        public ActionResult Edit(int id)
        {
            _logger.Info("GrupyController.Edit => HTTP GET");
            ViewBag.Current = "Grupy";
            ViewBag.ListaSem = _repo1.GetAllSemestry;
            Grupy grupa = _repo.GetGroupByID(id);
            return View(grupa);
        }

        [HttpPost]
        public ActionResult Edit(Grupy grupa)
        {
            _logger.Info("GrupyController.Edit => Entering| HTTP POST");
            if (!_repo.GrupaExists(grupa))
            if (ModelState.IsValid)
            {
                _repo.EditGroup(grupa);
                _repo.Save();
                _logger.Info("GrupyController.Edit => SUCCES = Edit Semester| HTTP POST");
                TempData["message"] = "Zauktalizowano grupę!";
                TempData["status"] = "valid";
                return RedirectToAction("List");
            }
            _logger.Info("GrupyController.Edit => FAILED = Edit Semester| HTTP POST");
            TempData["message"] = "Nie udało się uaktualnić grupy! Taki grupa istnieje!";
            TempData["status"] = "invalid";
            ViewBag.Current = "Grupy";
            ViewBag.ListaSem = _repo1.GetAllSemestry;
            return View(grupa);
        }
 
        public ActionResult Delete(int id)
        {
            _logger.Info("GrupyController.Delete => HTTP GET");
            ViewBag.Current = "Grupy";
            Grupy grupa = _repo.GetGroupByID(id);
            return View(grupa);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _logger.Info("SemestryController.Delete => SUCCES = Delete Semester| HTTP POST");
            Grupy grupa = _repo.GetGroupByID(id);
            _repo.DeleteGroup(grupa);
            _repo.Save();
            TempData["message"] = "Usunięto grupę!";
            TempData["status"] = "valid";
            return RedirectToAction("List");
        }

        protected override void Dispose(bool disposing)
        {
            _repo.Dispose();
            base.Dispose(disposing);
        }
    }
}