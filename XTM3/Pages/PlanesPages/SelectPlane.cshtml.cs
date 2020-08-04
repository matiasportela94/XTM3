using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using XTMCore;
using XTMData;

namespace XTM3.Pages.PlanesPages
{
    public class SelectPlaneModel : PageModel
    {
        private readonly IAvionData planesData;
        private readonly IBookingData bookingsData;

        public Avion SelectedPlane { get; set; }

        [BindProperty]
        public int SelectedPlaneID { get; set; }

        public IEnumerable<Avion> Planes { get; set; }
        public IEnumerable<Booking> Bookings { get; set; }




        public SelectPlaneModel(IAvionData planesData, IBookingData bookingsData)
        {
            this.planesData = planesData;
            this.bookingsData = bookingsData;
        }

        public void OnGet()
        {
            Planes = planesData.GetAll();
            Bookings = bookingsData.GetAll();
            SelectedPlane = new Avion();

        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {

                var isPlane = planesData.GetPlanesByID(SelectedPlaneID);

                if (isPlane != null)
                {
                    var PendingBooking = Bookings.Last<Booking>();
                    PendingBooking.PlaneID = SelectedPlaneID;
                    PendingBooking.Price = bookingsData.GetPrice();
                    bookingsData.Update(PendingBooking);
                    return RedirectToPage("/BookingsPages/ConfirmBooking");
                }

                return RedirectToPage("/PlanesPages/SelectPlane");
            }

            return Page();
        }
    }
}