using Microsoft.AspNetCore.Mvc;
using Tera_Web.Entities;
using Tera_Web.Models;

namespace Tera_Web.Controllers
{
    public class SiteController : Controller
    {
        SiteObj siteObj = new SiteObj();
        SiteModel siteModel = new SiteModel();

        // GET: Site/List
        public ActionResult List()
        {
            var siteList = siteModel.GetSiteList();
            return View(siteList);
        }

        // GET: Site/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: Site/Register
        [HttpPost]
        public ActionResult Register(SiteObj siteObj)
        {
            if (ModelState.IsValid)
            {
                siteModel.Register(siteObj);
                return RedirectToAction("List", "Site");
            }
            else
            {
                return View(siteObj);
            }
        }

        // GET: Site/Edit/5
        public ActionResult EditSite(int id)
        {
            var site = siteModel.GetSite(id);
            if (site == null)
            {
                return NotFound();
            }
            return View(site);
        }

        // POST: Site/Edit/5
        [HttpPost]
        public ActionResult EditSite(int id, SiteObj siteObj)
        {
            if (ModelState.IsValid)
            {
                siteModel.EditSide(siteObj);
                return RedirectToAction("List", "Site");
            }
            else
            {
                return View(siteObj);
            }
        }

        // GET: Site/Delete/5
        public ActionResult Delete(int id)
        {
            var site = siteModel.GetSite(id);
            if (site == null)
            {
                return NotFound();
            }
            return View(site);
        }

        // POST: Site/Delete/5
        [HttpPost]

        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                siteModel.DeleteSite(id);
                return RedirectToAction(nameof(List));
            }
            catch
            {
                return View();
            }
        }
    }
}
