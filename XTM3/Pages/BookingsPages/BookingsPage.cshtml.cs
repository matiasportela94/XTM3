using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using XTMCore;
using XTMData;

namespace XTM3.Pages.BookingsPages
{
    public class BookingsPageModel : PageModel
    {

        private readonly IBookingData bookingsData;
        private readonly IClientData clientData;
        private readonly IHtmlHelper htmlHelper;

        public IEnumerable<SelectListItem> Origins;
        public IEnumerable<SelectListItem> Destinations;

        public string Today;
        public Booking BookingMade { get; set; }
        [BindProperty]
        public Booking PendingReservation { get; set; }      //ACA SE GUARDAN LOS DATOS DE LA RESERVA INGRESADOS POR EL USUARIO, CON ESTOS DATOS:
                                                             //1) SE VERIFICAN CUALES SON LOS AVIONES DISPONIBLES PARA LA FECHA INGRESADA
                                                             //2) EN CASO DE CONFIRMARSE LA RESERVA SE USARIA PARA ANADIR LOS DATOS A LA BASE DE DATOS


        public BookingsPageModel(IBookingData bookingsData, IClientData clientData, IHtmlHelper htmlHelper)
        {
            this.bookingsData = bookingsData;
            this.clientData = clientData;
            this.htmlHelper = htmlHelper;
        }

        public IActionResult OnGet()
        {
            Origins = htmlHelper.GetEnumSelectList<Ciudad>();
            Destinations = htmlHelper.GetEnumSelectList<Ciudad>();
            PendingReservation = new Booking();

            return Page();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                if (PendingReservation.OriginCity.Equals(PendingReservation.DestinyCity) || PendingReservation.OriginCity.Equals(Ciudad.NONE) || PendingReservation.OriginCity.Equals(Ciudad.NONE))
                {
                    return RedirectToPage("/BookingsPages/BookingsPage");
                }
                else
                {
                    var client = clientData.GetClientsByID(PendingReservation.UserID);

                    if (client != null)
                    {
                        PendingReservation.PlaneID = 0;
                        PendingReservation.Price = 0.00;
                        bookingsData.Add(PendingReservation);
                        bookingsData.Commit();
                        return RedirectToPage("/PlanesPages/SelectPlane");
                    }

                    return RedirectToPage("/PlanesPages/SelectPlane");
                    //SI NO EXISTE VUELVE A INDEX (SI PUEDO PONER UN MENSAJE DE ERROR MEJOR)

                }

            }
            else
            {
                Origins = htmlHelper.GetEnumSelectList<Ciudad>();
                Destinations = htmlHelper.GetEnumSelectList<Ciudad>();
                return Page();
            }

        }
    }
}