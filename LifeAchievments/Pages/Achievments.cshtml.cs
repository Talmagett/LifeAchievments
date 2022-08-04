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

        [BindProperty(SupportsGet = true)]
        public int EditingAchievmentId { get; set; }
        [BindProperty(SupportsGet = true)]
        public int EditingAchievmentProgress { get; set; }

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
        public void OnPost()
        {
            if (GetAchievmentById(EditingAchievmentId) != null)
            {
                _context.AchievmentsCollection.Where(achievment => achievment.Id == EditingAchievmentId).FirstOrDefault().Progress= EditingAchievmentProgress;
            }
        }
        public IActionResult OnGetProgressOfEditingId(int id)
        {
            EditingAchievmentId = id;

            Achievment? achievment = GetAchievmentById(EditingAchievmentId);
            if (achievment != null)
                EditingAchievmentProgress = achievment.Progress;
            return new JsonResult(EditingAchievmentProgress);
        }
        private Achievment? GetAchievmentById(int id) 
        {
            return Achievments.Where(achievment => achievment.Id == id).FirstOrDefault();
        }
    }
}
