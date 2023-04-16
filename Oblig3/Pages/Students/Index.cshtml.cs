using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.View;
using Oblig3.Data;
using Oblig3.Models;

namespace Oblig3.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly Oblig3.Data.Dat154Context _context;

        public IndexModel(Oblig3.Data.Dat154Context context)
        {
            _context = context;
        }

        public IList<Student> Student { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchStringId { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? SearchStringName { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? SearchStringAge { get; set; }
        public SelectList? sStudents { get; set; }

        public async Task OnGetAsync()
        {
            
            if (_context.Students != null)
            {
                Student = await _context.Students.ToListAsync();


                if (!string.IsNullOrEmpty(SearchStringName))
                {
                   Student = Student.Where(s => s.Studentname.IndexOf(SearchStringName, 0, StringComparison.OrdinalIgnoreCase) != -1).ToList();
                }
                if (!string.IsNullOrEmpty(SearchStringAge))
                {
                    try
                    {
                        int iAge = int.Parse(SearchStringAge);
                        Student = Student.Where(s => s.Studentage.Equals(iAge)).ToList();
                    } catch { }
                }
                if (!string.IsNullOrEmpty(SearchStringId))
                {
                    try
                    {
                        int iId = int.Parse(SearchStringId);
                        Student = Student.Where(s => s.Id.Equals(iId)).ToList();
                    }
                    catch { }
                }
            }
        }
    }
}
