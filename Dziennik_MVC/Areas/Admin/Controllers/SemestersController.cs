using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Dziennik_MVC.Models.Entities;
using Dziennik_MVC.Models.Data.Concrete;
using Dziennik_MVC.Models.Data.Abstract;

namespace Dziennik_MVC.Areas.Admin.Controllers
{ 
    [Authorize(Roles="Admin")]
    public class SemestersController : Controller
    {
        private ISemestersRepository _repo;

        public SemestersController(ISemestersRepository repo) 
        {
            _repo = repo;
        }

        //
        // GET: /Semesters/

        public ViewResult List(string sortOrder, int? page)
        {
            ViewBag.Current = "Semestry";    // Aktualne zaznaczenie zakladki Profil w Menu 
            ViewBag.CurrentSort = sortOrder;
            ViewBag.TypeSortParm = sortOrder == "Type asc" ? "Type desc" : "Type asc";
            ViewBag.YearSortParm = sortOrder == "Year asc" ? "Year desc" : "Year asc";

            var semesters = _repo.GetAllSemesters;

            switch (sortOrder)
            {
                case "Type desc":
                    semesters = semesters.OrderByDescending(s => s.Type);
                    break;
                case "Type asc":
                    semesters = semesters.OrderBy(s => s.Type);
                    break;
                case "Year desc":
                    semesters = semesters.OrderByDescending(s => s.Year);
                    break;
                case "Year asc":
                    semesters = semesters.OrderBy(s => s.Year);
                    break;
                default:
                    semesters = semesters.OrderBy(s => s.SemesterID);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return View(semesters.ToPagedList(pageNumber, pageSize));
        }
        
        // GET: /Semesters/Add

        public ActionResult Add()
        {
            return View();
        } 

        //
        // POST: /Admin/Semesters/Add

        [HttpPost]
        public ActionResult Add(Semesters semesters)
        {
            if (ModelState.IsValid)
            {
                _repo.AddSemester(semesters);
                _repo.Save();
                TempData["message"] = "Pomyślnie dodano nowy semestr!";
                return RedirectToAction("List");  
            }

            return View(semesters);
        }
        
        //
        // GET: /Semesters/Edit/5
 
        public ActionResult Edit(int id)
        {
            Semesters semesters = _repo.GetSemesterByID(id);
            return View(semesters);
        }

        //
        // POST: /Admin/Semesters/Edit/5

        [HttpPost]
        public ActionResult Edit(Semesters semesters)
        {
            if (ModelState.IsValid)
            {
                _repo.EditSemester(semesters);
                _repo.Save();
                TempData["message"] = "Zauktalizowano semestr!";
                return RedirectToAction("List");
            }
            return View(semesters);
        }

        //
        // GET: /Admin/Semesters/Delete/5
 
        public ActionResult Delete(int id)
        {
            Semesters semesters = _repo.GetSemesterByID(id);
            return View(semesters);
        }

        //
        // POST: /Admin/Semesters/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Semesters semesters = _repo.GetSemesterByID(id);
            _repo.DeleteSemester(semesters);
            _repo.Save();
            TempData["message"] = "Usunięto semestr!";
            return RedirectToAction("List");
        }

        protected override void Dispose(bool disposing)
        {
            _repo.Dispose();
            base.Dispose(disposing);
        }
    }
}