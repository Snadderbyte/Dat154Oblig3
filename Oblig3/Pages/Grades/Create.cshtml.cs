using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Oblig3.Data;
using Oblig3.Models;

namespace Oblig3.Pages.Grades
{
    public class CreateModel : PageModel
    {
        private readonly Oblig3.Data.Dat154Context _context;

        public CreateModel(Oblig3.Data.Dat154Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["Coursecode"] = new SelectList(_context.Courses, "Coursecode", "Coursecode");
        ViewData["Studentid"] = new SelectList(_context.Students, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Grade Grade { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (_context.Grades == null || Grade == null)
            {
                return Page();
            }

            _context.Grades.Add(Grade);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
