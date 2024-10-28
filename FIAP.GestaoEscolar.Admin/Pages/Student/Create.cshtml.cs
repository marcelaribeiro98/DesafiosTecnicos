using FIAP.GestaoEscolar.Admin.Models.Student;
using FIAP.GestaoEscolar.Admin.Services.Student;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FIAP.GestaoEscolar.Admin.Pages.Student
{
    public class CreateModel : PageModel
    {
        IStudentService _studentService;
        public CreateModel(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [TempData]
        public string Message { get; set; }

        [BindProperty]
        public StudentModel Student { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var response = await _studentService.CreateAsync(Student);

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
