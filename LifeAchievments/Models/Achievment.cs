namespace LifeAchievments.Models
{
    public class Achievment
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<Category>? Categories { get; set; }
        public string? Description { get; set; }
        public string? Award { get; set; }
        public string? Comment { get; set; }
        public int MaxAmount { get; set; } //if 0 incremental if 1 boolean if 1+ counting 
        public int Progress { get; set; }
        public State State { get; set; }//not settable
        public string? AchievedIconTitle { get; set; }
        public string? UnachievedIconTitle { get; set; }
    }
    public enum State { 
        Uncompleted,
        Completed,
        Incremental
    }
}
