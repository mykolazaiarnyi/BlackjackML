namespace BlackjackML.Core
{
    public class Deck
    {
        private readonly Stack<int> _cards;

        public Deck()
        {
            var cards = new[]
            {
                2,  2,  2,  2,
                3,  3,  3,  3,
                4,  4,  4,  4,
                5,  5,  5,  5,
                6,  6,  6,  6,
                7,  7,  7,  7,
                8,  8,  8,  8,
                9,  9,  9,  9,
                10, 10, 10, 10,
                10, 10, 10, 10,
                10, 10, 10, 10,
                10, 10, 10, 10,
                11, 11, 11, 11,
            };
            var r = new Random();
            var shuffled = cards.OrderBy(_ => r.Next()).ToArray();
            _cards = new Stack<int>(shuffled);
        }

        public int DrawCard() => _cards.Pop();
    }
}
