var lines = File.ReadAllLines(@"C:\Code\AoC\2022\Day6-2022\Input-Large.txt");

foreach (var line in lines)
{
    var rrc4 = new List<string>();
    var nextCharIndex = 0;

    for (var k = 0; k < line.Length; k++)
    {
        for (var x = 0; x < 4 && rrc4.Count != 4; x++)
        {
            rrc4.Add(line.Substring(nextCharIndex++, 1));
        }

        if (rrc4.Distinct().Count() == rrc4.Count)
        {
            var startOfPacketMarker = nextCharIndex;
            Console.WriteLine(startOfPacketMarker);
            break;
        }

        rrc4.RemoveAt(0);
    }
}

Console.WriteLine("------");

foreach (var line in lines)
{
    var rrc4 = new List<string>();
    var nextCharIndex = 0;

    for (var k = 0; k < line.Length; k++)
    {
        for (var x = 0; x < 14 && rrc4.Count != 14; x++)
        {
            rrc4.Add(line.Substring(nextCharIndex++, 1));
        }

        if (rrc4.Distinct().Count() == rrc4.Count)
        {
            var startOfPacketMarker = nextCharIndex;
            Console.WriteLine(startOfPacketMarker);
            break;
        }

        rrc4.RemoveAt(0);
    }
}