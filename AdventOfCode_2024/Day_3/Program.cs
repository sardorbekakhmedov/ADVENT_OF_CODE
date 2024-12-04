using System.Text.RegularExpressions;

Solution1();
Solution2();

static void Solution2()
{
    var input = GetData();
    
    bool isMulEnabled = true;
    int total = 0;
    
    var pattern = @"(mul\((\d{1,3}),(\d{1,3})\))|(do\(\))|(don't\(\))";
        
    var matches = Regex.Matches(input, pattern);

    foreach (Match match in matches)
    {
        if (match.Groups[2].Success && isMulEnabled) 
        {
            var x = int.Parse(match.Groups[2].Value);
            var y = int.Parse(match.Groups[3].Value);
            total += x * y;
        }
        else if (match.Groups[4].Success)
        {
            isMulEnabled = true;
        }
        else if (match.Groups[5].Success) 
        {
            isMulEnabled = false;
        }
    }
    
    Console.WriteLine($"Res 2: {total}");
}

static void Solution1()
{
    var input = GetData();
    
    var pattern = @"mul\((\d{1,3}),(\d{1,3})\)";
    var matches = Regex.Matches(input, pattern);

    var total = 0;

    foreach (Match match in matches)
    {
        var x = int.Parse(match.Groups[1].Value); 
        var y = int.Parse(match.Groups[2].Value);
        total += x * y; 
    }

    Console.WriteLine($"Res 1: {total}");
}

static string GetData()
{
    var filePath = "C:\\XLAM\\AdventOfCode\\Day_3\\task3.txt";
    if (File.Exists(filePath))
        return File.ReadAllText(filePath);
    
    Console.WriteLine($"File {filePath} not found!.");
    return "";
}