using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Restaurants
{
    public class ListModel : PageModel
    {
        private readonly IConfiguration _config;
        private readonly IRestaurantData _restaurantData;

        public string Message { get; set; }
        public  IEnumerable<Restaurant> Restaurants { get; set; }

        public ListModel(IConfiguration config, IRestaurantData restaurantData)
        {
            this._config= config;
            this._restaurantData = restaurantData;
        }
        
        public void OnGet()
        {
            Message = this._config["Message"];
            Restaurants = this._restaurantData.GetAll();
        }
    }
}