namespace BlackjackML.Core
{
    public abstract class Player
    {
        public Guid PlayerId { get; } = Guid.NewGuid();
        public int CardSum { get; private set; }
        public int CardCount { get; private set; }

        public virtual void DealCard(int card)
        {
            CardSum += card;
            CardCount++;
        }

        public abstract Task<bool> PromptHitAsync();
        public abstract void NotifyGameEnd(GameResult result);
    }
}
