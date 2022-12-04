using System.Runtime.CompilerServices;

var lines = File.ReadAllLines(@"C:\Users\aman-agrawal\Code\AoC\2022\Day4-2022\Input.txt").ToList();

var count = 0;

foreach (var line in lines)
{
    var expandedAssignmentSequences = GetAssignmentSequence(line);

    if (expandedAssignmentSequences.PairSequence1.IsSuperSetOf(expandedAssignmentSequences.PairSequence2) ||
        expandedAssignmentSequences.PairSequence2.IsSuperSetOf(expandedAssignmentSequences.PairSequence1))
        count++;
}

Console.WriteLine(count);

count = 0;

foreach (var line in lines)
{
    var expandedAssignmentSequences = GetAssignmentSequence(line);

    if (expandedAssignmentSequences.PairSequence1.Intersect(expandedAssignmentSequences.PairSequence2).Any())
        count++;
}

Console.WriteLine(count);

static (int[] PairSequence1, int[] PairSequence2) GetAssignmentSequence(string rangePair)
{
    var pairParts = rangePair.Split(',');

    var sequence1 = ExpandSequenceFromRange(pairParts[0]);
    var sequence2 = ExpandSequenceFromRange(pairParts[1]);

    return (sequence1, sequence2);
}

static int[] ExpandSequenceFromRange(string range)
{
    var min = int.Parse(range.Split('-')[0]);
    var max = int.Parse(range.Split('-')[1]);
    var sequenceSize = (max - min) + 1;
    var sequence = new int[sequenceSize];

    for (var x = 0; x < sequenceSize; x++)
    {
        sequence[x] = min + x;
    }

    return sequence;
}

internal static class Extns
{
    internal static bool IsSuperSetOf(this int[] setA, int[] setB)
    {
        bool isSuperSet = false;

        if (setB == null || !setB.Any())
            return true;

        if (!setA.Except(setB).Any())
            return true;

        foreach (var item in setB)
        {
            isSuperSet &= setA.Contains(item);
        }

        return isSuperSet;
    }
}