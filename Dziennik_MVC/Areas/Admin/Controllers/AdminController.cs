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

namespace Dziennik_MVC.Areas.Admin.Controllers
{ 
    public class AdminController : Controller
    {
        private IUzytkownicyRepository _repo;

        public AdminController(IUzytkownicyRepository repo) 
        {
            _repo = repo;
        }

        [Authorize(Roles = "Admin,Wykladowca")]  // Akcja odpowiedzialna za wyświetlenie profilu Admina/Wykladowcy
        public ViewResult Profile(string login ) {
            
            ViewBag.Current = "Profile";    // Aktyalne zaznaczenie zakladki Profil w Menu 

            var user = _repo.GetUser(login);

            return View(user);
        }

        public ViewResult Index()
        {
           // var uzytkownicy = db.Uzytkownicy.Include(u => u.Uprawnienia);
            return View();
        }

        //
        // GET: /Admin/Admin/Details/5

        public ViewResult Details(int id)
        {
           // Uzytkownicy uzytkownicy = db.Uzytkownicy.Find(id);
            return View();
        }

        //
        // GET: /Admin/Admin/Create

        public ActionResult Create()
        {
           // ViewBag.ID_uprawnienia = new SelectList(db.Uprawnienia, "ID_uprawnienia", "Nazwa_uprawnienia");
            return View();
        } 

        //
        // POST: /Admin/Admin/Create

        [HttpPost]
        public ActionResult Create(Uzytkownicy uzytkownicy)
        {
            if (ModelState.IsValid)
            {
              //  db.Uzytkownicy.Add(uzytkownicy);
              //  db.SaveChanges();
                return RedirectToAction("Index");  
            }

           // ViewBag.ID_uprawnienia = new SelectList(db.Uprawnienia, "ID_uprawnienia", "Nazwa_uprawnienia", uzytkownicy.ID_uprawnienia);
            return View(uzytkownicy);
        }
        
        //
        // GET: /Admin/Admin/Edit/5
 
        public ActionResult Edit(int id)
        {
            //Uzytkownicy uzytkownicy = db.Uzytkownicy.Find(id);
           // ViewBag.ID_uprawnienia = new SelectList(db.Uprawnienia, "ID_uprawnienia", "Nazwa_uprawnienia", uzytkownicy.ID_uprawnienia);
            return View();
        }

        //
        // POST: /Admin/Admin/Edit/5

        [HttpPost]
        public ActionResult Edit(Uzytkownicy uzytkownicy)
        {
            if (ModelState.IsValid)
            {
               // db.Entry(uzytkownicy).State = EntityState.Modified;
              //  db.SaveChanges();
                return RedirectToAction("Index");
            }
           // ViewBag.ID_uprawnienia = new SelectList(db.Uprawnienia, "ID_uprawnienia", "Nazwa_uprawnienia", uzytkownicy.ID_uprawnienia);
            return View();
        }

        //
        // GET: /Admin/Admin/Delete/5
 
        public ActionResult Delete(int id)
        {
          //  Uzytkownicy uzytkownicy = db.Uzytkownicy.Find(id);
            return View();
        }

        //
        // POST: /Admin/Admin/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
           // Uzytkownicy uzytkownicy = db.Uzytkownicy.Find(id);
          //  db.Uzytkownicy.Remove(uzytkownicy);
          //  db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            _repo.Dispose();
            base.Dispose(disposing);
        }
    }
}