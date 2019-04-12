using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using logic;

namespace Web.Pages
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {

        }

        // public async Task<IActionResult> OnPostAsync()
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                Endpoints.Clear();
                try{
                    if(OpcUaServerUrl == null || OpcUaServerUrl == String.Empty)
                    {
                        Endpoints = MyUaClient.GetEndpoints(new Uri("opc.tcp://localhost:4840"));
                    }
                    else
                    {
                        Endpoints = MyUaClient.GetEndpoints(new Uri(OpcUaServerUrl));
                    }
                    
                }
                catch(Exception e){
                    System.Console.WriteLine(e.Message);
                }

                return Page();
            }
            return RedirectToPage("/Index");
        }

        [BindProperty, Required(ErrorMessage="Please supply a URL"), Display(Name="URL:")]
        public string OpcUaServerUrl { get; set; }

        [BindProperty]
        public List<List<string>> Endpoints { get; set; } = new List<List<string>>();

        public string Greet => MyUaClient.Greet();
    }
}
