using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Restaurants
{
    public class DownloadModel : PageModel
    {
        private readonly IRestaurantData _restaurantData;
        public Restaurant Restaurant { get; set; }

        public DownloadModel(IRestaurantData restaurantData)
        {
            _restaurantData = restaurantData;
        }
        public void OnGet(int restaurantId)
        {
            Restaurant = this._restaurantData.GetById(restaurantId);

            XmlSerializer serializer = new XmlSerializer(typeof(Restaurant));
            serializer.Serialize(System.IO.File.Create($"{Restaurant.Name}_{restaurantId}.xml"), Restaurant);
        }
    }
}