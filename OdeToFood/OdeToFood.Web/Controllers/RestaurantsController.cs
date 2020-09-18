using OdeToFood.Data.Models;
using OdeToFood.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OdeToFood.Web.Controllers
{
    public class RestaurantsController : Controller
    {
        private readonly IRestaurantData db;
        // inject something that implements IRestaurant data to avoid low-level knowledge of specific components like InMemoryRstaurantData - 
        //IoC container used to build and analyze objects like HomeController-what dependency is required, what to inject from ctor
        //IoC container instantiate a HomeController, then configure container so that it knows what to do.

        public RestaurantsController(IRestaurantData db)
        {
            this.db = db;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var model = db.GetAll();
            return View(model);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var model = db.Get(id);
            if(model == null)
            {
                return View("NotFound");
            }
            return View(model);
        }

        // one action that returns a view which contains the form user can type into
        [HttpGet]
        public ActionResult Create() 
        {
            return View();
        }

        // action that receives restaurant information back from user
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Restaurant restaurant)
        { // receive user input into restaurant object that MVC framework will create using model 
          // binding then save restaurant into data source
          //  if (string.IsNullOrEmpty(restaurant.Name)) 
          //  {; alternate way is use data annotations in restaurant model class
          //      ModelState.AddModelError(nameof(restaurant.Name), "The name is required.");
          //  }

            if (ModelState.IsValid)
            {
                db.Add(restaurant);
                // redirect to details of restaurant(restaurants/details/{id} just added to model with new object  id=restaurant.Id
                return RedirectToAction("Details", new { id = restaurant.Id });
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = db.Get(id);
            if(model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Restaurant restaurant)
        { 
            if (ModelState.IsValid)
            {
                db.Update(restaurant);
                TempData["Message"] = "You have saved the restaurant !";
                return RedirectToAction("Details", new { id = restaurant.Id });
            }
            return View();
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var model = db.Get(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, FormCollection form)
        {
            db.Delete(id);
            return RedirectToAction("Index");
            
        }

    }
}