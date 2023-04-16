using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Oblig3.Data;
using Oblig3.Models;

namespace Oblig3.Pages.Grades
{
    public class DetailsModel : PageModel
    {
        private readonly Oblig3.Data.Dat154Context _context;

        public DetailsModel(Oblig3.Data.Dat154Context context)
        {
            _context = context;
        }

      public Grade Grade { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || _context.Grades == null)
            {
                return NotFound();
            }

            var grade = await _context.Grades.FirstOrDefaultAsync(m => m.Coursecode == id);
            if (grade == null)
            {
                return NotFound();
            }
            else 
            {
                Grade = grade;
            }
            return Page();
        }
    }
}
