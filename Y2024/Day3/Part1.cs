using System.Text.RegularExpressions;

namespace Day3;

public class Part1
{
    public static async Task Run(string inputPath = "d3_input.txt")
    {
        var text = await File.ReadAllTextAsync(inputPath);
        var regex = new Regex(@"mul\((\d{1,3}),(\d{1,3})\)");
        var sum = 0;
        
        foreach (Match match in regex.Matches(text))
        {
            sum += int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value);
        }
        
        Console.WriteLine(sum);
    }
}