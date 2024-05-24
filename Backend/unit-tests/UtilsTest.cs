using FracturedJson.Parsing;
using Xunit;
namespace WebApp;
public class UtilsTest
{
    private static readonly Arr mockUsers = JSON.Parse(
        File.ReadAllText(FilePath("json", "mock-users.json"))
        );
    private static readonly Arr mockDomains = JSON.Parse(
        File.ReadAllText(FilePath("json", "mock-domains.json"))
    );


   [Fact]
    public void TestSumInt()
    {
        Assert.Equal(12, Utils.SumInts(7, 5));
        Assert.Equal(-3, Utils.SumInts(6, -9));
    }

    [Theory]
    [InlineData("abC9#fgh", true)]  // ok
    [InlineData("stU5/xyz", true)]  // ok too
    [InlineData("abC9#fg", false)]  // too short
    [InlineData("abCd#fgh", false)] // no digit
    [InlineData("abc9#fgh", false)] // no capital letter
    [InlineData("abC9efgh", false)] // no special character
    public void TestIsPasswordGoodEnough(string password, bool expected)
    {
        Assert.Equal(expected, Utils.IsPasswordGoodEnough(password));
    }

    [Theory]
    [InlineData(
        "---",
        "Hello, I am going through hell. Hell is a real fucking place " +
            "outside your goddamn comfy tortoiseshell!",
        "Hello, I am going through ---. --- is a real --- place " +
            "outside your --- comfy tortoiseshell!"
    )]
    [InlineData(
        "---",
        "Rhinos have a horny knob? (or what should I call it) on " +
            "their heads. And doorknobs are damn round.",
        "Rhinos have a --- ---? (or what should I call it) on " +
            "their heads. And doorknobs are --- round."
    )]
    public void TestRemoveBadWords(string replaceWith, string original, string expected)
    {
        Assert.Equal(expected, Utils.RemoveBadWords(original, replaceWith));
    }

    [Fact]
    public void TestCreateMockUsers()
    {
        // Get all users from the database
        Arr usersInDb = SQLQuery("SELECT email FROM users");
        Arr emailsInDb = usersInDb.Map(user => user.email);
        // Only keep the mock users not already in db
        Arr mockUsersNotInDb = mockUsers.Filter(
            mockUser => !emailsInDb.Contains(mockUser.email)
        );
        // Get the result of running the method in our code
        var result = Utils.CreateMockUsers();
        // Assert that the CreateMockUsers only return
        // newly created users in the db
        Console.WriteLine($"The test expected that {mockUsersNotInDb.Length} users should be added.");
        Console.WriteLine($"And {result.Length} users were added.");
        Console.WriteLine("The test also asserts that the users added " +
            "are equivalent (the same) as the expected users!");
        Assert.Equivalent(mockUsersNotInDb, result);
        Console.WriteLine("The test passed!");
    }

    [Fact]
    public void TestRemoveMockUsers()
    {
        Arr mockUsersFromDb = SQLQuery("SELECT * FROM users");
        Arr mockUsersShouldBeDeleted = Arr();

        foreach(var user in mockUsersFromDb)
        {
            foreach(var mock in mockUsers)
            if (mock.email == user.email)
            {
                user.Delete("password");
                mockUsersShouldBeDeleted.Push(mock);
            }
        }

        Arr deletedUsers = Utils.RemoveMockUsers();

        Console.WriteLine($"The amount of users found in {mockUsersShouldBeDeleted.Length} should");
        Console.WriteLine($"be same as the ones in {deletedUsers.Length}");
        Console.WriteLine($"The test also asserts that these two Arrs are the same");
        Assert.Equivalent(deletedUsers, mockUsersShouldBeDeleted);
    }

    [Fact]
    public void TestCountDomainsFromUserEmails()
    {

        Arr countDomainsUsingQuery = SQLQuery(@"SELECT 
        SUBSTR(email, INSTR(email, '@') + 1) AS domain, 
        COUNT(*) AS domain_count
        FROM users
        GROUP BY domain
        ORDER BY domain_count DESC;");

        Obj countDomainsFromQuery = Obj();

        foreach(var row in countDomainsUsingQuery)
        {
            countDomainsFromQuery[row.domain] = row.domain_count;
        }

        Obj countDomainsMethod = Utils.CountDomainsFromUserEmails();

        Console.WriteLine($"The number of mock domains"); 
        Assert.Equivalent(countDomainsMethod, countDomainsFromQuery); 

    }
}