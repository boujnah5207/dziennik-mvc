using System.Linq;
using System.Web.Mvc;
using Dziennik_MVC.Infrastructure.Logging;
using Dziennik_MVC.Models.Data.Abstract;
using Dziennik_MVC.Models.Entities;
using PagedList;

namespace Dziennik_MVC.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PrzedmiotyController : Controller
    {
        private IGrupyRepository _repo;
        private IPrzedmiotyRepository _repo1;
        private readonly ILogger _logger;

        public PrzedmiotyController(IGrupyRepository repo, IPrzedmiotyRepository repo1, ILogger logger) 
        {
            _repo = repo;
            _repo1 = repo1;
            _logger = logger;
        }

        //
        // GET: /Admin/Przedmioty/

        public ViewResult List(string sortOrder, int? page)
        {
            _logger.Info("PrzedmiotyController.List => HTTP POST");
            ViewBag.Current = "Przedmioty";    // Aktualne zaznaczenie zakladki Profil w Menu 
            ViewBag.CurrentSort = sortOrder;    // Zachowanie sortowania pomiędzy przejściami stron

            ViewBag.IDPrzedmiotuSortParm = sortOrder == "ID Przedmiotu asc" ? "ID Przedmiotu desc" : "ID Przedmiotu asc";
            ViewBag.NameSortParm = sortOrder == "Name asc" ? "Name desc" : "Name asc";

            var przedmioty = _repo1.GetAllPrzedmioty;

            switch (sortOrder)
            {
                case "ID Przedmiotu desc":
                    przedmioty = przedmioty.OrderByDescending(s => s.id_przedmiotu);
                    break;
                case "ID Przedmiotu asc":
                    przedmioty = przedmioty.OrderBy(s => s.id_przedmiotu);
                    break;
                case "Name desc":
                    przedmioty = przedmioty.OrderByDescending(s => s.nazwa_przedmiotu);
                    break;
                case "Name asc":
                    przedmioty = przedmioty.OrderBy(s => s.nazwa_przedmiotu);
                    break;                
                default:
                    przedmioty = przedmioty.OrderBy(s => s.id_przedmiotu);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1); // Jeśli page == null to page =1

            return View(przedmioty.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /Admin/Przedmioty/Add

        public ActionResult Add()
        {
            _logger.Info("PrzedmiotyController.Add => HTTP GET");
            ViewBag.Current = "Przedmioty";
            return View();
        }

        //
        // POST: /Admin/Przedmioty/Add

        [HttpPost]
        public ActionResult Add(Przedmioty przedmiot)
        {
            _logger.Info("PrzedmiotyController.Add => Entering | HTTP POST");

            if (ModelState.IsValid && !_repo1.PrzedmiotExists(przedmiot))
            {
                _repo1.AddPrzedmiot(przedmiot);
                _repo1.Save();
                _logger.Info("PrzedmiotyController.Add => SUCCES = Add Przedmioty| HTTP POST");
                TempData["message"] = "Pomyślnie dodano nowy przedmiot!";
                TempData["status"] = "valid";
                return RedirectToAction("List");
            }
            _logger.Info("PrzedmiotyController.Add => FAILED = Add Przedmioty | HTTP POST");
            TempData["message"] = "Nie udało się dodać przedmiotu! Taki przedmiot istnieje!";
            TempData["status"] = "invalid";
            ViewBag.Current = "Przedmioty";
            ViewBag.ListaGrup = _repo.GetAllGroups;
            return View(przedmiot);
        }

        //
        // GET: /Admin/Przedmioty/Edit/5

        public ActionResult Edit(int id)
        {
            _logger.Info("PrzedmiotyController.Edit => HTTP GET");
            ViewBag.Current = "Students";
            Przedmioty przedmiot = _repo1.GetPrzedmiotByID(id);
            return View(przedmiot);
        }

        //
        // POST: /Admin/Przedmioty/Edit/5

        [HttpPost]
        public ActionResult Edit(Przedmioty przedmiot)
        {
            _logger.Info("PrzedmiotyController.Edit => Entering| HTTP POST");
            if (ModelState.IsValid && !_repo1.PrzedmiotExists(przedmiot))
            {
                _repo1.EditPrzedmiot(przedmiot);
                _repo1.Save();
                _logger.Info("StudenciController.Edit => SUCCES = Edit Przedmioty| HTTP POST");
                TempData["message"] = "Zauktalizowano przedmiot!";
                TempData["status"] = "valid";
                return RedirectToAction("List");
            }
            _logger.Info("PrzedmiotyController.Edit => FAILED = Edit Przedmioty| HTTP POST");
           
            TempData["message"] = "Nie udało się uaktualnić przedmiotu!";
            TempData["status"] = "invalid";
            ViewBag.Current = "Przedmioty";
            return View(przedmiot);
        }

        //
        // GET: /Admin/Przedmioty/Delete/5

        public ActionResult Delete(int id)
        {
            _logger.Info("PrzedmiotyController.Delete => HTTP GET");
            ViewBag.Current = "Przedmioty";
            Przedmioty przedmiot = _repo1.GetPrzedmiotByID(id);
            return View(przedmiot);
        }

        //
        // POST: /Admin/Przedmioty/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _logger.Info("PrzedmiotyController.Delete => SUCCES = Delete Przedmioty| HTTP POST");
            Przedmioty przedmiot = (Przedmioty)_repo1.GetPrzedmiotByID(id);
            _repo1.DeletePrzedmiot(przedmiot);
            _repo1.Save();
            TempData["message"] = "Usunięto przedmiot!";
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