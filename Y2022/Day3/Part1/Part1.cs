namespace Day3.Part1;

public static class Part1
{
    private const string Priorities = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
    
    public static async Task Run(string inputPath = "Part1/d3_input.txt")
    {
        var prioritySum = 0;

        using var inputStream = File.OpenText(inputPath);
        while (await inputStream.ReadLineAsync() is var lineStr && lineStr != null)
        {
            var left = lineStr[..(lineStr.Length / 2)].Distinct();
            var right = lineStr[(lineStr.Length / 2)..].Distinct();

            var item = left.FirstOrDefault(x => right.Any(c => c.Equals(x)));

            prioritySum += Priorities.IndexOf(item) + 1;
        }

        Console.WriteLine($"\tSum of item priorities: {prioritySum}");
    }
}