using FracturedJson.Parsing;
using Xunit;
namespace WebApp;
public class UtilsTest
{
    private static readonly Arr mockUsers = JSON.Parse(
        File.ReadAllText(FilePath("json", "mock-users.json"))
        );
   [Fact]
    public void TestSumInt()
    {
        Assert.Equal(12, Utils.SumInts(7, 5));
        Assert.Equal(-3, Utils.SumInts(6, -9));
    }

    [Fact]
    public void TestSumChars()
    {
        Assert.False(Utils.IsPasswordGoodEnough("hellom"));
        Assert.False(Utils.IsPasswordGoodEnough("hellothis"));
        Assert.False(Utils.IsPasswordGoodEnough("howareyou8"));
        Assert.False(Utils.IsPasswordGoodEnough("Howareyoufrend"));
        Assert.True(Utils.IsPasswordGoodEnough("Hellomyfrend1!"));
        Assert.False(Utils.IsPasswordGoodEnough("Hellomyfrend1k"));
    }
    [Fact]
    public void TestRemoveBadWords()
    {
        
        string text = "Hello, how are you, you bum. fuck you please!";
        string replacement = "bunny";

        string result = Utils.RemoveBadWords(text, replacement);

        string expectation = "Hello, how are you, you bum. bunny you please! ";

        Assert.Equal(expectation, result);

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
        Arr allUsersInDb = SQLQuery("SELECT email FROM users");

        Arr mockUsersInDb = allUsersInDb.Map(
            user => user.email.contains("1@"));

        var result = Utils.RemoveMockUsers();
        Console.WriteLine($"The amount of users found in {mockUsersInDb.Length} should");
        Console.WriteLine($"be same as the ones in {result.Length}");
        Console.WriteLine($"The test also asserts that these two Arrs are the same");
        Assert.Equivalent(mockUsersInDb, result);


    }

    // Now write the two last ones yourself!
    // See: https://sys23m-jensen.lms.nodehill.se/uploads/videos/2021-05-18T15-38-54/sysa-23-presentation-2024-05-02-updated.html#8
}