using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portal.Models.EmOpov;
using Portal.Models.GU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Controllers
{
    public class EmOpovController : Controller
    {
        IEmOpovRepository repo;
        public EmOpovController(IEmOpovRepository r)
        {
            repo = r;
        }
        [Authorize(Roles = "GU2V")]
        public ActionResult EmOpov()
        {
            return View(repo.GetEmOpov());
        }

        public ActionResult Details(int id)
        {
            EmOpov emOpov = repo.Get(id);
            if (emOpov != null)
                return View(emOpov);
            return NotFound();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(EmOpov emOpov)
        {
            repo.Create(emOpov);
            return RedirectToAction("EmOpov");
        }

        public ActionResult Edit(int id)
        {
            EmOpov emOpov = repo.Get(id);
            if (emOpov != null)
                return View(emOpov);
            return NotFound();
        }

        [HttpPost]
        public ActionResult Edit(EmOpov emOpov)
        {
            repo.Update(emOpov);
            return RedirectToAction("EmOpov");
        }

        [HttpGet]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            EmOpov emOpov = repo.Get(id);
            if (emOpov != null)
                return View(emOpov);
            return NotFound();
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            repo.Delete(id);
            return RedirectToAction("EmOpov");
        }
    }
}
