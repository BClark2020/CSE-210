public class SplitHandTwo : Game
{
    public List<string> hand1 = new List<string>();
    public List<string> hand2 = new List<string>();
    public List<string> hand3 = new List<string>();
    public int hand1_bet;
    public int hand2_bet;
    public int hand3_bet;
    public void Main(List<string> splitting_hand)
    {
        game._split_hand3 = true;
        int _bet = 0;
        if (split.Split_hand0 == splitting_hand)
        {
            hand1_bet = split.hand1_bet;
            hand1 = split.Split_hand1;
            _bet = split.hand0_bet;
        }
        else
        {   
            hand1_bet = split.hand0_bet;
            hand1 = split.Split_hand0;
            _bet = split.hand1_bet;
        }
        int round = 0;
        Console.Clear();
        hand2 = createHands(splitting_hand, _bet);
        game._continue = true;
        
        if (Thee_hand == 1)
        {
        DisplayThreeHands(false,3);
        while (game._continue)
        {
            round += 1;
            string _action = dealer.GetAction(round, hand1_bet, hand1);
            if (_action == "double" || _action == "2x" || _action == "2")
            {
                hand1_bet += hand1_bet;
            }
            Thee_hand = 1;
            hand1 = game.DoAction(_action, hand1);
            if (_action == "split")
            {
                return;
            }
            Thee_hand = 2;
            if (game._continue == true)
            {
                DisplayThreeHands(false, 1);
            }
        }
        }
        if (Thee_hand== 2)
        {
        round = 0;
        DisplayThreeHands(false,2);
        game._continue = true;
        while (game._continue)
        {
            round += 1;
            string _action = dealer.GetAction(round, _bet, hand2);
            if (_action == "double" || _action == "2x" || _action == "2")
            {
                hand2_bet += hand2_bet;
            }
            Thee_hand = 2;
            hand2 = game.DoAction(_action, hand2);
            if (_action == "split")
            {
                return;
            }
            Thee_hand = 3;
            if (game._continue == true)
            {
                DisplayThreeHands(false, 2);
            }
        }
        }
        if (Thee_hand== 3)
        {
        round = 0;
        DisplayThreeHands(false,3);
        game._continue = true;
        while (game._continue)
        {
            round += 1;
            string _action = dealer.GetAction(round, _bet, hand3);
            if (_action == "double" || _action == "2x" || _action == "2")
            {
                hand3_bet += hand3_bet;
            }
            Thee_hand = 3;
            hand3 = game.DoAction(_action, hand3);
            if (_action == "split")
            {
                return;
            }
            Thee_hand = 4;
            if (game._continue == true)
            {
                DisplayThreeHands(false, 3);
            }
        }
        }
    
        RunThreeHands();  
        Thread.Sleep(3000);
        
    }
    public List<string> createHands(List<string> splitting_hand, int _bet)
    {
        string first_card = splitting_hand[0];
        string second_card = splitting_hand[1];
        splitting_hand.Clear();
        splitting_hand.Add(first_card);
        splitting_hand = dealer.Hit(splitting_hand);
        hand3.Add(second_card);
        hand3 = dealer.Hit(hand3);
        hand3_bet = _bet;
        hand2_bet = _bet;
        return splitting_hand;
    }
    public void DisplayThreeHands(bool dealer_display, int current_hand)
    {
        split.DisplayDealerOnly(dealer_display);
        Console.WriteLine("\n\n\n");
        if(current_hand == 1)
        {
            Console.WriteLine("CURRENT HAND");
        }
        else
        {
            Console.WriteLine("Hand 1");
        }
        split.DisplayOneHand(hand1, hand1_bet);
        Console.WriteLine("\n");
        if(current_hand == 2)
        {
            Console.WriteLine("CURRENT HAND");
        }
        else
        {
            Console.WriteLine("Hand 2");
        }
        split.DisplayOneHand(hand2, hand2_bet);
        Console.WriteLine("\n");
        if(current_hand == 3)
        {
            Console.WriteLine("CURRENT HAND");
        }
        else
        {
            Console.WriteLine("Hand 3");
        }
        split.DisplayOneHand(hand3, hand3_bet);
     
    }
    public void RunThreeHands()
    {
        RunDealerHandOnly();
        split.DisplayDealerOnly(true);
        split.DisplayOneHand(hand1, hand1_bet);
        Thread.Sleep(500);
        split.CompareHands(hand1_bet, dealer.Hand, hand1);
        Console.WriteLine("\n");
        Thread.Sleep(500);
        split.DisplayOneHand(hand2, hand2_bet);
        Thread.Sleep(500);
        split.CompareHands(hand2_bet, dealer.Hand, hand2);
        Console.WriteLine("\n");
        Thread.Sleep(500);
        split.DisplayOneHand(hand3, hand3_bet);
        Thread.Sleep(500);
        split.CompareHands(hand3_bet, dealer.Hand, hand3);
        Thread.Sleep(500);
    }
    public void RunDealerHandOnly()
    {
        DisplayThreeHands(true, -1);
        Thread.Sleep(1000);
        deck.CalculateHandValue(dealer.Hand);
        while (deck.HandValue < 17)
        {
            dealer.Hand = dealer.Hit(dealer.Hand);
            DisplayThreeHands(true, -1);
            Thread.Sleep(1000);
            deck.CalculateHandValue(dealer.Hand);
        }
    }
}