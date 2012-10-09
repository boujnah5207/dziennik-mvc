using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using Dziennik_MVC.Infrastructure.Logging;
using Dziennik_MVC.Models.Data.Abstract;
using Dziennik_MVC.Models.Entities;
using PagedList;

namespace Dziennik_MVC.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StudenciController : Controller
    {
        private IGrupyRepository _repo;
        private IUsersRepository _repo1;
        private readonly ILogger _logger;

        public StudenciController(IGrupyRepository repo, IUsersRepository repo2, ILogger logger) 
        {
            _repo = repo;
            _repo1 = repo2;
            _logger = logger;
        }

        //
        // GET: /Admin/Students/

        public ViewResult List(string sortOrder, int? page)
        {
             _logger.Info("StudenciController.List => HTTP POST");
            ViewBag.Current = "Students";    // Aktualne zaznaczenie zakladki Profil w Menu 
            ViewBag.CurrentSort = sortOrder;    // Zachowanie sortowania pomiędzy przejściami stron

            ViewBag.IDStudentaSortParm = sortOrder == "ID Studenta asc" ? "ID Studenta desc" : "ID Studenta asc";
            ViewBag.NameSortParm = sortOrder == "Name asc" ? "Name desc" : "Name asc";
            ViewBag.LastNameSortParm = sortOrder == "LastName asc" ? "LastName desc" : "LastName asc";
            ViewBag.GroupParm = sortOrder == "Group asc" ? "Group desc" : "Group asc";
            ViewBag.IndexSortParm = sortOrder == "Index asc" ? "Index desc" : "Index asc";

            var students = _repo1.GetAllStudents;

            switch (sortOrder)
            {
                case "ID Studenta desc":
                    students = students.OrderByDescending(s => s.id_studenta);
                    break;
                case "ID Studenta asc":
                    students = students.OrderBy(s => s.id_studenta);
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
                case "Group desc":
                    students = students.OrderByDescending(s => s.Grupy.nazwa_grupy);
                    break;
                case "Group asc":
                    students = students.OrderBy(s => s.Grupy.nazwa_grupy);
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

            int pageSize = 10;
            int pageNumber = (page ?? 1); // Jeśli page == null to page =1

            return View(students.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /Admin/Students/Add

        public ActionResult Add()
        {
            _logger.Info("StudenciController.Add => HTTP GET");
            ViewBag.Current = "Students";
            ViewBag.ListaGrup = _repo.GetAllGroups;
            return View();
        }

        //
        // POST: /Admin/Students/Add

        [HttpPost]
        public ActionResult Add(Studenci student)
        {
            _logger.Info("StudenciController.Add => Entering | HTTP POST");
            if (!_repo1.StudentExists(student) && !_repo1.IndeksExists(student))
            if (ModelState.IsValid)
            {
                student.haslo = FormsAuthentication.HashPasswordForStoringInConfigFile(student.haslo.Trim(), "md5");
                _repo1.AddStudent(student);
                _repo1.Save();
                _logger.Info("StudenciController.Add => SUCCES = Add Studenci| HTTP POST");
                TempData["message"] = "Pomyślnie dodano nowego studenta!";
                TempData["status"] = "valid";
                return RedirectToAction("List");
            }
            _logger.Info("StudenciController.Add => FAILED = Add Studenci | HTTP POST");
            TempData["message"] = "Nie udało się dodać studenta! Taki student istnieje!";
            TempData["status"] = "invalid";
            ViewBag.Current = "Students";
            ViewBag.ListaGrup = _repo.GetAllGroups;
            return View(student);
        }

        //
        // GET: /Admin/Students/Edit/5

        public ActionResult Edit(int id)
        {
            _logger.Info("StudenciController.Edit => HTTP GET");
            ViewBag.Current = "Students";
            Studenci student = _repo1.GetStudentByID(id);
            ViewBag.ListaGrup = _repo.GetAllGroups;
            return View(student);
        }

        //
        // POST: /Admin/Students/Edit/5

        [HttpPost]
        public ActionResult Edit(Studenci student)
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

        public ActionResult Delete(int id)
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