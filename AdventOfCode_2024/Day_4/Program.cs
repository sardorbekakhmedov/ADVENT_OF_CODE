
Solution1();
Solution2();

static void Solution2()
{
    var grid = GetData();
    var rows = grid.Length;
    var cols = grid[0].Length;
    var count = 0;

    for (var i = 1; i < rows - 1; i++) 
    {
        for (var j = 1; j < cols - 1; j++)
        {
            if (grid[i][j] == 'A')
            {
                // Yuqori chap va pastki o'ng diagonallarni tekshiring
                if ((grid[i - 1][j - 1] == 'M' && grid[i + 1][j + 1] == 'S') ||
                    (grid[i - 1][j - 1] == 'S' && grid[i + 1][j + 1] == 'M'))
                {
                    // Yuqori o'ng va pastki chap diagonallarni tekshiring
                    if ((grid[i - 1][j + 1] == 'M' && grid[i + 1][j - 1] == 'S') ||
                        (grid[i - 1][j + 1] == 'S' && grid[i + 1][j - 1] == 'M'))
                    {
                        count++;
                    }
                }
            }
        }
    }

    Console.WriteLine($"Res 2: {count}");
}

static void Solution1()
{
    var grid = GetData();
    var rows = grid.Length;
    var cols = grid[0].Length;
    var word = "XMAS";
    var count = 0;

    for (var i = 0; i < rows; i++)
    {
        for (var j = 0; j < cols; j++)
        {
            // Gorizontal o'ng tomonga
            if (j + word.Length <= cols)
            {
                var horizontalRight = grid[i].Substring(j, word.Length);
                if (horizontalRight == word)
                {
                    count++;
                }
            }

            // Gorizontal chap tomonga
            if (j - word.Length + 1 >= 0)
            {
                var horizontalLeft = grid[i].Substring(j - word.Length + 1, word.Length);
                if (horizontalLeft == "SAMX")
                {
                    count++;
                }
            }

            // Vertikal pastga
            if (i + word.Length <= rows)
            {
                var verticalDown = "";
                for (var k = 0; k < word.Length; k++)
                    verticalDown += grid[i + k][j];
                if (verticalDown == word)
                {
                    count++;
                }
            }

            // Vertikal yuqoriga
            if (i - word.Length + 1 >= 0)
            {
                var verticalUp = "";
                for (var k = 0; k < word.Length; k++)
                    verticalUp += grid[i - k][j];
                if (verticalUp == word)
                {
                    count++;
                }
            }

            // Diagonal pastga-o'ngga
            if (i + word.Length <= rows && j + word.Length <= cols)
            {
                var diagonalDownRight = "";
                for (var k = 0; k < word.Length; k++)
                    diagonalDownRight += grid[i + k][j + k];
                if (diagonalDownRight == word)
                {
                    count++;
                }
            }

            // Diagonal yoqoriga-chapga
            if (i - word.Length + 1 >= 0 && j - word.Length + 1 >= 0)
            {
                var diagonalUpLeft = "";
                for (var k = 0; k < word.Length; k++)
                    diagonalUpLeft += grid[i - k][j - k];
                if (diagonalUpLeft == word)
                {
                    count++;
                }
            }

            // Diagonal pastga-chapga
            if (i + word.Length <= rows && j - word.Length + 1 >= 0)
            {
                var diagonalDownLeft = "";
                for (var k = 0; k < word.Length; k++)
                    diagonalDownLeft += grid[i + k][j - k];
                if (diagonalDownLeft == word)
                {
                    count++;
                }
            }

            // Diagonal yuqoriga-o'ngga
            if (i - word.Length + 1 >= 0 && j + word.Length <= cols)
            {
                var diagonalUpRight = "";
                for (var k = 0; k < word.Length; k++)
                    diagonalUpRight += grid[i - k][j + k];
                if (diagonalUpRight == word)
                {
                    count++;
                }
            }
        }
    }

    Console.WriteLine("Res 1: " + count);
}


/*
static string[] GetDataTest()
{
    return new[]
    {
        "MMMSXXMASM",
        "MSAMXMSMSA",
        "AMXSXMAAMM",
        "MSAMASMSMX",
        "XMASAMXAMM",
        "XXAMMXXAMA",
        "SMSMSASXSS",
        "SAXAMASAAA",
        "MAMMMXMMMM",
        "MXMXAXMASX"
    };
}
*/


static string[] GetData()
{
    var filePath = @"C:\XLAM\ADVENT_OF_CODE\AdventOfCode_2024\Day_4\task4.txt";
    if (File.Exists(filePath))
        return File.ReadAllLines(filePath);

    Console.WriteLine($"File {filePath} not found!");
    return new string[] { };
}