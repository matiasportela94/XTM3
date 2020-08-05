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
    public class AddClientModel : PageModel
    {
        private readonly IClientData clientsData;

        [BindProperty]
        public string Submit { get; set; }
        [BindProperty]
        public string Cancel { get; set; }
        [BindProperty]
        public Client NewClient { get; set; }

        public AddClientModel(IClientData clientsData)
        {
            this.clientsData = clientsData;
        }


        public IActionResult OnGet()
        {
            NewClient = new Client();


            return Page();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                if (Submit != null)
                {
                    //avionData.SetPlaneID(NewPlane);
                    clientsData.Add(NewClient);
                    var commit = clientsData.Commit();

                    if (commit > 0)
                    {
                        return RedirectToPage("/ClientPages/ClientPage");
                    }
                    else
                    {
                        return RedirectToPage("/ClientPages/ClientNotFound");
                    }
                }

                if (Cancel != null)
                {
                    var clients = clientsData.GetAll();
                    var lastClient = clients.Last<Client>();

                    clientsData.Delete(lastClient.UserID);

                    return RedirectToPage("/Index");
                }

                return RedirectToPage("/ClientPages/ClientNotFound");
            }
            else
            {
                if (Cancel != null)
                {
                    return RedirectToPage("/Index");
                }
                return RedirectToPage("/NotFound");
            }
        }
    }
}
