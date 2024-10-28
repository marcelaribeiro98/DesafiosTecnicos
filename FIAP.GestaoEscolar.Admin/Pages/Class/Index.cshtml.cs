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

        [BindProperty]
        public List<ClassModel> ListClasses { get; set; }

        public async Task OnGetAsync()
        {
            var data = await _classService.GetAllAsync();

            ListClasses = data ?? new List<ClassModel>();
        }
    }
}
