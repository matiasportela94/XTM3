using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using XTMCore;
using XTMData;

namespace XTM3.Pages.ClientPages
{
    public class OurClientsModel : PageModel
    {

        private readonly IClientData clientData;
        public IEnumerable<Client> Clients;


        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public OurClientsModel(IClientData clientData)
        {
            this.clientData= clientData;
            this.SearchTerm = null;
        }

        public IActionResult OnGet(string searchTerm)
        {
            // si no se ingresaron parametros de busqueda, la pagina muestra la lista completa de aviones
            if (searchTerm == null)
            {
                Clients = clientData.GetAll();
                if (Clients == null)
                {
                    return RedirectToPage("/ClientNotFound");
                }
                else
                {
                    return Page();
                }
            }
            else
            {

                Clients = clientData.GetClients(searchTerm);

                //si la lista de aviones esta vacia (es decir, no se encontraron aviones con los parametros de busqueda ingresados)
                //se redirecciona a la pagina "/NotFound"
                if (!Clients.Any())
                {
                    return RedirectToPage("/ClientPages/ClientNotFound");
                }
            }

            return Page();
        }

    
    }
}
