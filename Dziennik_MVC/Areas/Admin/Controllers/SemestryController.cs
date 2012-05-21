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
using Dziennik_MVC.Infrastructure.Logging;

namespace Dziennik_MVC.Areas.Admin.Controllers
{ 
    [Authorize(Roles="Admin")]
    public class SemestryController : Controller
    {
        private ISemestryRepository _repo;
        private readonly ILogger _logger;

        public SemestryController(ISemestryRepository repo, ILogger logger) 
        {
            _repo = repo;
            _logger = logger;
        }

        public ViewResult List(string sortOrder, int? page)
        {
            _logger.Info("SemestryController.List => HTTP GET");
            ViewBag.Current = "Semestry";    // Aktualne zaznaczenie zakladki Profil w Menu 
            ViewBag.CurrentSort = sortOrder;    // Zachowanie sortowania pomiędzy przejściami stron
            ViewBag.IDSortParm = sortOrder == "ID asc" ? "ID desc" : "ID asc";
            ViewBag.TypeSortParm = sortOrder == "Type asc" ? "Type desc" : "Type asc";
            ViewBag.YearSortParm = sortOrder == "Year asc" ? "Year desc" : "Year asc";

            var semesters = _repo.GetAllSemestry;

            switch (sortOrder)
            {
                case "ID desc":
                    semesters = semesters.OrderByDescending(s => s.id_semestru);
                    break;
                case "ID asc":
                    semesters = semesters.OrderBy(s => s.id_semestru);
                    break;
                case "Type desc":
                    semesters = semesters.OrderByDescending(s => s.typ);
                    break;
                case "Type asc":
                    semesters = semesters.OrderBy(s => s.typ);
                    break;
                case "Year desc":
                    semesters = semesters.OrderByDescending(s => s.rok);
                    break;
                case "Year asc":
                    semesters = semesters.OrderBy(s => s.rok);
                    break;
                default:
                    semesters = semesters.OrderBy(s => s.id_semestru);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1); // Jeśli page == null to page =1

            return View(semesters.ToPagedList(pageNumber, pageSize));
        }
        
        // GET: /Semesters/Add

        public ActionResult Add()
        {
            _logger.Info("SemestryController.Add => HTTP GET");
            ViewBag.Current = "Semestry";
            return View();
        } 

        //
        // POST: /Admin/Semesters/Add

        [HttpPost]
        public ActionResult Add(Semestry semesters)
        {
            _logger.Info("SemestryController.Add => Entering | HTTP POST");
            var result = _repo.GetAllSemestry.Where(m => m.typ == semesters.typ && m.rok == semesters.rok);            

            if (result.Count() == 0 )
                if (ModelState.IsValid)
                {
                    _repo.AddSemestr(semesters);
                    _repo.Save();
                    _logger.Info("SemestryController.Add => SUCCES = Add Semester| HTTP POST");
                    TempData["message"] = "Pomyślnie dodano nowy semestr!";
                    TempData["status"] = "valid";
                 return RedirectToAction("List");  
                }
            _logger.Info("SemestryController.Add => FAILED = Add Semester | HTTP POST");
            TempData["message"] = "Nie udało się dodać semestru! Taki semestr istnieje!";
            TempData["status"] = "invalid";
            ViewBag.Current = "Semestry";
            return View(semesters);
        }
        
        //
        // GET: /Semesters/Edit/5
 
        public ActionResult Edit(int id)
        {
            _logger.Info("SemestryController.Edit => HTTP GET");
            ViewBag.Current = "Semestry";
            Semestry semesters = _repo.GetSemestrByID(id);
            return View(semesters);
        }

        //
        // POST: /Admin/Semesters/Edit/5

        [HttpPost]
        public ActionResult Edit(Semestry semesters)
        {
            _logger.Info("SemestryController.Edit => Entering| HTTP POST");
            var result = _repo.GetAllSemestry.Where(m=>m.typ == semesters.typ && m.rok == semesters.rok);

            if (result.Count() == 0 || (result.Where(m => m.id_semestru == semesters.id_semestru).Count() == 1))
               if (ModelState.IsValid){
                _repo.EditSemestr(semesters);
                _repo.Save();
                _logger.Info("SemestryController.Edit => SUCCES = Edit Semester| HTTP POST");
                TempData["message"] = "Zauktalizowano semestr!";
                TempData["status"] = "valid";
                return RedirectToAction("List");
                }
            _logger.Info("SemestryController.Edit => FAILED = Edit Semester| HTTP POST");
            TempData["message"] = "Nie udało się uaktualnić semestru! Taki semestr istnieje!";
            TempData["status"] = "invalid";
            ViewBag.Current = "Semestry";
            return View(semesters);
        }

        //
        // GET: /Admin/Semesters/Delete/5
 
        public ActionResult Delete(int id)
        {
            _logger.Info("SemestryController.Delete => HTTP GET");
            ViewBag.Current = "Semestry";
            Semestry semesters = _repo.GetSemestrByID(id);
            return View(semesters);
        }

        //
        // POST: /Admin/Semesters/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _logger.Info("SemestryController.Delete => SUCCES = Delete Semester| HTTP POST");
            Semestry semesters = _repo.GetSemestrByID(id);
            _repo.DeleteSemestr(semesters);
            _repo.Save();
            TempData["message"] = "Usunięto semestr!";
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