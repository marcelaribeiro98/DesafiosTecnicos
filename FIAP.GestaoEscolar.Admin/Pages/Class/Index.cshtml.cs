using FIAP.GestaoEscolar.Admin.Models.Class;
using FIAP.GestaoEscolar.Admin.Services.Class;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FIAP.GestaoEscolar.Admin.Pages.Class
{
    public class IndexModel : PageModel
    {
        IClassService _classService;
        public IndexModel(IClassService classService)
        {
            _classService = classService;
        }

        [TempData]
        public string Message { get; set; }

        [BindProperty]
        public List<ClassModel> ListClasses { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var response = await _classService.GetAllAsync();

            if (response == null || response?.Data?.Count == 0)
                ListClasses = new List<ClassModel>();

            if (response != null && !response.Success)
            {
                Message = response.Message;
            }

            if (response?.Data != null && response?.Data.Count > 0)
                ListClasses = response.Data ?? new List<ClassModel>();

            return Page();
        }
        public async Task<IActionResult> OnPostActivateAsync(int id)
        {
            var response = await _classService.UpdateActiveAsync(id);

            if (response != null && !response.Success)
            {
                Message = response.Message;
            }

            return RedirectToPage("./Index");
        }
    }
}
