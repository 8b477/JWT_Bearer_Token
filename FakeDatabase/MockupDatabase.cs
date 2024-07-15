using JWT_Bearer_Token.Model;

namespace JWT_Bearer_Token.FakeDatabase
{
    public static class MockupDatabase
    {

        public static string[] GetDataWeather()
        {
            return
            [
                "Freezing",
                "Bracing",
                "Chilly",
                "Cool",
                "Mild",
                "Warm",
                "Balmy",
                "Hot",
                "Sweltering",
                "Scorching"
            ];
        }

        public static UserModel[] GetDataUser()
        {
            return [
                    new UserModel(1, "li","lily@mail.be", "lily", ""),
                    new UserModel(2, "uwu","user@mail.be", "user", "User"),
                    new UserModel(3, "awa","admin@mail.be", "admin", "Admin")
                    ];
        }


    }
}
