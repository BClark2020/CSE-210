public class Player : Game
{
    
    public List<string> Hand = new List<string>();
    public void CardView(bool dealer_display, int _bet)
    {
        Console.Clear();
        if (dealer_display == false)
        {
            int counter = 0;
            foreach (string card in dealer.Hand)
            {
                counter += 1;
                if (counter == 1)
                {
                    Console.Write(card + " ");
                }
                else
                {
                    Console.Write("XX ");
                }
            }
        }
        else
        {
            foreach (string card in dealer.Hand)
            {
                Console.Write(card + " ");
            }
            deck.CalculateHandValue(dealer.Hand);
            Console. WriteLine($"Value {deck.HandValue}");
        }
        Console.WriteLine("\n\n\n");
        deck.CalculateHandValue(Hand);
        foreach (string card in Hand)
        {
            Console.Write(card + " ");
        }
        if (deck.Ace)
        {
            Console.Write($"    Value: {deck.HandValue}/{deck.OptionalHandValue}");
        }
        else
        {
            Console.Write($"    Value: {deck.HandValue}");
        }
        Console.WriteLine($"\nBet: {_bet}");
    }
}