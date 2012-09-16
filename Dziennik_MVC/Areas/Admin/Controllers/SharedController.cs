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
using System.Web.Security;
using Dziennik_MVC.Infrastructure.Logging;
using Dziennik_MVC.Areas.Admin.ViewModels;

namespace Dziennik_MVC.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SharedController : Controller
    {
        private IGrupyRepository _repo;
        private IUsersRepository _repo1;
        private readonly ILogger _logger;

        public SharedController(IGrupyRepository repo, IUsersRepository repo1, ILogger logger) 
        {
            _repo = repo;
            _repo1 = repo1;
            _logger = logger;
        }

        public ViewResult ListaStudentow(int id, string sortOrder, string nazwa)
        {
            _logger.Info("GrupyController.ListaStudentow => HTTP POST");
            ViewBag.Current = "Grupy";    // Aktualne zaznaczenie zakladki Profil w Menu 
            ViewBag.CurrentSort = sortOrder;    // Zachowanie sortowania pomiędzy przejściami stron

            ViewBag.NazwaGrupy = _repo.GetGroupByID(id).nazwa_grupy;

            ViewBag.IDStudentaSortParm = sortOrder == "ID Studenta asc" ? "ID Studenta desc" : "ID Studenta asc";
            ViewBag.NameSortParm = sortOrder == "Name asc" ? "Name desc" : "Name asc";
            ViewBag.LastNameSortParm = sortOrder == "LastName asc" ? "LastName desc" : "LastName asc";
            ViewBag.IndexSortParm = sortOrder == "Index asc" ? "Index desc" : "Index asc";

            var students = _repo1.getStudentsInGroup(id);

            switch (sortOrder)
            {
                case "ID Studenta desc":
                    students = students.OrderByDescending(s => s.imie);
                    break;
                case "ID Studenta asc":
                    students = students.OrderBy(s => s.imie);
                    break;
                case "Name desc":
                    students = students.OrderByDescending(s => s.imie);
                    break;
                case "Name asc":
                    students = students.OrderBy(s => s.imie);
                    break;
                case "LastName desc":
                    students = students.OrderByDescending(s => s.nazwisko);
                    break;
                case "LastName asc":
                    students = students.OrderBy(s => s.nazwisko);
                    break;
                case "Index desc":
                    students = students.OrderByDescending(s => s.nr_indeksu);
                    break;
                case "Index asc":
                    students = students.OrderBy(s => s.nr_indeksu);
                    break;
                default:
                    students = students.OrderBy(s => s.id_studenta);
                    break;
            }

            var listaStudentowViewModel = new ListaStudentowViewModel
            {
                grupa = _repo.GetGroupByID(id),
                studenci = students,
                nazwaKontrolera = nazwa
            };

            return View(listaStudentowViewModel);
        }

        //
        // GET: /Admin/StudentEdit/Edit/5

        public ActionResult StudentEdit(int id)
        {
            _logger.Info("StudenciController.Edit => HTTP GET");
            ViewBag.Current = "Students";
            Studenci student = _repo1.GetStudentByID(id);
            ViewBag.ListaGrup = _repo.GetAllGroups;
            return View(student);
        }

        //
        // POST: /Admin/StudentEdit/Edit/5

        [HttpPost]
        public ActionResult StudentEdit(Studenci student)
        {
            _logger.Info("StudenciController.Edit => Entering| HTTP POST");
            if (ModelState.IsValid)
            {
                _repo1.EditStudent(student);
                _repo1.Save();
                _logger.Info("StudenciController.Edit => SUCCES = Edit Studenci| HTTP POST");
                TempData["message"] = "Zauktalizowano studenta!";
                TempData["status"] = "valid";
                return RedirectToAction("List");
            }
            _logger.Info("StudenciController.Edit => FAILED = Edit Studenci| HTTP POST");

            ViewBag.ListaGrup = _repo.GetAllGroups;
            TempData["message"] = "Nie udało się uaktualnić studenta! Taki student istnieje!";
            TempData["status"] = "invalid";
            ViewBag.Current = "Students";
            return View(student);
        }

        //
        // GET: /Admin/Students/Delete/5

        public ActionResult StudentDelete(int id)
        {
            _logger.Info("StudenciController.Delete => HTTP GET");
            ViewBag.Current = "Students";
            Studenci student = _repo1.GetStudentByID(id);
            return View(student);
        }

        //
        // POST: /Admin/Students/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _logger.Info("StudenciController.Delete => SUCCES = Delete Studenci| HTTP POST");
            Studenci student = (Studenci)_repo1.GetStudentByID(id);
            _repo1.DeleteStudent(student);
            _repo1.Save();
            TempData["message"] = "Usunięto Studenta!";
            TempData["status"] = "valid";
            return RedirectToAction("List");
        }

        protected override void Dispose(bool disposing)
        {
            _repo1.Dispose();
            base.Dispose(disposing);
        }
    }
}