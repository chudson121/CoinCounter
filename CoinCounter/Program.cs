// See https://aka.ms/new-console-template for more information
using NUnit.Framework;

Console.WriteLine("Hello, World!");

var coinvalidator = new CoinValidator();

Console.WriteLine(coinvalidator.CoinsToReturn(6));

Console.ReadLine();


public class CoinValidator
{
    const int penny = 1;
    const int nickel = 5;
    const int dime = 10;
    const int quarter = 25;
    const int halfdollar = 50;

    public int[] CoinsToReturn(int x)
    {
        List<int> coins = new();
        int runningTotal = x;

        do
        {
            runningTotal = IdentifyCoins(coins, runningTotal);

        } while (runningTotal > 0);

        return coins.ToArray();

    }

    private static int IdentifyCoins(List<int> coins, int runningTotal)
    {
        //if nickels are all out, we need to have a till tracker for each coin type, and return 

        if (runningTotal >= halfdollar) 
        {
            coins.Add(halfdollar);
            runningTotal -= halfdollar;
            return runningTotal;
        }

        if (runningTotal >= quarter)
        {
            coins.Add(quarter);
            runningTotal -= quarter;
            return runningTotal;
        }

        if (runningTotal >= dime)
        {
            coins.Add(dime);
            runningTotal -= dime;
            return runningTotal;
        }

        if (runningTotal >= nickel)
        {
            coins.Add(nickel);
            runningTotal -= nickel;
            return runningTotal;
        }

        if (runningTotal > 0)
        {
            coins.Add(penny);
            runningTotal -= penny;
            return runningTotal;
        }

        return runningTotal; //Should be zero

    }
}

[TestFixture]
public class CoinValidatorTests
{
    private CoinValidator coinvalidator;

    [SetUp]
    public void Setup()
    {
        coinvalidator = new CoinValidator();
    }

    [Test]
    public void CoinsToReturn_Returns()
    {
        var retval = coinvalidator.CoinsToReturn(1);
        Assert.IsTrue(retval.Count() >= 0);
        Assert.IsTrue(retval[0] >= 0);

    }

    [TestCase(1, ExpectedResult = new[] { 1 })]
    [TestCase(6, ExpectedResult = new[] { 5, 1 })]
    [TestCase(16, ExpectedResult = new[] { 10, 5, 1 })]
    [TestCase(25, ExpectedResult = new[] { 25 })]
    [TestCase(33, ExpectedResult = new[] { 25, 5, 1, 1, 1 })]
    [TestCase(52, ExpectedResult = new[] { 50, 1, 1 })]
    [TestCase(66, ExpectedResult = new[] { 50, 10, 5, 1 })]
    [TestCase(91, ExpectedResult = new[] { 50, 25, 10, 5, 1 })]
    [TestCase(99, ExpectedResult = new[] { 50, 25, 10, 10, 1, 1, 1, 1 })]
    public int[] CoinsToReturn_ReturnsMinimumCointCounts(int totalCoinValue)
    {
        var retval = coinvalidator.CoinsToReturn(totalCoinValue);
        return retval;

    }

}


