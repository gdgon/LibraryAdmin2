using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryAdmin2.ViewModels
{
    public class SearchViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Isbn { get; set; }
        public string Title { get; set; }
        public string SearchType { get; set; }
        public bool? Partial { get; set; }
        public string ListLabel { get; set; }
        public int ListLabelClass { get; set; }

    }
}