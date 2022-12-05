var lines = File.ReadAllLines(@"C:\Users\aman-agrawal\Code\AoC\2022\Day5-2022\Input.txt").ToList();

var stackLines = lines.TakeWhile(x => x != string.Empty).ToArray();
var arrangementSteps = File.ReadAllLines(@"C:\Users\aman-agrawal\Code\AoC\2022\Day5-2022\Steps.txt").ToArray();//lines.Except(stackLines).Where(x => x != string.Empty).ToArray();

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