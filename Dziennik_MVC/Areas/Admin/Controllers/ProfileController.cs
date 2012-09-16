using System.Web.Mvc;
using Dziennik_MVC.Models.Data.Abstract;
using Dziennik_MVC.Models.Entities;

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
            var user = _repo.GetProwadzacyByName(User.Identity.Name);

            return View(user);
        }
       
        //
        // GET: /Admin/Edit/1
 
        public ViewResult Edit(int id)
        {
            Prowadzacy user = _repo.GetProwadzacyById(id);
            ViewBag.Current = "Profile";  // Aktualne zaznaczenie zakladki Profil w Menu            

            return View(user);
        }

        //
        // POST: /Profile/Edit/1

        [HttpPost]
        public ActionResult Edit(Prowadzacy user )
        {
           // var admin = user as Admins;
            if (ModelState.IsValid)
            {
                _repo.EditProwadzacy(user);
                _repo.Save();
                TempData["message"] = "Zauktalizowano twój profil!";                     // wiadomość w _AdminLayout
                return RedirectToRoute("Admin_default", new { action = "Profile" });
            }
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