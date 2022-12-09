using System.Text;

namespace Day7;

public static class Parts
{
    public static async Task Run(string inputPath = "d7_input.txt")
    {
        var root = new FsDirectory("/");
        var cwd = root;
        using var inputStream = File.OpenText(inputPath);
        while (await inputStream.ReadLineAsync() is var lineStr && lineStr != null)
        {
            if (lineStr.StartsWith('$'))
            {
                if (lineStr.StartsWith("$ cd"))
                {
                    var targetPath = lineStr.Split(" ", StringSplitOptions.RemoveEmptyEntries).Last();

                    switch (targetPath)
                    {
                        case "..":
                        {
                            var lastIndex = cwd.FullPath.LastIndexOf("/", StringComparison.Ordinal);
                            var targetDir = cwd.FullPath[..(lastIndex == 0 ? 0 : lastIndex)];
                            cwd = lastIndex > 0 ? root.GetChildDirectory(targetDir) : root;

                            if (cwd == null)
                            {
                                throw new Exception($"Error: {targetDir} not found.");
                            }

                            break;
                        }
                        case "/":
                            cwd = root;
                            break;
                        default:
                        {
                            if (cwd.FullPath == targetPath) continue;

                            var targetDir = string.Join(Path.AltDirectorySeparatorChar, cwd.FullPath.NormalizePath(),
                                targetPath);
                            cwd = root.GetChildDirectory(targetDir);

                            if (cwd == null)
                            {
                                throw new Exception($"Error: {targetDir} not found.");
                            }

                            break;
                        }
                    }
                }
                else if (lineStr.StartsWith("$ ls"))
                {
                    // ignore, no use
                }
            }
            else if (lineStr.StartsWith("dir "))
            {
                // Define dir
                var inputParts = lineStr.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                if (cwd == null) throw new Exception("CWD is null");
                cwd.Children.Add(
                    new FsDirectory(
                        $"{string.Join(Path.AltDirectorySeparatorChar, cwd.FullPath.NormalizePath(), inputParts.Last())}"));
            }
            else
            {
                // Define File
                var inputParts = lineStr.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                if (cwd == null) throw new Exception("CWD is null");
                cwd.Children.Add(new FsFile(
                    $"{string.Join(Path.AltDirectorySeparatorChar, cwd.FullPath.NormalizePath(), inputParts.Last())}",
                    int.Parse(inputParts.First())));
            }
        }

        Console.WriteLine(
            $"\tTotal size of directories smaller than 100000: {root.GetDirectoriesWithSizeLess(100000)}");
        Console.WriteLine(
            "Part 2: Find the smallest directory that, if deleted, would free up enough space on the filesystem to run the update. What is the total size of that directory?");
        Console.WriteLine(
            $"\tTotal size of directory to delete for update: {root.GetSmallestDirectoryToDeleteForUpdate(root.Size)}");
        //Console.Write(root.TreeRepresentation());
    }

    private static string NormalizePath(this string input) => input == $"{Path.AltDirectorySeparatorChar}" ? "" : input;
}

public abstract class FsEntity
{
    public string FullPath { get; set; }
    public string Name { get; set; }
    public virtual int Size { get; init; }
}

public class FsFile : FsEntity
{
    public FsFile(string fullPath, int size)
    {
        FullPath = fullPath;
        Size = size;
        Name = Path.GetFileName(fullPath);
    }

    public override string ToString() => $"{Name} (file, size={Size})";
}

public class FsDirectory : FsEntity
{
    public FsDirectory(string fullPath)
    {
        FullPath = fullPath;
        Name = fullPath.Split("/", StringSplitOptions.RemoveEmptyEntries).LastOrDefault() ?? "/";
    }

    public List<FsEntity> Children { get; } = new();

    public FsDirectory? GetChildDirectory(string fullPath)
    {
        foreach (var dir in Children.OfType<FsDirectory>())
        {
            if (dir.FullPath == fullPath) return dir;

            var subDir = dir.GetChildDirectory(fullPath);
            if (subDir != null) return subDir;
        }

        return null;
    }

    public override int Size => Children.Sum(x => x.Size);

    public int GetDirectoriesWithSizeOver(int minSize)
    {
        var size = 0;

        foreach (var dir in Children.OfType<FsDirectory>())
        {
            if (dir.Size >= minSize) size += dir.Size;

            size += dir.GetDirectoriesWithSizeOver(minSize);
        }

        return size;
    }

    public int GetDirectoriesWithSizeLess(int maxSize)
    {
        var size = 0;

        foreach (var dir in Children.OfType<FsDirectory>())
        {
            if (dir.Size <= maxSize) size += dir.Size;

            size += dir.GetDirectoriesWithSizeLess(maxSize);
        }

        return size;
    }

    public int GetSmallestDirectoryToDeleteForUpdate(int rootSize, int dirSize = 0)
    {
        var result = dirSize == 0 ? Size : dirSize;
        var freeSpace = 70000000 - rootSize;
        const int updateReq = 30000000;

        foreach (var dir in Children.OfType<FsDirectory>())
        {
            if (dir.Size < result && freeSpace + dir.Size >= updateReq) result = dir.Size;
            
            var nestedSize = dir.GetSmallestDirectoryToDeleteForUpdate(rootSize, result);
            if (nestedSize < result && freeSpace + nestedSize >= updateReq) result = nestedSize;
        }

        return result;
    }

    public override string ToString() => $"{Name} (dir)";

    public string TreeRepresentation(int indent = 0)
    {
        var sb = new StringBuilder();
        var prefix = string.Concat(Enumerable.Repeat("  ", indent));

        sb.AppendLine($"{prefix}- {Name} (dir, size={Size}, items={Children.Count})");

        var test = indent + 1;
        prefix = string.Concat(Enumerable.Repeat("  ", test));
        foreach (var fse in Children)
        {
            switch (fse)
            {
                case FsFile f:
                    sb.AppendLine($"{prefix}- {f.Name} (file, size={f.Size}) {f.FullPath}");
                    break;
                case FsDirectory dir:

                    sb.Append(dir.TreeRepresentation(test));
                    break;
            }
        }

        return sb.ToString();
    }
}