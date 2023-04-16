using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Oblig3.Data;
using Oblig3.Models;

namespace Oblig3.Pages.Grades
{
    public class IndexModel : PageModel
    {
        private readonly Oblig3.Data.Dat154Context _context;

        public IndexModel(Oblig3.Data.Dat154Context context)
        {
            _context = context;
        }

        public IList<Grade> Grade { get;set; } = default!;
        public IList<string> Course { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? FilterCourseName { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? FilterGrade { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? SearchGrade { get; set; }

        public async Task OnGetAsync()
        {
            if (_context.Grades != null)
            {
                Course = await _context.Courses.Select(c => c.Coursecode).ToListAsync();
                Grade = await _context.Grades
                .Include(g => g.CoursecodeNavigation)
                .Include(g => g.Student).OrderBy(g => g.Studentid).ToListAsync();

                if (!string.IsNullOrEmpty(FilterCourseName))
                {
                    Grade = Grade.Where(g => g.Coursecode.Contains(FilterCourseName)).ToList();
                }
                if (!string.IsNullOrEmpty(FilterGrade))
                {
                    Grade = Grade.Where(g => g.Grade1.CompareTo(FilterGrade) <= 0).OrderBy(g => g.Grade1).ToList();
                }
                if (!string.IsNullOrEmpty(SearchGrade)) 
                {
                    Grade = Grade.Where(g => g.Grade1.Equals(SearchGrade.ToUpper())).ToList();
                }
            }
        }
    }
}
//< select asp -for= "FilterCourseName" asp - items = "@(new SelectList(Model.Grade.Select(g => g.Coursecode).Distinct()))" >
