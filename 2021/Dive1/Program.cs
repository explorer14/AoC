var depth = 0;
var hor = 0;
var aim = 0;

var smallCourse = new string[]
{
    "forward 5",
    "down 5",
    "forward 8",
    "up 3",
    "down 8",
    "forward 2"
};

var inputData = File.ReadAllLines(@"C:\Users\aman-agrawal\Code\AoC\Dive1.txt");

// correct answer = 2036120
//FollowCourse(inputData);
// correct answer = 2015547716
FollowCourseWithAim(inputData);

void FollowCourse(string[] smallCourse)
{
    foreach (var move in smallCourse)
    {
        var direction = move.Split(" ")[0];
        var distance = int.Parse(move.Split(" ")[1]);

        if (direction == "forward")
            hor += distance;
        else if (direction == "down")
            depth += distance;
        else if (direction == "up")
            depth -= distance;
    }

    Console.WriteLine($"Final value {hor * depth}");
}

void FollowCourseWithAim(string[] smallCourse)
{
    foreach (var move in smallCourse)
    {
        var direction = move.Split(" ")[0];
        var distance = int.Parse(move.Split(" ")[1]);

        if (direction == "forward")
        {
            hor += distance;
            depth += aim * distance;
        }
        else if (direction == "down")
        {
            //depth += distance;
            aim += distance;
        }
        else if (direction == "up")
        {
            //depth -= distance;
            aim -= distance;
        }
    }

    Console.WriteLine($"Final value {hor * depth}");
}