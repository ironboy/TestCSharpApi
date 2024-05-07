namespace WebApp;

public static class Utils
{
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
}