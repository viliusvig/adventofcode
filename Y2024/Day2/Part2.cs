namespace Day2;

public class Part2
{
    public static async Task Run(string inputPath = "d2_input.txt")
    {
        var safeReports = 0;
        using var inputStream =  File.OpenText(inputPath);

        while (await inputStream.ReadLineAsync() is var lineStr && lineStr != null)
        {
            var parts = Array.ConvertAll(lineStr.Split(" "), int.Parse);
            var isIncreasingOrder = parts[0] < parts[1];

            if (parts[0] == parts[1])
            {
                isIncreasingOrder = parts[1] < parts[2];
            } 
            else if (parts[1] == parts[2])
            {
                isIncreasingOrder = parts[2] < parts[3];
            }
            else if (parts[2] == parts[3])
            {
                isIncreasingOrder = parts[3] < parts[4];
            }
            
            var isSafe = true;
            var dampenerUsed = false;
            var i = 0;
            for (i = 0; i < parts.Length; i++)
            {
                if (i != parts.Length - 1)
                {
                    var diff = isIncreasingOrder ? parts[i + 1] - parts[i] : parts[i] - parts[i + 1];
                    if (!(diff is >= 1 and <= 3))
                    {
                        if (!dampenerUsed)
                        {
                            dampenerUsed = true;
                            //Console.WriteLine($"Removing value {(isIncreasingOrder ? parts[i+1] : parts[i])} at position {(isIncreasingOrder ? i + 1 : i)}");
                            parts = parts.Where((source, index) => isIncreasingOrder ? index != i + 1 : index != i).ToArray();
                            i = 0;
                            continue;
                        }
                        isSafe = false;
                        break;
                    }
                }
            }

            if (isSafe)
            {
                safeReports++;
            }
            // else
            // {
            //     Console.WriteLine($"{lineStr} - {isSafe} - {dampenerUsed}");
            //     Console.WriteLine($"{string.Join("   ", Enumerable.Repeat("", i + 2))}|");
            //     Console.WriteLine(string.Join(" ", parts));
            //     Console.WriteLine("-------------------------");
            //     Console.WriteLine();
            // }



            //if (safeReports == 5) break;
        }
        
        Console.WriteLine(safeReports);

        //652
        //Safe reports: 658
        // result is off by 6

        //var s = (await File.ReadAllLinesAsync(inputPath)).Select(a => a.Split(' ').Select(b => int.Parse(b)).ToList()).ToList();
        //Console.WriteLine($"Safe reports: {CountSafeDampened(s)}");
    }

    #region check
    public static int CountSafeDampened(List<List<int>> reports)
    {
        int count = 0;

        foreach (var report in reports)
        {
            if (IsReportSafe(report))
            {
                count++;
            }
            else
            {
                for (int i = 0; i < report.Count; i++)
                {
                    if (IsReportSafe(report.Where((x, index) => index != i).ToList()))
                    {
                        count++;
                        break;
                    }
                }
            }
        }

        return count;
    }

    private static bool IsReportSafe(List<int> report)
    {
        bool increasing = false;

        for (int i = 0; i < report.Count; i++)
        {
            if (i == 0)
            {
                if (report[1] > report[0])
                {
                    increasing = true;
                }
                else
                {
                    increasing = false;
                }
            }
            else
            {
                if ((increasing && report[i - 1] > report[i]) || (!increasing && report[i - 1] < report[i]))
                {
                    return false;
                }

                if (Math.Abs(report[i - 1] - report[i]) < 1 || Math.Abs(report[i - 1] - report[i]) > 3)
                {
                    return false;
                }
            }
        }

        return true;
    }
    #endregion

}