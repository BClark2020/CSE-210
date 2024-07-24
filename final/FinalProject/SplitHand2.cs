public class SplitHandTwo : Hand
{
    public List<string> _handOne = new List<string>();
    public List<string> _handTwo = new List<string>();
    public List<string> _handThree = new List<string>();
    public int _handOneBet;
    public int _handTwoBet;
    public int _handThreeBet;
    public override void Main(int _bet, List<string> _splittingHand)
    {
        game._splitHandTwo = false;
        game._splitHandThree = true;
        _bet = 0;
        if (split._handOne == _splittingHand)
        {
            _handOneBet = split._handOneBet;
            _handOne = split._handTwo;
            _bet = split.hand0_bet;
        }
        else
        {
            _handOneBet = split.hand0_bet;
            _handOne = split._handOne;
            _bet = split._handOneBet;
        }
        int _round = 0;
        Console.Clear();
        _handTwo = createHands(_splittingHand, _bet);
        game._continue = true;

        if (_theeHand == 1)
        {
            DisplayHands(false, 1);
            while (game._continue)
            {
                _round += 1;
                string _action = dealer.GetAction(_round, _handOneBet, _handOne);
                if (_action == "double" || _action == "2x" || _action == "2")
                {
                    _handOneBet += _handOneBet;
                }
                _theeHand = 1;
                _handOne = game.DoAction(_action, _handOne);
                if (_action == "split")
                {
                    return;
                }
                _theeHand = 2;
                if (game._continue == true)
                {
                    DisplayHands(false, 1);
                }
            }
        }
        if (_theeHand == 2)
        {
            _round = 0;
            DisplayHands(false, 2);
            game._continue = true;
            while (game._continue)
            {
                _round += 1;
                string _action = dealer.GetAction(_round, _bet, _handTwo);
                if (_action == "double" || _action == "2x" || _action == "2")
                {
                    _handTwoBet += _handTwoBet;
                }
                _theeHand = 2;
                _handTwo = game.DoAction(_action, _handTwo);
                if (_action == "split")
                {
                    return;
                }
                _theeHand = 3;
                if (game._continue == true)
                {
                    DisplayHands(false, 2);
                }
            }
        }
        if (_theeHand == 3)
        {
            _round = 0;
            DisplayHands(false, 3);
            game._continue = true;
            while (game._continue)
            {
                _round += 1;
                string _action = dealer.GetAction(_round, _bet, _handThree);
                if (_action == "double" || _action == "2x" || _action == "2")
                {
                    _handThreeBet += _handThreeBet;
                }
                _theeHand = 3;
                _handThree = game.DoAction(_action, _handThree);
                if (_action == "split")
                {
                    return;
                }
                _theeHand = 4;
                if (game._continue == true)
                {
                    DisplayHands(false, 3);
                }
            }
        }

        RunHands();
        Thread.Sleep(3000);

    }
    private List<string> createHands(List<string> _splittingHand, int _bet)
    {
        string first_card = _splittingHand[0];
        string second_card = _splittingHand[1];
        _splittingHand.Clear();
        _splittingHand.Add(first_card);
        _splittingHand = dealer.Hit(_splittingHand);
        _handThree.Add(second_card);
        _handThree = dealer.Hit(_handThree);
        _handThreeBet = _bet;
        _handTwoBet = _bet;
        return _splittingHand;
    }
    private void DisplayHands(bool _dealerDisplay, int current_hand)
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

    }
    private void RunHands()
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
    }
    private void RunDealerHandOnly()
    {
        DisplayHands(true, -1);
        Thread.Sleep(1000);
        deck.CalculateHandValue(dealer._hand);
        while (deck._handValue < 17)
        {
            dealer._hand = dealer.Hit(dealer._hand);
            DisplayHands(true, -1);
            Thread.Sleep(1000);
            deck.CalculateHandValue(dealer._hand);
        }
    }
}