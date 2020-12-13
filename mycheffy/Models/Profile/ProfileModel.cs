using mycheffy.Models.Address;
using System;
using System.Collections.Generic;
using System.Text;

namespace mycheffy.Models.Profile
{
    public class ProfileModel
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string photo { get; set; }
        public IEnumerable<string> favorites { get; set; }
        public bool is_vendor { get; set; }

        public bool is_admin { get; set; }
        public string phone_number { get; set; }
        public string birthday { get; set; }
        public IEnumerable<AddressModel> addresses { get; set; }

        public ProfileModel()
        {

        }
    }
}
