using System;
using Newtonsoft.Json;

namespace mycheffy.Models.CartList
{
    public class CartListModel
    {
        public class CartList
        {
            [JsonProperty(PropertyName = "item_name")] //depends on the json return from api
            public string ItemName { get; set; }

            [JsonProperty(PropertyName = "qty")]
            public int Qty { get; set; }

            [JsonProperty(PropertyName = "amount")]
            public double Amount { get; set; }
        }
    }
}
