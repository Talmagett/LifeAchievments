using LifeAchievments.Data;
using LifeAchievments.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LifeAchievments.Pages
{
    public class AchievmentsModel : PageModel
    {
        public List<Category> Categories { get; set; }
        public List<Achievment> Achievments { get; set; }

        private readonly ApplicationDbContext _context;
        public AchievmentsModel(ApplicationDbContext context)
        {
            _context = context;
            Categories=_context.CategoryCollection.ToList();
            Achievments= _context.AchievmentsCollection.ToList();
        }
        public void OnGet()
        {
        }
    }
}
