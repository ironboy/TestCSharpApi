namespace WebApp;

public class UtilsTest(Xlog Console)
{

    [Fact]
    public void TestIsPasswordGoodEnough()
    {
        bool strongPassword = Utils.IsPasswordGoodEnough("Benny123.");
        Assert.True(strongPassword);
    }

    
       [Fact]
    public void TestRemoveBadWords()
    {
        string input = "Hej ditt asshole";
        string expectedOutput = "Hej ditt ****";
        string actualOutput = Utils.RemoveBadWords(input);

        Assert.Equal(expectedOutput, actualOutput);
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
        Console.WriteLine($"The test expected that {mockUsersNotInDb.Length} users should be added.");
        Console.WriteLine($"And {result.Length} users were added.");
        Console.WriteLine("The test also asserts that the users added " +
            "are equivalent (the same) to the expected users!");
        Assert.Equivalent(mockUsersNotInDb, result);
        Console.WriteLine("The test passed!");
    }

    [Fact]
    public void TestRemoveMockUsers()
    {
        //read in json file
        var read = File.ReadAllText(FilePath("json", "mock-users.json"));
        Arr mockUsers = JSON.Parse(read);

        //select unique email from DB
        Arr usersInDb = SQLQuery("SELECT email FROM users");

        //create array of users based on user.email
        Arr emailsInDb = usersInDb.Map(user => user.email);

        //compare and filter. Only keep emails that exist in mockUser.email (json file)
        Arr mockUsersInDb = mockUsers.Filter(mockUser => emailsInDb.Contains(mockUser.email));

        //get result from our program
        var result = Utils.RemoveMockUsers();

        //show the equivalency of the two
        Assert.Equivalent(mockUsersInDb, result);
        //output.WriteLine("test ok");
    }


}

