using AdventOfCode.Common;

namespace AdventOfCode.Day22;

public class Solver
{
    public string SolvePart1(string[] data)
    {
        long total = 0;

        long secret;
        foreach (string item in data)
        {
            secret = long.Parse(item);
            for (int i = 0; i < 2000; i++)
            {
                secret = Step1(secret);
                secret = Step2(secret);
                secret = Step3(secret);
            }
            total += secret;
        }

        return total.ToString();
    }

    public string SolvePart2(string[] data)
    {
        //data = new string[] { "1", "2", "3", "2024" };

        HashSet<string> uniqueSequences = new();
        Dictionary<long, List<(int, int)>> allChanges = new();

        long secret;
        Queue<int> seq = new Queue<int>();

        foreach (string item in data)
        {
            secret = long.Parse(item);
            allChanges.Add(secret, new List<(int, int)>());
            long prevSecret;
            for (int i = 0; i < 2000; i++)
            {
                prevSecret = secret;
                int pricePrev = (int)(prevSecret % 10);

                secret = Step1(secret);
                secret = Step2(secret);
                secret = Step3(secret);

                int priceCurr = (int)(secret % 10);
                int change = priceCurr - pricePrev;
                allChanges[long.Parse(item)].Add((change, priceCurr));
                if (seq.Count == 4)
                {
                    string key = string.Join(",", seq);
                    if (!uniqueSequences.Contains(key))
                        uniqueSequences.Add(key);
                    seq.Dequeue();
                }
                seq.Enqueue(change);
            }
        }

        int maxPriceTotal = 0;
        Console.WriteLine($"Found {uniqueSequences.Count} unique sequences.");
        int countNumSoFar = 0;
        foreach (string currSeq in uniqueSequences)
        {
            Console.WriteLine($"Testing sequence: {currSeq}");
            int[] matchSeq = currSeq.Split(',').Select(int.Parse).ToArray();
            List<int> maxPrices = new();
            int maxPriceCurr = 0;
            foreach (var item in allChanges)
            {
                List<(int change, int price)> chgprice = item.Value;

                for (int i = 0; i <= chgprice.Count - matchSeq.Length; i++)
                {
                    // Compare 4 consecutive integers in largeList to the pattern
                    bool match = true;
                    for (int j = 0; j < matchSeq.Length; j++)
                    {
                        if (chgprice[i + j].change != matchSeq[j])
                        {
                            match = false;
                            break;
                        }
                    }

                    if (match)
                    {
                        maxPrices.Add(chgprice[i + matchSeq.Length - 1].price);
                        break;
                    }
                }
            }

            maxPriceCurr = maxPrices.Sum();
            if (maxPriceCurr > maxPriceTotal)
            {
                maxPriceTotal = maxPriceCurr;
                Console.WriteLine($"Found a new max, {maxPriceTotal}.");
                Console.WriteLine($"With {maxPrices.Count} vendors matching the sequence.");
            }

            countNumSoFar++;
            Console.WriteLine($"{uniqueSequences.Count - countNumSoFar} sequences left to check.");
        }

        // 1882 is too low
        return maxPriceTotal.ToString();
    }

    public long Step1(long secret)
    {
        // Calculate the result of multiplying the secret number by 64. 
        // Then, mix this result into the secret number. 
        // Finally, prune the secret number.
        long mul64 = secret * 64;
        secret ^= mul64;
        secret %= 16777216;
        return secret;
    }

    public long Step2(long secret)
    {
        // Calculate the result of dividing the secret number by 32. 
        // Round the result down to the nearest integer. 
        // Then, mix this result into the secret number. 
        // Finally, prune the secret number.
        long div32 = (int)(secret / 32);
        secret ^= div32;
        secret %= 16777216;
        return secret;
    }

    public long Step3(long secret)
    {
        // Calculate the result of multiplying the secret number by 2048. 
        // Then, mix this result into the secret number. 
        // Finally, prune the secret number.
        long mul2048 = secret * 2048;
        secret ^= mul2048;
        secret %= 16777216;
        return secret;
    }
}