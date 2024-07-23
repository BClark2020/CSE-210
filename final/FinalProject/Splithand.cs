public class SplitHand : Game
{
    public List<string> Split_hand0 = new List<string>();
    public List<string> Split_hand1 = new List<string>();
    public int hand0_bet;
    public int hand1_bet;
    public void Main(int _bet)
    {
        game._split_hand2 = true;
        int round = 0;
        Console.Clear();
        MakeHands(_bet);
        DisplayTwoHands(false, 0);
        Thread.Sleep(1000);
        game._continue = true;
        while (game._continue)
        {
            round += 1;
            string _action = dealer.GetAction(round, _bet, Split_hand0);
            if (_action == "double" || _action == "2x" || _action == "2")
            {
                hand0_bet += hand0_bet;
            }
            Thee_hand = 1; 
            Split_hand0 = game.DoAction(_action, Split_hand0);
            if (_action == "split")
            {
                return;
            }
            Thee_hand = 2;
            if (game._continue == true)
            {
                DisplayTwoHands(false, 0);
            }
        }
        round = 0;
        DisplayTwoHands(false, 1);
        game._continue = true;
        while (game._continue)
        {
            round += 1;
            string _action = dealer.GetAction(round, _bet, Split_hand1);
            if (_action == "double" || _action == "2x" || _action == "2")
            {
                hand1_bet += hand1_bet;
            }
            Thee_hand = 2; 
            Split_hand1 = game.DoAction(_action, Split_hand1);
            if (_action == "split")
            {
                return;
            }
            Thee_hand = 3;
            if (game._continue == true)
            {
                DisplayTwoHands(false, 1);
            }
        }
        DisplayTwoHands(false, 1);
        RunTwoHands();   
        Thee_hand = 3;  
    }
    public void MakeHands(int _bet)
    {
        Split_hand0.Clear();
        Split_hand1.Clear();
        Split_hand0.Add(player.Hand[0]);
        Split_hand1.Add(player.Hand[1]);
        Split_hand0 = dealer.Hit(Split_hand0);
        Split_hand1 = dealer.Hit(Split_hand1);
        hand0_bet = _bet;
        hand1_bet = _bet;
    }
    public void RunTwoHands()
    {
        RunDealerHandOnly();
        DisplayDealerOnly(true);
        DisplayOneHand(Split_hand0, hand0_bet);
        CompareHands(hand0_bet, dealer.Hand, Split_hand0);
        Console.WriteLine("\n");
        DisplayOneHand(Split_hand1, hand1_bet);
        Thread.Sleep(2000);
        CompareHands(hand1_bet, dealer.Hand, Split_hand1);
        Thread.Sleep(2000);
    }
    public void DisplayDealerOnly(bool dealer_display)
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
            Console.WriteLine("\n\n\n");
        }
    }
    public void DisplayOneHand(List<string> display_hand, int _bet)
    {
        deck.CalculateHandValue(display_hand);
        foreach (string card in display_hand)
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
    public void DisplayTwoHands(bool dealer_display, int current_hand)
    {
        DisplayDealerOnly(dealer_display);
        Console.WriteLine("\n\n\n");
        if(current_hand == 0)
        {
            Console.WriteLine("CURRENT HAND");
        }
        else if(current_hand == 1)
        {
            Console.WriteLine("Hand 1");
        }
        DisplayOneHand(Split_hand0,hand0_bet);
        Console.WriteLine("\n");
        if(current_hand == 1)
        {
            Console.WriteLine("CURRENT HAND");
        }
        else if (current_hand == 0)
        {
            Console.WriteLine("Hand 2");
        }
        DisplayOneHand(Split_hand1, hand1_bet);
    }
    public void RunDealerHandOnly()
    {
        DisplayTwoHands(true, -1);
        Thread.Sleep(1000);
        deck.CalculateHandValue(dealer.Hand);
        while (deck.HandValue < 17)
        {
            dealer.Hand = dealer.Hit(dealer.Hand);
            DisplayTwoHands(true, -1);
            Thread.Sleep(1000);
            deck.CalculateHandValue(dealer.Hand);
        }
    }
    public void CompareHands(int _bet, List<string> dealer_hand, List<string> player_hand)
    {
        deck.CalculateHandValue(dealer_hand);
        int dealer_value = deck.HandValue;
        deck.CalculateHandValue(player_hand);
        int player_value = 0;
        if (deck.OptionalHandValue > deck.HandValue && deck.OptionalHandValue <= 21)
        {
            player_value = deck.OptionalHandValue;
        }
        else
        {
            player_value = deck.HandValue;
        }
        if (dealer_value < player_value)
        {
            if (player_value == 21)
            {
                bank.BlackJackWin(_bet);
            }
            else if (player_value > 21)
            {
                bank.PlayerBust(_bet);
            }
            else
            {
                bank.PlayerWin(_bet);
            }
        }
        else if(dealer_value == player_value)
        {
            if (player_value > 21)
            {
                bank.PlayerBust(_bet);
            }
            else
            {
                bank.PlayerPush();
            }
            
        }
        else if(dealer_value > player_value)
        {
            if (player_value == 21)
            {
                bank.BlackJackWin(_bet);
            }
            else if (player_value > 21)
            {
                bank.PlayerBust(_bet);
            }
            else if (dealer_value > 21)
            {
                bank.DealerBust(_bet);
            }
            else
            {
                bank.PLayerLoss(_bet);
            }
        }
    }
}