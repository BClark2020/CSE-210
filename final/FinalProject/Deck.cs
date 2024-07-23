public class Deck
{
    public int _handValue { get; private set; }
    public int _optionalHandValue { get; private set; }
    public bool _ace { get; private set; }
    public List<string> ShuffledDeck { get; private set; }
    private List<string> deck;
    private Random random = new Random();
    public Deck()
    {
        deck = new List<string>
        {
            "A♠", "2♠", "3♠", "4♠", "5♠", "6♠", "7♠", "8♠", "9♠", "10♠", "J♠", "Q♠", "K♠",
            "A♦", "2♦", "3♦", "4♦", "5♦", "6♦", "7♦", "8♦", "9♦", "10♦", "J♦", "Q♦", "K♦",
            "A♥", "2♥", "3♥", "4♥", "5♥", "6♥", "7♥", "8♥", "9♥", "10♥", "J♥", "Q♥", "K♥",
            "A♣", "2♣", "3♣", "4♣", "5♣", "6♣", "7♣", "8♣", "9♣", "10♣", "J♣", "Q♣", "K♣",
            "A♠", "2♠", "3♠", "4♠", "5♠", "6♠", "7♠", "8♠", "9♠", "10♠", "J♠", "Q♠", "K♠",
            "A♦", "2♦", "3♦", "4♦", "5♦", "6♦", "7♦", "8♦", "9♦", "10♦", "J♦", "Q♦", "K♦",
            "A♥", "2♥", "3♥", "4♥", "5♥", "6♥", "7♥", "8♥", "9♥", "10♥", "J♥", "Q♥", "K♥",
            "A♣", "2♣", "3♣", "4♣", "5♣", "6♣", "7♣", "8♣", "9♣", "10♣", "J♣", "Q♣", "K♣",
            "A♠", "2♠", "3♠", "4♠", "5♠", "6♠", "7♠", "8♠", "9♠", "10♠", "J♠", "Q♠", "K♠",
            "A♦", "2♦", "3♦", "4♦", "5♦", "6♦", "7♦", "8♦", "9♦", "10♦", "J♦", "Q♦", "K♦",
            "A♥", "2♥", "3♥", "4♥", "5♥", "6♥", "7♥", "8♥", "9♥", "10♥", "J♥", "Q♥", "K♥",
            "A♣", "2♣", "3♣", "4♣", "5♣", "6♣", "7♣", "8♣", "9♣", "10♣", "J♣", "Q♣", "K♣",
            "A♠", "2♠", "3♠", "4♠", "5♠", "6♠", "7♠", "8♠", "9♠", "10♠", "J♠", "Q♠", "K♠",
            "A♦", "2♦", "3♦", "4♦", "5♦", "6♦", "7♦", "8♦", "9♦", "10♦", "J♦", "Q♦", "K♦",
            "A♥", "2♥", "3♥", "4♥", "5♥", "6♥", "7♥", "8♥", "9♥", "10♥", "J♥", "Q♥", "K♥",
            "A♣", "2♣", "3♣", "4♣", "5♣", "6♣", "7♣", "8♣", "9♣", "10♣", "J♣", "Q♣", "K♣",
            "A♠", "2♠", "3♠", "4♠", "5♠", "6♠", "7♠", "8♠", "9♠", "10♠", "J♠", "Q♠", "K♠",
            "A♦", "2♦", "3♦", "4♦", "5♦", "6♦", "7♦", "8♦", "9♦", "10♦", "J♦", "Q♦", "K♦",
            "A♥", "2♥", "3♥", "4♥", "5♥", "6♥", "7♥", "8♥", "9♥", "10♥", "J♥", "Q♥", "K♥",
            "A♣", "2♣", "3♣", "4♣", "5♣", "6♣", "7♣", "8♣", "9♣", "10♣", "J♣", "Q♣", "K♣",
            "A♠", "2♠", "3♠", "4♠", "5♠", "6♠", "7♠", "8♠", "9♠", "10♠", "J♠", "Q♠", "K♠",
            "A♦", "2♦", "3♦", "4♦", "5♦", "6♦", "7♦", "8♦", "9♦", "10♦", "J♦", "Q♦", "K♦",
            "A♥", "2♥", "3♥", "4♥", "5♥", "6♥", "7♥", "8♥", "9♥", "10♥", "J♥", "Q♥", "K♥",
            "A♣", "2♣", "3♣", "4♣", "5♣", "6♣", "7♣", "8♣", "9♣", "10♣", "J♣", "Q♣", "K♣",
            "A♠", "2♠", "3♠", "4♠", "5♠", "6♠", "7♠", "8♠", "9♠", "10♠", "J♠", "Q♠", "K♠",
            "A♦", "2♦", "3♦", "4♦", "5♦", "6♦", "7♦", "8♦", "9♦", "10♦", "J♦", "Q♦", "K♦",
            "A♥", "2♥", "3♥", "4♥", "5♥", "6♥", "7♥", "8♥", "9♥", "10♥", "J♥", "Q♥", "K♥",
            "A♣", "2♣", "3♣", "4♣", "5♣", "6♣", "7♣", "8♣", "9♣", "10♣", "J♣", "Q♣", "K♣",
            "A♠", "2♠", "3♠", "4♠", "5♠", "6♠", "7♠", "8♠", "9♠", "10♠", "J♠", "Q♠", "K♠",
            "A♦", "2♦", "3♦", "4♦", "5♦", "6♦", "7♦", "8♦", "9♦", "10♦", "J♦", "Q♦", "K♦",
            "A♥", "2♥", "3♥", "4♥", "5♥", "6♥", "7♥", "8♥", "9♥", "10♥", "J♥", "Q♥", "K♥",
            "A♣", "2♣", "3♣", "4♣", "5♣", "6♣", "7♣", "8♣", "9♣", "10♣", "J♣", "Q♣", "K♣",
            "A♠", "2♠", "3♠", "4♠", "5♠", "6♠", "7♠", "8♠", "9♠", "10♠", "J♠", "Q♠", "K♠",
            "A♦", "2♦", "3♦", "4♦", "5♦", "6♦", "7♦", "8♦", "9♦", "10♦", "J♦", "Q♦", "K♦",
            "A♥", "2♥", "3♥", "4♥", "5♥", "6♥", "7♥", "8♥", "9♥", "10♥", "J♥", "Q♥", "K♥",
            "A♣", "2♣", "3♣", "4♣", "5♣", "6♣", "7♣", "8♣", "9♣", "10♣", "J♣", "Q♣", "K♣"
        };
        ShuffledDeck = new List<string>(deck);
        ShuffleDeck();
    }
    public void ShuffleDeck()
    {
        ShuffledDeck = new List<string>(deck);
        Shuffle(ShuffledDeck);
        Shuffle(ShuffledDeck);
        ShuffledDeck = Cut(ShuffledDeck);
        Shuffle(ShuffledDeck);
    }
    private static void Shuffle(List<string> deck)
    {
        Random rand = new Random();
        int n = deck.Count;
        for (int i = n - 1; i > 0; i--)
        {
            int j = rand.Next(i + 1);
            string temp = deck[i];
            deck[i] = deck[j];
            deck[j] = temp;
        }
    }
    private List<string> Cut(List<string> deck)
    {
        int cutter = random.Next(2, 6);
        int cutPoint = deck.Count / cutter;
        List<string> topHalf = deck.Take(cutPoint).ToList();
        List<string> bottomHalf = deck.Skip(cutPoint).ToList();
        List<string> newDeck = new List<string>();
        newDeck.AddRange(bottomHalf);
        newDeck.AddRange(topHalf);
        return newDeck;
    }
    public void CalculateHandValue(List<string> _hand)
    {
        _handValue = 0;
        _optionalHandValue = 0;
        _ace = false;
        int _aces = 0;
        foreach (string _card in _hand)
        {
            if ((_card == "A♠" || _card == "A♦" || _card == "A♥" || _card == "A♣") && _aces == 0)
            {
                _ace = true;
                _aces++;
            }
            else if (_card == "A♠" || _card == "A♦" || _card == "A♥" || _card == "A♣" && _aces >= 1)
            {
                _handValue += 1;
                _optionalHandValue += 1;
            }
            else
            {
                _handValue += _handValues[_card];
                _optionalHandValue += _handValues[_card];
            }
        }
        if (_ace == true)
        {
            if (_handValue + 11 > 21)
            {
                _handValue += 1;
                _ace = false;
            }
            else
            {
                _optionalHandValue += 11;
                _handValue += 1;
                _ace = true;
            }
        }
    }
    public readonly Dictionary<string, int> _handValues = new Dictionary<string, int>
    {
        {"A♠", 1}, { "2♠", 2 }, { "3♠", 3 }, { "4♠", 4 }, { "5♠", 5 }, { "6♠", 6 }, { "7♠", 7 }, { "8♠", 8 }, { "9♠", 9 }, { "10♠", 10 }, { "J♠", 10 }, { "Q♠", 10 }, { "K♠", 10 },
        {"A♦", 1}, { "2♦", 2 }, { "3♦", 3 }, { "4♦", 4 }, { "5♦", 5 }, { "6♦", 6 }, { "7♦", 7 }, { "8♦", 8 }, { "9♦", 9 }, { "10♦", 10 }, { "J♦", 10 }, { "Q♦", 10 }, { "K♦", 10 },
        {"A♥", 1}, { "2♥", 2 }, { "3♥", 3 }, { "4♥", 4 }, { "5♥", 5 }, { "6♥", 6 }, { "7♥", 7 }, { "8♥", 8 }, { "9♥", 9 }, { "10♥", 10 }, { "J♥", 10 }, { "Q♥", 10 }, { "K♥", 10 },
        {"A♣", 1}, { "2♣", 2 }, { "3♣", 3 }, { "4♣", 4 }, { "5♣", 5 }, { "6♣", 6 }, { "7♣", 7 }, { "8♣", 8 }, { "9♣", 9 }, { "10♣", 10 }, { "J♣", 10 }, { "Q♣", 10 }, { "K♣", 10 }
    };
}