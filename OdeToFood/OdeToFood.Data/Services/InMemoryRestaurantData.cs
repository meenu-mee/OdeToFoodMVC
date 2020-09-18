using OdeToFood.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OdeToFood.Data.Services
{
    public class InMemoryRestaurantData : IRestaurantData
    {
        List<Restaurant> restaurants;
        public InMemoryRestaurantData()
        {
            restaurants = new List<Restaurant>()
            {
                new Restaurant { Id = 1, Name = "Elly's Pizza", Cuisine = CuisineType.Italian },
                new Restaurant { Id = 2, Name = "Butter Naan", Cuisine = CuisineType.Indian },
                new Restaurant { Id = 3, Name = "Veg Gimbap", Cuisine = CuisineType.Korean },
            };
        }       

        public IEnumerable<Restaurant> GetAll()
        {
            return restaurants.OrderBy(r => r.Name);
        }

        public Restaurant Get(int id) =>
            //if restaurant's Id is equal to parameter id then return restaurant object
            restaurants.FirstOrDefault(r => r.Id == id);

        public void Add(Restaurant restaurant)
        {
            restaurants.Add(restaurant);
            restaurant.Id = restaurants.Max(r => r.Id) + 1;//look for restaurant with max id and add 1 to id
        }

        public void Update(Restaurant restaurant)
        {
            var existing = Get(restaurant.Id);
            if (existing != null)
            {
                existing.Name = restaurant.Name;
                existing.Cuisine = restaurant.Cuisine;
            }
        }

        public void Delete(int id)
        {
            var restaurant = Get(id);
            if (restaurant != null)
            {
                restaurants.Remove(restaurant);
            }
        }
    }

}
