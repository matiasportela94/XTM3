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
    public class DeletePlaneModel : PageModel
    {
        private readonly IAvionData planesData;

        public Avion SelectedPlane { get; set; }

        [BindProperty]
        public int SelectedPlaneID { get; set; }

        public IEnumerable<Avion> Planes { get; set; }




        public DeletePlaneModel(IAvionData planesData)
        {
            this.planesData = planesData;
        }

        public IActionResult OnGet()
        {
            Planes = planesData.GetAll();
            SelectedPlane = new Avion();

            if (Planes != null)
            {
                return Page();
            }
            else
            {
                return RedirectToPage("/AvionNotFound");
            }

        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                var isPlane = planesData.GetPlanesByID(SelectedPlaneID);

                if (isPlane != null)
                {
                    planesData.Delete(SelectedPlaneID);
                    var saveChanges = planesData.Commit();
                    if (saveChanges > 0)
                        return RedirectToPage("/PlanesPages/OurPlanes");
                    else
                        return Page();
                }

                return RedirectToPage("/PlanesPages/NotFound");
            }

            return Page();
        }
    }
}