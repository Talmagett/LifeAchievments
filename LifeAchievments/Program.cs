using LifeAchievments.Data;
using LifeAchievments;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

Host.CreateDefaultBuilder(args)
.ConfigureWebHostDefaults(webBuilder =>
{
    var port = Environment.GetEnvironmentVariable("PORT");
    webBuilder.UseUrls($"http://+:{port}");
  
 //   webBuilder.UseStartup<Startup> & lt; Startup & gt; ();

});


var IsDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";

var connectionString = IsDevelopment ? builder.Configuration.GetConnectionString("DefaultConnection") : Extenstions.GetHerokuConnectionString();

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AddPageRoute("/Achievments", "");
});
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");

var app = builder.Build();

//builder.Serviceservices.AddDbContext<TodoContext>(options => options.UseNpgsql(connectionString));

// Configure the HTTP request pipeline.
/*if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}*/

app.UseDefaultFiles();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
