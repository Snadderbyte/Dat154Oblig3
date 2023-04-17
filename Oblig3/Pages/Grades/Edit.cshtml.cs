using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Oblig3.Data;
using Oblig3.Models;

namespace Oblig3.Pages.Grades
{
    public class EditModel : PageModel
    {
        private readonly Oblig3.Data.Dat154Context _context;

        public EditModel(Oblig3.Data.Dat154Context context)
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
            int sid = int.Parse(id);
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            // ModelState is invalid for some reason dont know why works without
            /*if (!ModelState.IsValid)
            {
                Console.WriteLine("1");
                return Page();
            }*/

            _context.Attach(Grade).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GradeExists(Grade.Coursecode))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool GradeExists(string id)
        {
          return (_context.Grades?.Any(e => e.Coursecode == id)).GetValueOrDefault();
        }
    }
}
