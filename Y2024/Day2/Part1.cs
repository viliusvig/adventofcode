namespace Day2;

public class Part1
{
    public static async Task Run(string inputPath = "d2_input.txt")
    {
        var safeReports = 0;
        using var inputStream =  File.OpenText(inputPath);

        while (await inputStream.ReadLineAsync() is var lineStr && lineStr != null)
        {
            var parts = Array.ConvertAll(lineStr.Split(" "), int.Parse);
            var isIncreasingOrder = parts[0] < parts[1];
            
            if (parts[0] == parts[1]) continue;

            var isSafe = true;
            for (int i = 0; i < parts.Length; i++)
            {
                if (i != parts.Length - 1)
                {
                    var diff = isIncreasingOrder ? parts[i + 1] - parts[i] : parts[i] - parts[i + 1];
                    if (!(diff is >= 1 and <= 3))
                    {
                        isSafe = false;
                        break;
                    }
                }
            }

            if (isSafe)
            {
                safeReports++;
            }

            //Console.WriteLine($"{lineStr} - {isSafe}");

            //if (safeReports == 5) break;
        }
        
        Console.WriteLine(safeReports);
    }
}