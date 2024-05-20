using System.Collections.Immutable;
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

/*
        private static readonly Arr goodWords = ((Arr)JSON.Parse(
        File.ReadAllText(FilePath("json", "good-words.json"))
    )).Sort((a, b) => ((string)b).Length - ((string)a).Length);
*/
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
            }
        }
        return successfullyDeletedUsers;

    }


    public static bool IsPasswordGoodEnough(string password)
{
     if (password.Length < 8 || 
        !password.Any(char.IsLower) || 
        !password.Any(char.IsUpper) || 
        !password.Any(char.IsDigit) || 
        password.All(char.IsLetterOrDigit))

     {
        return false;
    }
    else
    {
        return true;
    }
}
/*
    public static string RemoveBadWords(string comment, string replaceWith = "---")
    {
        comment = " " + comment;
        replaceWith = " " + replaceWith + "$1";
        goodWords.ForEach(bad =>
        {
            var pattern = @$" {bad}([\,\.\!\?\:\; ])";
            comment = Regex.Replace(
                comment, pattern, replaceWith, RegexOptions.IgnoreCase);
        });
        return comment[1..];
    }
    */






}