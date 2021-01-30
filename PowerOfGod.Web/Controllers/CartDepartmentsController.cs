using PowerOfGod.Business.ShoppingLogic;
using PowerOfGod.Domain.Entity.Shopping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PowerOfGod.Web.Controllers
{
    public class CartDepartmentsController : Controller
    {
        Department_Service department_Service;

        public CartDepartmentsController()
        {
            this.department_Service = new Department_Service();
        }

        public ActionResult Index()
        {
            return View(department_Service.allDepartments());
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
                return RedirectToAction("Bad_Request", "Error");
            if (department_Service.findDepartment_by_id(id) != null)
                return View(department_Service.findDepartment_by_id(id));
            else
                return RedirectToAction("Not_Found", "Error");
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CartDepartment model)
        {
            if (ModelState.IsValid)
            {
                department_Service.addDepartment(model);
                return RedirectToAction("Index");
            }

            return View(model);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return RedirectToAction("Bad_Request", "Error");
            if (department_Service.findDepartment_by_id(id) != null)
                return View(department_Service.findDepartment_by_id(id));
            else
                return RedirectToAction("Not_Found", "Error");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CartDepartment model)
        {
            if (ModelState.IsValid)
            {
                department_Service.editDepartment(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return RedirectToAction("Bad_Request", "Error");
            if (department_Service.findDepartment_by_id(id) != null)
                return View(department_Service.findDepartment_by_id(id));
            else
                return RedirectToAction("Not_Found", "Error");
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            department_Service.deleteDepartment(department_Service.findDepartment_by_id(id));
            return RedirectToAction("Index");
        }
    }
}