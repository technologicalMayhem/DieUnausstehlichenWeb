using System.Collections.Generic;
using DieUnausstehlichenWeb.Helper;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DieUnausstehlichenWeb.Pages
{
    public class TextModel : PageModel
    {
        public IEnumerable<string> Paragraphs { get; set; }

        public void OnGet()
        {
            Paragraphs = Text.LoremIpsum(12, 22, 10, 15, 8);
        }
    }
}