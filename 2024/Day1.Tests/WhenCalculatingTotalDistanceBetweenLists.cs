using FluentAssertions;
using Xunit.Abstractions;

namespace Day1.Tests;

public class WhenCalculatingTotalDistanceBetweenLists(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void GivenEmptyListDistanceShouldBeZero()
    {
        var input = "";

        var totalDistance = CalculateTotalDistanceBetweenLists(input);
        totalDistance.Should().Be(0);
    }

    [Fact]
    public void GivenListWithOnlyeOneLocationEachDistanceShouldBeSumOfAbsoluteDifference()
    {
        var input = "3   4";
        var totalDistance = CalculateTotalDistanceBetweenLists(input);
        totalDistance.Should().Be(1);
    }

    [Fact]
    public void GivenListWithMultipleLocationsEachDistanceShouldBeSumOfAbsoluteDifferencesOfAscendingNumbers()
    {
        var input = "3   4\n4   3\n2   5\n1   3\n3   9\n3   3";
        var totalDistance = CalculateTotalDistanceBetweenLists(input);
        totalDistance.Should().Be(11);
    }

    [Fact]
    public void GivenListsShouldCalculateSimilarityScore()
    {
        var input = "3   4\n4   3\n2   5\n1   3\n3   9\n3   3";
        var totalSimilarityScore = CalculateTotalSimilarityScore(input);
        totalSimilarityScore.Should().Be(31);
    }

    [Fact]
    public void RunAgainstActualInput()
    {
        var input = File.ReadAllText("day1.txt");
        var totalDistance = CalculateTotalDistanceBetweenLists(input);
        var totalSimilarityScore = CalculateTotalSimilarityScore(input);
        testOutputHelper.WriteLine($"Total Distance {totalDistance}");
        testOutputHelper.WriteLine($"Total Simialrty {totalSimilarityScore}");
    }

    private int CalculateTotalSimilarityScore(string input)
    {
        var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        var leftList = new List<int>();
        var rightList = new List<int>();

        foreach (var line in lines)
        {
            var numberOnLeftList = int.Parse(line.Split("   ")[0]);
            var numberOnRightList = int.Parse(line.Split("   ")[1]);

            leftList.Add(numberOnLeftList);
            rightList.Add(numberOnRightList);
        }

        var runningSum = 0;

        for (int i = 0; i < leftList.Count(); i++)
        {
            runningSum += leftList.ElementAt(i) * rightList.FindAll(item => item == leftList.ElementAt(i)).Count;
        }

        return runningSum;

    }

    private int CalculateTotalDistanceBetweenLists(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return 0;
        }

        var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        var leftList = new List<int>();
        var rightList = new List<int>();

        foreach (var line in lines)
        {
            var numberOnLeftList = int.Parse(line.Split("   ")[0]);
            var numberOnRightList = int.Parse(line.Split("   ")[1]);

            leftList.Add(numberOnLeftList);
            rightList.Add(numberOnRightList);
        }
        var leftListSorted = leftList.Order();
        var rightListSorted = rightList.Order();

        var runningSum = 0;

        for (int i = 0; i < leftListSorted.Count(); i++)
        {
            runningSum += Math.Abs(leftListSorted.ElementAt(i) - rightListSorted.ElementAt(i));
        }

        return runningSum;
    }
}