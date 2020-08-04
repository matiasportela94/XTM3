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
    public class OurPlanesModel : PageModel
    {
        private readonly IAvionData planesData;
        public IEnumerable<Avion> Planes;


        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public List<Propulsion> propulsions;
        [BindProperty]
        public string AddPlane { get; set; }

        public OurPlanesModel(IAvionData planesData)
        {
            this.planesData = planesData;
            this.SearchTerm = null;
            this.AddPlane = null;
            this.propulsions = planesData.GetPropulsions();
        }

        public IActionResult OnGet(string searchTerm)
        {
            // si no se ingresaron parametros de busqueda, la pagina muestra la lista completa de aviones
            if (searchTerm == null)
            {
                Planes = planesData.GetAll();
                if (Planes == null)
                {
                    return RedirectToPage("/AvionNotFound");
                }
                else
                {
                    return Page();
                }
            }
            else
            {

                Planes = planesData.GetPlanesByNameOrID(searchTerm);

                //si la lista de aviones esta vacia (es decir, no se encontraron aviones con los parametros de busqueda ingresados)
                //se redirecciona a la pagina "/NotFound"
                if (!Planes.Any())
                {
                    return RedirectToPage("/PlanesPages/AvionNotFound");
                }
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                if (AddPlane != null)
                {
                    return RedirectToPage("/PlanesPages/AddPlane");
                }

            }

            return Page();



        }



    }
}