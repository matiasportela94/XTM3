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
    public class AllBookingsModel : PageModel
    {
        private readonly IBookingData bookingsData;
        public IEnumerable<Booking> ReservedFlights { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }      //PARAMETRO DE BUSQUEDA DE RESERVAS (ID DE RESERVA O FECHA)

        public AllBookingsModel(IBookingData bookingsData)
        {
            this.bookingsData = bookingsData;
            this.SearchTerm = null;

        }
        public IActionResult OnGet()
        {
            if (SearchTerm == null)
            {
                ReservedFlights = bookingsData.GetAll();
                if (ReservedFlights == null)
                {
                    return RedirectToPage("/BookingNotFound");
                }
                else
                {
                    return Page();
                }
            }
            else
            {
                ReservedFlights = bookingsData.GetBookingsByDateOrPlaneID(SearchTerm);
                if (!ReservedFlights.Any())
                    return RedirectToPage("/BookingsPages/BookingNotFound");
            }
            return Page();
        }
    }
}
