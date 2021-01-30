using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PowerOfGod.Business.ShoppingLogic;
using PowerOfGod.Domain.Context;
using PowerOfGod.Domain.Entity.Shopping;

namespace PowerOfGod.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        Category_Service Category_Service = new Category_Service();
        Department_Service department_Service = new Department_Service();
        public CategoriesController()
        {

        }
        public ActionResult Index()
        {
            return View(Category_Service.allCategories());
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
                return RedirectToAction("Bad_Request", "Error");
            if (Category_Service.findCategory_by_id(id) != null)
                return View(Category_Service.findCategory_by_id(id));
            else
                return RedirectToAction("Not_Found", "Error");
        }
        public ActionResult Create()
        {
            ViewBag.Department_ID = new SelectList(department_Service.allDepartments(), "Department_ID", "Department_Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category model)
        {
            ViewBag.Department_ID = new SelectList(department_Service.allDepartments(), "Department_ID", "Department_Name");
            if (ModelState.IsValid)
            {
                Category_Service.addCategory(model);
                return RedirectToAction("Index");
            }

            return View(model);
        }
        public ActionResult Edit(int? id)
        {
            ViewBag.Department_ID = new SelectList(department_Service.allDepartments(), "Department_ID", "Department_Name");
            if (id == null)
                return RedirectToAction("Bad_Request", "Error");
            if (Category_Service.findCategory_by_id(id) != null)
                return View(Category_Service.findCategory_by_id(id));
            else
                return RedirectToAction("Not_Found", "Error");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category model)
        {
            if (ModelState.IsValid)
            {
                Category_Service.editCategory(model);
                return RedirectToAction("Index");
            }
            ViewBag.Department_ID = new SelectList(department_Service.allDepartments(), "Department_ID", "Department_Name");
            return View(model);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return RedirectToAction("Bad_Request", "Error");
            if (Category_Service.findCategory_by_id(id) != null)
                return View(Category_Service.findCategory_by_id(id));
            else
                return RedirectToAction("Not_Found", "Error");
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category_Service.deleteCategory(Category_Service.findCategory_by_id(id));
            return RedirectToAction("Index");
        }
    }
}
