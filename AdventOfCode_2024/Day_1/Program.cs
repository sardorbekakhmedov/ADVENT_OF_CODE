

Solution1();
Solution2();

static void Solution2()
{
    string filePath = @"C:\XLAM\ADVENT_OF_CODE\AdventOfCode_2024\Day_1\task1.txt";

    var lines = File.ReadAllLines(filePath);

    int[] left = new int[lines.Length];
    int[] right = new int[lines.Length];

    for (int i = 0; i < lines.Length; i++)
    {
        var parts = lines[i].Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
        left[i] = int.Parse(parts[0]);
        right[i] = int.Parse(parts[1]);
    }
    
    var frequencyMap = new Dictionary<int, int>();
    foreach (var num in right)
    {
        if (!frequencyMap.ContainsKey(num))
        {
            frequencyMap[num] = 0;
        }
        frequencyMap[num]++;
    }

    int similarityScore = 0;
    foreach (var num in left)
    {
        if (frequencyMap.TryGetValue(num, out int count))
        {
            similarityScore += num * count;
        }
    }

    Console.WriteLine($"Res 2: {similarityScore}");
}

static void Solution1()
{
    string filePath = @"C:\XLAM\ADVENT_OF_CODE\AdventOfCode_2024\Day_1\task1.txt";

    var lines = File.ReadAllLines(filePath);

    int[] input1 = new int[lines.Length];
    int[] input2 = new int[lines.Length];

    for (int i = 0; i < lines.Length; i++)
    {
        var parts = lines[i].Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
        input1[i] = int.Parse(parts[0]);
        input2[i] = int.Parse(parts[1]);
    }
    
    Array.Sort(input1);
    Array.Sort(input2);

    int sumDis = 0;

    for (int i = 0; i < input1.Length; i++)
    {
        sumDis += Math.Abs(input1[i] - input2[i]);
    }

    Console.WriteLine($"Res 1: {sumDis}");
}