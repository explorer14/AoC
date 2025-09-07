using System.Diagnostics;
using System.Text.RegularExpressions;
using Xunit.Abstractions;

namespace Day1.Tests;

public class Day3(ITestOutputHelper testOutputHelper)
{
    [Theory]
    [InlineData("day3-example-2.txt")]
    [InlineData("day3-example.txt")]
    [InlineData("day3-actual.txt")]
    public void ShouldSolve(string inputFileName)
    {
        var memory = File.ReadAllText(inputFileName);
        var timer = Stopwatch.StartNew();
        var result = ProcessMemory(memory);
        timer.Stop();
        testOutputHelper.WriteLine($"Answer {result} in {timer.Elapsed.TotalMilliseconds} ms");
    }

    private int ProcessMemory(string memory)
    {
        var pattern = @"mul\(\d*,\d*\)";
        var matches = Regex.Matches(memory, pattern);
        var result = 0;

        foreach (Match match in matches)
        {
            result += Exec(match.Value);            
        }

        return result;
    }

    private int Exec(string mulInstruction)
    {
        var csvNums = mulInstruction[4..^1]
            .Split(",", StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToList();
        
        return csvNums[0] * csvNums[1];
    }
}