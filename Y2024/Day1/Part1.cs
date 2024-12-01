namespace Day1.Part1;

public class Part1
{
    public static async Task Run(string inputPath = "d1_input.txt")
    {
        var leftLocations = new List<int>();
        var rightLocations = new List<int>();

        using var inputStream =  File.OpenText(inputPath);

        while (await inputStream.ReadLineAsync() is var lineStr && lineStr != null)
        {
            var split = lineStr.Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            leftLocations.Add(int.Parse(split[0]));
            rightLocations.Add(int.Parse(split[1]));
        }

        leftLocations.Sort();
        rightLocations.Sort();

        var distance = leftLocations.Select((t, i) => Math.Abs(t - rightLocations[i])).Sum();
        
        Console.WriteLine(distance);
    }
}