var lines = File.ReadAllLines(@"C:\Users\aman-agrawal\Code\AoC\2022\Day3-2022\Input.txt").ToList();

var itemTypePriority = new Dictionary<char, int>();

var diff = 0;

for (var i = 97; i <= 122; i++)
{
    ++diff;
    itemTypePriority.Add((char)i, i - (i - diff));
}

for (var i = 65; i <= 90; i++)
{
    itemTypePriority.Add((char)i, i - 38);
}

var prioritySum = 0;

foreach (var line in lines)
{
    var compartment1 = line.Substring(0, line.Length / 2);

    var compartment2 = line.Substring(line.Length / 2);

    var overlappingItemType = compartment1.Intersect(compartment2).ToList();
    prioritySum += itemTypePriority[overlappingItemType.FirstOrDefault()];
}

Console.WriteLine(prioritySum);

prioritySum = 0;

for (var x = 0; x < lines.Count;)
{
    var elfGroup = lines.Skip(x).Take(3).ToList();

    var commonItemType = elfGroup[0].Intersect(elfGroup[1]).Intersect(elfGroup[2]).First();

    prioritySum += itemTypePriority[commonItemType];

    x += 3;
}

Console.WriteLine(prioritySum);