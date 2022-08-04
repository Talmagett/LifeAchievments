using LifeAchievments.Data;
using LifeAchievments.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LifeAchievments.Pages
{
	public class CreateEditAchievmentModel : PageModel
	{
		[BindProperty(SupportsGet = true)]
		public static int ChangingAchievmentId { get; set; } = -1;

		[BindProperty(SupportsGet = true)]
		public Achievment NewCreatingAchievment { get; set; }

		[BindProperty(SupportsGet = true)]
		public bool IsEdit { get; set; }
		private readonly ApplicationDbContext _context;
		public CreateEditAchievmentModel(ApplicationDbContext context)
		{
			_context = context;
		}
		public void OnGet()
		{
			if (NewCreatingAchievment != null)
			{
				//Fill field
			}
		}
		public void OnPost()
		{
			if (NewCreatingAchievment.MaxAmount <= 0)
				NewCreatingAchievment.State = State.Incremental;
			else
			{
				if (NewCreatingAchievment.Progress < NewCreatingAchievment.MaxAmount)
					NewCreatingAchievment.State = State.Uncompleted;
				else NewCreatingAchievment.State = State.Completed;
			}

			_context.AchievmentsCollection.Add(NewCreatingAchievment);
			_context.SaveChanges();
		}
	}
}