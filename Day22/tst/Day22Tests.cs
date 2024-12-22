using AdventOfCode.Common;
using AdventOfCode.Day22;
using Xunit;

namespace AdventOfCode.Day22.Tests;

public class Day22Tests
{
    public Day22Tests()
    {
        data = sampleInput.Split(
            '\n',
            StringSplitOptions.TrimEntries |
            StringSplitOptions.RemoveEmptyEntries
            );
    }

    [Fact]
    public void Part1_WithExampleData_ShouldBe5908254AfterTenSteps()
    {
        Solver solver = new();

        long secret = 123;

        secret = solver.Step1(secret);
        secret = solver.Step2(secret);
        secret = solver.Step3(secret);
        Assert.Equal(15887950, secret);

        secret = solver.Step1(secret);
        secret = solver.Step2(secret);
        secret = solver.Step3(secret);
        Assert.Equal(16495136, secret);

        secret = solver.Step1(secret);
        secret = solver.Step2(secret);
        secret = solver.Step3(secret);
        Assert.Equal(527345, secret);

        secret = solver.Step1(secret);
        secret = solver.Step2(secret);
        secret = solver.Step3(secret);
        Assert.Equal(704524, secret);

        secret = solver.Step1(secret);
        secret = solver.Step2(secret);
        secret = solver.Step3(secret);
        Assert.Equal(1553684, secret);

        secret = solver.Step1(secret);
        secret = solver.Step2(secret);
        secret = solver.Step3(secret);
        Assert.Equal(12683156, secret);

        secret = solver.Step1(secret);
        secret = solver.Step2(secret);
        secret = solver.Step3(secret);
        Assert.Equal(11100544, secret);

        secret = solver.Step1(secret);
        secret = solver.Step2(secret);
        secret = solver.Step3(secret);
        Assert.Equal(12249484, secret);

        secret = solver.Step1(secret);
        secret = solver.Step2(secret);
        secret = solver.Step3(secret);
        Assert.Equal(7753432, secret);

        secret = solver.Step1(secret);
        secret = solver.Step2(secret);
        secret = solver.Step3(secret);
        Assert.Equal(5908254, secret);
    }

    [Fact]
    public void TheNumber1_After2000Steps_ShouldBe8685429()
    {
        Solver solver = new();
        long secret = 1;

        for (int i = 0; i < 2000; i++)
        {
            secret = solver.Step1(secret);
            secret = solver.Step2(secret);
            secret = solver.Step3(secret);
        }

        Assert.Equal(8685429, secret);
    }

    [Fact]
    public void Part1_WithSampleData_ShouldBe37327623()
    {
        Solver solver = new();

        long total = long.Parse(solver.SolvePart1(data));

        Assert.Equal(37327623, total);
    }

    [Fact]
    public void GetSequence_FromExampleData_ShouldBe2113()
    {
        Solver solver = new();

        string[] testData = { "1", "2", "3", "2024" };

        Queue<int> winningSeq = new Queue<int>();
        int maxPriceTotal = 0;
        for (int i = 0; i < 2000; i++)
        {
            Queue<int> maxPriceSeq = new Queue<int>();
            List<int> maxPrices = new();
            long secret = long.Parse(testData[0]);
            long prevSecret;
            maxPriceSeq.Enqueue((int)(secret % 10));
            for (int j = 0; j < i; j++)
            {
                prevSecret = secret;
                int pricePrev = (int)(prevSecret % 10);

                secret = solver.Step1(secret);
                secret = solver.Step2(secret);
                secret = solver.Step3(secret);

                int priceCurr = (int)(secret % 10);
                int change = priceCurr - pricePrev;
                maxPriceSeq.Enqueue(change);
                if (maxPriceSeq.Count == 5)
                    maxPriceSeq.Dequeue();
            }

            if (maxPriceSeq.Count == 4)
            {
                int maxPriceCurr = 0;
                foreach (string item in testData)
                {
                    Queue<int> currPriceSeq = new Queue<int>();
                    secret = long.Parse(item);
                    currPriceSeq.Enqueue((int)(secret % 10));
                    int changeCount = 0;
                    bool sequenceFound = false;
                    while (!sequenceFound && changeCount < 2000)
                    {
                        prevSecret = secret;
                        int pricePrev = (int)(prevSecret % 10);

                        secret = solver.Step1(secret);
                        secret = solver.Step2(secret);
                        secret = solver.Step3(secret);

                        int priceCurr = (int)(secret % 10);
                        int change = priceCurr - pricePrev;
                        currPriceSeq.Enqueue(change);
                        if (currPriceSeq.Count == 5)
                            currPriceSeq.Dequeue();

                        if (currPriceSeq.SequenceEqual(maxPriceSeq))
                        {
                            maxPrices.Add(priceCurr);
                            sequenceFound = true;
                        }
                        changeCount++;
                    }
                }
                maxPriceCurr = maxPrices.Sum();
                if (maxPriceCurr > maxPriceTotal)
                {
                    maxPriceTotal = maxPriceCurr;
                    winningSeq = new Queue<int>(maxPriceSeq);
                }
            }
        }

        Assert.Equal(new Queue<int>(new[] { -2, 1, -1, 3 }), winningSeq);
        Assert.Equal(23, maxPriceTotal);
    }

    [Fact]
    public void Part2_WithSampleData_ShouldBeXXX()
    {
        throw new NotImplementedException();
    }

    private string[] data;
    private string sampleInput = """
1
10
100
2024
""";
}
