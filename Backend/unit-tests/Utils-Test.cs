using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace WebApp;
public class UtilsTest
{
    // The following lines are needed to get 
    // output to the Console to work in xUnit tests!
    // (also needs the using Xunit.Abstractions)
    // Note: You need to use the following command line command 
    // dotnet test --logger "console;verbosity=detailed"
    // for the logging to work
    private readonly ITestOutputHelper output;
    public UtilsTest(ITestOutputHelper output)
    {
        this.output = output;
    }


    [Fact]
    // A simple initial example
    public void TestSumInt()
    {
        Assert.Equal(12, Utils.SumInts(7, 5));
        Assert.Equal(-3, Utils.SumInts(6, -9));
    }

   [Fact]
    public void TestCreateMockUsers()
    {
        // Read all mock users from the JSON file
        var read = File.ReadAllText(FilePath("json", "mock-users.json"));
        Arr mockUsers = JSON.Parse(read);
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
        output.WriteLine($"The test expected that {mockUsersNotInDb.Length} users should be added.");
        output.WriteLine($"And {result.Length} users were added.");
        output.WriteLine("The test also asserts that the users added " +
            "are equivalent (the same) to the expected users!");
        Assert.Equivalent(mockUsersNotInDb, result);
        output.WriteLine("The test passed!");
    }

    
    [Fact]
    public void TestRemoveMockUsers()
    {
        // Read all mock users from the JSON file
        var read = File.ReadAllText(FilePath("json", "mock-users.json"));
        Arr mockUsers = JSON.Parse(read);
        // Get all users from the database
        Arr usersInDb = SQLQuery("SELECT email FROM users");
        Arr emailsInDb = usersInDb.Map(user => user.email);
        // Only keep the mock users not already in db
        Arr mockUsersInDb = mockUsers.Filter(
            mockUser => emailsInDb.Contains(mockUser.email)
        );
        // Get the result of running the method in our code
        var result = Utils.RemoveMockUsers();
        // Assert that the RemoveMockUsers only return
        // newly removed users from the db
        output.WriteLine($"The test expected that {mockUsersInDb.Length} users should be removed.");
        output.WriteLine($"And {result.Length} users were removed.");
        output.WriteLine("The test also asserts that the users removed " +
            "are equivalent (the same) to the expected users!");
        Assert.Equivalent(mockUsersInDb, result);
        output.WriteLine("The test passed!");
    }

     [Theory]
     [InlineData("aAbBcC9!", true)]
     [InlineData("aAbB9!", false)]
     [InlineData("abcdef9!", false)]
     [InlineData("ABCDEF9!", false)]
     [InlineData("AbCdEf?!", false)]
     [InlineData("AbCdEf12", false)]     
    public void TestIsPasswordGoodEnough(string password, bool isEnough)
    {
            // Arrange

            // Act
            var result = Utils.IsPasswordGoodEnough(password);

            // Assert
            Assert.Equal(isEnough, result);
    }

     [Theory]
     [InlineData("I love you!", "*bleep*", "I love you!")]
     [InlineData("You bitch", "*bleep*", "You *bleep*")]
    // [InlineData("You fucking asshole", "*bleep*", "You *bleep* *bleep*")]
     [InlineData("Bastard", "*bleep*", "*bleep*")]
     [InlineData("fuckfuck", "*bleep*", "*bleep**bleep*")]
     [InlineData("fuckyou!", "*bleep*", "*bleep*you!")]  
    public void TestRemo­ve­Bad­Words(string inputText, string replacementText, string expectedText)
    {
            // Act
            var result = Utils.RemoveBadWords(inputText);
            
            // Assert
            Assert.Equal(expectedText, result);
        
    }

    [Fact]
    public void TestCount­Do­mains­FromU­se­rE­mails()
    {
        Arr result = Utils.Count­Do­mains­FromU­se­rE­mails();
        
        int totalCount = 0;
        foreach (var item in result)
        {
            totalCount += item.count;
        }

        //1. test - sum of domain counts
        //Get all users from the database
        Arr usersInDb = SQLQuery("SELECT email FROM users");
        var emailCount = usersInDb.Length;
        Assert.Equal(totalCount, emailCount);

        //2. test format - contains > 0 "." and 0 "@" (in each domainname)
        bool hasCorrectFormat = false;
        foreach (var item in result)
        {
            if (!item.domain.Contains(".") || item.domain.Contains("@"))
            {
                hasCorrectFormat = false;
                break;
            }
            else hasCorrectFormat = true;
        }
        Assert.True(hasCorrectFormat);
    }
}