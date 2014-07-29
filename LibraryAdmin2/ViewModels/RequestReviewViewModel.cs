using LibraryAdmin2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryAdmin2.ViewModels
{
    public class RequestReviewViewModel : CheckoutRequest
    {
        public List<Borrower> Matches { get; set; }
    }
}