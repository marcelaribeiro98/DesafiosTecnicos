using FIAP.GestaoEscolar.Admin.Models.Student;
using FIAP.GestaoEscolar.Admin.Services.Student;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FIAP.GestaoEscolar.Admin.Pages.Student
{
    public class EditModel : PageModel
    {
        IStudentService _studentService;
        public EditModel(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [TempData]
        public string Message { get; set; }

        [BindProperty]
        public StudentModel Student { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var response = await _studentService.GetByIdAsync(id);
            if (response == null)
                return NotFound();

            Student = response.Data;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var response = await _studentService.UpdateAsync(Student);

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
