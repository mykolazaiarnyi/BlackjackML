using Microsoft.ML.Data;

public class DataItem
{
    public float CardSum { get; set; }

    public float CardCount { get; set; }

    public bool ShouldHit { get; set; }
}

public class DataItemPrediction : DataItem
{

    [ColumnName("PredictedLabel")]
    public bool Prediction { get; set; }
}
