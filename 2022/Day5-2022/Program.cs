using System.Data.Common;

var lines = File.ReadAllLines(@"C:\Users\aman-agrawal\Code\AoC\2022\Day5-2022\Input.txt").ToList();

var stackLines = lines.TakeWhile(x => x != string.Empty).ToArray();
var arrangementSteps = File.ReadAllLines(@"C:\Users\aman-agrawal\Code\AoC\2022\Day5-2022\Steps.txt").ToArray();

//lines.Except(stackLines).Where(x => x != string.Empty).ToArray();

var stacks = ParseStacks(stackLines);

//ArrangeStacksPart1(arrangementSteps, stacks);
ArrangeStacksPart2(arrangementSteps, stacks);

void ArrangeStacksPart2(string[] arrangementSteps, Dictionary<int, Stack<string>> stacks)
{
    for (var i = 0; i < arrangementSteps.Length; i++)
    {
        var stepParts = arrangementSteps[i].Split(" ", StringSplitOptions.RemoveEmptyEntries);
        var numberOfItemsToMove = int.Parse(stepParts[1]);
        var sourceStackNumber = int.Parse(stepParts[3]);
        var destinationStackNumber = int.Parse(stepParts[5]);

        var sourceStack = stacks[sourceStackNumber];
        var destinationStack = stacks[destinationStackNumber];

        var tempStack = new List<string>();

        while (numberOfItemsToMove > 0)
        {
            if (sourceStack.Count > 0)
                tempStack.Add(sourceStack.Pop());

            --numberOfItemsToMove;
        }

        tempStack.Reverse();

        foreach (var item in tempStack)
        {
            destinationStack.Push(item);
        }
    }
}

var message = "";

foreach (var stack in stacks.OrderBy(x => x.Key))
{
    if (stack.Value.Count > 0)
    {
        var top = stack.Value.Pop();
        message += top;
    }
}

Console.WriteLine(message);

static void ArrangeStacksPart1(string[] arrangementSteps, Dictionary<int, Stack<string>> stacks)
{
    for (var i = 0; i < arrangementSteps.Length; i++)
    {
        var stepParts = arrangementSteps[i].Split(" ", StringSplitOptions.RemoveEmptyEntries);
        var numberOfItemsToMove = int.Parse(stepParts[1]);
        var sourceStackNumber = int.Parse(stepParts[3]);
        var destinationStackNumber = int.Parse(stepParts[5]);

        var sourceStack = stacks[sourceStackNumber];
        var destinationStack = stacks[destinationStackNumber];

        while (numberOfItemsToMove > 0)
        {
            if (sourceStack.Count > 0)
                destinationStack.Push(sourceStack.Pop());

            --numberOfItemsToMove;
        }
    }
}

static Dictionary<int, Stack<string>> ParseStacks(string[] stackLines)
{
    var stacks = new Dictionary<int, Stack<string>>();
    var lateralIndex = 0;

    foreach (var stackNumber in stackLines.Last().Split(" ", StringSplitOptions.RemoveEmptyEntries))
    {
        stacks.Add(int.Parse(stackNumber), new Stack<string>());

        var stackItems = new List<string>();

        for (var x = 0; x < stackLines.Length - 1; x++)
        {
            var stackItem = stackLines[x].Substring(lateralIndex, 3).Trim();

            if (stackItem != string.Empty)
                stackItems.Add(stackItem);
        }

        stackItems.Reverse();

        var itemsInPushOrder = stackItems
            .Select(x => x.Replace("[", string.Empty).Replace("]", string.Empty))
            .ToArray();

        foreach (var item in itemsInPushOrder)
        {
            stacks[int.Parse(stackNumber)].Push(item);
        }

        lateralIndex += 4;
    }

    return stacks;
}