using FIAP.GestaoEscolar.Admin.Models.Student;
using FIAP.GestaoEscolar.Admin.Services.Student;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FIAP.GestaoEscolar.Admin.Pages.Student
{
    public class IndexModel : PageModel
    {
        IStudentService _studentService;
        public IndexModel(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [TempData]
        public string Message { get; set; }

        [BindProperty]
        public List<StudentModel> ListStudents { get; set; }

        public async Task OnGetAsync()
        {
            var response = await _studentService.GetAllAsync();

            if (response != null && !response.Success)
            {
                Message = response.Message;
            }

            if (response?.Data != null && response?.Data.Count > 0)
                ListStudents = response.Data ?? new List<StudentModel>();
        }
        public async Task<IActionResult> OnPostActivateAsync(int id)
        {
            var response = await _studentService.UpdateActiveAsync(id);

            if (response != null && !response.Success)
            {
                Message = response.Message;
            }

            return RedirectToPage("./Index");
        }
    }
}
