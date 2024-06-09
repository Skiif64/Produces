namespace Produces.Example.Repositories;

public class UserRepository
{
    private static readonly Dictionary<string, User> _users = new();

    public static void Create(User user) => _users.Add(user.Email, user);
    public static User? GetByEmail(string email) => _users.GetValueOrDefault(email);
}

public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public User(string email, string password)
    {
        Id = Guid.NewGuid();
        Email = email;
        Password = password;
    }
}