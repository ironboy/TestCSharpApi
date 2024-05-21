using Xunit.Sdk;

namespace WebApp;

public static class Utils
{
        // Read all mock users from file
    private static readonly Arr mockUsers = JSON.Parse(
        File.ReadAllText(FilePath("json", "mock-users.json"))
    );
    private static readonly Arr badWords = ((Arr)JSON.Parse(
        File.ReadAllText(FilePath("json", "bad-words.json"))
    )).Sort((a, b) => ((string)b).Length - ((string)a).Length);
    public static int SumInts(int a, int b)
    {
        return a + b;
    }
    public static bool IsPasswordGoodEnough(string password)
    {
        return password.Length >= 8
            && password.Any(Char.IsDigit)
            && password.Any(Char.IsLower)
            && password.Any(Char.IsUpper)
            && password.Any(x => !Char.IsLetterOrDigit(x));
    }
    public static string RemoveBadWords(string comment, string replaceWith = "---")
    {

        comment = " " + comment;
        replaceWith = " " + replaceWith + "$1";
        badWords.ForEach(bad =>
        {
            var pattern = @$" {bad}([\,\.\!\?\:\; ])";
            comment = Regex.Replace(
                comment, pattern, replaceWith, RegexOptions.IgnoreCase);
        });
        return comment[1..];
    }
    public static Arr CreateMockUsers()
    {
        Arr successFullyWrittenUsers = Arr();
        foreach (var user in mockUsers)
        {
            string[] half = user.email.Split("@");
            user.email = half[0] + "1@" + half[1];

            var result = SQLQueryOne(
                @"INSERT INTO users(firstName,lastName,email,password)
                VALUES($firstName, $lastName, $email, $password)
            ", user);
            // If we get an error from the DB then we haven't added
            // the mock users, if not we have so add to the successful list
            if (!result.HasKey("error"))
            {
                // The specification says return the user list without password
                user.Delete("password");
                successFullyWrittenUsers.Push(user);
            }
        }
        return successFullyWrittenUsers;
    }
    public static Arr RemoveMockUsers()
    {
        Arr deletedUsers = new();
        Arr mockUsersInDb = SQLQuery(@"SELECT * FROM users WHERE email LIKE '%1@%'");

        foreach(var user in mockUsersInDb)
        {
            user.Delete("password");
            deletedUsers.Push(user);
        }

        var result = SQLQuery(@"
        DELETE FROM users
        WHERE email LIKE '%1@%'");

        return deletedUsers;
    }
}