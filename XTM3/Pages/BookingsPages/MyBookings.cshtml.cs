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
    public class MyBookingsModel : PageModel
    {
        private readonly IBookingData bookingsData;
        public IEnumerable<Booking> ReservedFlights { get; set; }

        [BindProperty(SupportsGet = true)]
        public int SearchTerm { get; set; }      //PARAMETRO DE BUSQUEDA DE RESERVAS (ID DE RESERVA O FECHA)

        public MyBookingsModel(IBookingData bookingsData)
        {
            this.bookingsData = bookingsData;
            this.SearchTerm = 0;

        }
        public IActionResult OnGet()
        {
            if (SearchTerm == 0)
            {
                    ReservedFlights = bookingsData.GetAll();
                return Page();
            }
            else
            {
                ReservedFlights = bookingsData.GetUserIDBookings(SearchTerm);
                if (!ReservedFlights.Any())
                    return RedirectToPage("/BookingsPages/BookingNotFound");
            }
            return Page();
        }
    }
}
