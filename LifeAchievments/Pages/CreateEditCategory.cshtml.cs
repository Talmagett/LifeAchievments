using LifeAchievments.Data;
using LifeAchievments.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LifeAchievments.Pages
{
    public class CreateEditCategoryModel : PageModel
    {
        public static int ChangingCategoryId { get; set; } = -1;
        [BindProperty(SupportsGet = true)]
        public string? NewCategoryName { get; set; }
        private HashSet<Category> Categories { get; set; }
        private readonly ApplicationDbContext _context;
        public CreateEditCategoryModel(ApplicationDbContext context)
        {
            _context = context;
            Categories = _context.CategoryCollection.ToHashSet();
        }
        public void OnGet()
        {
            string path = Request.QueryString.ToString();
            //System.Diagnostics.Debug.WriteLine("-------------------"+path+ "-------------------" + path2);
            string id = path.Split('=')[1];
            int parsedId = int.Parse(id);
            ChangingCategoryId = parsedId;
            if (ChangingCategoryId == -1)
            {
                NewCategoryName = "New Category";
            }
            else
            {
                Category? category = GetCategoryById(ChangingCategoryId);
                if (category == null) return;
                NewCategoryName = category.Name;
            }
        }
        public IActionResult OnPost()
        {
            if (string.IsNullOrWhiteSpace(NewCategoryName) || NewCategoryName == "New Category") return RedirectToPage(("/Achievments"));
            if (Categories.Any(category => category.Name == NewCategoryName)) return RedirectToPage(("/Achievments"));

            if (ChangingCategoryId == -1)
                _context.CategoryCollection.Add(new Category() { Name = NewCategoryName });
            else
            {
                Category? category = GetCategoryById(ChangingCategoryId);
                if (category == null) return RedirectToPage(("/Achievments"));

                _context.CategoryCollection.Where(cat => cat.Id == ChangingCategoryId).FirstOrDefault().Name = NewCategoryName;
            }

            _context.SaveChanges();
            Categories = _context.CategoryCollection.ToHashSet();
            return RedirectToPage("/Achievments");
        }
        public IActionResult OnPostDelete()
        {
            Category? category= _context.CategoryCollection.Where(cat => cat.Id == ChangingCategoryId).FirstOrDefault();
            if (category == null) return RedirectToPage(("/Achievments"));
            _context.CategoryCollection.Remove(category);

            _context.SaveChanges();
            Categories = _context.CategoryCollection.ToHashSet();
            return RedirectToPage("/Achievments");
        }

        private Category? GetCategoryById(int id)
        {
            return Categories.Where(cat => cat.Id == id).FirstOrDefault();
        }
    }
}
