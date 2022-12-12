var lines = File.ReadAllLines(@"C:\Users\aman-agrawal\Code\AoC\2022\Day10-2022\Input-Large.txt");

var x = 1;
var cycle = 0;
var totalSignalStrength = 0;

foreach (var line in lines)
{
    if (line == "noop")
    {
        ++cycle;

        if (cycle == 20 ||
            cycle == 60 ||
            cycle == 100 ||
            cycle == 140 ||
            cycle == 180 ||
            cycle == 220)
            totalSignalStrength += cycle * x;
    }

    if (line.StartsWith("addx"))
    {
        ++cycle;
        var add = int.Parse(line.Substring(5, line.Length - 5));

        if (cycle == 20 ||
            cycle == 60 ||
            cycle == 100 ||
            cycle == 140 ||
            cycle == 180 ||
            cycle == 220)
            totalSignalStrength += cycle * x;

        ++cycle;

        if (cycle == 20 ||
            cycle == 60 ||
            cycle == 100 ||
            cycle == 140 ||
            cycle == 180 ||
            cycle == 220)
            totalSignalStrength += cycle * x;

        x += add;
    }
}

// Part 2
foreach (var line in lines)
{
    if (line == "noop")
    {
        ++cycle;

        if (cycle == 20 ||
            cycle == 60 ||
            cycle == 100 ||
            cycle == 140 ||
            cycle == 180 ||
            cycle == 220)
            totalSignalStrength += cycle * x;
    }

    if (line.StartsWith("addx"))
    {
        ++cycle;
        var add = int.Parse(line.Substring(5, line.Length - 5));

        if (cycle == 20 ||
            cycle == 60 ||
            cycle == 100 ||
            cycle == 140 ||
            cycle == 180 ||
            cycle == 220)
            totalSignalStrength += cycle * x;

        ++cycle;

        if (cycle == 20 ||
            cycle == 60 ||
            cycle == 100 ||
            cycle == 140 ||
            cycle == 180 ||
            cycle == 220)
            totalSignalStrength += cycle * x;

        x += add;
    }
}

Console.WriteLine(totalSignalStrength);