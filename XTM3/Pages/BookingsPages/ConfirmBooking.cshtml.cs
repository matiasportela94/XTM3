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

        public IEnumerable<Booking> Bookings { get; set; }

        public ConfirmBookingModel(IBookingData bookingData)
        {
            this.bookingData = bookingData;
            this.pendingBooking = new Booking();
        }

        public IActionResult OnGet()
        {
            Bookings = bookingData.GetAll();
            pendingBooking = Bookings.Last<Booking>(); 
            Submit = null;
            Cancel = null;

            return Page();
        }


        public IActionResult OnPost()
        {
            Bookings = bookingData.GetAll();
            pendingBooking = Bookings.Last<Booking>();
            if (ModelState.IsValid)
            {
                if (Submit != null)
                {
                    return RedirectToPage("/Index");
                }
                else if (Cancel != null)
                {
                    bookingData.Delete(pendingBooking.BookingID);
                    return RedirectToPage("/BookingsPages/BookingsPage");
                }

                bookingData.Delete(pendingBooking.BookingID);
                return RedirectToPage("/NotFound");
            }
            else
            {
                bookingData.Delete(pendingBooking.BookingID);
                return Page();
            }
        }
    }
}