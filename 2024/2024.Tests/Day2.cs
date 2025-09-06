using System;
using System.Diagnostics;
using FluentAssertions;
using Xunit.Abstractions;

namespace Day1.Tests;

public class Day2(ITestOutputHelper testOutputHelper)
{
    [Theory]
    [InlineData("day2-example.txt")]
    [InlineData("day2-actual.txt")]
    public void ShouldSolveExample(string inputFileName)
    {
        var lines = File.ReadAllLines(inputFileName);
        var reportsOfLevels = new List<List<int>>();
        var totalNumberOfSafeReports = 0;

        foreach (var line in lines)
        {
            reportsOfLevels.Add(
                [.. line.Split(" ",
                    StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => int.Parse(x))]);
        }

        var timer = Stopwatch.StartNew();

        foreach (var report in reportsOfLevels)
        {
            // if the level is all increasing or decreasing => safe
            // AND
            // if the delta between each adjacent pair of levels between 1 and 3 => safe

            if ((AllIncreasingOrDecreasing(report) &&
                DeltaBetweenAdjacentPairsWithinLimits(report)) ||
                SafeAfterRemovingRedundantLevels(report))
            {
                ++totalNumberOfSafeReports;
            }
        }

        timer.Stop();

        testOutputHelper.WriteLine($"Answer {totalNumberOfSafeReports} in {timer.Elapsed.TotalMilliseconds} ms");
    }

    private bool SafeAfterRemovingRedundantLevels(List<int> report)
    {
        for (int i = 0; i < report.Count; i++)
        {
            report[i]
        }
    }

    private bool DeltaBetweenAdjacentPairsWithinLimits(List<int> report)
    {
        var deltaWithinLimits = true;

        for (var i = 0; i < report.Count; i++)
        {
            if (i + 1 < report.Count)
            {
                var diff = Math.Abs(report[i] - report[i + 1]);

                if (!(diff >= 1 && diff <= 3))
                {
                    deltaWithinLimits = false;
                    break;
                }
            }
        }

        return deltaWithinLimits;
    }

    private bool AllIncreasingOrDecreasing(List<int> report)
    {
        var allIncreasing = true;
        var allDecreasing = true;

        for (int i = 0; i < report.Count; i++)
        {
            if (i + 1 < report.Count)
            {
                allIncreasing &= report[i] < report[i + 1];
                allDecreasing &= report[i] > report[i + 1];
            }
        }

        return allIncreasing || allDecreasing;
    }
}
