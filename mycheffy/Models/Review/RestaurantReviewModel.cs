using mycheffy.Models.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace mycheffy.Models.Review
{
    public class RestaurantReviewModel
    {
        public string id { get; set; }
        public string restaurant_id { get; set; }
        public string photo { get; set; }
        public UserModel user { get; set; }
        public int rating { get; set; }
        public string comment { get; set; }
        public DateTimeOffset created { get; set; }
    }
}