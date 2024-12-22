using System.Text;
using AdventOfCode.Common;

namespace AdventOfCode.Day09;

public class Solver
{
    public string SolvePart1(string[] data)
    {
        List<long> hdd = ExplodeMap(data[0]);
        hdd = Defrag(hdd);
        long checksum = CalcChecksum(hdd);

        return checksum.ToString();
    }

    public string SolvePart2(string[] data)
    {
        List<long> hdd = ExplodeMap(data[0]);
        hdd = DefragContiguous(hdd);
        long checksum = CalcChecksum(hdd);

        // 6415163644414 too high
        return checksum.ToString();
    }

    public List<long> ExplodeMap(string map)
    {
        List<long> hdd = new();
        int fileId = 0;
        for (int i = 0; i < map.Length; i += 2)
        {
            int blocksize = int.Parse(map[i].ToString());
            for (int j = 0; j < blocksize; j++)
                hdd.Add(fileId);
            if (i + 1 < map.Length)
            {
                int freespace = int.Parse(map[i + 1].ToString());
                for (int j = 0; j < freespace; j++)
                    hdd.Add(-1);
            }
            fileId++;
        }

        return hdd;
    }

    public List<long> Defrag(List<long> hdd)
    {
        List<long> defragged = new List<long>(hdd);
        int lIndex = 0;
        int rIndex = defragged.Count() - 1;

        while (lIndex < rIndex)
        {
            // move left index forward until it hits free space
            while (defragged[lIndex] != -1)
                lIndex++;
            // move right index backwards until it hits data
            while (defragged[rIndex] == -1)
                rIndex--;
            if (lIndex < rIndex)
            {
                defragged[lIndex] = defragged[rIndex];
                defragged[rIndex] = -1;
            }
        }

        return defragged;
    }

    public List<long> DefragContiguous(List<long> hdd)
    {
        List<long> defragged = new List<long>(hdd);
        long currFileId = hdd.Max();
        int rIndex = defragged.Count() - 1;

        while (currFileId > 0)
        {
            // get info about rightmost file
            while (defragged[rIndex] != currFileId) rIndex--;
            int endFile = rIndex;
            while (defragged[rIndex] == currFileId) rIndex--;
            int startFile = rIndex + 1;
            int fileLength = endFile - startFile + 1;

            //           1         2         3         4
            // 012345678901234567890123456789012345678901
            // 00...111...2...333.44.5555.6666.777.888899
            // 0099.111...2...333.44.5555.6666.777.8888..
            // 0099.1117772...333.44.5555.6666.....8888..
            // 0099.111777244.333....5555.6666.....8888..
            // 00992111777.44.333....5555.6666.....8888..

            bool spaceFound = false;
            int lIndex = 0;
            // now keep testing space on the left until there's no more space to test
            while (!spaceFound && lIndex < rIndex)
            {
                while (defragged[lIndex] != -1) lIndex++;
                int startFreespace = lIndex;
                while (defragged[lIndex] == -1) lIndex++;
                int endFreespace = lIndex;
                int amtFreespace = endFreespace - startFreespace;

                if (amtFreespace >= fileLength && lIndex < rIndex)
                {
                    spaceFound = true;
                    for (int i = 0; i < fileLength; i++)
                    {
                        defragged[i + startFreespace] = currFileId;
                        defragged[i + startFile] = -1;
                    }
                }
            }
            currFileId--;
        }

        return defragged;
    }

    public long CalcChecksum(List<long> hdd)
    {
        long total = 0;

        for (int i = 0; i < hdd.Count(); i++)
            if (hdd[i] != -1)
                total += i * hdd[i];

        return total;
    }
}