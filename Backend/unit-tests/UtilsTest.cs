


namespace WebApp;
public class UtilsTest(Xlog Console)
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
    public void TestRemoveMockUsers()
  {
      var read = File.ReadAllText(Path.Combine("json", "MOCK-DATA.json"));
      Arr mockUsers = JSON.Parse(read);
      Arr usersInDb = SQLQuery("Select * FROM users");
      Arr emailsInDb = usersInDb.Map(user => user.email);
      Arr mockUserInDb = mockUsers.Filter(
          mockUser => emailsInDb.Contains(mockUser.email));

      var result = Utils.DeleteMockUsers();

      Assert.Equal(mockUserInDb.Length, result.Length);
  } 
*/

    [Fact]
    public void TestCreateMockUsers()
    {
        // Read all mock users from the JSON file
        var read = File.ReadAllText(FilePath("json", "MOCK-DATA.json"));
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
    public void TestCountDomainsFromUserEmails()
{
    var expectedDomainCounts = Utils.CountDomainsFromUserEmails();
    var actualDomainCounts = new Obj();

    var query = SQLQuery(@"SELECT SUBSTR(email, INSTR(email, '@') + 1) AS domain,
        COUNT(*) AS count FROM users GROUP BY domain ORDER BY count DESC;");

    foreach (var email in query)
    {
        actualDomainCounts[$"{email.domain}"] = email.count;
    }

    foreach (var domain in expectedDomainCounts.GetKeys())
    {
        string expected = $"{domain} {expectedDomainCounts[domain]}";
        string actual = $"{domain} {actualDomainCounts[domain]}";
        Assert.Equal(expected, actual);

    }
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


}
