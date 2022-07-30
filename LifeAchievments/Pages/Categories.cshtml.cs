using LifeAchievments.Data;
using LifeAchievments.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LifeAchievments.Pages
{
    public class CategoriesModel : PageModel
    {
        public List<Category> Categories { get; set; }
        [BindProperty()]
        public string NewCategoryName { get; set; }
        [BindProperty()]
        public Category EditingCategory { get; set; } = new Category();

        private readonly ApplicationDbContext _context;
        public CategoriesModel(ApplicationDbContext context)
        {
            _context = context;
            Categories= _context.CategoryCollection.ToList();
        }
        public void OnGet()
        {
        }
        public void OnPost() {
            _context.CategoryCollection.Add(new Category() { Name = NewCategoryName });
            NewCategoryName = "";
            _context.SaveChanges();
            Categories = _context.CategoryCollection.ToList();
        }
        public IActionResult DeleteCategory()
        {
            if (EditingCategory == null)
            {
                return NotFound();
            }
            Category? category = _context.CategoryCollection.Find(EditingCategory.Id);
            if (category == null)
            {
                return NotFound();
            }
            _context.CategoryCollection.Remove(category);
            _context.SaveChanges();
            Categories = _context.CategoryCollection.ToList();

            return RedirectToPage(this);
        }
    }
}
