using System.Diagnostics;

var lines = File.ReadAllLines(@"C:\Users\aman-agrawal\Code\AoC\2022\Day8-2022\Input-Large.txt");

var rows = lines.Length;
var columns = lines[0].Length;

var defaultVisibleTrees = (rows * 2) + (rows - 2) * 2;

var treeGrid = new int[rows, columns];

for (var x = 0; x < rows; x++)
    for (var y = 0; y < columns; y++)
        treeGrid[x, y] = int.Parse(lines[x][y].ToString());

var visibleCount = 0;

var timer = Stopwatch.StartNew();

for (var x = 1; x < rows - 1; x++)
{
    int currentTreeHeight;

    for (var y = 1; y < columns - 1; y++)
    {
        var currentTreeVisibleTop = true;
        var currentTreeVisibleBelow = true;
        var currentTreeVisibleRight = true;
        var currentTreeVisibleLeft = true;

        currentTreeHeight = treeGrid[x, y];

        // check right
        for (var t = y + 1; t < columns; t++)
        {
            currentTreeVisibleRight &= treeGrid[x, t] < currentTreeHeight;

            if (!currentTreeVisibleRight)
                break;
        }

        // check left
        for (var t = y - 1; t >= 0; t--)
        {
            currentTreeVisibleLeft &= treeGrid[x, t] < currentTreeHeight;

            if (!currentTreeVisibleLeft)
                break;
        }

        // check top
        for (var t = x - 1; t >= 0; t--)
        {
            currentTreeVisibleTop &= treeGrid[t, y] < currentTreeHeight;

            if (!currentTreeVisibleTop)
                break;
        }

        // check below
        for (var t = x + 1; t < rows; t++)
        {
            currentTreeVisibleBelow &= treeGrid[t, y] < currentTreeHeight;

            if (!currentTreeVisibleBelow)
                break;
        }

        if (currentTreeVisibleBelow ||
            currentTreeVisibleLeft ||
            currentTreeVisibleRight ||
            currentTreeVisibleTop)
            ++visibleCount;
    }
}

timer.Stop();

Console.WriteLine(visibleCount + defaultVisibleTrees);
Console.WriteLine(timer.Elapsed);