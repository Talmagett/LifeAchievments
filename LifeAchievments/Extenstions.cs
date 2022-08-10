namespace LifeAchievments
{
    public static class Extenstions
    {
        public static string GetHerokuConnectionString()
        {
            string connectionUrl = Environment.GetEnvironmentVariable("postgres://vhsheenxvugshb:b6258586aae5ac3ecf6746f9432ba6896ffe2258d01315fc34c8fb500c3b8d99@ec2-34-199-68-114.compute-1.amazonaws.com:5432/dc4rvderfarvth");

            var databaseUri = new Uri(connectionUrl);

            string db = databaseUri.LocalPath.TrimStart('/');
            string[] userInfo = databaseUri.UserInfo.Split(':', StringSplitOptions.RemoveEmptyEntries);

            return $"User ID={userInfo[0]};Password={userInfo[1]};Host={databaseUri.Host};Port={databaseUri.Port};Database={db};Pooling=true;SSL Mode=Require;Trust Server Certificate=True;";
        }
    }
}
