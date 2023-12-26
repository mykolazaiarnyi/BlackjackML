using BlackjackML.Console;
using BlackjackML.Core;
using BlackjackML.Model;

var player1 = new ConsolePlayer();
var player2 = new MLPlayer();

var game = new Game([player1, player2]);
var gameTask = game.StartAsync();

Console.WriteLine("Players 1 turn.");
await ProccessTurn(player1);
Console.WriteLine();
Console.WriteLine("Players 2 turn.");

await gameTask;
Console.WriteLine("Game has ended, results:");
Console.WriteLine($"Player1: {player1.CardSum}");
Console.WriteLine($"Player2: {player2.CardSum}");


static async Task ProccessTurn(ConsolePlayer player)
{
    while (true)
    {
        await player.WaitForHitPromptAsync();
        await Console.Out.WriteLineAsync($"Your sum is {player.CardSum}.");
        await Console.Out.WriteLineAsync($"Hit - 1, Stand - 0.");

        var choise = await Console.In.ReadLineAsync();
        if (choise == "1")
        {
            var card = await player.HitAsync();
            await Console.Out.WriteLineAsync($"Your card is {card}.");
        }
        else
        {
            player.Stand();
            return;
        }
    }
}

