using System;
using System.Collections.Generic;
using System.Text;

namespace mycheffy.Models.FoodItem
{
    public class FoodItemGrid
    {
        public FoodItemModel FirstItem;
        public FoodItemModel SecondItem;
        public FoodItemGrid(FoodItemModel firstItem, FoodItemModel secondItem)
        {
            FirstItem = firstItem;
            SecondItem = secondItem;
        }
    }
}
