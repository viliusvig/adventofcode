namespace Day5;

public class Part1
{
    public static async Task Run(string inputPath = "d5_input.txt")
    {
        var input = await File.ReadAllLinesAsync(inputPath);
        var stacks = new Dictionary<int, Stack<char>>
        {
            { 1, new Stack<char>(new char[] { 'D', 'T', 'W', 'F', 'J', 'S', 'H', 'N' }) },
            { 2, new Stack<char>(new char[] { 'H', 'R', 'P', 'Q', 'T', 'N', 'B', 'G' }) },
            { 3, new Stack<char>(new char[] { 'L', 'Q', 'V' }) },
            { 4, new Stack<char>(new char[] { 'N', 'B', 'S', 'W', 'R', 'Q' }) },
            { 5, new Stack<char>(new char[] { 'N', 'D', 'F', 'T', 'V', 'M', 'B' }) },
            { 6, new Stack<char>(new char[] { 'M', 'D', 'B', 'V', 'H', 'T', 'R' }) },
            { 7, new Stack<char>(new char[] { 'D', 'B', 'Q', 'J' }) },
            { 8, new Stack<char>(new char[] { 'D', 'N', 'J', 'V', 'R', 'Z', 'H', 'Q' }) },
            { 9, new Stack<char>(new char[] { 'B', 'N', 'H', 'M', 'S' }) },
        };

        using var inputStream = File.OpenText(inputPath);
        while (await inputStream.ReadLineAsync() is var lineStr && lineStr != null)
        {
            var inputParts = lineStr.Replace("move", string.Empty).Replace("from", string.Empty)
                .Replace("to", string.Empty)
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);
            var amount = int.Parse(inputParts[0]);
            var from = int.Parse(inputParts[1]);
            var to = int.Parse(inputParts[2]);

            for (var i = 0; i < amount; i++)
            {
                if (stacks[from].TryPop(out var crate))
                {
                    stacks[to].Push(crate);
                }
            }
        }

        var topCrates = string.Join("", stacks.Values.Select(x => x.Peek()).ToList());


        Console.WriteLine($"\tTop crates are: {topCrates}");
    }
}