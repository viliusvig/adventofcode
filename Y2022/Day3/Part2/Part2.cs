namespace Day3.Part2;

public static class Part2
{
    private const string Priorities = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
    
    public static async Task Run(string inputPath = "Part1/d3_input.txt")
    {
        var prioritySum = 0;

        var elves = new List<string>();
        using var inputStream = File.OpenText(inputPath);
        while (await inputStream.ReadLineAsync() is var lineStr)
        {
            if (elves.Count == 3)
            {
                var item = elves[0].First(x => elves[1].Any(c => c.Equals(x)) && elves[2].Any(c => c.Equals(x)));

                prioritySum += Priorities.IndexOf(item) + 1;
                elves.Clear();
            }

            if (string.IsNullOrEmpty(lineStr)) break;
            
            elves.Add(string.Join("", lineStr.Distinct()));
        }

        Console.WriteLine($"\tSum of item priorities: {prioritySum}");
    }
}