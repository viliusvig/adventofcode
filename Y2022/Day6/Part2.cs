namespace Day6;

public class Part2
{
    public static async Task Run(string inputPath = "d6_input.txt")
    {
        var input = await File.ReadAllTextAsync(inputPath);
        var startOfSignal = 0;
        var offset = 0;
        
        while (offset + 14 < input.Length)
        {
            var chars = input.Substring(offset, 14);

            if (chars.Distinct().Count() == 14)
            {
                startOfSignal = input.IndexOf(chars, StringComparison.InvariantCultureIgnoreCase) + 14;
                break;
            }

            offset++;
        }

        Console.WriteLine($"\tCharacters to be processed before first message: {startOfSignal}");
    }
}