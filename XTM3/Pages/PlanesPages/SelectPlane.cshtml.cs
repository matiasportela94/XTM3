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
        public IEnumerable<Avion> Planes { get; set; }
        private readonly IBookingData bookingsData;
        public IEnumerable<Booking> ReservedFlights { get; set; }

        

        [BindProperty]
        public int SelectedPlaneID { get; set; }
        public Avion SelectedPlane { get; set; }

        public Booking PendingBooking { get; set; }




        public SelectPlaneModel(IAvionData planesData, IBookingData bookingsData)
        {
            this.planesData = planesData;
            this.bookingsData = bookingsData;
            this.PendingBooking = new Booking();
        }

        public IActionResult OnGet()
        {
            Planes = planesData.GetAll();
            
            SelectedPlane = new Avion();

            return Page();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {

                SelectedPlane = planesData.GetPlanesByID(SelectedPlaneID);

                if (SelectedPlane != null)
                {
                    ReservedFlights = bookingsData.GetAll();
                    PendingBooking = ReservedFlights.Last<Booking>();

                    PendingBooking.PlaneID = SelectedPlane.PlaneID;
                    PendingBooking.Price = bookingsData.SetFlightPrice(PendingBooking, SelectedPlane);
                    bookingsData.Update(PendingBooking);
                    bookingsData.Commit();

                    
                   
                    return RedirectToPage("/BookingsPages/ConfirmBooking");
                }

                return RedirectToPage("/PlanesPages/SelectPlane");
            }

            return Page();
        }
    }
}