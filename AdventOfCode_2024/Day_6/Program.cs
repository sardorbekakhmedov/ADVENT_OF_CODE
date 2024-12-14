
Solution1();
Solution2();


static void Solution1()
{
    var lines = GetData();

    var rows = lines.Length;
    var cols = lines[0].Length;
    var map = new char[rows, cols];
    (int x, int y) guardPosition = (0, 0);
    var direction = '^';

    for (var i = 0; i < rows; i++)
    {
        for (var j = 0; j < cols; j++)
        {
            map[i, j] = lines[i][j];
            if ("^>v<".Contains(map[i, j]))
            {
                guardPosition = (i, j);
                direction = map[i, j];
                map[i, j] = '.'; 
            }
        }
    }

    (int dx, int dy)[] directions = { (-1, 0), (0, 1), (1, 0), (0, -1) };
    var currentDirection = "^>v<".IndexOf(direction);

    var visited = new HashSet<(int, int)>();
    visited.Add(guardPosition);

    while (true)
    {
        var (dx, dy) = directions[currentDirection];
        var (nx, ny) = (guardPosition.x + dx, guardPosition.y + dy);

        if (nx < 0 || ny < 0 || nx >= rows || ny >= cols)
            break;

        if (map[nx, ny] == '#')
        {
            currentDirection = (currentDirection + 1) % 4;
        }
        else
        {
            guardPosition = (nx, ny);
            visited.Add(guardPosition);
        }
    }

    Console.WriteLine($"Res 1: {visited.Count}");
}


static void Solution2()
{
    var lines = GetData();

    var rows = lines.Length;
    var cols = lines[0].Length;
    var map = new char[rows, cols];
    (int x, int y) guardStart = (0, 0);
    var direction = '^';

    for (var i = 0; i < rows; i++)
    {
        for (var j = 0; j < cols; j++)
        {
            map[i, j] = lines[i][j];
            if ("^>v<".Contains(map[i, j]))
            {
                guardStart = (i, j);
                direction = map[i, j];
                map[i, j] = '.'; 
            }
        }
    }

    (int dx, int dy)[] directions = { (-1, 0), (0, 1), (1, 0), (0, -1) };
    var startDirection = "^>v<".IndexOf(direction);

    bool SimulateGuard((int x, int y) start, int initialDirection, char[,] currentMap)
    {
        HashSet<(int x, int y, int dir)> visitedStates = new();
        var guardPosition = start;
        var currentDirection = initialDirection;

        while (true)
        {
            if (!visitedStates.Add((guardPosition.x, guardPosition.y, currentDirection)))
            {
                return true;
            }

            var (dx, dy) = directions[currentDirection];
            var (nx, ny) = (guardPosition.x + dx, guardPosition.y + dy);

            if (nx < 0 || ny < 0 || nx >= rows || ny >= cols)
                return false;

            if (currentMap[nx, ny] == '#')
            {
                currentDirection = (currentDirection + 1) % 4;
            }
            else
            {
                guardPosition = (nx, ny);
            }
        }
    }
    
    var validPositions = 0;
    for (var i = 0; i < rows; i++)
    {
        for (var j = 0; j < cols; j++)
        {
            if (map[i, j] == '.' && (i, j) != guardStart)
            {
                map[i, j] = '#';

                if (SimulateGuard(guardStart, startDirection, map))
                {
                    validPositions++;
                }

                map[i, j] = '.';
            }
        }
    }

    Console.WriteLine($"Res 2: {validPositions}");
}



static string[] GetData()
{
    var filePath = @"C:\XLAM\ADVENT_OF_CODE\AdventOfCode_2024\Day_6\task6.txt";
    if (File.Exists(filePath))
        return File.ReadAllLines(filePath);

    Console.WriteLine($"File {filePath} not found!");
    return new string[] { };
}
