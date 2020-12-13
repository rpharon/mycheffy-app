using System;
using System.Collections.Generic;
using System.Text;
using mycheffy.Models.Restaurant;

namespace mycheffy.Models
{
    public partial class MetaData
    {
        public int total { get; set; }
        public int currentPage { get; set; }
        public int lastPage { get; set; }
        public int perPage { get; set; }
        public string previousPageUrl { get; set; }
        public string nextPageUrl { get; set; }
        public string url { get; set; }
    }

    public class Pagination<T>
    {
        public IEnumerable<T> data { get; set; }
        public MetaData meta { get; set; }
    }
}