namespace BlackjackML.Core
{
    public class Game
    {
        private readonly Deck _deck = new();

        public Player[] Players { get; init; }

        public Guid GameId { get; } = Guid.NewGuid(); 

        public Game(Player[] players)
        {
            Players = players;
        }

        public void Hit(Player player)
        {
            player.DealCard(_deck.DrawCard());
        }

        public async Task StartAsync()
        {
            foreach (var player in Players)
            {
                Hit(player);
                Hit(player);
            }

            foreach (var player in Players)
            {
                while (await player.PromptHitAsync())
                {
                    Hit(player);
                }
            }

            var result = new GameResult
            {
                Players = Players
            };

            foreach (var player in Players)
            {
                player.NotifyGameEnd(result);
            }
        }
    }
}
