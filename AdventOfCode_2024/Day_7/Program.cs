
Solution1();
Solution2();

static void Solution1()
{
    var inputData = @"
    190: 10 19
    3267: 81 40 27
    83: 17 5
    156: 15 6
    7290: 6 8 6 15
    161011: 16 10 13
    192: 17 8 14
    21037: 9 7 18 13
    292: 11 6 16 20
    ";

    var data = GetData();

    var equations = ParseInput(data);

    var result = CalculateCalibrationSum(equations);

    Console.WriteLine($"Res 1: {result}");

    static List<(decimal Target, List<decimal> Numbers)> ParseInput(string[] inputData)
    {
        var equations = new List<(decimal Target, List<decimal> Numbers)>();

        foreach (var line in inputData)
        {
            var parts = line.Split(": ");
            var target = decimal.Parse(parts[0]);
            var numbers = parts[1].Split(' ').Select(decimal.Parse).ToList();
            equations.Add((target, numbers));
        }

        return equations;
    }

    static decimal CalculateCalibrationSum(List<(decimal Target, List<decimal> Numbers)> equations)
    {
        decimal totalSum = 0;

        foreach (var (target, numbers) in equations)
        {
            if (CanSolveEquation(target, numbers))
            {
                totalSum += target;
            }
        }

        return totalSum;
    }

    static bool CanSolveEquation(decimal target, List<decimal> numbers)
    {
        var n = numbers.Count;
        var operatorCount = n - 1;

        foreach (var operators in GetOperatorCombinations(operatorCount))
        {
            if (EvaluateExpression(numbers, operators) == target)
            {
                return true;
            }
        }

        return false;
    }

    static IEnumerable<string> GetOperatorCombinations(int count)
    {
        var totalCombinations = (int)Math.Pow(2, count);
        for (var i = 0; i < totalCombinations; i++)
        {
            yield return Convert.ToString(i, 2).PadLeft(count, '0').Replace('0', '+').Replace('1', '*');
        }
    }

    static decimal EvaluateExpression(List<decimal> numbers, string operators)
    {
        var result = numbers[0];

        for (var i = 0; i < operators.Length; i++)
        {
            if (operators[i] == '+')
            {
                result += numbers[i + 1];
            }
            else if (operators[i] == '*')
            {
                result *= numbers[i + 1];
            }
        }

        return result;
    }
}

static void Solution2()
{
    var data = GetData();
    
    var equations = ParseInput(data);

    var result = CalculateCalibrationSum(equations);

    Console.WriteLine($"Res 2: {result}");
    
    static List<(decimal Target, List<decimal> Numbers)> ParseInput(string[] inputData)
    {
        var equations = new List<(decimal Target, List<decimal> Numbers)>();

        foreach (var line in inputData)
        {
            var parts = line.Split(": ");
            var target = decimal.Parse(parts[0]);
            var numbers = parts[1].Split(' ').Select(decimal.Parse).ToList();
            equations.Add((target, numbers));
        }

        return equations;
    }
    
    static IEnumerable<string> GetOperatorCombinations(int count)
    {
        char[] operatorSet = { '+', '*', '|' };
        var totalCombinations = (int)Math.Pow(operatorSet.Length, count);

        for (var i = 0; i < totalCombinations; i++)
        {
            var combination = new char[count];
            var temp = i;

            for (var j = 0; j < count; j++)
            {
                combination[j] = operatorSet[temp % operatorSet.Length];
                temp /= operatorSet.Length;
            }

            yield return new string(combination);
        }
    }
    
    static bool CanSolveEquation(decimal target, List<decimal> numbers)
    {
        var n = numbers.Count;
        var operatorCount = n - 1;

        foreach (var operators in GetOperatorCombinations(operatorCount))
        {
            if (EvaluateExpression(numbers, operators) == target)
            {
                return true;
            }
        }

        return false;
    }
    
    static decimal CalculateCalibrationSum(List<(decimal Target, List<decimal> Numbers)> equations)
    {
        decimal totalSum = 0;

        foreach (var (target, numbers) in equations)
        {
            if (CanSolveEquation(target, numbers))
            {
                totalSum += target;
            }
        }

        return totalSum;
    }
    
    
    static decimal EvaluateExpression(List<decimal> numbers, string operators)
    {
        var result = numbers[0];

        for (var i = 0; i < operators.Length; i++)
        {
            if (operators[i] == '+')
            {
                result += numbers[i + 1];
            }
            else if (operators[i] == '*')
            {
                result *= numbers[i + 1];
            }
            else if (operators[i] == '|')
            {
                result = decimal.Parse(result.ToString() + numbers[i + 1].ToString());
            }
        }

        return result;
    }
}


static string[] GetData()
{
    var filePath = @"C:\XLAM\ADVENT_OF_CODE\AdventOfCode_2024\Day_7\task7.txt";
    if (File.Exists(filePath))
        return File.ReadAllLines(filePath);

    Console.WriteLine($"File {filePath} not found!");
    return [];
}