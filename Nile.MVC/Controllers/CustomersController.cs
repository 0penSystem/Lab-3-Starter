using Nile.Data;
using Nile.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nile.MVC.Controllers
{
    public class CustomersController : Controller
    {

        Database _database;


        public CustomersController(Database database)
        {
            if(database == null)
            {
                _database = new Database("NileDatabase");

            }
            else
            {
                _database = database;
            }
        }


        public CustomersController() : this(null) { }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                _database.Dispose();
            }
        }



        // GET: Customers
        public ActionResult Index()
        {
            var customers = _database.Customers.GetAll();
            List<CustomerModel> models = new List<CustomerModel>();

            foreach (Customer c in customers)
            {
                models.Add(new CustomerModel(c));
            }

            models.Sort((m1, m2) => m1.FullName.CompareTo(m2.FullName));


            return View(models);
        }

        public ActionResult Edit(int id)
        {
            try
            {
                var customer = _database.Customers.Get(id);

                return View(new CustomerModel(customer));

            }
            catch (ArgumentOutOfRangeException e)
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        public ActionResult Edit(CustomerModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                var customer = model.toCustomer();
                try
                {
                    _database.Customers.Update(customer);
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.ToString());
                    return View(model);
                }
            }
        }

        public ActionResult Create()
        {
            return View(new CustomerModel());
        }

        [HttpPost]
        public ActionResult Create(CustomerModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                var customer = model.toCustomer();
                try
                {
                    _database.Customers.Add(customer);
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