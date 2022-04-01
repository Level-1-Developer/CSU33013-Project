using System.Collections.Generic;
using web_app.Models;
using web_app.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace web_app.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public DatabaseService DataService { get; }
        public IEnumerable<ActionItem>? ActionItems { get; private set; }

        public IndexModel(ILogger<IndexModel> logger, DatabaseService databaseService)
        {
            _logger = logger;
            DataService = databaseService;
        }

        public void OnGet()
        {
            ActionItems = DataService.GetActionItems();
        }
    }
}