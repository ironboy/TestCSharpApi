namespace WebApp;

using System.Data;

public class UtilsTest(Xlog Console)
{

    // Read all mock users from file
    private static readonly Arr mockUsers = JSON.Parse(
        File.ReadAllText(FilePath("json", "mock-users.json"))
    );



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
    [InlineData("abC9#fgh", true)]  // ok
    [InlineData("stU5/xyz", true)]  // ok too
    [InlineData("abC9#fg", false)]  // too short
    [InlineData("abCd#fgh", false)] // no digit
    [InlineData("abc9#fgh", false)] // no capital letter
    [InlineData("abC9efgh", false)] // no special character
    public void TestIsPasswordGoodEnoughRegexVersion(string password, bool expected)
    {
        Assert.Equal(expected, Utils.IsPasswordGoodEnoughRegexVersion(password));
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


    public Arr RemoveMockUsers()
    {
        Console.WriteLine("NOW I AM USING CONSOLE ONCE.");
        Arr removedMockUsers = Arr();
        Arr mockUserToRemove = UtilsTest.mockUsers.Filter(
            mockUser => mockUser.IsMockUser);

        foreach (var user in mockUserToRemove)
        {
            removedMockUsers.Push(new
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
            });
        }
        return removedMockUsers;
    }

/*
    [Fact]
    public void CountDomainsFromUserEmails()
    {
        var result = Obj();

        // SQL-fråga för att hämta alla e-postadresser från databasen
        string query = "SELECT email FROM users";


        // Hämta alla e-postadresser från databasen
        DataTable _db_sqlite3 = SQLQuery(query);

        foreach (DataRow row in _db.sqlite3.Rows)
        {
            // Hämta e-postadressen från raden
            string email = row["email"].ToString();

            // Hämta domänen från e-postadressen
            string domain = GetDomainFromEmail(email);

            // Om domänen redan finns i resultatet, öka antalet
            if (result.ContainsKey(domain))
            {
                result[domain]++;
            }
            // Annars, lägg till domänen i resultatet
            else
            {
                result[domain] = 1;
            }
        }

        foreach (var kvp in result)
        {
            Console.WriteLine($"{kvp.Key}: {kvp.Value}");
        }

    }
    */
}