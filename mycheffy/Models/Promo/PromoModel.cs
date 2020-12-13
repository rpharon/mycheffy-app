using System;
using System.Collections.Generic;
using System.Text;

namespace mycheffy.Models.Promo
{
    public class PromoModel
    {
        public string id { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public string photo { get; set; }
        public string title { get; set; }
        public string description1 { get; set; }
        public string description2 { get; set; }
        public bool isExpanded { get; set; }

        public PromoModel(string _id, string _name, string _code, string _photo, string _title, string _description1, string _description2)
        {
            id = _id;
            name = _name;
            code = _code;
            photo = _photo;
            title = _title;
            description1 = _description1;
            description2 = _description2;
            isExpanded = false;
        }
    }
}
