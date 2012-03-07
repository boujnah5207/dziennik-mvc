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
    public class ProfileController : Controller
    {
        private IUsersRepository _repo;

        public ProfileController(IUsersRepository repo) 
        {
            _repo = repo;
        }

        //
        // GET: Admin/Profile

        [Authorize(Roles = "Admin")] 
        public ViewResult Profile() {
            
            ViewBag.Current = "Profile";    // Aktualne zaznaczenie zakladki Profil w Menu 

            var user = _repo.GetUser(User.Identity.Name) as Admins;

            return View(user);
        }
       
        //
        // GET: /Admin/Edit/1
 
        public ViewResult Edit(int id)
        {
            Admins user = _repo.GetUser(id) as Admins;

            ViewBag.ListaUprawnien = new SelectList(_repo.GetAllRoles, "RoleID", "RoleName", user.UserID); // Lista Uprawnien
            ViewBag.Current = "Profile";  // Aktualne zaznaczenie zakladki Profil w Menu 
           

            return View(user);
        }

        //
        // POST: /Profile/Edit/1

        [HttpPost]
        public ActionResult Edit(Admins user )
        {
           // var admin = user as Admins;
            if (ModelState.IsValid)
            {
                _repo.EditUser(user);
                _repo.Save();
                TempData["message"] = "Zauktalizowano twój profil!";                     // wiadomość w _AdminLayout
                return RedirectToRoute("Admin_default", new { action = "Profile" });
            }
            ViewBag.ListaUprawnien = new SelectList(_repo.GetAllRoles, "RoleID", "RoleName", user.UserID);      // Lista
            TempData["message"] = "Nie udało się zaktualizować twojego profilu!";                                   // wiadomość w _AdminLayout
            return View(user);
        }
               
        protected override void Dispose(bool disposing)
        {
            _repo.Dispose();
            base.Dispose(disposing);
        }
    }
}