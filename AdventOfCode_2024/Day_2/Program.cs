
Solution1();
Solution2();

static void Solution2()
{
    var dataList = GetData();

    var reportsCount = 0;
    
    foreach (var list in dataList)
    {
        if (IsSafe2(list))
        {
            reportsCount++;
            continue;
        }
        
        for (int i = 0; i < list.Count; i++)
        {
            var modifiedReport = new List<int>(list);
            modifiedReport.RemoveAt(i);
            
            if (IsSafe2(modifiedReport))
            {
                reportsCount++;
                break; 
            }
        }
    }
    
    Console.WriteLine("Res 2: " + reportsCount);
}

static bool IsSafe2(List<int> list)
{
    var differences = new List<int>();
    for (var i = 0; i < list.Count - 1; i++)
    {
        var diffNum = list[i + 1] - list[i];
        
        if ((differences.Count > 0 && differences[0] > 0 && diffNum <= 0 ) || 
            (differences.Count > 0 && differences[0] < 0 && diffNum >= 0 ))
        {
            return false;
        }
        
        differences.Add(diffNum);
    }

    return differences.All(diff => diff >= 1 && diff <= 3) || differences.All(diff => diff <= -1 && diff >= -3);
}


static void Solution1()
{
    var dataList = GetData();

    var reportsCount = 0;
    
    foreach (var list in dataList)
    {
        if (IsSafe1(list))
        {
            reportsCount++;
        }
    }
    
    Console.WriteLine("Res 1: " + reportsCount);
}

static bool IsSafe1(List<int> list)
{
    var differences = new List<int>();
    for (var i = 0; i < list.Count - 1; i++)
    {
        var diffNum = list[i + 1] - list[i];
        
        if ((differences.Count > 0 && differences[0] > 0 && diffNum <= 0 ) || 
            (differences.Count > 0 && differences[0] < 0 && diffNum >= 0 ))
        {
            return false;
        }
        
        differences.Add(diffNum);
    }

    return differences.All(diff => diff >= 1 && diff <= 3) || differences.All(diff => diff <= -1 && diff >= -3);
}


static List<List<int>> GetData()
{
    var lines = File.ReadAllLines(@"C:\XLAM\AdventOfCode\Day_2\task2.txt");

    return lines.Select(line =>
            line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries) 
                .Select(int.Parse) 
                .ToList() 
    ).ToList(); 
}
