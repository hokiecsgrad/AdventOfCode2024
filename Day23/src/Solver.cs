using AdventOfCode.Common;

namespace AdventOfCode.Day23;

public class Solver
{
    public string SolvePart1(string[] data)
    {
        (List<string> computers, Dictionary<string, HashSet<string>> pairs) parsed =
            ParseData(data);
        List<string> comps = parsed.computers;
        Dictionary<string, HashSet<string>> pairs = parsed.pairs;

        List<List<string>> trips = GetTrips(comps, pairs);

        long total = trips.Where(c => c[0].StartsWith("t") || c[1].StartsWith("t") || c[2].StartsWith("t")).Count();

        return total.ToString();
    }

    public string SolvePart2(string[] data)
    {
        return String.Empty;
    }

    public List<List<string>> GetTrips(List<string> comps, Dictionary<string, HashSet<string>> pairs)
    {
        List<List<string>> trips = new();

        foreach (string comp in comps)
        {
            List<List<string>> allTrips = FindConnections(comp, new List<string>() { comp }, pairs);
            foreach (List<string> trip in allTrips)
            {
                List<string> orderedTrip = trip.OrderBy(x => x).ToList();
                if (orderedTrip.Count == 3 && !trips.Any(t => t.SequenceEqual(orderedTrip)))
                    trips.Add(orderedTrip);
            }
        }

        return trips;
    }

    public List<List<string>> FindConnections(
        string comp,
        List<string> curr,
        Dictionary<string, HashSet<string>> pairs)
    {
        if (curr.Count == 3 && pairs[comp].Contains(curr[0])) return new List<List<string>>() { curr };
        if (curr.Count == 3) return new List<List<string>>();

        List<string> connections = pairs[comp].ToList();
        List<List<string>> allTrips = new();
        foreach (string connection in connections)
        {
            if (!curr.Contains(connection))
                allTrips.AddRange(FindConnections(connection, new List<string>(curr) { connection }, pairs));
        }

        return allTrips;
    }

    public (List<string>, Dictionary<string, HashSet<string>>) ParseData(string[] data)
    {
        Dictionary<string, HashSet<string>> pairs = new();
        HashSet<string> computers = new();
        foreach (string pair in data)
        {
            string first = pair.Substring(0, 2);
            string second = pair.Substring(3, 2);
            if (!pairs.ContainsKey(first))
                pairs.Add(first, new());
            pairs[first].Add(second);
            if (!pairs.ContainsKey(second))
                pairs.Add(second, new());
            pairs[second].Add(first);
            computers.Add(first);
            computers.Add(second);
        }

        List<string> sorted = computers.OrderBy(x => x).ToList();

        return (sorted, pairs);
    }
}