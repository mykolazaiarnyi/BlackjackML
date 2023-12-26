using BlackjackML.Core;
using Microsoft.ML;

namespace BlackjackML.Model
{
    public class MLPlayer : Player
    {
        private readonly PredictionEngine<DataItem, DataItemPrediction> _engine;

        public MLPlayer()
        {
            var mlContext = new MLContext();
            var model = mlContext.Model.Load("model.zip", out _);

            _engine = mlContext.Model.CreatePredictionEngine<DataItem, DataItemPrediction>(model);
        }

        public override void NotifyGameEnd(GameResult result) { }

        public override Task<bool> PromptHitAsync()
        {
            return Task.FromResult(ShouldHit);
        }

        private bool ShouldHit => _engine.Predict(new DataItem { CardSum = CardSum, CardCount = CardCount }).Prediction;
    }
}
