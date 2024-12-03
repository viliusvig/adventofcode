using System.Text.RegularExpressions;

namespace Day3;

public class Part2
{
    public static async Task Run(string inputPath = "d3_input.txt")
    {
        var text = await File.ReadAllTextAsync(inputPath);
        var doRegex = new Regex(@"do\(\)(.+?)(don't\(\)|$)", RegexOptions.Singleline | RegexOptions.IgnoreCase);
        var regex = new Regex(@"mul\((\d{1,3}),(\d{1,3})\)");
        var dos = new List<string>();
        var sum = 0;

        dos.Add(text.Split("don't")[0]);

        foreach (Match match in doRegex.Matches(text))
        {
            dos.Add(match.Groups[1].Value);
        }
        
        foreach (Match match in regex.Matches(string.Join("", dos)))
        {
            sum += int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value);
        }
        
        Console.WriteLine(sum);
    }
}