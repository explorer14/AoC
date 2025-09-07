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
    public void ShouldSolve(string inputFileName)
    {
        var lines = File.ReadAllLines(inputFileName);
        var reportsOfLevels = new List<List<int>>();
        var totalNumberOfSafeReports = 0;

        foreach (var line in lines)
        {
            reportsOfLevels.Add(
                [.. line.Split(" ",
                    StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)]);
        }

        var timer = Stopwatch.StartNew();
        //reportsOfLevels.Clear();
        //reportsOfLevels.Add([48, 44 ,42, 41, 40, 41, 39, 35]);

        foreach (var report in reportsOfLevels)
        {
            if (IsReportSafe(report) || SafeAfterIgnoringSingleBadLevel(report))
            {
                ++totalNumberOfSafeReports;
            }
        }

        timer.Stop();

        testOutputHelper.WriteLine($"Answer {totalNumberOfSafeReports} in {timer.Elapsed.TotalMilliseconds} ms");
    }

    private bool IsReportSafe(List<int> report) =>
        DeltaBetweenAdjacentPairsWithinLimits(report) &&
        AllIncreasingOrDecreasing(report);

    private bool SafeAfterIgnoringSingleBadLevel(List<int> report)
    {
        for (int i = 0; i < report.Count; i++)
        {
            if (IsIndexWithinBounds(i))
            {
                if (IsCurrentLevelBad(i))
                {
                    var reportWithoutBadLevel = report[..i].Concat(report[(i + 1)..report.Count]).ToList();
                    
                    if (IsReportSafe(reportWithoutBadLevel))
                    {
                        return true;
                    }

                    //break;
                    return SafeAfterIgnoringSingleBadLevel(reportWithoutBadLevel);
                }    
            }
        }

        return false;

        bool IsCurrentLevelBetweenTwoSmallerOrEqualLevels(int index) => 
            report[index] >= report[index - 1] && report[index] >= report[index + 1];
        
        bool IsCurrentLevelBetweenTwoLargerOrEqualLevels(int index) => 
            report[index] <= report[index - 1] && report[index] <= report[index + 1];

        bool IsCurrentLevelSameAsNextLevel(int index) => 
            report[index] == report[index + 1];

        bool IsCurrentLevelBad(int index) =>
            IsCurrentLevelBetweenTwoSmallerOrEqualLevels(index) ||
            IsCurrentLevelBetweenTwoLargerOrEqualLevels(index);

        bool IsIndexWithinBounds(int index) => 
            index - 1 >= 0 && index + 1 < report.Count;
    }

    private bool DeltaBetweenAdjacentPairsWithinLimits(List<int> report)
    {
        var deltaWithinLimits = true;

        for (var i = 0; i < report.Count; i++)
        {
            if (i + 1 < report.Count)
            {
                var diff = Math.Abs(report[i] - report[i + 1]);

                if (!(diff is >= 1 and <= 3))
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
                allIncreasing &= report[i] <= report[i + 1];
                allDecreasing &= report[i] >= report[i + 1];
            }
        }

        return allIncreasing || allDecreasing;
    }
}
