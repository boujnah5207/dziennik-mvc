using System;
using System.Linq;
using System.Web.Mvc;
using Dziennik_MVC.Infrastructure.Logging;
using Dziennik_MVC.Models.Data.Abstract;
using Dziennik_MVC.Models.Entities;
using PagedList;
using Dziennik_MVC.Areas.Admin.ViewModels;
using System.Collections.Generic;

namespace Dziennik_MVC.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class GrupyController : Controller
    {
        private IGrupyRepository _repo;
        private IUsersRepository _repo1;
        private IPrzedmiotyRepository _repo2;
        private readonly ILogger _logger;

        public GrupyController(IGrupyRepository repo, IUsersRepository repo1, IPrzedmiotyRepository repo2, ILogger logger) 
        {
            _repo = repo;
            _repo1 = repo1;
            _repo2 = repo2;
            _logger = logger;
        }

        public ActionResult PrzypiszPrzedmioty()
        {
            ViewBag.Current = "Grupy";    // Aktualne zaznaczenie zakladki Profil w Menu 

            PrzedmiotyGrupyViewModel model = new PrzedmiotyGrupyViewModel { AvailablePrzedmioty = _repo2.GetAllPrzedmioty.ToList(), RequestedPrzedmioty = new List<Przedmioty>() };
            return View(model);
        }

        [HttpPost]
        public ActionResult PrzypiszPrzedmioty(PrzedmiotyGrupyViewModel model, string add, string remove, string send, int id)
        {
            ViewBag.Current = "Grupy";    // Aktualne zaznaczenie zakladki Profil w Menu 

            //Need to clear model state or it will interfere with the updated model
            ModelState.Clear();
            RestoreSavedState(model);
            if (!string.IsNullOrEmpty(add))
                AddProducts(model);
            else if (!string.IsNullOrEmpty(remove))
                RemoveProducts(model);
            else if (!string.IsNullOrEmpty(send))
            {
                Validate(model);
                if (ModelState.IsValid)
                {
                    var grupa = _repo.GetGroupByID(id);
                    foreach(Przedmioty przedmiot in model.RequestedPrzedmioty){
                        grupa.Przedmioty.Add(przedmiot);
                    }
                    _repo.EditGroup(grupa);
                    _repo.Save();
                    _logger.Info("GrupyController.Edit => SUCCES = Edit Semester| HTTP POST");
                    TempData["message"] = "Zauktalizowano grupę!";
                    TempData["status"] = "valid";
                    return RedirectToAction("List");
                }
                //todo: implement SendListToSanta method...
            }
            SaveState(model);
            return View(model);
        }

        private void Validate(PrzedmiotyGrupyViewModel model)
        {           
            if (string.IsNullOrEmpty(model.SavedRequested))
                ModelState.AddModelError("", "Nie wybrałeś żadnych przedmiotów!");
        }

        void SaveState(PrzedmiotyGrupyViewModel model)
        {
            //create comma delimited list of product ids
            model.SavedRequested = string.Join(",", model.RequestedPrzedmioty.Select(p => p.id_przedmiotu.ToString()).ToArray());

            //Available products = All - Requested
            var wszystkiePrzedmioty = _repo2.GetAllPrzedmioty.ToList();
            var zaznaczonePrzedmioty = model.RequestedPrzedmioty.ToList();

            var bezZaznaczonego = wszystkiePrzedmioty.Except(zaznaczonePrzedmioty).ToList();

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

        public ViewResult List(string sortOrder, int? page)
        {
            _logger.Info("GrupyController.List => HTTP POST");
            ViewBag.Current = "Grupy";    // Aktualne zaznaczenie zakladki Profil w Menu 
            ViewBag.CurrentSort = sortOrder;    // Zachowanie sortowania pomiędzy przejściami stron

            ViewBag.IDGrupySortParm = sortOrder == "ID Grupy asc" ? "ID Grupy desc" : "ID Grupy asc";
            ViewBag.NameSortParm = sortOrder == "Name asc" ? "Name desc" : "Name asc";

            var grupy = _repo.GetAllGroups;

            switch (sortOrder)
            {
                case "ID Grupy desc":
                    grupy = grupy.OrderByDescending(s => s.id_grupy);
                    break;
                case "ID Grupy asc":
                    grupy = grupy.OrderBy(s => s.id_grupy);
                    break;              
                case "Name desc":
                    grupy = grupy.OrderByDescending(s => s.nazwa_grupy);
                    break;
                case "Name asc":
                    grupy = grupy.OrderBy(s => s.nazwa_grupy);
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
            return View();
        } 

        [HttpPost]
        public ActionResult Add(Grupy grupa)
        {
            _logger.Info("GrupyController.Add => Entering | HTTP POST");
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
                ViewBag.Current = "Grupy";    
        
            return View(grupa);
        }
 
        public ActionResult Edit(int id)
        {
            _logger.Info("GrupyController.Edit => HTTP GET");
            ViewBag.Current = "Grupy";
            Grupy grupa = _repo.GetGroupByID(id);
            return View(grupa);
        }

        [HttpPost]
        public ActionResult Edit(Grupy grupa)
        {
            _logger.Info("GrupyController.Edit => Entering| HTTP POST");
            
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
            try
            {
                Grupy grupa = _repo.GetGroupByID(id);
                _repo.DeleteGroup(grupa);
                _repo.Save();
                TempData["message"] = "Usunięto grupę!";
                TempData["status"] = "valid";
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                TempData["message"] = "Nie można usunąć grupy. W grupie znajdują się studenci !!!";
                TempData["status"] = "invalid";
                return RedirectToAction("List");
            }           
        }

        protected override void Dispose(bool disposing)
        {
            _repo.Dispose();
            base.Dispose(disposing);
        }
    }
}