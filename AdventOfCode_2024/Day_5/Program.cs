

Solution1();
Solution2();


static void Solution1()
{
    var lines = GetData();

    var rulesInput = new List<string>();
    var updatesInput = new List<string>();
    var isUpdateSection = false;

    foreach (var line in lines)
    {
        if (string.IsNullOrWhiteSpace(line))
        {
            isUpdateSection = true;
            continue;
        }

        if (isUpdateSection)
            updatesInput.Add(line);
        else
            rulesInput.Add(line);
    }

    var rules = new List<(int X, int Y)>();
    foreach (var rule in rulesInput)
    {
        var parts = rule.Split('|').Select(int.Parse).ToArray();
        rules.Add((parts[0], parts[1]));
    }

    var totalMiddleSum = 0;

    foreach (var update in updatesInput)
    {
        var pages = update.Split(',').Select(int.Parse).ToList();

        if (IsValidUpdate(pages, rules))
        {
            var middleIndex = pages.Count / 2;
            var middlePage = pages[middleIndex];

            totalMiddleSum += middlePage;
        }
    }

    Console.WriteLine($"Res 1: {totalMiddleSum}");
}


static void Solution2()
{
    var lines = GetData();
    
    var rulesInput = new List<string>();
    var updatesInput = new List<string>();
    var isUpdateSection = false;

    foreach (var line in lines)
    {
        if (string.IsNullOrWhiteSpace(line))
        {
            isUpdateSection = true;
            continue;
        }

        if (isUpdateSection)
            updatesInput.Add(line);
        else
            rulesInput.Add(line);
    }

    var rules = new List<(int X, int Y)>();
    foreach (var rule in rulesInput)
    {
        var parts = rule.Split('|').Select(int.Parse).ToArray();
        rules.Add((parts[0], parts[1]));
    }

    var correctedMiddleSum = 0;

    foreach (var update in updatesInput)
    {
        var pages = update.Split(',').Select(int.Parse).ToList();

        if (!IsValidUpdate(pages, rules))
        {
            var correctedPages = CorrectUpdateOrder(pages, rules);

            var middleIndex = correctedPages.Count / 2;
            var middlePage = correctedPages[middleIndex];

            correctedMiddleSum += middlePage;
        }
    }
    
    
    Console.WriteLine($"Res 1: {correctedMiddleSum}");
}


static bool IsValidUpdate(List<int> pages, List<(int X, int Y)> rules)
{
    var pageIndices = pages.Select((page, index) => (page, index))
        .ToDictionary(x => x.page, x => x.index);

    foreach (var rule in rules)
    {
        if (pageIndices.ContainsKey(rule.X) && pageIndices.ContainsKey(rule.Y))
        {
            if (pageIndices[rule.X] >= pageIndices[rule.Y])
            {
                return false;
            }
        }
    }

    return true;
}

static List<int> CorrectUpdateOrder(List<int> pages, List<(int X, int Y)> rules)
{
    var graph = new Dictionary<int, List<int>>();
    foreach (var page in pages)
    {
        graph[page] = new List<int>();
    }

    foreach (var rule in rules)
    {
        if (graph.ContainsKey(rule.X) && graph.ContainsKey(rule.Y))
        {
            graph[rule.X].Add(rule.Y);
        }
    }

    var sortedPages = TopologicalSort(graph);
    return sortedPages;
}

static List<int> TopologicalSort(Dictionary<int, List<int>> graph)
{
    var visited = new HashSet<int>();
    var result = new List<int>();
    var tempMark = new HashSet<int>();

    void Visit(int node)
    {
        if (tempMark.Contains(node))
            throw new InvalidOperationException("Graph has a cycle!");
        if (!visited.Contains(node))
        {
            tempMark.Add(node);
            foreach (var neighbor in graph[node])
            {
                Visit(neighbor);
            }
            tempMark.Remove(node);
            visited.Add(node);
            result.Add(node);
        }
    }

    foreach (var node in graph.Keys)
    {
        Visit(node);
    }

    return result.Where(graph.ContainsKey).ToList();
}

static string[] GetData()
{
    var filePath = @"C:\XLAM\ADVENT_OF_CODE\AdventOfCode_2024\Day_5\task5.txt";
    if (File.Exists(filePath))
        return File.ReadAllLines(filePath);

    Console.WriteLine($"File {filePath} not found!");
    return new string[] { };
}
