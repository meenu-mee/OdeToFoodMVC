using OdeToFood.Data.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdeToFood.Data.Models
{
    public class SqlRestaurantData : IRestaurantData
    {
        private readonly OdeToFoodDbContext db;

        public SqlRestaurantData(OdeToFoodDbContext db)
        {
            this.db = db;
        }
        public void Add(Restaurant restaurant)
        {
            db.Restaurants.Add(restaurant); // add to Restaurants table in 'db' database
            db.SaveChanges(); // save changes made in Restaurants table
        }

        public void Delete(int id)
        {
            var restaurant = Get(id); //or use db.Restaurants.Find(id)
            db.Restaurants.Remove(restaurant);
            db.SaveChanges();
        }

        public Restaurant Get(int id)
        {
            return db.Restaurants.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Restaurant> GetAll()
        {
            // return db.Restaurants.OrderBy(r=>r.Name)
            return from r in db.Restaurants
                   orderby r.Name
                   select r;
        }

        public void Update(Restaurant restaurant)
        {
            var entry = db.Entry(restaurant); // usong for optimistic concurrency-multiple users trying to change same data
            //give Restaurant object in modified state so changes must be saved for it
            entry.State = EntityState.Modified; 
            db.SaveChanges();
        }
    }
}
