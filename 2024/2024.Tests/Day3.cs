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
        var result = ProcessMemory2(memory);
        timer.Stop();
        testOutputHelper.WriteLine($"Answer {result} in {timer.Elapsed.TotalMilliseconds} ms");
    }

    // private int ProcessMemory(string memory)
    // {
    //     var pattern = @"mul\(\d*,\d*\)";
    //     var matches = Regex.Matches(memory, pattern);
    //     
    //     var result = 0;
    //
    //     foreach (Match match in matches)
    //     {
    //         result += Exec(match.Value);            
    //     }
    //
    //     return result;
    // }

    private int ProcessMemory2(string memory)
    {
        var instructionBuffer = string.Empty;
        var result = 0;

        foreach (var c in memory)
        {
            instructionBuffer += c;
            var isMulEnabled = IsMulEnabled(instructionBuffer);
            var (isMulInstruction, mulInstructions) = ContainsMulInstruction(instructionBuffer);

            if (isMulInstruction && isMulEnabled)
            {
                result += Exec(mulInstructions);
                instructionBuffer = string.Empty;
            }
        }
        return result;
    }

    private (bool, string[]) ContainsMulInstruction(string instructionBuffer)
    {
        var pattern = @"mul\(\d*,\d*\)";
        var matches = Regex.Matches(instructionBuffer, pattern);
        return (matches.Count > 0, matches.Select(m => m.Value).ToArray());
    }

    // find a way to determine when the flag should flip
    // do -> don't => disabled
    // don't -> do => enabled
    // do -> do => enabled
    // don't -> don't => disabled
    private bool IsMulEnabled(string instructionBuffer)
    {
        var disablePattern = @"don't\(\)";
        var enablePattern = @"do\(\)";
        var disableMatch = Regex.Match(instructionBuffer, disablePattern);
        var enableMatch = Regex.Match(instructionBuffer, enablePattern);
        
        return disableMatch.Success | enableMatch.Success;
    }

    private int Exec(string[] mulInstructions)
    {
        var result = 0;
        foreach (var mulInstruction in mulInstructions)
        {
            var csvNums = mulInstruction[4..^1]
                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();
        
            result += csvNums[0] * csvNums[1];    
        }

        return result;
    }
}