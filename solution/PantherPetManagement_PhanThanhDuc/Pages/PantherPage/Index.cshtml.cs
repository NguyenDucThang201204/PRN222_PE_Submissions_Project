using Bussiness.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PantherPetManagement_PhanThanhDuc.Pages.PantherPage
{
    [Authorize(Roles = "Administrator,Manager,Staff")]
    public class IndexModel : PageModel
    {
        private readonly IPantherService _service;

        public IndexModel(IPantherService service)
        {
            _service = service;
        }

        public IEnumerable<PantherViewModel> PantherProfiles { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }

        public void OnGet(int pageNumber = 1)
        {
            const int pageSize = 3;
            PantherProfiles = _service.GetPantherProfiles(pageNumber, pageSize);
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(_service.GetTotalCount() / (double)pageSize);
        }
    }
}
