using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using XTMCore;
using XTMData;

namespace XTM3.Pages.BookingsPages
{
    public class ConfirmBookingModel : PageModel
    {
        public readonly IBookingData bookingData;


        public Booking pendingBooking;
        [BindProperty]
        public string Submit { get; set; }

        [BindProperty]
        public string Cancel { get; set; }

        public ConfirmBookingModel(IBookingData bookingData)
        {
            this.bookingData = bookingData;
            this.pendingBooking = new Booking();
        }

        public IActionResult OnGet()
        {
            pendingBooking = bookingData.GetLastBooking();
            Submit = null;
            Cancel = null;

            return Page();
        }


        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                if (Submit != null)
                {
                    return RedirectToPage("/Index");
                }
                else if (Cancel != null)
                {
                    return RedirectToPage("/BookingsPages/BookingsPage");
                }

                return RedirectToPage("/NotFound");
            }
            else
            {
                return Page();
            }
        }
    }
}