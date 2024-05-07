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
        
        string text = "Hello, how are you, you bum. fuck you please!";
        string replacement = "bunny";

        string result = Utils.RemoveBadWords(text, replacement);

        string expectation = "Hello, how are you, you bum. bunny you please! ";

        Assert.Equal(expectation, result);

    }
}