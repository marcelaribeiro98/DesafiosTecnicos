using FIAP.GestaoEscolar.Admin.Models.Class;
using FIAP.GestaoEscolar.Admin.Services.Class;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FIAP.GestaoEscolar.Admin.Pages.Class
{
    public class CreateModel : PageModel
    {
        IClassService _classService;
        public CreateModel(IClassService classService)
        {
            _classService = classService;
        }

        [TempData]
        public string Message { get; set; }

        [BindProperty]
        public ClassModel Class { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var response = await _classService.CreateAsync(Class);

                if (response != null && !response.Success)
                {
                    Message = response.Message;
                }

                Message = response.Message;
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
