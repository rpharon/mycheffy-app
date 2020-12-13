using System;
using System.Collections.Generic;
using mycheffy.Models.FoodItem;
using mycheffy.Models.Review;
using Newtonsoft.Json;

namespace mycheffy.Models.Restaurant
{
    public class RestaurantDetailModel
    {
        public string name { get; set; }
        public string slug { get; set; }
        public string photo { get; set; }
        public IEnumerable<RestaurantReviewModel> reviews { get; set; }
        public IEnumerable<FoodItemModel> products { get; set; }
    }
}
