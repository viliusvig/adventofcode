using Day2.Part1;

namespace Day2.Part2;


public static class Part2
{
    public static async Task Run(string inputPath = "Part1/d2_input.txt")
    {
        var score = 0;

        using var inputStream = File.OpenText(inputPath);

        while (await inputStream.ReadLineAsync() is var lineStr && lineStr != null)
        {
            var hands = lineStr.Split(" ");
            if (hands.Length != 2) continue;
            var opponentHand = hands.First().ToRpc();
            var playerHand = opponentHand.DecipherCheat(hands.Last());
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

    private static Rpc DecipherCheat(this Rpc input, string guide) => guide switch
    {
        // Lose
        "X" => input switch {
            Rpc.Rock => Rpc.Scissors,
            Rpc.Paper => Rpc.Rock,
            Rpc.Scissors => Rpc.Paper,
            _ => throw new ArgumentOutOfRangeException(nameof(input), input, null)
        },
        "Y" => input,
        // Win
        "Z" => input switch {
            Rpc.Rock => Rpc.Paper,
            Rpc.Paper => Rpc.Scissors,
            Rpc.Scissors => Rpc.Rock,
            _ => throw new ArgumentOutOfRangeException(nameof(input), input, null)
        },
        _ => throw new ArgumentOutOfRangeException(nameof(input), input, null)
    };

    private static Rpc ToRpc(this string input) =>
        input switch
        {
            "A" => Rpc.Rock,
            "B" => Rpc.Paper,
            "C" => Rpc.Scissors,
            _ => throw new ArgumentOutOfRangeException(nameof(input), input, $"Failed to parse hand: {input}")
        };

    private static int Value(this Rpc input) => (int)input;
}