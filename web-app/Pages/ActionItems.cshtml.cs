using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace web_app.Pages
{
    public class ActionItemsModel : PageModel
    {
        public string itemId { get; set; }
        public void OnGet(string ID)
        {
            if (ID != null)
            {
                itemId = "17981491";
            }
        }
    }
}
