namespace WebApp;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Security;
using Xunit.Sdk;

public static class Utils
{
    public static int SumInts(int a, int b)
    {
        return a + b;
    }

    public static Arr CreateMockUsers()
    {
        // Read all mock users from the JSON file
        var read = File.ReadAllText(FilePath("json", "mock-users.json"));
        Arr mockUsers = JSON.Parse(read);
        Arr successFullyWrittenUsers = Arr();
        foreach (var user in mockUsers)
        {
            //user.password = "12345678";
            var userRecord = SQLQueryOne(
                @"INSERT INTO users(firstName,lastName,email,password)
                VALUES($firstName, $lastName, $email, $password)
            ", user);
            // If we get an error from the DB then we haven't added
            // the mock users, if not we have so add to the successful list
            if (!userRecord.HasKey("error"))
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
        // Read all mock users from the JSON file
        var read = File.ReadAllText(FilePath("json", "mock-users.json"));
        Arr mockUsers = JSON.Parse(read);
        Arr mockEmails = mockUsers.Map(user => user.email);

        //Get all users from the database
        //Arr usersInDb = SQLQueryOne("SELECT * FROM users");
        Arr usersInDb = SQLQuery("SELECT email FROM users");
        Arr emailsInDb = usersInDb.Map(user => user.email);        
        Arr successFullyRemovedUsers = Arr();

        foreach (var user in usersInDb)
        {
            if (mockEmails.Contains(user.email))
            {
            var removeThisRecords = SQLQueryOne(
            @"DELETE FROM users WHERE email = $email",
            new { email = user.email });  
            successFullyRemovedUsers.Push(user);
            }
        }
        return successFullyRemovedUsers;
    }

    public static bool IsPasswordGoodEnough(string password)
    {
        string passwordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$";

        // Check if the input string matches the password pattern
        if (Regex.IsMatch(password, passwordPattern))
        {
            Console.WriteLine("The password is strong enough.");
            return true;
        }
        else
        {
            Console.WriteLine("The password is not strong enough.");
            return false;
        }
    } 

    public static string[] ParseJsonToArray(string json)
    {
        return JsonConvert.DeserializeObject<string[]>(json);
    }

     public static string RemoveBadWords(string text)
    {
        //text = "This is a sample text with a certain word.";
        string replacement = "*bleep*";
        string lowcaseText = text.ToLower();

        // Read the JSON file
        var read = File.ReadAllText(FilePath("json", "badwords.json"));
        JObject badWordsObj = JObject.Parse(read);
        // Parse the JSON array
        // Extract the array of bad words from the "badwords" key
        JArray badWordsArr = (JArray)badWordsObj["badwords"];
        
        // Convert the JSON array to an array of strings
        string[] badWords = badWordsArr.ToObject<string[]>();

    foreach (var word in badWords)
        {
            string pattern = Regex.Escape(word);
            string newText = Regex.Replace(text, pattern, replacement, RegexOptions.IgnoreCase);
            text = newText;
        }
        
        return text;
    }
    public static Arr Count­Do­mains­FromU­se­rE­mails()
    {
        //Get all users from the database
        Arr usersInDb = SQLQuery("SELECT email FROM users");
        Arr emailsInDb = usersInDb.Map(user => user.email);

        //var uniqueDomains = new Arr();
        Dictionary<string, int> domainCounts = new Dictionary<string, int>();

        foreach(var email in emailsInDb)
        {
            //Extract the domain from the email address
            string domain = email.Substring(email.IndexOf('@') + 1);

            // Check if the domain already exists in the dictionary
            if (domainCounts.ContainsKey(domain))
            {
            // If it does, increment its count
            domainCounts[domain]++;
            }
            else
            {
            domainCounts[domain] = 1;
            }
        }
     
        // Convert dictionary entries into objects and add them to a new Arr
        Arr uniqueDomains = new Arr();
        
        foreach (var item in domainCounts)
        {
            uniqueDomains.Push(new { domain = item.Key, count = item.Value });
        }
        return uniqueDomains;
    }

}