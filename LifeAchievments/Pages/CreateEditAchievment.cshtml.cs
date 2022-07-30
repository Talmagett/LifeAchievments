using LifeAchievments.Data;
using LifeAchievments.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LifeAchievments.Pages
{
	public class CreateEditAchievmentModel : PageModel
	{
		[BindProperty(SupportsGet = true)]
		public Achievment EditingAchievment { get; set; }

		[BindProperty(SupportsGet = true)]
		public bool IsEdit { get; set; }
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
		public void OnPost()
		{
			if (EditingAchievment.MaxAmount <= 0)
				EditingAchievment.State = State.Incremental;
			else
			{
				if (EditingAchievment.Progress < EditingAchievment.MaxAmount)
					EditingAchievment.State = State.Uncompleted;
				else EditingAchievment.State = State.Completed;
			}

			_context.AchievmentsCollection.Add(EditingAchievment);
			_context.SaveChanges();
		}
	}
}