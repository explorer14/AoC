// correct answer=1195
var previousDepth = TestData.depths[0];
var currentDepth = previousDepth;
var totalDepthIncreases = 0;

for (var i = 1; i < TestData.depths.Length; i++)
{
    currentDepth = TestData.depths[i];

    if (currentDepth > previousDepth)
        ++totalDepthIncreases;

    previousDepth = currentDepth;
}

Console.WriteLine($"Total depth increases {totalDepthIncreases}");