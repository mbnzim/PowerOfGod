using PowerOfGod.Business.ShoppingLogic;
using PowerOfGod.Domain.Context;
using PowerOfGod.Domain.Entity.Shopping;
using PowerOfGod.ViewModel.ShoppingViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;


namespace PowerOfGod.Web.Controllers.Shopping
{
    //[RoutePrefix("api/apiCintroller")]
    public class apiCategoriesController : ApiController
    {
        private ApplicationDbContext db;
        Category_Service category_Service;

        public apiCategoriesController()
        {
            this.db = new ApplicationDbContext();
            this.category_Service = new Category_Service();
        }
        // GET: api/apiCategories
        public IEnumerable<CategoryModel> GetCategories(int id)
        {
            return category_Service.allCategories().Where(p => p.Department_ID == id).Select(x => new CategoryModel()
            {
                Category_ID = x.Category_ID,
                Name = x.Name,
                Department_ID = x.Department_ID
            });
        }

        //// GET: api/apiCategories/5
        //[ResponseType(typeof(Category))]
        //public IHttpActionResult GetCategory(int id)
        //{
        //    Category category = db.Categories.Find(id);
        //    if (category == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(category);
        //}

        // PUT: api/apiCategories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCategory(int id, Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != category.Category_ID)
            {
                return BadRequest();
            }

            db.Entry(category).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/apiCategories
        [ResponseType(typeof(Category))]
        public IHttpActionResult PostCategory(Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Categories.Add(category);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = category.Category_ID }, category);
        }

        // DELETE: api/apiCategories/5
        [ResponseType(typeof(Category))]
        public IHttpActionResult DeleteCategory(int id)
        {
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            db.Categories.Remove(category);
            db.SaveChanges();

            return Ok(category);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CategoryExists(int id)
        {
            return db.Categories.Count(e => e.Category_ID == id) > 0;
        }
    }
}