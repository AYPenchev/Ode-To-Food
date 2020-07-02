﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Restaurants
{
    public class EditModel : PageModel
    {
        private readonly IRestaurantData _restaurantData;
        private readonly IHtmlHelper _htmlHelper;

        [BindProperty]
        public Restaurant Restaurant { get; set; }
        public IEnumerable<SelectListItem> Cuisines { get; set; }

        public EditModel(IRestaurantData restaurantData, IHtmlHelper htmlHelper)
        {
            this._restaurantData = restaurantData;
            _htmlHelper = htmlHelper;
            this._htmlHelper = htmlHelper;
        }
        public IActionResult OnGet(int restaurantId)
        {
            Cuisines = this._htmlHelper.GetEnumSelectList<CuisineType>();

            Restaurant = this._restaurantData.GetById(restaurantId);
            if (Restaurant == null)
            {
                return RedirectToPage("./NotFound");
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                this._restaurantData.Update(Restaurant);
                this._restaurantData.Commit();
                return RedirectToPage("./Detail", new {restaurantId = Restaurant.Id});
            }
            Cuisines = this._htmlHelper.GetEnumSelectList<CuisineType>();

            return Page();
        }
    }
}