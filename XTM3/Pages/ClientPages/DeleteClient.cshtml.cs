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
    public class DeleteClientModel : PageModel
    {
        
        private readonly IClientData clientData;

        public Client SelectedClient { get; set; }

        [BindProperty]
        public int SelectedClientID { get; set; }

        public IEnumerable<Client> Clients { get; set; }




        public DeleteClientModel(IClientData clientData)
        {
            this.clientData = clientData;
        }

        public IActionResult OnGet()
        {
            Clients = clientData.GetAll();
            SelectedClient = new Client();

            if (Clients != null)
            {
                return Page();
            }
            else
            {
                return RedirectToPage("/ClientNotFound");
            }

        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                var clientToDelete = clientData.GetClientsByID(SelectedClientID);

                if (clientToDelete != null)
                {
                    clientData.Delete(SelectedClientID);
                    var saveChanges = clientData.Commit();
                    if (saveChanges > 0)
                        return RedirectToPage("/ClientPages/OurClients");
                    else
                        return Page();
                }

                return RedirectToPage("/PlanesPages/NotFound");
            }

            return Page();
        }
    }
}
