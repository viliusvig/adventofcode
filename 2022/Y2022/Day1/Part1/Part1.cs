namespace Day1.Part1;

public static class Part1
{
    public static async Task Run(string inputPath = "Part1/input.txt")
    {
        var sum = 0;
        var max = 0;
        
        using var inputStream =  File.OpenText(inputPath);

        while (await inputStream.ReadLineAsync() is var lineStr && lineStr != null)
        {
            if (string.IsNullOrWhiteSpace(lineStr))
            {
                if (sum > max) max = sum;
                sum = 0;
            }
            else if (int.TryParse(lineStr, out var cals))
            {
                sum += cals;
            }
            else
            {
                throw new InvalidCastException($"\tFailed to parse calories: {cals}");
            }
        }
        
        Console.WriteLine($"\tLargest amount of calories in a single inventory: {max}");
    }
}