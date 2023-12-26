using Microsoft.ML;
using System.Text.Json;


var mlContext = new MLContext();

var data = JsonSerializer.Deserialize<DataItem[]>(File.ReadAllText("data.json"));

var trainingData = mlContext.Data.LoadFromEnumerable(data);

var pipeline = mlContext.Transforms.Concatenate("Features", new[] { nameof(DataItem.CardSum), nameof(DataItem.CardCount) })
    .Append(mlContext.BinaryClassification.Trainers.AveragedPerceptron(nameof(DataItem.ShouldHit)));

Console.WriteLine("Training started");
var model = pipeline.Fit(trainingData);
Console.WriteLine("Training completed");

mlContext.Model.Save(model, trainingData.Schema, "model.zip");
