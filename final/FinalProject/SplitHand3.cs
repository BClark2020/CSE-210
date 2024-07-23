public class SplitHandThree : Game
{
    public List<string> hand1 = new List<string>();
    public List<string> hand2 = new List<string>();
    public List<string> hand3 = new List<string>();
    public List<string> hand4 = new List<string>();
    public int hand1_bet;
    public int hand2_bet;
    public int hand3_bet;
    public int hand4_bet;
    public void Main(List<string> splitting_hand)
    {
        hand1.Clear();
        hand2.Clear();
        hand3.Clear();
        hand4.Clear();
        int _bet = 0;
        game._split_hand3 = true;
        if (split2.hand1 == splitting_hand)
        {
            hand1_bet = split2.hand2_bet;
            hand2_bet = split2.hand3_bet;
            hand1 = split2.hand2;
            hand2 = split2.hand3;
            _bet = split2.hand1_bet;
        }
        else if (split2.hand2 == splitting_hand)
        {   
            hand1_bet = split2.hand1_bet;
            hand1 = split2.hand1;
            hand2 = split2.hand3;
            hand2_bet = split2.hand3_bet;
            _bet = split2.hand2_bet;
        }
        else
        {
            hand1_bet = split2.hand1_bet;
            hand1 = split2.hand1;
            hand2 = split2.hand2;
            hand2_bet = split2.hand2_bet;
            _bet = split2.hand3_bet;
        }
        int round = 0;
        Console.Clear();
        hand4 = CreateHands(splitting_hand, _bet);
        game._continue = true;
        if (Thee_hand== 1)
        {
        DisplayFourHands(false, 1);
        while (game._continue)
        {
            round += 1;
            string _action = dealer.GetAction(round, hand1_bet, hand1);
            if (_action == "double" || _action == "2x" || _action == "2")
            {
                hand1_bet += hand1_bet;
            }
            hand1 = game.DoAction(_action, hand1);
            Thee_hand = 2;
            if (game._continue == true)
            {
                DisplayFourHands(false, 1);
            }
        }
        }
        if (Thee_hand== 2)
        {
        round = 0;
        DisplayFourHands(false,2);
        game._continue = true;
        while (game._continue)
        {
            round += 1;
            string _action = dealer.GetAction(round, _bet, hand2);
            if (_action == "double" || _action == "2x" || _action == "2")
            {
                hand2_bet += hand2_bet;
            }
            hand2 = game.DoAction(_action, hand2);
            Thee_hand = 3;
            if (game._continue == true)
            {
                DisplayFourHands(false, 2);
            }
        }
        }
        if (Thee_hand== 3)
        {
        round = 0;
        DisplayFourHands(false,3);
        game._continue = true;
        while (game._continue)
        {
            round += 1;
            string _action = dealer.GetAction(round, _bet, hand3);
            if (_action == "double" || _action == "2x" || _action == "2")
            {
                hand3_bet += hand3_bet;
            }
            Thee_hand = 4;
            hand3 = game.DoAction(_action, hand3);
            if (game._continue == true)
            {
                DisplayFourHands(false, 3);
            }
        }
        }
        if (Thee_hand== 4)
        {
        round = 0;
        DisplayFourHands(false,3);
        game._continue = true;
        while (game._continue)
        {
            round += 1;
            string _action = dealer.GetAction(round, _bet, hand4);
            if (_action == "double" || _action == "2x" || _action == "2")
            {
                hand4_bet += hand4_bet;
            }
            hand4 = game.DoAction(_action, hand4);
            Thee_hand = 1;
            if (game._continue == true)
            {
                DisplayFourHands(false, 4);
            }
        }
        }
        
        RunFourHands();  
        Thread.Sleep(3000);
        
    }
    public void DisplayFourHands(bool dealer_display, int current_hand)
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
            Console.WriteLine("\n");
            if(current_hand == 4)
            {
                Console.WriteLine("CURRENT HAND");
            }
            else
            {
                Console.WriteLine("Hand 4");
            }
            split.DisplayOneHand(hand4, hand4_bet);


        }
    public void RunFourHands()
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
            Console.WriteLine("\n");
            Thread.Sleep(500);
            split.DisplayOneHand(hand4, hand4_bet);
            Thread.Sleep(500);
            split.CompareHands(hand4_bet, dealer.Hand, hand4);
            Thread.Sleep(500); 
        }
    public List<string> CreateHands(List<string> hand, int _bet)
    {
        string first_card = hand[0];
        string second_card = hand[1];
        hand.Clear();
        hand3.Add(first_card);
        hand3 = dealer.Hit(hand3);
        hand.Add(second_card);
        hand = dealer.Hit(hand);
        hand4_bet = _bet;
        hand3_bet = _bet;
        return hand;
    }
    public void RunDealerHandOnly()
    {
        DisplayFourHands(true, -1);
        Thread.Sleep(1000);
        deck.CalculateHandValue(dealer.Hand);
        while (deck.HandValue < 17)
        {
            dealer.Hand = dealer.Hit(dealer.Hand);
            DisplayFourHands(true, -1);
            Thread.Sleep(1000);
            deck.CalculateHandValue(dealer.Hand);
        }
    }
}