var lines = File.ReadAllLines(@"C:\Users\aman-agrawal\Code\AoC\2022\Day2-2022\Input.txt").ToList();

SolvePart1(lines);

SolvePart2(lines);

static int GetRoundScore(char opponentMove, char myMove)
{
    var roundOutcomeLost = 0;
    var roundOutcomeDraw = 3;
    var roundOutcomeWin = 6;

    char opponentRock = 'A';
    char responsePaper = 'Y';

    char opponentPaper = 'B';
    char responseRock = 'X';

    if (opponentMove == opponentRock)
    {
        if (myMove == responseRock)
            return roundOutcomeDraw;
        else if (myMove == responsePaper)
            return roundOutcomeWin;
        else
            return roundOutcomeLost;
    }
    else if (opponentMove == opponentPaper)
    {
        if (myMove == responseRock)
            return roundOutcomeLost;
        else if (myMove == responsePaper)
            return roundOutcomeDraw;
        else
            return roundOutcomeWin;
    }
    else
    {
        if (myMove == responseRock)
            return roundOutcomeWin;
        else if (myMove == responsePaper)
            return roundOutcomeLost;
        else
            return roundOutcomeDraw;
    }
}

static (char MyMove, int RoundScore) GetRoundScoreAndMyMoveForDesiredResult(char opponentMove, char result)
{
    char rock = 'A';
    char paper = 'B';
    char scissors = 'C';

    char resultLose = 'X';
    char resultWin = 'Z';

    var roundOutcomeLost = 0;
    var roundOutcomeDraw = 3;
    var roundOutcomeWin = 6;

    if (opponentMove == rock)
    {
        if (result == resultWin)
            return (paper, roundOutcomeWin);
        else if (result == resultLose)
            return (scissors, roundOutcomeLost);
        else
            return (rock, roundOutcomeDraw);
    }
    else if (opponentMove == paper)
    {
        if (result == resultWin)
            return (scissors, roundOutcomeWin);
        else if (result == resultLose)
            return (rock, roundOutcomeLost);
        else
            return (paper, roundOutcomeDraw);
    }
    else
    {
        if (result == resultWin)
            return (rock, roundOutcomeWin);
        else if (result == resultLose)
            return (paper, roundOutcomeLost);
        else
            return (scissors, roundOutcomeDraw);
    }
}

static void SolvePart1(List<string> lines)
{
    char responsePaper = 'Y';
    char responseRock = 'X';
    char responseScissors = 'Z';
    var totalScore = 0;

    var moveScoreMap = new Dictionary<char, int>();
    moveScoreMap.Add(responseRock, 1);
    moveScoreMap.Add(responsePaper, 2);
    moveScoreMap.Add(responseScissors, 3);

    foreach (var line in lines)
    {
        var roundOutcome = 0;
        var opponentMove = char.Parse(line.Split(' ')[0]);
        var myMove = char.Parse(line.Split(' ')[1]);

        roundOutcome = moveScoreMap[myMove] + GetRoundScore(opponentMove, myMove);

        totalScore += roundOutcome;
    }

    Console.WriteLine(totalScore);
}

static void SolvePart2(List<string> lines)
{
    char rock = 'A';
    char paper = 'B';
    char scissors = 'C';
    var moveScoreMap2 = new Dictionary<char, int>();
    moveScoreMap2.Add(rock, 1);
    moveScoreMap2.Add(paper, 2);
    moveScoreMap2.Add(scissors, 3);
    var totalScore = 0;

    foreach (var line in lines)
    {
        var roundOutcome = 0;
        var opponentMove = char.Parse(line.Split(' ')[0]);
        var expectedOutcome = char.Parse(line.Split(' ')[1]);

        var result = GetRoundScoreAndMyMoveForDesiredResult(opponentMove, expectedOutcome);
        roundOutcome = moveScoreMap2[result.MyMove] + result.RoundScore;

        totalScore += roundOutcome;
    }

    Console.WriteLine(totalScore);
}