namespace Day4;

public static class Part2
{
    
    public static async Task Run(string inputPath = "d4_input.txt")
    {
        var coveredAssignments = 0;

        using var inputStream = File.OpenText(inputPath);
        while (await inputStream.ReadLineAsync() is var lineStr && lineStr != null)
        {
            var elfAssignments = lineStr.Split(',');
            var left = elfAssignments.First().Split('-');
            var leftFrom = int.Parse(left.First());
            var leftTo = int.Parse(left.Last());
            var right = elfAssignments.Last().Split('-');
            var rightFrom = int.Parse(right.First());
            var rightTo = int.Parse(right.Last());

            if (leftFrom <= rightFrom)
            {
                coveredAssignments += leftTo >= rightFrom ? 1 : 0;
            }
            else
            {
                coveredAssignments += rightTo >= leftFrom ? 1 : 0;
            }
        }

        Console.WriteLine($"\tAmount of partially covered assignment ranges: {coveredAssignments}");
    }
}