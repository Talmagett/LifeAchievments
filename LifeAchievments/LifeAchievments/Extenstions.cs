namespace LifeAchievments
{
    public static class Extenstions
    {
        public static string GetHerokuConnectionString()
        {
            /*
            string connectionUrl = Environment.GetEnvironmentVariable("postgres://vhsheenxvugshb:b6258586aae5ac3ecf6746f9432ba6896ffe2258d01315fc34c8fb500c3b8d99@ec2-34-199-68-114.compute-1.amazonaws.com:5432/dc4rvderfarvth");

            var databaseUri = new Uri(connectionUrl);

            string db = databaseUri.LocalPath.TrimStart('/');
            string[] userInfo = databaseUri.UserInfo.Split(':', StringSplitOptions.RemoveEmptyEntries);
            */
            string userID = "vhsheenxvugshb";
            string password = "b6258586aae5ac3ecf6746f9432ba6896ffe2258d01315fc34c8fb500c3b8d99";
            string Host = "ec2-34-199-68-114.compute-1.amazonaws.com";
            string port = "5432";
            string database = "dc4rvderfarvth";
            string Connection= $"User ID={userID};Password={password};Host={Host};Port={port};Database={database};Pooling=true;SSL Mode=Require;Trust Server Certificate=True;";
            System.Diagnostics.Debug.WriteLine(Connection);
            return Connection;
        }
    }
}

/*
  Host
    ec2-34-199-68-114.compute-1.amazonaws.com
Database
    dc4rvderfarvth
User
    vhsheenxvugshb
Port
    5432
Password
    b6258586aae5ac3ecf6746f9432ba6896ffe2258d01315fc34c8fb500c3b8d99
URI
    postgres://vhsheenxvugshb:b6258586aae5ac3ecf6746f9432ba6896ffe2258d01315fc34c8fb500c3b8d99@ec2-34-199-68-114.compute-1.amazonaws.com:5432/dc4rvderfarvth
Heroku CLI
    heroku pg:psql postgresql-contoured-31560 --app personal-achievments
*/
