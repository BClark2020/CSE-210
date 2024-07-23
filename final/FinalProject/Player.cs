public class Player : Game
{
    public List<string> _hand = new List<string>();
    public void CardView(bool _dealerDisplay, int _bet)
    {
        Console.Clear();
        if (_dealerDisplay == false)
        {
            int counter = 0;
            foreach (string _card in dealer._hand)
            {
                counter += 1;
                if (counter == 1)
                {
                    Console.Write(_card + " ");
                }
                else
                {
                    Console.Write("XX ");
                }
            }
        }
        else
        {
            foreach (string _card in dealer._hand)
            {
                Console.Write(_card + " ");
            }
            deck.CalculateHandValue(dealer._hand);
            Console.WriteLine($"Value {deck._handValue}");
        }
        Console.WriteLine("\n\n\n");
        deck.CalculateHandValue(_hand);
        foreach (string _card in _hand)
        {
            Console.Write(_card + " ");
        }
        if (deck._ace)
        {
            Console.Write($"    Value: {deck._handValue}/{deck._optionalHandValue}");
        }
        else
        {
            Console.Write($"    Value: {deck._handValue}");
        }
        Console.WriteLine($"\nBet: {_bet}");
    }
}