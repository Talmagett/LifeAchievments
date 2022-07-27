using Microsoft.EntityFrameworkCore;
using LifeAchievments.Models;

namespace LifeAchievments.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Achievment> AchievmentsCollection { get; set; }
        public DbSet<Category> CategoryCollection { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
    }
}
