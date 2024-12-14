
//Solution1();
Solution2();

static void Solution1()
{
    var initialStones = ReadStonesFromFile();

    if (initialStones.Count == 0)
    {
        Console.WriteLine("Fileda xatolik.");
        return;
    }

    var blinks = 75;

    var result = SimulateBlinks(initialStones, blinks);

    Console.WriteLine($"Res 1:  {blinks}  , : {result}");
}

static void Solution2()
{
    var initialStones = ReadStonesFromFile();

    if (initialStones.Count == 0)
    {
        Console.WriteLine("Fileda xatolik.");
        return;
    }

    var blinks = 75;

    var result = SimulateOptimized(initialStones, blinks);

    Console.WriteLine($"Res 2:  {blinks}  , : {result}");
}

static List<long> ReadStonesFromFile()
{
    try
    {
       var lines = GetData();

        return lines.SelectMany(line => line.Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries))
            .Select(long.Parse)
            .ToList();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Xatolik: {ex.Message}");
        return null;
    }
}


static int SimulateBlinks(List<long> stones, int blinks)
{
    for (int i = 0; i < blinks; i++)
    {
        List<long> newStones = new List<long>();

        foreach (var stone in stones)
        {
            if (stone == 0)
            {
                newStones.Add(1);
            }
            else if (stone.ToString().Length % 2 == 0) 
            {
                string stoneStr = stone.ToString();
                int mid = stoneStr.Length / 2;
                long left = long.Parse(stoneStr.Substring(0, mid));
                long right = long.Parse(stoneStr.Substring(mid));
                newStones.Add(left);
                newStones.Add(right);
            }
            else
            {
                newStones.Add(stone * 2024);
            }
        }

        stones = newStones;
    }

    return stones.Count;
}

static long SimulateOptimized(List<long> stones, int blinks)
{
    var stoneCounts = new Dictionary<long, long>();

    foreach (var stone in stones)
    {
        if (!stoneCounts.ContainsKey(stone))
            stoneCounts[stone] = 0;
        stoneCounts[stone]++;
    }

    for (var i = 0; i < blinks; i++)
    {
        var newStoneCounts = new Dictionary<long, long>();

        foreach (var kvp in stoneCounts)
        {
            var stone = kvp.Key;
            var count = kvp.Value;

            if (stone == 0)
            {
                AddStone(newStoneCounts, 1, count);
            }
            else if (stone.ToString().Length % 2 == 0)
            {
                var stoneStr = stone.ToString();
                var mid = stoneStr.Length / 2;
                var left = long.Parse(stoneStr.Substring(0, mid));
                var right = long.Parse(stoneStr.Substring(mid));

                AddStone(newStoneCounts, left, count);
                AddStone(newStoneCounts, right, count);
            }
            else
            {
                AddStone(newStoneCounts, stone * 2024, count);
            }
        }

        stoneCounts = newStoneCounts;
    }

    return stoneCounts.Values.Sum();
}

static void AddStone(Dictionary<long, long> stoneCounts, long stone, long count)
{
    if (!stoneCounts.ContainsKey(stone))
        stoneCounts[stone] = 0;
    stoneCounts[stone] += count;
}


static string[] GetData()
{
    var filePath = @"C:\XLAM\ADVENT_OF_CODE\AdventOfCode_2024\Day_11\task11.txt";
    if (File.Exists(filePath))
        return File.ReadAllLines(filePath);

    Console.WriteLine($"File {filePath} not found!");
    return new string[] { };
}