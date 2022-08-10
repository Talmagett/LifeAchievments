using LifeAchievments.Data;
using LifeAchievments.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace LifeAchievments.Pages
{
    public class IconManagerModel : PageModel
    {
        [BindProperty]
        public IFormFile FileUploadAchieved { get; set; }
        public List<Icon> Icons { get; set; }
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly ApplicationDbContext _context;
        public IconManagerModel(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            _context = context;
            Icons = _context.IconsCollection.ToList();
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            string wwwPath = this._hostEnvironment.WebRootPath;
            string contentPath = this._hostEnvironment.ContentRootPath;

            string path = Path.Combine(this._hostEnvironment.WebRootPath, "images");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            //image loading
            if (FileUploadAchieved != null && FileUploadAchieved.Length > 0)
            {
                using (var stream = new FileStream(Path.Combine(path, FileUploadAchieved.FileName), FileMode.Create))
                {
                    await FileUploadAchieved.CopyToAsync(stream);
                }
                using (var memoryStream = new MemoryStream())
                {
                    await FileUploadAchieved.CopyToAsync(memoryStream);
                    //Upload the file if less than 2 MB
                    if (memoryStream.Length < 2097152)
                    {
                        var file = new Icon();
                        file.Name = FileUploadAchieved.FileName;
                        //file.Content = memoryStream.ToArray();
                        _context.IconsCollection.Add(file);
                        _context.SaveChanges();
                    }
                    else
                    {
                        ModelState.AddModelError("File", "The file is too large");
                    }
                }
            }
            return RedirectToPage("/IconManager");
        }
        public IActionResult OnGetDelete(int id)
        {
            Icon? icon = _context.IconsCollection.Where(icon => icon.Id == id).FirstOrDefault();
            if (icon == null)
                return NotFound();

            string wwwPath = this._hostEnvironment.WebRootPath;
            string contentPath = this._hostEnvironment.ContentRootPath;

            string path = Path.Combine(this._hostEnvironment.WebRootPath, "images");
            if (!Directory.Exists(path))
            {
                return NotFound();
            }
            // Check if file exists with its full path    
            try
            {
                if (System.IO.File.Exists(Path.Combine(path, icon.Name)))
                {
                    // If file found, delete it    
                    System.IO.File.Delete(Path.Combine(path, icon.Name));
                }
            }
            catch (IOException ioExp)
            {
                Console.WriteLine(ioExp.Message);
            }

            _context.IconsCollection.Remove(icon);
            _context.SaveChanges();
            Icons = _context.IconsCollection.ToList();
            return RedirectToPage("/IconManager");
        }
    }
}
