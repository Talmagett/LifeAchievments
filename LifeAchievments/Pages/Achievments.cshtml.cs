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
        public static bool CanEdit { get; private set; }
        private readonly ApplicationDbContext _context;
        public AchievmentsModel(ApplicationDbContext context)
        {
            _context = context;
            Categories = _context.CategoryCollection.OrderBy(t => t.Name).ToList();
            Achievments = _context.AchievmentsCollection.OrderBy(t => t.Name).ToList();
        }
        public IActionResult OnGetDeleteAchievment(int id)
        {
            Achievment? achievmentInDb = GetAchievmentById(id);
            if (achievmentInDb != null)
                _context.AchievmentsCollection.Remove(achievmentInDb);
            Achievment? achievment = Achievments.Where(achievment => achievment.Id == id).FirstOrDefault();
            if (achievment != null)
                Achievments.Remove(achievment);
            _context.SaveChanges();
            return new JsonResult("successfully deleted");
        }
        public IActionResult OnGetProgressOfEditingId(int id)
        {
            Achievment? achievment = GetAchievmentById(id);
            if (achievment != null)
                return new JsonResult(achievment);
            else
                return new JsonResult(new Achievment());
        }
        public IActionResult OnGetSetProgress(int id, int progress)
        {
            int editingProgress = progress;
            int x = id + progress;
            Achievment achievmentInDb = GetAchievmentById(id);
            if (achievmentInDb != null)
            {
                if (achievmentInDb.MaxAmount == 0)
                {
                    editingProgress = Math.Max(0, editingProgress);
                    achievmentInDb.State = State.Incremental;
                }
                else
                {
                    editingProgress = Math.Clamp(editingProgress, 0, achievmentInDb.MaxAmount);

                    if (editingProgress < achievmentInDb.MaxAmount)
                        achievmentInDb.State = State.Uncompleted;
                    else achievmentInDb.State = State.Completed;
                }
            }
            achievmentInDb.Progress = editingProgress;


            _context.SaveChanges();
            Achievment achievment = Achievments.Where(achievment => achievment.Id == id).FirstOrDefault();
            if (achievment != null)
                achievment.Progress = editingProgress;
            return new JsonResult(achievmentInDb.Progress);
        }

        public IActionResult OnGetTryLogin(string password)
        {
            if (password == "Edition")
            {
                CanEdit = true;
                return new JsonResult("Successful");
            }
            else
            {
                CanEdit = false;
                return new JsonResult("Failed");
            }
        }
        private Achievment? GetAchievmentById(int id)
        {
            return _context.AchievmentsCollection.Where(achievment => achievment.Id == id).FirstOrDefault();
        }
    }
}
