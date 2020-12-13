using mycheffy.Models.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace mycheffy.Models.Restaurant
{
    public class RestaurantEvents
    {
        public EventActions action { get; set; }
        public object data { get; set; }

        public RestaurantEvents(EventActions _action, object _data)
        {
            action = _action;
            data = _data;
        }
    }
}
