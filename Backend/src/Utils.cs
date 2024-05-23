using System.Collections.Immutable;
using System.Data;
using System.Net.NetworkInformation;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SQLitePCL;


namespace WebApp;
public static class Utils
{
    public static int SumInts(int a, int b)
    {
        return a + b;
    }

    
    public static Arr CreateMockUsers()
    {
        // Read all mock users from the JSON file
        var read = File.ReadAllText(FilePath("json", "mock-data.json"));
        Arr mockUsers = JSON.Parse(read);
        Arr successFullyWrittenUsers = Arr();
        foreach (var user in mockUsers)
        {
            user.password = "12345678";
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
    




public static Arr DeleteMockUsers()
{
    var read = File.ReadAllText(Path.Combine("json", "MOCK-DATA.json"));
    Arr mockUsers = JSON.Parse(read);
    Arr successfullyDeletedUsers = Arr();
    foreach (var user in mockUsers)
    {
        var result = SQLQueryOne(@"DELETE FROM users WHERE email = $email", user);

        if (!result.HasKey("error"))
        {
            user.Delete("password");
            successfullyDeletedUsers.Push(user);

            // Print out the user that has been deleted
            Console.WriteLine($"User with email '{user["email"]}' has been deleted.");
        }
    }
    return successfullyDeletedUsers;
}




 public static Obj CountDomainsFromUserEmails()
{
    {
        Obj domainCount = Obj();

        Arr emailsInDb = SQLQuery("SELECT email FROM users");

        Arr emailAddresses = emailsInDb.Map(user => user["email"]);

        foreach (string email in emailAddresses) 
        {
            string domain = email.Split('@')[1];
            if (!domainCount.HasKey(domain)) {
                domainCount[domain] = 0;
            }
            domainCount[domain]++;
        }
/*
        List<KeyValuePair<string, int>> keyValuePairs = new List<KeyValuePair<string, int>>();
        foreach (var key in domainCount.GetKeys())
        {
            keyValuePairs.Add(new KeyValuePair<string, int>((string)key, (int)domainCount[key]));
        }

        keyValuePairs.Sort((x, y) => y.Value.CompareTo(x.Value));

        Obj sortedDomainCountObj = Obj();
        foreach (var pair in keyValuePairs)
        {
            sortedDomainCountObj[pair.Key] = pair.Value;
        }

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(sortedDomainCountObj);
        Console.ResetColor();*/
        return domainCount;

    }
}




  public static bool IsPasswordGoodEnough(string password)
    {
        return password.Length >= 8
            && password.Any(Char.IsDigit)
            && password.Any(Char.IsLower)
            && password.Any(Char.IsUpper)
            && password.Any(x => !Char.IsLetterOrDigit(x));
    }


    private static readonly Arr badWords = ((Arr)JSON.Parse(
        File.ReadAllText(FilePath("json", "bad-words.json"))
    )).Sort((a, b) => ((string)b).Length - ((string)a).Length);

    public static string RemoveBadWords(string comment, string replaceWith = "🤬🤬🤬")
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
}
