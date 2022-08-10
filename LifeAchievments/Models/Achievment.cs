namespace LifeAchievments.Models
{
    public class Achievment
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string CategoriesIdName { get; set; } = "";
        public IEnumerable<int>? CategoriesIDList
        {
            get
            {
                if (CategoriesIdName != "")
                {
                    string[] strings = CategoriesIdName.Split(',');
                    int[] ints = new int[strings.Length];
                    for (int i = 0; i < ints.Length; i++)
                    {
                        ints[i] = int.Parse(strings[i]);
                    }
                    return ints;
                }
                else return null;
            }
        }
        public string? Award { get; set; }
        public string? Comment { get; set; }
        public int MaxAmount { get; set; } //if 0 incremental if 1 boolean if 1+ counting 
        public int Progress { get; set; }
        public State State { get; set; }//not settable
        public string AchievedIconName { get; set; } = "award.png";
    }
    public enum State
    {
        Uncompleted,
        Completed,
        Incremental
    }
}
