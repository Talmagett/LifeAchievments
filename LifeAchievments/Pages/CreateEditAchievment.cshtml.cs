using LifeAchievments.Data;
using LifeAchievments.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text.Json;

namespace LifeAchievments.Pages
{
	public class CreateEditAchievmentModel : PageModel
	{
		[BindProperty(SupportsGet = true)]
		public Achievment NewCreatingAchievment { get; set; } = new Achievment();
		public List<Category> Categories { get; set; }
		public List<Icon> Icons { get; set; }
		public bool IsEdit { get; set; }
		private readonly ApplicationDbContext _context;
		public CreateEditAchievmentModel(ApplicationDbContext context)
		{
			_context = context;
			Icons = _context.IconsCollection.ToList();
			Categories = _context.CategoryCollection.ToList();
		}
		public void OnGet(int id)
		{
			NewCreatingAchievment.Id = id;
			IsEdit = NewCreatingAchievment.Id != -1;
			if (NewCreatingAchievment.Id == -1)
			{
				NewCreatingAchievment.Name = "";
				NewCreatingAchievment.Description = "";
				NewCreatingAchievment.Award = "";
				NewCreatingAchievment.Comment = "";
				NewCreatingAchievment.Progress = 0;
				NewCreatingAchievment.MaxAmount = 0;
				NewCreatingAchievment.CategoriesIdName = "";
			}
			else
			{
				Achievment? achievmentInDb = _context.AchievmentsCollection.Where(achievment => achievment.Id == NewCreatingAchievment.Id).FirstOrDefault();
				NewCreatingAchievment = achievmentInDb;
			}
		}
		public IActionResult OnGetCategories(string json)
		{
			return new JsonResult(json);
		}
		public IActionResult OnGetSetData(string json)
		{
			Achievment achievment = JsonConvert.DeserializeObject<Achievment>(json);
			NewCreatingAchievment = achievment;


			if (NewCreatingAchievment.MaxAmount <= 0)
				NewCreatingAchievment.State = State.Incremental;
			else
			{
				NewCreatingAchievment.Progress = Math.Clamp(NewCreatingAchievment.Progress, 0, NewCreatingAchievment.MaxAmount);
				if (NewCreatingAchievment.Progress < NewCreatingAchievment.MaxAmount)
					NewCreatingAchievment.State = State.Uncompleted;
				else NewCreatingAchievment.State = State.Completed;
			}

			if (NewCreatingAchievment.Id == -1)
			{
				NewCreatingAchievment.Id = 0;
				_context.AchievmentsCollection.Add(NewCreatingAchievment);
			}
			else
			{
				Achievment? achievmentInDb = _context.AchievmentsCollection.Where(achievment => achievment.Id == NewCreatingAchievment.Id).FirstOrDefault();
				if (achievmentInDb != null)
				{
					achievmentInDb.Name = NewCreatingAchievment.Name;
					achievmentInDb.Description = NewCreatingAchievment.Description;
					achievmentInDb.CategoriesIdName = NewCreatingAchievment.CategoriesIdName;
					achievmentInDb.Award = NewCreatingAchievment.Award;
					achievmentInDb.Comment = NewCreatingAchievment.Comment;
					achievmentInDb.MaxAmount = NewCreatingAchievment.MaxAmount;
					achievmentInDb.Progress = NewCreatingAchievment.Progress;
					achievmentInDb.AchievedIconName = NewCreatingAchievment.AchievedIconName;
					achievmentInDb.State = NewCreatingAchievment.State;
				}
			}
			_context.SaveChanges();
			return new JsonResult("/Achievments");
		}
	}
}