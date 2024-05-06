using FracturedJson.Parsing;
using Xunit;
namespace WebApp;
public class UtilsTest
{
   [Fact]
    public void TestSumInt()
    {
        Assert.Equal(12, Utils.SumInts(7, 5));
        Assert.Equal(-3, Utils.SumInts(6, -9));
    }

    [Fact]
    public void TestSumChars()
    {
        Assert.False(Utils.IsPasswordGoodEnough("hellom"));
        Assert.False(Utils.IsPasswordGoodEnough("hellothis"));
        Assert.False(Utils.IsPasswordGoodEnough("howareyou8"));
        Assert.False(Utils.IsPasswordGoodEnough("Howareyoufrend"));
        Assert.True(Utils.IsPasswordGoodEnough("Hellomyfrend1!"));
        Assert.False(Utils.IsPasswordGoodEnough("Hellomyfrend1k"));
    }
    [Fact]
    public void TestRemoveBadWords()
    {
        var read = File.ReadAllText(FilePath("json", "bad-words.json"));
        Arr badwords = JSON.Parse(read).badwords;

        string text = "Hello, how are you, you bum. fuck you please!";
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
                Console.WriteLine(word);

            }
            if (goodWord == true)
            {
                newText += word + " ";
            }
            else
            {
                newText += "rabbit ";
            }
        }
        Log(newText);
    }
}