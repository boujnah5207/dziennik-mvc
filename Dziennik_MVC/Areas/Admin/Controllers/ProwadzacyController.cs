using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using Dziennik_MVC.Areas.Admin.ViewModels;
using Dziennik_MVC.Infrastructure.Logging;
using Dziennik_MVC.Models.Data.Abstract;
using Dziennik_MVC.Models.Entities;
using PagedList;

namespace Dziennik_MVC.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProwadzacyController : Controller
    {
        private IGrupyRepository _repo;
        private IUsersRepository _repo1;
        private IPrzedmiotyRepository _repo2;
        private readonly ILogger _logger;

        public ProwadzacyController(IGrupyRepository repo, IUsersRepository repo1, IPrzedmiotyRepository repo2, ILogger logger) 
        {
            _repo = repo;
            _repo1 = repo1;
            _repo2 = repo2;
            _logger = logger;
        }

        public ActionResult PrzypiszPrzedmioty(int id)
        {
            ViewBag.Current = "Prowadzacy";    // Aktualne zaznaczenie zakladki Profil w Menu 

            var prowadzacy = _repo1.GetProwadzacyById(id);

            if (prowadzacy != null)
            {
                var przedmiotyPrzypisane = prowadzacy.Przedmioty.ToList();
                var wszystkieNiePrzypisanePrzedmioty = _repo2.GetPrzedmiotyNiePrzypisane.ToList();
                var mozliweDoWyboru = wszystkieNiePrzypisanePrzedmioty.Except(przedmiotyPrzypisane).ToList();

                PrzedmiotyGrupyViewModel model = new PrzedmiotyGrupyViewModel { AvailablePrzedmioty = mozliweDoWyboru, RequestedPrzedmioty = przedmiotyPrzypisane };
                model.SavedRequested = string.Join(",", model.RequestedPrzedmioty.Select(p => p.id_przedmiotu.ToString()).ToArray());
                return View(model);
            }
            else
            {
                TempData["message"] = "Taki prowadzacy nie istnieje!";
                TempData["status"] = "invalid";
                return View();
            }
        }

        [HttpPost]
        public ActionResult PrzypiszPrzedmioty(PrzedmiotyGrupyViewModel model, string add, string remove, string send, int id)
        {
            ViewBag.Current = "Prowadzacy";    // Aktualne zaznaczenie zakladki Profil w Menu 

            //Need to clear model state or it will interfere with the updated model
            ModelState.Clear();
            RestoreSavedState(model);
            if (!string.IsNullOrEmpty(add))
                AddProducts(model);
            else if (!string.IsNullOrEmpty(remove))
                RemoveProducts(model);
            else if (!string.IsNullOrEmpty(send))
            {

                if (ModelState.IsValid)
                {
                    var prowadzacy = _repo1.GetProwadzacyById(id);
                    prowadzacy.Przedmioty.Clear();
                    foreach (Przedmioty przedmiot in model.RequestedPrzedmioty)
                    {
                        prowadzacy.Przedmioty.Add(przedmiot);
                    }
                    _repo1.EditProwadzacy(prowadzacy);
                    _repo1.Save();
                    _logger.Info("ProwadzacyController.Edit => SUCCES = Edit PRZYPISZPRZEMDIOTY| HTTP POST");
                    TempData["message"] = "Zauktalizowano prowadzącego!";
                    TempData["status"] = "valid";
                    return RedirectToAction("List");
                }
                //todo: implement SendListToSanta method...
            }
            SaveState(model);
            return View(model);
        }

        void SaveState(PrzedmiotyGrupyViewModel model)
        {
            //create comma delimited list of product ids
            model.SavedRequested = string.Join(",", model.RequestedPrzedmioty.Select(p => p.id_przedmiotu.ToString()).ToArray());

            //Available products = All - Requested
            var wszystkieNiePrzypisanePrzedmioty = _repo2.GetPrzedmiotyNiePrzypisane.ToList();
            var zaznaczonePrzedmioty = model.RequestedPrzedmioty.ToList();

            var bezZaznaczonego = wszystkieNiePrzypisanePrzedmioty.Except(zaznaczonePrzedmioty).ToList();

            //var przedmioty = .Except(.AsQueryable()).ToList();
            model.AvailablePrzedmioty = bezZaznaczonego;
        }

        void RemoveProducts(PrzedmiotyGrupyViewModel model)
        {
            if (model.RequestedSelected != null)
            {
                model.RequestedPrzedmioty.RemoveAll(p => model.RequestedSelected.Contains(p.id_przedmiotu));
                model.RequestedSelected = null;
            }
        }

        void AddProducts(PrzedmiotyGrupyViewModel model)
        {
            if (model.AvailableSelected != null)
            {
                var prods = _repo2.GetAllPrzedmioty.Where(p => model.AvailableSelected.Contains(p.id_przedmiotu));
                model.RequestedPrzedmioty.AddRange(prods);
                model.AvailableSelected = null;
            }
        }

        void RestoreSavedState(PrzedmiotyGrupyViewModel model)
        {
            model.RequestedPrzedmioty = new List<Przedmioty>();

            //get the previously stored items
            if (!string.IsNullOrEmpty(model.SavedRequested))
            {
                int[] prodids = model.SavedRequested.Split(',').Select(s => int.Parse(s)).ToArray();
                var prods = _repo2.GetAllPrzedmioty.Where(p => prodids.Contains(p.id_przedmiotu));
                model.RequestedPrzedmioty.AddRange(prods);
            }
        }

        //
        // GET: /Admin/Prowadzacy/

        public ViewResult List(string sortOrder, int? page)
        {
            _logger.Info("ProwadzacyController.List => HTTP POST");
            ViewBag.Current = "Prowadzacy";    // Aktualne zaznaczenie zakladki Profil w Menu 
            ViewBag.CurrentSort = sortOrder;    // Zachowanie sortowania pomiędzy przejściami stron

            ViewBag.IDProwadzacegoSortParm = sortOrder == "ID Prowadzacy asc" ? "ID Prowadzacy desc" : "ID Prowadzacy asc";
            ViewBag.NameSortParm = sortOrder == "Name asc" ? "Name desc" : "Name asc";
            ViewBag.LastNameSortParm = sortOrder == "LastName asc" ? "LastName desc" : "LastName asc";
            ViewBag.AdminParm = sortOrder == "Admin asc" ? "Admin desc" : "Admin asc";

            var prowadzacy = _repo1.GetAllProwadzacy;

            switch (sortOrder)
            {
                case "ID Prowadzacy desc":
                    prowadzacy = prowadzacy.OrderByDescending(s => s.id_prowadzacego);
                    break;
                case "ID Prowadzacy asc":
                    prowadzacy = prowadzacy.OrderBy(s => s.id_prowadzacego);
                    break;
                case "Name desc":
                    prowadzacy = prowadzacy.OrderByDescending(s => s.imie);
                    break;
                case "Name asc":
                    prowadzacy = prowadzacy.OrderBy(s => s.imie);
                    break;
                case "LastName desc":
                    prowadzacy = prowadzacy.OrderByDescending(s => s.nazwisko);
                    break;
                case "LastName asc":
                    prowadzacy = prowadzacy.OrderBy(s => s.nazwisko);
                    break;
                case "Admin desc":
                    prowadzacy = prowadzacy.OrderByDescending(s => s.admin);
                    break;
                case "Admin asc":
                    prowadzacy = prowadzacy.OrderBy(s => s.admin);
                    break;
                default:
                    prowadzacy = prowadzacy.OrderBy(s => s.id_prowadzacego);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1); // Jeśli page == null to page =1

            return View(prowadzacy.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /Admin/Prowadzacy/Add

        public ActionResult Add()
        {
            _logger.Info("ProwadzacyController.Add => HTTP GET");
            ViewBag.Current = "Prowadzacy";
            return View();
        }

        //
        // POST: /Admin/Prowadzacy/Add

        [HttpPost]
        public ActionResult Add(Prowadzacy prowadzacy)
        {
            _logger.Info("ProwadzacyController.Add => Entering | HTTP POST");
            if (!_repo1.ProwadzacyExists(prowadzacy))
                if (ModelState.IsValid)
                {
                    prowadzacy.haslo = FormsAuthentication.HashPasswordForStoringInConfigFile(prowadzacy.haslo.Trim(), "md5");
                    _repo1.AddProwadzacy(prowadzacy);
                    _repo1.Save();
                    _logger.Info("ProwadzacyController.Add => SUCCES = Add Prowadzacy| HTTP POST");
                    TempData["message"] = "Pomyślnie dodano nowego prowadzącego!";
                    TempData["status"] = "valid";
                    return RedirectToAction("List");
                }
            _logger.Info("ProwadzacyController.Add => FAILED = Add Prowadzacy | HTTP POST");
            TempData["message"] = "Nie udało się dodać prowadzacego! Taki prowadzacy już istnieje!";
            TempData["status"] = "invalid";
            ViewBag.Current = "Prowadzacy";
            return View(prowadzacy);
        }

        //
        // GET: /Admin/Prowadzacy/Edit/5

        public ActionResult Edit(int id)
        {
            _logger.Info("ProwadzacyController.Edit => HTTP GET");
            ViewBag.Current = "Prowadzacy";
            Prowadzacy prowadzacy = _repo1.GetProwadzacyById(id);
            return View(prowadzacy);
        }

        //
        // POST: /Admin/Prowadzacy/Edit/5

        [HttpPost]
        public ActionResult Edit(Prowadzacy prowadzacy)
        {
            _logger.Info("ProwadzacyController.Edit => Entering| HTTP POST");
            if (ModelState.IsValid)
            {
                _repo1.EditProwadzacy(prowadzacy);
                _repo1.Save();
                _logger.Info("ProwadzacyController.Edit => SUCCES = Edit Prowadzacy| HTTP POST");
                TempData["message"] = "Zauktalizowano prowadzącego!";
                TempData["status"] = "valid";
                return RedirectToAction("List");
            }
            _logger.Info("ProwadzacyController.Edit => FAILED = Edit Prowadzacy| HTTP POST");
           
            TempData["message"] = "Nie udało się uaktualnić prowadzącego! Taki prowadzący już istnieje!";
            TempData["status"] = "invalid";
            ViewBag.Current = "Prowadzacy";
            return View(prowadzacy);
        }

        //
        // GET: /Admin/Prowadzacy/Delete/5

        public ActionResult Delete(int id)
        {
            _logger.Info("ProwadzacyController.Delete => HTTP GET");
            ViewBag.Current = "Prowadzacy";
            Prowadzacy prowadzacy = _repo1.GetProwadzacyById(id);
            return View(prowadzacy);
        }

        //
        // POST: /Admin/Prowadzacy/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _logger.Info("ProwadzacyController.Delete => SUCCES = Delete Prowadzacy| HTTP POST");
            Prowadzacy prowadzacy = (Prowadzacy)_repo1.GetProwadzacyById(id);
            _repo1.DeleteProwadzacy(prowadzacy);
            _repo1.Save();
            TempData["message"] = "Usunięto Prowadzącego!";
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