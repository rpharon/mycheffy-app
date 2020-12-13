using System;
namespace mycheffy.Models.Address
{
    public class AddressModel
    {
        public int id { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public string complete_address { get; set; }
        public string full_google_address { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string phone_number { get; set; }
        public bool is_primary { get; set; }
    }
}
