using System.Text.RegularExpressions;

var lines = File.ReadAllLines(@"C:\Users\aman-agrawal\Code\AoC\2022\Day7-2022\Input-Large.txt");

var totalSize = 0;
var fdPattern = @"^[0-9]+\s{1}\w+\.{0,1}\w{0,3}$";
var currentDir = string.Empty;

var dirSizes = new Dictionary<string, int>();
var t = new Thing();

for (var x = 0; x < lines.Length; x++)
{
    if (lines[x].Contains("$ cd"))
    {
        t.ChangeDirTo(lines[x].Split(" ", StringSplitOptions.RemoveEmptyEntries)[2]);
    }

    if (lines[x].Contains("dir"))
    {
        t.AddDirUnder(t.Current, lines[x].Split(" ", StringSplitOptions.RemoveEmptyEntries)[1]);
    }

    if (Regex.IsMatch(lines[x], fdPattern))
    {
        t.AddFileTo(t.Current,
            lines[x].Split(" ", StringSplitOptions.RemoveEmptyEntries)[1],
            int.Parse(lines[x].Split(" ", StringSplitOptions.RemoveEmptyEntries)[0]));
    }
}

var dirSz = new List<(string DirName, int Size)>();

foreach (var d in t.Dirs.First().Dirs)
{
    GetSize(d, dirSz);
}

Console.WriteLine(dirSz.Where(x => x.Size <= 100_000).Sum(x => x.Size));

// Part 2
var totalAvailableSpace = 70000000;
var minSpaceNeeded = 30000000;

var unusedSpace = totalAvailableSpace - t.Dirs.First().TotalDirSize;
var targetCleanupSize = minSpaceNeeded - unusedSpace;

Console.WriteLine(dirSz.OrderBy(x => x.Size).Where(x => x.Size >= targetCleanupSize).FirstOrDefault().Size);

// End Part 2

void GetSize(Dir? d, List<(string DirName, int Size)>? list)
{
    list.Add((d.Name, d.TotalDirSize));

    foreach (var dnext in d.Dirs)
    {
        GetSize(dnext, list);
    }
}

public class Dir
{
    public List<Dir> Dirs { get; set; } = new();
    public List<MyFile> Files { get; set; } = new();
    public bool IsCurrent { get; set; }
    public string Name { get; set; }
    public Dir? Parent { get; set; }
    public int TotalDirSize => Files.Sum(x => x.Size) + Dirs.Sum(x => x.TotalDirSize);

    public void AddDir(string dir, bool? isCurrent = null) => Dirs.Add(new Dir
    {
        Name = dir,
        Parent = this,
        IsCurrent = isCurrent.GetValueOrDefault()
    });

    public void AddFile(string file, int size) => Files.Add(new MyFile { Name = file, Size = size });
}

public class MyFile
{
    public string Name { get; set; }
    public int Size { get; set; }
}

public class Thing
{
    public Thing()
    {
        var root = new Dir { IsCurrent = true, Name = "/" };
        Dirs.Add(root);
        Current = root;
    }

    public Dir Current { get; private set; }

    public List<Dir> Dirs { get; set; } = new();
    public List<MyFile> Files { get; set; } = new();

    public void AddDir(string dir)
    {
        var root = Dirs.FirstOrDefault(x => x.Parent == null);
        root.AddDir(dir);
    }

    public void AddDirUnder(Dir parentDir, string childDir)
    {
        parentDir.AddDir(childDir);
    }

    public void AddFileTo(Dir dir, string file, int size)
    {
        dir.AddFile(file, size);
    }

    public void AddFileToRoot(string file, int size)
    {
        var root = Dirs.FirstOrDefault(x => x.Parent == null);
        root.AddFile(file, size);
    }

    public void ChangeDirTo(string dir)
    {
        if (dir != "/")
        {
            if (dir == "..")
            {
                Current.IsCurrent = false;
                Current = Current.Parent!;
                Current.IsCurrent = true;
            }
            else
            {
                var switchDirTo = Current.Dirs.FirstOrDefault(x => x.Name == dir);
                switchDirTo.IsCurrent = true;
                Dirs.First().IsCurrent = false;
                Current.IsCurrent = false;
                Current = switchDirTo;
            }
        }
    }
}