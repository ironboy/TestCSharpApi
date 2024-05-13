

namespace WebApp;
public class UtilsTest(Xlog output)
{
    
    [Fact]
    // A simple initial example
    public void TestSumInt()
    {
        Assert.Equal(12, Utils.SumInts(7, 5));
        Assert.Equal(-3, Utils.SumInts(6, -9));
    }
/*
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
    
    */

    [Fact]
    public void Testp()
    {
        Arr passInDb = SQLQuery("SELECT PASSWORD FROM users WHERE id=6");
        string password = passInDb[0]["password"].ToString();
        output.WriteLine("Your password is: "+password);
        bool isPasswordGoodEnough = Utils.IsPasswordGoodEnough(password); 

        Assert.True(isPasswordGoodEnough, "The password is not considered good enough.");

        if (isPasswordGoodEnough==false) {
            output.WriteLine("Not Good Enough");
        }

    }

}