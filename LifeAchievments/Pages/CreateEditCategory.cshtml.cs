using LifeAchievments.Data;
using LifeAchievments.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LifeAchievments.Pages
{
    public class CreateEditCategoryModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string? NewCategoryName { get; set; }
        public List<Category> Categories { get; set; }
        private readonly ApplicationDbContext _context;
        public CreateEditCategoryModel(ApplicationDbContext context)
        {
            _context = context;
            Categories = _context.CategoryCollection.OrderBy(category => category.Name).ToList();
        }
        public IActionResult OnPost()
        {
            if (string.IsNullOrWhiteSpace(NewCategoryName) || NewCategoryName == "New Category") return RedirectToPage(("/CreateEditCategory"));
            if (Categories.Any(category => category.Name == NewCategoryName)) return RedirectToPage(("/CreateEditCategory"));
            _context.CategoryCollection.Add(new Category() { Name = NewCategoryName });
            _context.SaveChanges();
            Categories = _context.CategoryCollection.OrderBy(category => category.Name).ToList();
            return RedirectToPage("/CreateEditCategory");
        }
        public IActionResult OnGetDelete(int id)
        {
            Category? category = _context.CategoryCollection.Where(cat => cat.Id == id).FirstOrDefault();
            if (category == null) return NotFound();
            _context.CategoryCollection.Remove(category);
            _context.SaveChanges();
            Categories = _context.CategoryCollection.OrderBy(category => category.Name).ToList();
            return new JsonResult("Deleted");
        }
        public IActionResult OnGetRename(int id,string newName)
        {
            Category? category = _context.CategoryCollection.Where(cat => cat.Id == id).FirstOrDefault();
            if (category == null) return NotFound();

            if (string.IsNullOrWhiteSpace(newName) || newName == "New Category")
                _context.CategoryCollection.Remove(category);

            if (Categories.Any(category => category.Name == newName))
                return new JsonResult("Exist");

            category.Name = newName;
            _context.SaveChanges();
            Categories = _context.CategoryCollection.OrderBy(category=>category.Name).ToList();
            return new JsonResult("Renamed");
        }
    }
}
