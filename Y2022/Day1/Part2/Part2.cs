public static class Part2
{
    public static async Task Run(string inputPath = "Part1/input.txt", int topNumber = 3)
    {
        var sum = 0;
        var max = 0;
        var inventories = new List<int>();
        
        using var inputStream =  File.OpenText(inputPath);

        while (await inputStream.ReadLineAsync() is var lineStr && lineStr != null)
        {
            if (string.IsNullOrWhiteSpace(lineStr))
            {
                inventories.Add(sum);
                if (sum > max) max = sum;
                sum = 0;
            }
            else if (int.TryParse(lineStr, out var cals))
            {
                sum += cals;
            }
            else
            {
                throw new InvalidCastException($"Failed to parse calories: {cals}");
            }
        }
        
        inventories.Sort();
        inventories.Reverse();

        var topNSum = inventories.GetRange(0, topNumber).Sum();
        Console.WriteLine($"\tTop {topNumber} inventories carry {topNSum} calories in total");

    }
}