using Bussiness.Interfaces;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PantherPetManagement_PhanThanhDuc.Pages.PantherPage
{
    [Authorize(Roles = "Administrator,Manager,Staff")]
    public class DetailModel : PageModel
    {
        private readonly IPantherService _service;

        public DetailModel(IPantherService service)
        {
            _service = service;
        }

        public PantherProfile PantherProfile { get; set; }

        public IActionResult OnGet(int id)
        {
            PantherProfile = _service.GetPantherProfileById(id);
            if (PantherProfile == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
