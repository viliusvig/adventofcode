namespace Day8;

public class Part1
{
    public static async Task Run(string inputPath = "d8_input.txt")
    {
        var input = await File.ReadAllLinesAsync(inputPath);
        var rows = input.Length;
        var cols = input[0].Length;
        var map = new int[rows, cols];
        var visibleTrees = rows * 2 + (cols - 2) * 2;

        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < input[i].Length; j++) map[i, j] = int.Parse(input[i][j].ToString());
        }

        for (var row = 1; row < rows - 1; row++)
        {
            for (var col = 1; col < cols - 1; col++)
            {
                var treeSize = map[row, col];

                // Check left
                var leftTrees = new List<int>();
                for (var leftIndex = col - 1; leftIndex > -1; leftIndex--)
                {
                    leftTrees.Add(map[row, leftIndex]);
                }

                var isVisibleFromLeft = leftTrees.All(x => x < treeSize);

                // Check Top
                var topTrees = new List<int>();
                for (var topIndex = row - 1; topIndex > -1; topIndex--)
                {
                    topTrees.Add(map[topIndex, col]);
                }
                
                var isVisibleFromTop = topTrees.All(x => x < treeSize);

                
                // Check right
                var rightTrees = new List<int>();
                for (var rightIndex = col + 1; rightIndex < cols; rightIndex++)
                {
                    rightTrees.Add(map[row, rightIndex]);
                }
                
                var isVisibleFromRight = rightTrees.All(x => x < treeSize);

                
                // Check Top
                var botTrees = new List<int>();
                for (var botIndex = row + 1; botIndex < rows; botIndex++)
                {
                    botTrees.Add(map[botIndex, col]);
                }
                
                var isVisibleFromBottom = botTrees.All(x => x < treeSize);

                if (isVisibleFromLeft || isVisibleFromTop || isVisibleFromRight || isVisibleFromBottom)
                {
                    visibleTrees++;
                }

            }
        }
        
        Console.WriteLine($"Total visible trees: {visibleTrees}");
    }
}