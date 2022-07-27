using LifeAchievments.Data;
using LifeAchievments.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LifeAchievments.Pages
{
    public class CategoriesModel : PageModel
    {
        public List<Category> Categories { get { return _context.CategoryCollection.ToList(); }}

        [BindProperty()]
        public string NewCategoryName { get; set; }
        [BindProperty()]
        public string EditingCategoryName { get; set; }
        private readonly ApplicationDbContext _context;
        public CategoriesModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
        }
        public void OnPost() {
            if (string.IsNullOrWhiteSpace(NewCategoryName))
            {
                return;
            }
            _context.CategoryCollection.Add(new Category() { Name = NewCategoryName });
            NewCategoryName = "";
            _context.SaveChanges();
        }
    }
}
