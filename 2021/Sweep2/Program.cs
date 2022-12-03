var _depths = new int[]
{
    199,
    200,
    208,
    210,
    200,
    207,
    240,
    269,
    260,
    263
};
// correct answer=1235
var totalDepthIncreases = 0;
var window3 = new List<int>();
var window3Sum = new List<int>();

for (var i = 0; i < TestData.depths.Length; i++)
{
    if (i + 1 < TestData.depths.Length && i + 2 < TestData.depths.Length)
        window3Sum.Add(TestData.depths[i] + TestData.depths[i + 1] + TestData.depths[i + 2]);
}

var previousDepth = window3Sum[0];
var currentDepth = previousDepth;

for (var i = 1; i < window3Sum.Count; i++)
{
    currentDepth = window3Sum[i];

    if (currentDepth > previousDepth)
        ++totalDepthIncreases;

    previousDepth = currentDepth;
}

Console.WriteLine($"Total depth increases {totalDepthIncreases}");