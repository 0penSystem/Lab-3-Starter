using Nile.Data;
using Nile.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nile.MVC.Controllers
{
    public class ProductsController : Controller
    {
        Database _database;


        public ProductsController(Database database)
        {
            if (database == null)
            {
                _database = new Database("NileDatabase");

            }
            else
            {
                _database = database;
            }
        }


        public ProductsController() : this(null) { }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                _database.Dispose();
            }
        }

        // GET: Products
        public ActionResult Index()
        {
            var products = _database.Products.GetAll();
            List<ProductModel> models = new List<ProductModel>();

            foreach (Product p in products)
            {
                models.Add(new ProductModel(p));
            }

            models.Sort((m1, m2) => m1.Name.CompareTo(m2.Name));


            return View(models);
        }

        [HttpPost]
        public ActionResult Edit(ProductModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                try
                {
                    _database.Products.Update(model.toProduct());
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.ToString());
                    return View(model);
                }
            }
        }


        public ActionResult Edit(int id)
        {
            try
            {
                var product = _database.Products.Get(id);
                return View(new ProductModel(product));
            }
            catch (Exception e)
            {
                return HttpNotFound();
            }

        }



        public ActionResult Create()
        {
            return View(new ProductModel());
        }


        [HttpPost]
        public ActionResult Create(ProductModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                try
                {
                    _database.Products.Add(model.toProduct());
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.ToString());
                    return View(model);
                }
            }
        }

    }
}