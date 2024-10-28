using FIAP.GestaoEscolar.Admin.Models.Class;
using FIAP.GestaoEscolar.Admin.Services.Class;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FIAP.GestaoEscolar.Admin.Pages.Class
{
    public class EditModel : PageModel
    {
        IClassService _classService;
        public EditModel(IClassService classService)
        {
            _classService = classService;
        }

        [TempData]
        public string Message { get; set; }

        [BindProperty]
        public ClassModel Class { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var response = await _classService.GetByIdAsync(id);
            if (response == null)
                return NotFound();

            Class = response.Data;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var response = await _classService.UpdateAsync(Class);

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
