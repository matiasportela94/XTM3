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
    public class DeleteBookingModel : PageModel
    {
        private readonly IBookingData BookingsData;

        public Booking SelectedBooking { get; set; }

        [BindProperty]
        public int SelectedBookingID { get; set; }

        public IEnumerable<Booking> Bookings { get; set; }




        public DeleteBookingModel(IBookingData bookingData)
        {
            this.BookingsData = bookingData;
        }

        public IActionResult OnGet()
        {
            Bookings = BookingsData.GetAll();
            SelectedBooking = new Booking();

            if (Bookings != null)
            {
                return Page();
            }
            else
            {
                return RedirectToPage("/BookingNotFound");
            }

        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                var reservedFlights = BookingsData.GetBookingByID(SelectedBookingID);

                if (reservedFlights != null)
                {
                    BookingsData.Delete(SelectedBookingID);
                    var saveChanges = BookingsData.Commit();
                    if (saveChanges > 0)
                        return RedirectToPage("/AdminPages/AdminPage");
                    else
                        return Page();
                }

                return RedirectToPage("/BookingsPages/BookingNotFound");
            }

            return Page();
        }
    }
}