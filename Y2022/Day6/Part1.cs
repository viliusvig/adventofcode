namespace Day6;

public class Part1
{
    public static async Task Run(string inputPath = "d6_input.txt")
    {
        var input = await File.ReadAllTextAsync(inputPath);
        var startOfSignal = 0;
        var offset = 0;
        
        while (offset + 4 < input.Length)
        {
            var chars = input.Substring(offset, 4);

            if (chars.Distinct().Count() == 4)
            {
                startOfSignal = input.IndexOf(chars, StringComparison.InvariantCultureIgnoreCase) + 4;
                break;
            }

            offset++;
        }

        Console.WriteLine($"\tCharacters to be processed before first signal: {startOfSignal}");
    }
}