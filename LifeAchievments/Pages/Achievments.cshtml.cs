using LifeAchievments.Data;
using LifeAchievments.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LifeAchievments.Pages
{
    public class AchievmentsModel : PageModel
    {
        public List<Category> Categories { get {
                return _context.CategoryCollection.ToList(); } }
        public List<Achievment> Achievments { get {
                return _context.AchievmentsCollection.ToList(); } }

        private readonly ApplicationDbContext _context;
        public AchievmentsModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
        }
    }
}
