namespace HerokuApp.TestData
{
    public record TestUser(string Login, string Password);

    public static class Users
    {
        public static TestUser ValidUser { get; } = new("tomsmith", "SuperSecretPassword!");
        public static TestUser InvalidUser { get; } = new("tomsmith", "wrong-password");
    }
}
