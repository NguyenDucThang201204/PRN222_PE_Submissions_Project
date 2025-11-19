using Bussiness.Interfaces;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PantherPetManagement_PhanThanhDuc.Pages.PantherPage
{
    [Authorize(Roles = "Administrator,Manager")]
    public class DeleteModel : PageModel
    {
        private readonly IPantherService _service;

        public DeleteModel(IPantherService service)
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

        public IActionResult OnPost(int id)
        {
            _service.DeletePantherProfile(id);
            return RedirectToPage("./Index");
        }
    }
}
