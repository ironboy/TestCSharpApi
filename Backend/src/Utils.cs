using Xunit.Sdk;

namespace WebApp;

public static class Utils
{
        // Read all mock users from file
    private static readonly Arr mockUsers = JSON.Parse(
        File.ReadAllText(FilePath("json", "mock-users.json"))
    );
    public static int SumInts(int a, int b)
    {
        return a + b;
    }
    public static bool IsPasswordGoodEnough(string password)
    {
        string symbols = "@|!#$%&/()=?»«@£§€{}.-;'<>_,";
        int lower = 0;
        int upper = 0;
        int digit = 0;
        int uniq = 0;

        foreach (char symb in symbols)
         {
            if (password.Contains(symb))
            {
                uniq++;
            }
        }
        foreach(var let in password) 
        {
            if (char.IsUpper(let))
            {
                lower++;
            }
            if (char.IsLower(let))
            {
                upper++;
            }
            if (char.IsDigit(let))
            {
                digit++;
            }
        }
        if(password.Length < 8 || digit < 1 || upper < 1 || lower < 1 || uniq < 1)
        {
            return false;
        }
        return true;
    }
    public static string RemoveBadWords(string text, string replacement)
    {
        var read = File.ReadAllText(FilePath("json", "bad-words.json"));
        Arr badwords = JSON.Parse(read).badwords;

        string newText = "";

        string[] singleWord = text.Split(" ");

        foreach (var word in singleWord)
        {
            bool goodWord = true;
            foreach (var badW in badwords)
            {
                if (badW == word)
                {
                    goodWord = false;
                }
            }
            if (goodWord == true)
            {
                newText += word + " ";
            }
            else
            {
                newText += replacement + " ";
            }
        }
        return newText;
    }
    public static Arr CreateMockUsers()
    {
        Arr successFullyWrittenUsers = Arr();
        foreach (var user in mockUsers)
        {
            string[] half = user.email.Split("@");
            user.password = half[0];
            user.email = half[0] + "1@" + half[1];

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
    public static Arr RemoveMockUsers()
    {
        Arr deletedUsers = Arr();
        Arr users = SQLQuery(@"
        SELECT * 
        FROM users 
        WHERE email
        LIKE '%1@%'");

        var usedToBees = users.Map(user => {
            Log(user.email);
        return user.email; // Return email for further processing if needed
        });
        
       foreach(var user in users)
        {
            user.Delete("password");
            deletedUsers.Push(user);
            Log(user);
        }

        var result = SQLQuery(@"
        DELETE FROM users
        WHERE email LIKE '%1@%'");

        return deletedUsers;
    }
}