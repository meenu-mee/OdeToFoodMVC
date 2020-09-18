using OdeToFood.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdeToFood.Data.Services
{
    public class OdeToFoodDbContext: DbContext 
    {
        //table named Restaurants with columns Id, Name, Cuisine
        //Db takes into consideration the DataAnnotations before creating table i.e. [Required]=Non-Nullable
        public DbSet<Restaurant> Restaurants { get; set; }

    }
}
