using mycheffy.Models.Category;
using mycheffy.Models.Review;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace mycheffy.Models.Restaurant
{
    public class RestaurantModel
    {
        public string id { get; set; }
        public string name { get; set; }
        public string slug { get; set; }
        public string photo { get; set; }
        public string description { get; set; }
        public int bookmarks { get; set; }
        public IEnumerable<RestaurantReviewModel> reviews { get; set; }
        public IEnumerable<CategoryModel> categories { get; set; }
    }
}