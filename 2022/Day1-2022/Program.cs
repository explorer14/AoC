var lines = File.ReadAllLines(@"C:\Users\aman-agrawal\Code\AoC\2022\Day1-2022\Calories.txt").ToList();

//DoPart1(lines);

DoPart2(lines);

void DoPart2(List<string> lines)
{
    var totalCals = 0;
    var totalCalList = new List<int>();

    for (var i = 0; i < lines.Count; i++)
    {
        if (lines[i] != string.Empty)
            totalCals += int.Parse(lines[i]);
        else
        {
            totalCalList.Add(totalCals);
            totalCals = 0;
        }
    }

    Console.WriteLine(totalCalList.OrderByDescending(x => x).Take(3).Sum(x => x));
}

static void DoPart1(List<string> lines)
{
    var totalCals = 0;
    var largestCalVal = totalCals;

    for (var i = 0; i < lines.Count;)
    {
        if (lines[i] != string.Empty)
            totalCals += int.Parse(lines[i]);
        else
        {
            if (totalCals > largestCalVal)
                largestCalVal = totalCals;

            totalCals = 0;
        }

        i += 1;
    }

    Console.WriteLine(largestCalVal);
}