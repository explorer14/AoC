var input = File.ReadAllLines(@"C:\Users\aman-agrawal\Code\AoC\Diag.txt");
//var bitmap = CreateBitmap(input);

int[,] CreateBitmap(string[] input)
{
    var bitmap = new int[input!.Length, input[0].Length];

    for (var i = 0; i < input.Length; i++)
    {
        for (var j = 0; j < input[i].Length; j++)
        {
            bitmap[i, j] = int.Parse(input[i][j].ToString());
        }
    }

    return bitmap;
}

// correct answer = 3847100
//ProcessPart1(bitmap);
// coorect answer = 4105235
ProcessPart2(input);

void ProcessPart2(string[] allNumbers)
{
    int oxygen = CalculateOxygenRating(allNumbers);
    int co2 = CalculateCo2Rating(allNumbers);

    Console.WriteLine(oxygen * co2);
}

int CalculateCo2Rating(string[] allNumbers)
{
    var numbersToConsider = new List<string>(allNumbers);

    for (var j = 0; j < allNumbers[0].Length; j++)
    {
        var zeroCount = 0;
        var oneCount = 0;
        var bitmap = CreateBitmap(numbersToConsider.ToArray());

        for (var i = 0; i < numbersToConsider.Count; i++)
        {
            if (bitmap[i, j] == 0)
                ++zeroCount;
            else
                ++oneCount;
        }

        var numbersToKeep = 0;
        var numberInPosition = 0;

        if (oneCount < zeroCount)
        {
            numbersToKeep = oneCount;
            numberInPosition = 1;
        }
        else
        {
            numbersToKeep = zeroCount;
        }

        for (var i = 0; i < allNumbers.Length; i++)
        {
            if (allNumbers[i].Substring(j, 1) != numberInPosition.ToString())
            {
                numbersToConsider.Remove(allNumbers[i]);
            }
        }

        if (numbersToConsider.Count == 1)
            break;
    }

    var co2String = numbersToConsider.Select(x => x.ToString()).Aggregate((a, b) => a + b);
    var co2Decimal = Convert.ToInt32(co2String, fromBase: 2);

    return co2Decimal;
}

int CalculateOxygenRating(string[] allNumbers)
{
    var numbersToConsider = new List<string>(allNumbers);

    for (var j = 0; j < allNumbers[0].Length; j++)
    {
        var zeroCount = 0;
        var oneCount = 0;
        var bitmap = CreateBitmap(numbersToConsider.ToArray());

        for (var i = 0; i < numbersToConsider.Count; i++)
        {
            if (bitmap[i, j] == 0)
                ++zeroCount;
            else
                ++oneCount;
        }

        var numbersToKeep = 0;
        var numberStartsWith = 0;

        if (oneCount >= zeroCount)
        {
            numbersToKeep = oneCount;
            numberStartsWith = 1;
        }
        else
        {
            numbersToKeep = zeroCount;
        }

        for (var i = 0; i < allNumbers.Length; i++)
        {
            if (allNumbers[i].Substring(j, 1) != numberStartsWith.ToString())
            {
                numbersToConsider.Remove(allNumbers[i]);
            }
        }
    }

    var oxyString = numbersToConsider.Select(x => x.ToString()).Aggregate((a, b) => a + b);
    var oxyDecimal = Convert.ToInt32(oxyString, fromBase: 2);

    return oxyDecimal;
}

void ProcessPart1(int[,] bitmap)
{
    var gammaBits = new int[input[0].Length];
    var epsilonBits = new int[input[0].Length];

    for (var j = 0; j < input[0].Length; j++)
    {
        var zeroCount = 0;
        var oneCount = 0;

        for (var i = 0; i < input.Length; i++)
        {
            if (bitmap[i, j] == 0)
                ++zeroCount;
            else
                ++oneCount;
        }

        gammaBits[j] = zeroCount > oneCount ? 0 : 1;
        epsilonBits[j] = zeroCount > oneCount ? 1 : 0;
    }

    var gammaString = gammaBits.Select(x => x.ToString()).Aggregate((a, b) => a + b);
    var gammaDecimal = Convert.ToInt32(gammaString, fromBase: 2);
    var epsilonString = epsilonBits.Select(x => x.ToString()).Aggregate((a, b) => a + b);
    var epsilonDecimal = Convert.ToInt32(epsilonString, fromBase: 2);

    Console.WriteLine($"{gammaDecimal * epsilonDecimal}");
}