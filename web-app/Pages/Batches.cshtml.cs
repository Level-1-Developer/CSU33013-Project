using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace web_app.Pages
{
    public class BatchesModel : PageModel
    {
        public Guid inputItem { get; set; }
        public void OnGet(Guid ID)
        {
            inputItem = ID;
        }
    }
}
