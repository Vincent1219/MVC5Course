using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC5Course.Models.ViewModel
{
    public class SearchClient
    {
        public int pageNo { set; get; }

        public int CreditRationFilter { set; get; }

        public int lastNameFilter { set; get; }
    }
}