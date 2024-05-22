using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;

namespace WebApp;
public static class Utils
{
    public static bool IsPasswordStrongEnough(string password)
    {
        // Kontrollera att lösenordet är minst 8 tecken långt
        if (password.Length < 8)
            return false;

        // Kontrollera att lösenordet innehåller liten bokstav
        if (!Regex.IsMatch(password, @"[a-z]"))
            return false;

        // Kontrollera att lösenordet innehåller stor bokstav
        if (!Regex.IsMatch(password, @"[A-Z]"))
            return false;

        // Kontrollera att lösenordet innehåller en siffra
        if (!Regex.IsMatch(password, @"\d"))
            return false;

        // Kontrollera att lösenordet innehåller ett specialtecken
        if (!Regex.IsMatch(password, @"[^a-zA-Z\d]"))
            return false;

        return true;

    }
    public static HashSet<string> BadWords { get;private set; }

    public static void LoadBadWords(string filepath)
    {
        var BwReadingList = File.ReadAllText(filepath);
        var data = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(BwReadingList);
        BadWords = new HashSet<string>(data["badwords"], StringComparer.OrdinalIgnoreCase);
    }

    public static string RemoveBadWord(string input , string replacement)
    {
        foreach(var word in BadWords)
        {
            var badwordz = new Regex("\\b" + Regex.Escape(word) + "\\b", RegexOptions.IgnoreCase);
            input = badwordz.Replace(input, replacement);
        }
        return input;
    }

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
           // user.password = "12345678";
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
                user["id"] = result["id"];
                successFullyWrittenUsers.Push(user);
            }
        }
        return successFullyWrittenUsers;
    }


}