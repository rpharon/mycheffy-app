using System;
using System.Collections.Generic;
using System.Text;

namespace mycheffy.Models.CartList
{
    public class CartItemModel
    {
        public string id { get; set; }
        public string name { get; set; }
        public int quantity { get; set; }
        public float price { get; set; }

    }
}
