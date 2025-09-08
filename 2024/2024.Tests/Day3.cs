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

    private int ProcessMemory(string memory)
    {
        var pattern = @"mul\(\d*,\d*\)";
        var matches = Regex.Matches(memory, pattern);
        
        var result = 0;
    
        foreach (Match match in matches)
        {
            result += Exec([match.Value]);            
        }
    
        return result;
    }

    private int ProcessMemory2(string memory)
    {
        var instructionBuffer = string.Empty;
        var result = 0;
        // ReSharper disable once TooWideLocalVariableScope
        var shouldExecMul = true;

        foreach (var c in memory)
        {
            instructionBuffer += c;
            var (containsDont, containsDo) = ContainsDoOrDont(instructionBuffer);

            // chop off preceding instructions to avoid duplicate calculation
            // of any past mul instructions. don't and do only influence the subsequent string
            // so we can safely chop-off everything before them. Its also important to
            // sub-string from the first occurrence of don't/do to ensure I don't miss any
            if (containsDo)
                instructionBuffer = ChopOffEverythingBeforeDo();
            
            if (containsDont)
                instructionBuffer = ChopOffEverythingBeforeDont();

            shouldExecMul = !containsDont || containsDo; 
            var (isMulInstruction, mulInstructions) = ContainsMulInstruction(instructionBuffer);

            if (isMulInstruction && shouldExecMul)
            {
                result += Exec(mulInstructions);
                instructionBuffer = string.Empty;
            }
        }
        return result;

        string ChopOffEverythingBeforeDo()
        {
            var indexOfDo = instructionBuffer.IndexOf("do()",
                StringComparison.InvariantCultureIgnoreCase);
            
            if (indexOfDo == -1)
                return instructionBuffer;
            
            return instructionBuffer[indexOfDo..instructionBuffer.Length];
        }


        string ChopOffEverythingBeforeDont()
        {
            var indexOfDont = instructionBuffer.IndexOf("don't()",
                StringComparison.InvariantCultureIgnoreCase);
            
            if (indexOfDont == -1)
                return instructionBuffer;
            
            return instructionBuffer[indexOfDont..instructionBuffer.Length];
        }
    }

    private (bool, string[]) ContainsMulInstruction(string instructionBuffer)
    {
        var pattern = @"mul\(\d*,\d*\)";
        var matches = Regex.Matches(instructionBuffer, pattern);
        return (matches.Count > 0, matches.Select(m => m.Value).ToArray());
    }
    
    private (bool containsDont, bool containsDo) ContainsDoOrDont(string instructionBuffer)
    {
        var disablePattern = @"don't\(\)";
        var enablePattern = @"do\(\)";
        var disableMatch = Regex.Match(instructionBuffer, disablePattern);
        var enableMatch = Regex.Match(instructionBuffer, enablePattern);
        
        return (disableMatch.Success, enableMatch.Success);
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