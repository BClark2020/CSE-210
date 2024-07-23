public class SplitHandThree : Hand
{
    private List<string> _handOne = new List<string>();
    private List<string> _handTwo = new List<string>();
    private List<string> _handThree = new List<string>();
    private List<string> _handFour = new List<string>();
    private int _handOneBet;
    private int _handTwoBet;
    private int _handThreeBet;
    private int _handFourBet;
    public void Main(List<string> splitting_hand)
    {
        _handOne.Clear();
        _handTwo.Clear();
        _handThree.Clear();
        _handFour.Clear();
        int _bet = 0;
        game._splitHandThree = true;
        if (split2._handOne == splitting_hand)
        {
            _handOneBet = split2._handTwoBet;
            _handTwoBet = split2._handThreeBet;
            _handOne = split2._handTwo;
            _handTwo = split2._handThree;
            _bet = split2._handOneBet;
        }
        else if (split2._handTwo == splitting_hand)
        {
            _handOneBet = split2._handOneBet;
            _handOne = split2._handOne;
            _handTwo = split2._handThree;
            _handTwoBet = split2._handThreeBet;
            _bet = split2._handTwoBet;
        }
        else
        {
            _handOneBet = split2._handOneBet;
            _handOne = split2._handOne;
            _handTwo = split2._handTwo;
            _handTwoBet = split2._handTwoBet;
            _bet = split2._handThreeBet;
        }
        int _round = 0;
        Console.Clear();
        _handFour = CreateHands(splitting_hand, _bet);
        game._continue = true;
        if (_theeHand == 1)
        {
            DisplayFourHands(false, 1);
            while (game._continue)
            {
                _round += 1;
                string _action = dealer.GetAction(_round, _handOneBet, _handOne);
                if (_action == "double" || _action == "2x" || _action == "2")
                {
                    _handOneBet += _handOneBet;
                }
                _handOne = game.DoAction(_action, _handOne);
                _theeHand = 2;
                if (game._continue == true)
                {
                    DisplayFourHands(false, 1);
                }
            }
        }
        if (_theeHand == 2)
        {
            _round = 0;
            DisplayFourHands(false, 2);
            game._continue = true;
            while (game._continue)
            {
                _round += 1;
                string _action = dealer.GetAction(_round, _bet, _handTwo);
                if (_action == "double" || _action == "2x" || _action == "2")
                {
                    _handTwoBet += _handTwoBet;
                }
                _handTwo = game.DoAction(_action, _handTwo);
                _theeHand = 3;
                if (game._continue == true)
                {
                    DisplayFourHands(false, 2);
                }
            }
        }
        if (_theeHand == 3)
        {
            _round = 0;
            DisplayFourHands(false, 3);
            game._continue = true;
            while (game._continue)
            {
                _round += 1;
                string _action = dealer.GetAction(_round, _bet, _handThree);
                if (_action == "double" || _action == "2x" || _action == "2")
                {
                    _handThreeBet += _handThreeBet;
                }
                _theeHand = 4;
                _handThree = game.DoAction(_action, _handThree);
                if (game._continue == true)
                {
                    DisplayFourHands(false, 3);
                }
            }
        }
        if (_theeHand == 4)
        {
            _round = 0;
            DisplayFourHands(false, 3);
            game._continue = true;
            while (game._continue)
            {
                _round += 1;
                string _action = dealer.GetAction(_round, _bet, _handFour);
                if (_action == "double" || _action == "2x" || _action == "2")
                {
                    _handFourBet += _handFourBet;
                }
                _handFour = game.DoAction(_action, _handFour);
                _theeHand = 1;
                if (game._continue == true)
                {
                    DisplayFourHands(false, 4);
                }
            }
        }

        RunFourHands();
        Thread.Sleep(3000);

    }
    private void DisplayFourHands(bool _dealerDisplay, int current_hand)
    {
        split.DisplayDealerOnly(_dealerDisplay);
        Console.WriteLine("\n\n\n");
        if (current_hand == 1)
        {
            Console.WriteLine("CURRENT HAND");
        }
        else
        {
            Console.WriteLine("Hand 1");
        }
        split.DisplayOneHand(_handOne, _handOneBet);
        Console.WriteLine("\n");
        if (current_hand == 2)
        {
            Console.WriteLine("CURRENT HAND");
        }
        else
        {
            Console.WriteLine("Hand 2");
        }
        split.DisplayOneHand(_handTwo, _handTwoBet);
        Console.WriteLine("\n");
        if (current_hand == 3)
        {
            Console.WriteLine("CURRENT HAND");
        }
        else
        {
            Console.WriteLine("Hand 3");
        }
        split.DisplayOneHand(_handThree, _handThreeBet);
        Console.WriteLine("\n");
        if (current_hand == 4)
        {
            Console.WriteLine("CURRENT HAND");
        }
        else
        {
            Console.WriteLine("Hand 4");
        }
        split.DisplayOneHand(_handFour, _handFourBet);


    }
    private void RunFourHands()
    {
        RunDealerHandOnly();
        split.DisplayDealerOnly(true);
        split.DisplayOneHand(_handOne, _handOneBet);
        Thread.Sleep(500);
        split.CompareHands(_handOneBet, dealer._hand, _handOne);
        Console.WriteLine("\n");
        Thread.Sleep(500);
        split.DisplayOneHand(_handTwo, _handTwoBet);
        Thread.Sleep(500);
        split.CompareHands(_handTwoBet, dealer._hand, _handTwo);
        Console.WriteLine("\n");
        Thread.Sleep(500);
        split.DisplayOneHand(_handThree, _handThreeBet);
        Thread.Sleep(500);
        split.CompareHands(_handThreeBet, dealer._hand, _handThree);
        Thread.Sleep(500);
        Console.WriteLine("\n");
        Thread.Sleep(500);
        split.DisplayOneHand(_handFour, _handFourBet);
        Thread.Sleep(500);
        split.CompareHands(_handFourBet, dealer._hand, _handFour);
        Thread.Sleep(500);
    }
    private List<string> CreateHands(List<string> _hand, int _bet)
    {
        string first_card = _hand[0];
        string second_card = _hand[1];
        _hand.Clear();
        _handThree.Add(first_card);
        _handThree = dealer.Hit(_handThree);
        _hand.Add(second_card);
        _hand = dealer.Hit(_hand);
        _handFourBet = _bet;
        _handThreeBet = _bet;
        return _hand;
    }
    private void RunDealerHandOnly()
    {
        DisplayFourHands(true, -1);
        Thread.Sleep(1000);
        deck.CalculateHandValue(dealer._hand);
        while (deck._handValue < 17)
        {
            dealer._hand = dealer.Hit(dealer._hand);
            DisplayFourHands(true, -1);
            Thread.Sleep(1000);
            deck.CalculateHandValue(dealer._hand);
        }
    }
}