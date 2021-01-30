using PowerOfGod.Business.ShoppingLogic;
using PowerOfGod.Domain.Context;
using PowerOfGod.Domain.Entity.Shopping;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PowerOfGod.Web.Controllers
{
    public class ItemsController : Controller
    {
        private ApplicationDbContext db;
        private Item_Service item;
        Category_Service category_Service;
        public string shoppingCartID { get; set; }
        public const string CartSessionKey = "CartId";
        public ItemsController()
        {
            this.db = new ApplicationDbContext();
            this.item = new Item_Service();
            this.category_Service = new Category_Service();
        }
        public ActionResult Index()
        {
            return View(item.allItems());
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
                return RedirectToAction("Bad_Request", "Error");
            if (item.findItem_by_id(id) != null)
                return View(item.findItem_by_id(id));
            else
                return RedirectToAction("Not_Found", "Error");
        }
        public ActionResult Create()
        {
            ViewBag.Category_ID = new SelectList(category_Service.allCategories(), "Category_ID", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Item model, HttpPostedFileBase img_upload)
        {
            //var itm = db.Items.Where(x=>x.QuantityInStock==model.QuantityInStock);
            ViewBag.Category_ID = new SelectList(category_Service.allCategories(), "Category_ID", "Name");
            byte[] data = null;
            data = new byte[img_upload.ContentLength];
            img_upload.InputStream.Read(data, 0, img_upload.ContentLength);
            model.Picture = data;
            // itm= model.QuantityInStock  ;
            if (ModelState.IsValid)
            {
                item.addItem(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult Edit(int? id)
        {
            ViewBag.Category_ID = new SelectList(category_Service.allCategories(), "Category_ID", "Name");
            if (id == null)
                return RedirectToAction("Bad_Request", "Error");
            if (item.findItem_by_id(id) != null)
                return View(item.findItem_by_id(id));
            else
                return RedirectToAction("Not_Found", "Error");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Item model, HttpPostedFileBase img_upload)
        {
            byte[] data = null;
            data = new byte[img_upload.ContentLength];
            img_upload.InputStream.Read(data, 0, img_upload.ContentLength);
            model.Picture = data;
            if (ModelState.IsValid)
            {
                item.editItem(model);
                return RedirectToAction("Index");
            }
            ViewBag.Category_ID = new SelectList(category_Service.allCategories(), "Category_ID", "Name");
            return View(model);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return RedirectToAction("Bad_Request", "Error");
            if (item.findItem_by_id(id) != null)
                return View(item.findItem_by_id(id));
            else
                return RedirectToAction("Not_Found", "Error");
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            item.deleteItem(item.findItem_by_id(id));
            return RedirectToAction("Index");
        }


        public ActionResult Fall_catalog()
        {
            return View(item.allItems());
        }
       
        public string GetCartID()
        {
            if (System.Web.HttpContext.Current.Session[CartSessionKey] == null)
            {
                if (!String.IsNullOrWhiteSpace(System.Web.HttpContext.Current.User.Identity.Name))
                {
                    System.Web.HttpContext.Current.Session[CartSessionKey] = System.Web.HttpContext.Current.User.Identity.Name;
                }
                else
                {
                    Guid temp_cart_ID = Guid.NewGuid();
                    System.Web.HttpContext.Current.Session[CartSessionKey] = temp_cart_ID.ToString();
                }
            }
            return System.Web.HttpContext.Current.Session[CartSessionKey].ToString();
        }
    }
}
