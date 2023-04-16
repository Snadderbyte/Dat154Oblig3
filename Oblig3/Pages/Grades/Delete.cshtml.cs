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
    public class DeleteModel : PageModel
    {
        private readonly Oblig3.Data.Dat154Context _context;

        public DeleteModel(Oblig3.Data.Dat154Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Grade Grade { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id, string course)
        {
            if (id == null || _context.Grades == null)
            {
                return NotFound();
            }
            if (course == null || _context.Courses == null)
            {
                return NotFound();
            }
            int sid =  int.Parse(id);
            var grade = await _context.Grades
                    .Where(x => x.Coursecode.Equals(course) 
                    && x.Studentid.Equals(sid)).FirstOrDefaultAsync();

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

        public async Task<IActionResult> OnPostAsync(string id, string course)
        {
            if (id == null || _context.Grades == null)
            {
                return NotFound();
            }
            int sid = int.Parse(id);
            var grade = await _context.Grades.FindAsync(course, sid);

            if (grade != null)
            {
                Grade = grade;
                _context.Grades.Remove(Grade);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
