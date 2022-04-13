using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace web_app.Pages
{
    public class ActionItemsModel : PageModel
    {
        public int inputItem { get; set; }
        public void OnGet(int ID)
        {
            inputItem = ID;
        }
    }
}
