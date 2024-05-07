using Xunit.Sdk;
using Xunit.Abstractions;
using Microsoft.VisualStudio.TestPlatform.Utilities;

namespace WebApp;
public static class Utils
{

    public static bool IsPasswordGoodEnough(string password)
    {

        bool strongPassword = false;

        if (password.Length > 7)
        {
            if (password.ToCharArray().Any(char.IsSymbol) || password.ToCharArray().Any(char.IsPunctuation)
            && password.ToCharArray().Any(char.IsUpper)
            && password.ToCharArray().Any(char.IsLower)
            && password.ToCharArray().Any(char.IsDigit))
            {
                strongPassword = true;
            }
        }

        return strongPassword;
    }

    
        public static string RemoveBadWords(string inputWord)
        {

            string cencor = "****";
            string readBadWords = File.ReadAllText(Path.Combine("json", "bad-words.json"));
            dynamic badWords = JSON.Parse(readBadWords);

            foreach (var word in badWords)
            {
                if (inputWord.Contains(word))
                {
                    Console.WriteLine(inputWord);
                    inputWord = inputWord.Replace(word, cencor);
                    Console.WriteLine(inputWord);
                }
                else
                {
                    Log("No fkedup word was used");
                }
            }

            return inputWord;
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
        var read = File.ReadAllText(FilePath("json", "mock-users.json"));
        Arr mockUsers = JSON.Parse(read);
        Arr successRemovedMockUsers = Arr();

        Arr usersInDb = SQLQuery("SELECT email FROM users");
        //create array of users based on user.email
        Arr emailsInDb = usersInDb.Map(user => user.email);
        //compare and filter. Only keep emails that exist in mockUser.email (json file)
        Arr mockUsersInDb = mockUsers.Filter(mockUser => emailsInDb.Contains(mockUser.email));
        //{"firstName":"Andreas","lastName":"Syphus","email":"asyphus8@odnoklassniki.ru"},

        Arr mockuserEmail = mockUsersInDb.Map(mockuser => mockuser.email);

        foreach (var email in mockuserEmail)
        {
            var removeUser = SQLQueryOne(
     @"DELETE FROM users WHERE email = '$email'", email
    //DELETE FROM users WHERE email = 'rstonardrr@wunderground.com';
 );

            successRemovedMockUsers.Push(email);
        }
        return successRemovedMockUsers;
    }

}