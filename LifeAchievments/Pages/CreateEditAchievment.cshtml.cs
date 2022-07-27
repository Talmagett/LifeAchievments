using LifeAchievments.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LifeAchievments.Data;

namespace LifeAchievments.Pages
{
    public class CreateEditAchievmentModel : PageModel
    {
        [BindProperty(SupportsGet =true)]
        public Achievment EditingAchievment { get; set; }
        private readonly ApplicationDbContext _context;
        public CreateEditAchievmentModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
            if (EditingAchievment != null) 
            { 
                //Fill field
            }
        }
        public void OnPost() {
            if (string.IsNullOrWhiteSpace(EditingAchievment.Name)
                ||
                string.IsNullOrWhiteSpace(EditingAchievment.Description)
                )
                return;

            if (EditingAchievment.MaxAmount == EditingAchievment.Progress) 
                EditingAchievment.State = State.Incremental;
            else if (EditingAchievment.MaxAmount <= 0)
                EditingAchievment.State = State.Completed;
            else EditingAchievment.State = State.Uncompleted;

            _context.AchievmentsCollection.Add(EditingAchievment);
            _context.SaveChanges();
        }
    }
}