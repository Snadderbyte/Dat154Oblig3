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
        public Course Course { get; set; } = default!;

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
            //not sure what do
            int sid = int.Parse(id);
            var grade =  await _context.Grades.FirstOrDefaultAsync(m => m.Studentid == sid);
            var icourse = await _context.Courses.FirstOrDefaultAsync(m => m.Coursecode == id);

            // know what do
            if (grade == null || icourse == null)
            {
                return NotFound();
            }
            Grade = grade;
            Course = icourse;

            //dont know what do
            ViewData["Coursecode"] = new SelectList(_context.Courses, "Coursecode", "Coursecode");
            ViewData["Studentid"] = new SelectList(_context.Students, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

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
