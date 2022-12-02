namespace Day2.Part1;

public enum Rpc
{
    Rock = 1,
    Paper = 2,
    Scissors = 3
}

public static class Part1
{
    public static async Task Run(string inputPath = "Part1/d2_input.txt")
    {
        var score = 0;

        using var inputStream = File.OpenText(inputPath);

        while (await inputStream.ReadLineAsync() is var lineStr && lineStr != null)
        {
            var hands = lineStr.Split(" ");
            if (hands.Length != 2) continue;
            var playerHand = hands.Last().ToRpc();
            var opponentHand = hands.First().ToRpc();
            var roundScore = playerHand.Value();
            
            roundScore += playerHand == opponentHand ? 3 : playerHand.Against(opponentHand);

            score += roundScore;
        }

        Console.WriteLine($"\tPlayer finished the tournament with total score of {score}");
    }

    private static int Against(this Rpc input, Rpc against) =>
        input switch
        {
            Rpc.Rock => against switch
            {
                Rpc.Scissors => 6,
                _ => 0
            },
            Rpc.Paper => against switch
            {
                Rpc.Rock => 6,
                _ => 0
            },
            Rpc.Scissors => against switch
            {
                Rpc.Paper => 6,
                _ => 0
            },
            _ => throw new ArgumentOutOfRangeException(nameof(input), input, null)
        };

    private static Rpc ToRpc(this string input) =>
        input switch
        {
            "A" or "X" => Rpc.Rock,
            "B" or "Y" => Rpc.Paper,
            "C" or "Z" => Rpc.Scissors,
            _ => throw new ArgumentOutOfRangeException(nameof(input), input, $"Failed to parse hand: {input}")
        };

    private static int Value(this Rpc input) => (int)input;
}