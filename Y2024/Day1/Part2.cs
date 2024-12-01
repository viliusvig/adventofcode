namespace Day1.Part2;

public class Part2
{
    public static async Task Run(string inputPath = "d1_input.txt")
    {
        var leftLocations = new List<int>();
        var rightLocations = new List<int>();
        var similarityScore = 0;

        using var inputStream =  File.OpenText(inputPath);

        while (await inputStream.ReadLineAsync() is var lineStr && lineStr != null)
        {
            var split = lineStr.Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            leftLocations.Add(int.Parse(split[0]));
            rightLocations.Add(int.Parse(split[1]));
        }

        foreach (var t in leftLocations)
        {
            similarityScore += t * rightLocations.FindAll(x => x == t).Count;
        }
        
        Console.WriteLine(similarityScore);
    }
}