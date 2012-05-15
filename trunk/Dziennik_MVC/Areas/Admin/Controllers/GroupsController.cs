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

namespace Dziennik_MVC.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class GroupsController : Controller
    {
        private IGroupsRepository _repo;
        private ISemestersRepository _repo1;

        public GroupsController(IGroupsRepository repo, ISemestersRepository repo1) 
        {
            _repo = repo;
            _repo1 = repo1;
        }

        //
        // GET: /Admin/Groups/

        public ViewResult List(string sortOrder, int? page)
        {
            ViewBag.Current = "Grupy";    // Aktualne zaznaczenie zakladki Profil w Menu 
            ViewBag.CurrentSort = sortOrder;    // Zachowanie sortowania pomiędzy przejściami stron

            ViewBag.NameSortParm = sortOrder == "Name asc" ? "Name desc" : "Name asc";
            ViewBag.SemesterSortParm = sortOrder == "Semester asc" ? "Semester desc" : "Semester asc"; 

            var groups = _repo.GetAllGroups;

            switch (sortOrder)
            {
                case "Name desc":
                    groups = groups.OrderByDescending(s => s.GroupName);
                    break;
                case "Name asc":
                    groups = groups.OrderBy(s => s.GroupName);
                    break;
                case "Semester desc":
                    groups = groups.OrderByDescending(s => s.Semesters.Year);
                    break;
                case "Semester asc":
                    groups = groups.OrderBy(s => s.Semesters.Year);
                    break;
                default:
                    groups = groups.OrderBy(s => s.GroupID);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1); // Jeśli page == null to page =1

            return View(groups.ToPagedList(pageNumber, pageSize));
        }
                
        //
        // GET: /Admin/Groups/Add

        public ActionResult Add()
        {
            ViewBag.Current = "Grupy";
            ViewBag.ListaSem = _repo1.GetAllSemesters;
            return View();
        } 

        //
        // POST: /Admin/Groups/Add

        [HttpPost]
        public ActionResult Add(Groups groups)
        {
            if (ModelState.IsValid)
            {
                _repo.AddGroup(groups);
                _repo.Save();
                TempData["message"] = "Pomyślnie dodano nową grupę!";
                TempData["status"] = "valid";
                return RedirectToAction("List");
            }
            TempData["message"] = "Nie udało się dodać grupy! Taka grupa istnieje!";
            TempData["status"] = "invalid";
            ViewBag.Current = "Grupy";
            return View(groups);
        }
        
        //
        // GET: /Admin/Groups/Edit/5
 
        public ActionResult Edit(int id)
        {
            ViewBag.Current = "Grupy";
            Groups groups = _repo.GetGroupByID(id);
            ViewBag.ListaSem = _repo1.GetAllSemesters;
            return View(groups);
        }

        //
        // POST: /Admin/Groups/Edit/5

        [HttpPost]
        public ActionResult Edit(Groups groups)
        {
            if (ModelState.IsValid)
            {
                _repo.EditGroup(groups);
                _repo.Save();
                TempData["message"] = "Zauktalizowano grupę!";
                TempData["status"] = "valid";
                return RedirectToAction("List");
            }
            TempData["message"] = "Nie udało się uaktualnić grupy! Taki grupa istnieje!";
            TempData["status"] = "invalid";
            ViewBag.Current = "Grupy";
            return View(groups);
        }

        //
        // GET: /Admin/Groups/Delete/5
 
        public ActionResult Delete(int id)
        {
            ViewBag.Current = "Grupy";
            Groups groups = _repo.GetGroupByID(id);
            return View(groups);
        }

        //
        // POST: /Admin/Groups/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Groups groups = _repo.GetGroupByID(id);
            _repo.DeleteGroup(groups);
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