namespace Reservations.API;

public class UserRepository
{
    private readonly Dictionary<string, User> _users;

    public UserRepository()
    {
        _users = CreateUsers().ToDictionary(t => t.Id);
    }

    public Task<User> GetUserByIdAsync(string id)
    {
        return Task.FromResult(_users[id]);
    }

    private static IEnumerable<User> CreateUsers()
    {
        yield return new User("1", "Ada Lovelace", "@ada");
        yield return new User("2", "Alan Turing", "@complete");
    }
}