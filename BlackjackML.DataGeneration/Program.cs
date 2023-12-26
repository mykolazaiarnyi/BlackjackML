using System.Text.Json;

const int DataSize = 100_000;
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
var usedCards = new bool[cards.Length];

var data = new List<DataItem>(DataSize + 21);

while (data.Count < DataSize)
{
    var r = new Random();
    int sum = 0;
    int cardCount = 2;

    var card = PickCard(r);
    sum += card;
    card = PickCard(r);
    sum += card;

    while (sum <= 21)
    {
        card = PickCard(r);
        var item = new DataItem
        {
            CardSum = sum,
            CardCount = cardCount,
            ShouldHit = sum + card <= 21
        };
        data.Add(item);
        sum += card;
        cardCount++;
    }

    usedCards.AsSpan().Fill(false);
}

var jsonData = JsonSerializer.Serialize(data);
File.WriteAllText("data.json", jsonData);

int PickCard(Random random)
{
    int index;
    do
    {
        index = random.Next(cards.Length);
    } while (usedCards[index]);

    return cards[index];
}