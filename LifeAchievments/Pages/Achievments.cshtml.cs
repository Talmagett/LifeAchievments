using LifeAchievments.Data;
using LifeAchievments.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;

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
		public IActionResult OnGetTryBackupAsync()
		{

			try
			{
				byte[] bytes;

				using (MemoryStream ms = new MemoryStream())
				{
					using (TextWriter tw = new StreamWriter(ms, System.Text.Encoding.UTF8))
					{
						StringBuilder stringBuilder = new StringBuilder();
						stringBuilder.Append("=Split(\"");
						stringBuilder.Append("Name" + "|");
						stringBuilder.Append("Description" + "|");
						stringBuilder.Append("Award" + "|");
						stringBuilder.Append("Comment" + "|");
						stringBuilder.Append("MaxAmount"+ "|");
						stringBuilder.Append("Progress"+ "|");
						stringBuilder.Append("CategoriesIdName" + "|");
						stringBuilder.Append("AchievedIconName");
						stringBuilder.Append("\", \"|\")");
						stringBuilder.AppendLine();
						tw.Write(stringBuilder.ToString());

						foreach (var achievment in Achievments)
						{
							stringBuilder = new StringBuilder();
							stringBuilder.Append("=Split(\"");
							stringBuilder.Append(achievment.Name == null?"#": achievment.Name.Replace("\n", "").Replace("\r", "") + "|");
							stringBuilder.Append(achievment.Description==null?"#": achievment.Description.Replace("\n", "").Replace("\r", "") + "|");
							stringBuilder.Append(achievment.Award == null?"#": achievment.Award.Replace("\n", "").Replace("\r", "") + "|");
							stringBuilder.Append(achievment.Comment == null?"#": achievment.Comment.Replace("\n", "").Replace("\r", "") + "|");
							stringBuilder.Append(achievment.MaxAmount + "|");
							stringBuilder.Append(achievment.Progress + "|");
							stringBuilder.Append(achievment.CategoriesIdName == null?"#": achievment.CategoriesIdName + "|");
							stringBuilder.Append(achievment.AchievedIconName == null?"#": achievment.AchievedIconName);
							stringBuilder.Append("\", \"|\")");
							stringBuilder.AppendLine();
							tw.Write(stringBuilder.ToString());
						}
					}

					bytes = ms.ToArray();
				}
				System.Diagnostics.Debug.WriteLine(bytes);

				string filename = "Achievments.txt";
				return File(bytes, "application/force-download", filename);
			}
			catch { }
			return new JsonResult("failed");
		}
		/*
		public IActionResult OnGetTryBackup()
		{
			using (MemoryStream ms = new MemoryStream())
			{
				using (TextWriter tw = new StreamWriter(ms, System.Text.Encoding.UTF8))
				{
					foreach (var achievment in Achievments)
					{
						tw.Write(Environment.NewLine +
							achievment.Name +
							achievment.Description +
							achievment.Award +
							achievment.Comment +
							achievment.MaxAmount +
							achievment.Progress +
							achievment.CategoriesIdName +
							achievment.AchievedIconName
							);
					}
				}

				var contentType = "APPLICATION/octet-stream";
				var fileName = DateTime.Today + "achievment.txt";
				return File(ms, contentType, fileName);
			}
		}*/
		private Achievment? GetAchievmentById(int id)
		{
			return _context.AchievmentsCollection.Where(achievment => achievment.Id == id).FirstOrDefault();
		}
	}
}
