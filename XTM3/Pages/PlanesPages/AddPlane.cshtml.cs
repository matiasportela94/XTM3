using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using XTMCore;
using XTMData;

namespace XTM3.Pages.PlanesPages
{
    public class AddPlaneModel : PageModel
    {
        private readonly IAvionData avionData;

        private readonly IHtmlHelper htmlHelper;

        [BindProperty]
        public string Submit { get; set; }
        [BindProperty]
        public string Cancel { get; set; }
        [BindProperty]
        public Avion NewPlane { get; set; }
        public IEnumerable<SelectListItem> Propulsions { get; set; }

        public AddPlaneModel(IAvionData avionData, IHtmlHelper htmlHelper)
        {
            this.avionData = avionData;
            this.htmlHelper = htmlHelper;
        }


        public IActionResult OnGet()
        {
            NewPlane = new Avion();
            Propulsions = htmlHelper.GetEnumSelectList<Propulsion>();


            return Page();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                if (Submit != null)
                {
                    //avionData.SetPlaneID(NewPlane);
                    avionData.Add(NewPlane);
                    var commit = avionData.Commit();

                    if (commit > 0)
                    {
                        return RedirectToPage("/PlanesPages/OurPlanes");
                    }
                    else
                    {
                        return RedirectToPage("/PlanesPages/AvionNotFound");
                    }
                }
                else if (Cancel != null)
                {
                    return RedirectToPage("/Index");
                }

                return RedirectToPage("/PlanesPages/AvionNotFound");
            }
            else
            {
                return RedirectToPage("/NotFound");
            }
        }


    }
}