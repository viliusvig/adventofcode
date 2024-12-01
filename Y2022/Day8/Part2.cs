namespace Day8;

public class Part2
{
    public static async Task Run(string inputPath = "d8_input.txt")
    {
        var input = await File.ReadAllLinesAsync(inputPath);
        var rows = input.Length;
        var cols = input[0].Length;
        var map = new int[rows, cols];
        var highestScenicScore = 0;

        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < input[i].Length; j++) map[i, j] = int.Parse(input[i][j].ToString());
        }

        for (var row = 0; row < rows; row++)
        {
            for (var col = 0; col < cols; col++)
            {
                var treeSize = map[row, col];

                // Check left
                var leftTrees = new List<int>();
                for (var leftIndex = col - 1; leftIndex > -1; leftIndex--)
                {
                    leftTrees.Add(map[row, leftIndex]);
                }

                var leftDistance = leftTrees.Count(x => x < treeSize);

                // Check Top
                var topTrees = new List<int>();
                for (var topIndex = row - 1; topIndex > -1; topIndex--)
                {
                    topTrees.Add(map[topIndex, col]);
                }
                
                var topDistance = topTrees.Count(x => x < treeSize);

                
                // Check right
                var rightTrees = new List<int>();
                var ri = col + 1;
                for (var rightIndex = ri; rightIndex < cols; rightIndex++)
                {
                    rightTrees.Add(map[row, rightIndex]);
                    ri++;
                }

                var rightDistance = rightTrees.Count(x => x < treeSize);

                
                // Check Top
                var botTrees = new List<int>();
                var bi = row + 1;
                for (var botIndex = bi; botIndex < rows; botIndex++)
                {
                    botTrees.Add(map[botIndex, col]);
                    bi = botIndex;
                }
                
                var bottomDistance = botTrees.Count(x => x < treeSize);

                var scenicScore = leftDistance * topDistance * rightDistance * bottomDistance;

                if (scenicScore > highestScenicScore) highestScenicScore = scenicScore;
            }
        }
        
        Console.WriteLine($"Highest scenic score: {highestScenicScore}");
    }
}