using mycheffy.Models.Category;
using mycheffy.Models.Restaurant;
using mycheffy.Models.Review;
using System;
using System.Collections.Generic;

namespace mycheffy.Models.FoodItem
{
    public class FoodItemModel
    {
        public string id { get; set; }
        public string name { get; set; }
        public string slug { get; set; }
        public string photo { get; set; }
        public double price { get; set; }
        public string description { get; set; }
        public CategoryModel category { get; set; }
        public IEnumerable<FoodReviewModel> reviews { get; set; }
        public RestaurantModel restaurant { get; set; }
        public DateTimeOffset created { get; set; }
    }
}