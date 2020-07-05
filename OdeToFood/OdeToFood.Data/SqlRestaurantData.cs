using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using OdeToFood.Core;

namespace OdeToFood.Data
{
    public class SqlRestaurantData : IRestaurantData
    {
        private readonly OdeToFoodDbContext _db;

        public SqlRestaurantData(OdeToFoodDbContext db)
        {
            this._db = db;
        }
        public Restaurant Add(Restaurant newRestaurant)
        {
            this._db.Restaurants.Add(newRestaurant);
            return newRestaurant;
        }

        public int Commit()
        {
            return this._db.SaveChanges();
        }

        public Restaurant Delete(int id)
        {
            var restaurant = GetById(id);
            if (restaurant != null)
            {
                this._db.Restaurants.Remove(restaurant);
            }

            return restaurant;
        }

        public Restaurant GetById(int id)
        {
            return this._db.Restaurants.Find(id);
        }

        public IEnumerable<Restaurant> GetRestaurantsByName(string name)
        {
            return from r in this._db.Restaurants
                   where r.Name.StartsWith(name) || string.IsNullOrEmpty(name)
                   orderby r.Name
                   select r;
        }

        public Restaurant Update(Restaurant updatedRestaurant)
        {
            var entity = this._db.Restaurants.Attach(updatedRestaurant);
            entity.State = EntityState.Modified;
            return updatedRestaurant;
        }
    }
}