var input = File.ReadAllLines(@"C:\Users\aman-agrawal\Code\AoC\Day4.txt");

static void Print(int[,] board)
{
    Console.WriteLine();
    for (var i = 0; i < 5; i++)
    {
        for (var j = 0; j < 5; j++)
        {
            Console.Write($"{board[i, j]} ");
        }

        Console.WriteLine();
    }
}

SolvePart1(input);

void SolvePart1(string[] input)
{
    var numbersToBeDrawn = input[0]
            .Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(x => int.Parse(x))
            .ToArray();

    List<int[,]> allBoards = new List<int[,]>();

    for (var i = 1; i < input.Length;)
    {
        if (input[i] != "")
        {
            allBoards.Add(CreateBoard(input[i], input[i + 1], input[i + 2], input[i + 3], input[i + 4]));
            i += 5;
        }
        else
        {
            ++i;
        }
    }

    var allWinningBoards = new List<(int[,] winningBoard, int numberDrawn)>();

    foreach (var numberDrawn in numbersToBeDrawn)
    {
        allBoards = MarkAllBoards(numberDrawn, allBoards);

        int[,]? winningBoard = GetWinningBoard(allBoards);

        //if (winningBoard != null)
        //{
        //    var score1 = CalculateScore(winningBoard, numberDrawn);
        //    Console.WriteLine(score1);
        //    break;
        //}

        if (winningBoard != null)
        {
            Console.WriteLine(numberDrawn);
            allWinningBoards.Add((winningBoard, numberDrawn));
        }
    }

    var score = CalculateScore(
        allWinningBoards.Last().winningBoard,
        allWinningBoards.Last().numberDrawn);
    Console.WriteLine(score);
}

int CalculateScore(int[,] winningBoard, int numberDrawn)
{
    var sumOfUnmarkedNumbers = 0;

    for (var i = 0; i < 5; i++)
    {
        for (var j = 0; j < 5; j++)
        {
            if (winningBoard[i, j] != -1)
                sumOfUnmarkedNumbers += winningBoard[i, j];
        }
    }

    return sumOfUnmarkedNumbers * numberDrawn;
}

int[,]? GetWinningBoard(List<int[,]> allBoards)
{
    foreach (var board in allBoards)
    {
        if ((board.IsAnyRowFullyMarked() && board.IsNoColumnFullyMarked()) ||
            (board.IsAnyColumnFullyMarked() && board.IsNoRowFullyMarked()))
        {
            Print(board);

            return board;
        }
    }

    return null;
}

List<int[,]> MarkAllBoards(int numberDrawn, List<int[,]> allBoards)
{
    foreach (var board in allBoards)
    {
        for (var i = 0; i < 5; i++)
        {
            for (var j = 0; j < 5; j++)
            {
                if (board[i, j] == numberDrawn)
                    board[i, j] = -1;
            }
        }
    }

    return allBoards;
}

int[,] CreateBoard(params string[] rows)
{
    var board = new int[5, 5];

    for (var i = 0; i < rows.Length; i++)
    {
        var numbersInRow = rows[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);

        for (var j = 0; j < 5; j++)
        {
            board[i, j] = int.Parse(numbersInRow[j]);
        }
    }

    return board;
}

public static class ArrayExtensions
{
    public static bool IsAnyColumnFullyMarked(this int[,] board)
    {
        if (board[0, 0] + board[1, 0] + board[2, 0] + board[3, 0] + board[4, 0] == -5 ||
            board[0, 1] + board[1, 1] + board[2, 1] + board[3, 1] + board[4, 1] == -5 ||
            board[0, 2] + board[1, 2] + board[2, 2] + board[3, 2] + board[4, 2] == -5 ||
            board[0, 3] + board[1, 3] + board[2, 3] + board[3, 3] + board[4, 3] == -5 ||
            board[0, 4] + board[1, 4] + board[2, 4] + board[3, 4] + board[4, 4] == -5)
        {
            return true;
        }

        return false;
    }

    public static bool IsAnyRowFullyMarked(this int[,] board)
    {
        if (board[0, 0] + board[0, 1] + board[0, 2] + board[0, 3] + board[0, 4] == -5 ||
            board[1, 0] + board[1, 1] + board[1, 2] + board[1, 3] + board[1, 4] == -5 ||
            board[2, 0] + board[2, 1] + board[2, 2] + board[2, 3] + board[2, 4] == -5 ||
            board[3, 0] + board[3, 1] + board[3, 2] + board[3, 3] + board[3, 4] == -5 ||
            board[4, 0] + board[4, 1] + board[4, 2] + board[4, 3] + board[4, 4] == -5)
        {
            return true;
        }

        return false;
    }

    public static bool IsNoColumnFullyMarked(this int[,] board)
    {
        if (board[0, 0] + board[1, 0] + board[2, 0] + board[3, 0] + board[4, 0] != -5 &&
            board[0, 1] + board[1, 1] + board[2, 1] + board[3, 1] + board[4, 1] != -5 &&
            board[0, 2] + board[1, 2] + board[2, 2] + board[3, 2] + board[4, 2] != -5 &&
            board[0, 3] + board[1, 3] + board[2, 3] + board[3, 3] + board[4, 3] != -5 &&
            board[0, 4] + board[1, 4] + board[2, 4] + board[3, 4] + board[4, 4] != -5)
        {
            return true;
        }

        return false;
    }

    public static bool IsNoRowFullyMarked(this int[,] board)
    {
        if (board[0, 0] + board[0, 1] + board[0, 2] + board[0, 3] + board[0, 4] != -5 &&
            board[1, 0] + board[1, 1] + board[1, 2] + board[1, 3] + board[1, 4] != -5 &&
            board[2, 0] + board[2, 1] + board[2, 2] + board[2, 3] + board[2, 4] != -5 &&
            board[3, 0] + board[3, 1] + board[3, 2] + board[3, 3] + board[3, 4] != -5 &&
            board[4, 0] + board[4, 1] + board[4, 2] + board[4, 3] + board[4, 4] != -5)
        {
            return true;
        }

        return false;
    }
}