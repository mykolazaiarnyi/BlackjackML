using BlackjackML.Core;

namespace BlackjackML.Console
{
    internal class ConsolePlayer : Player
    {
        private TaskCompletionSource _hitPromptCompletionSource = new();
        private TaskCompletionSource<bool> _hitPromptResultCompletionSource = new();
        private TaskCompletionSource<int> _hitResultCompletionSource = new();

        public override void NotifyGameEnd(GameResult result) { }

        public async Task WaitForHitPromptAsync()
        {
            await _hitPromptCompletionSource.Task;
            _hitPromptCompletionSource = new();
        }

        public override async Task<bool> PromptHitAsync()
        {
            _hitPromptCompletionSource.SetResult();
            var hit = await _hitPromptResultCompletionSource.Task;
            _hitPromptResultCompletionSource = new();
            return hit;
        }

        public override void DealCard(int card)
        {
            base.DealCard(card);

            if (CardCount <= 2)
                return;

            _hitResultCompletionSource.SetResult(card);
        }

        public async Task<int> HitAsync()
        {
            _hitPromptResultCompletionSource.SetResult(true);
            var card = await _hitResultCompletionSource.Task;
            _hitResultCompletionSource = new();
            return card;
        }

        public void Stand()
        {
            _hitPromptResultCompletionSource.SetResult(false);
        }
    }
}
