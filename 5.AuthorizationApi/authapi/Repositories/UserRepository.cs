namespace Globomantics.Backend.Repositories;

public class UserRepository
{
    private List<UserAuthZ> userAuthZs =
    [
        new UserAuthZ { UserId = "1", SettingType = "displayBids", SettingValue = "true" },
        new UserAuthZ { UserId = "2", SettingType = "displayBids", SettingValue = "false" }
    ];

    public Dictionary<string, string> GetSettings(string userId)
    {
        return userAuthZs
            .Where(us => us.UserId == userId)
            .ToDictionary(us => us.SettingType, us => us.SettingValue);
    }
}
