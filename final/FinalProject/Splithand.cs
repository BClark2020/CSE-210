public class SplitHand : Game
{
    public List<string> _handOne = new List<string>();
    public List<string> _handTwo = new List<string>();
    public int hand0_bet;
    public int _handOneBet;
    public void Main(int _bet)
    {
        game._splitHandTwo = true;
        int _round = 0;
        Console.Clear();
        MakeHands(_bet);
        DisplayTwoHands(false, 0);
        Thread.Sleep(1000);
        game._continue = true;
        while (game._continue)
        {
            _round += 1;
            string _action = dealer.GetAction(_round, _bet, _handOne);
            if (_action == "double" || _action == "2x" || _action == "2")
            {
                hand0_bet += hand0_bet;
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
                DisplayTwoHands(false, 0);
            }
        }
        _round = 0;
        DisplayTwoHands(false, 1);
        game._continue = true;
        while (game._continue)
        {
            _round += 1;
            string _action = dealer.GetAction(_round, _bet, _handTwo);
            if (_action == "double" || _action == "2x" || _action == "2")
            {
                _handOneBet += _handOneBet;
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
                DisplayTwoHands(false, 1);
            }
        }
        DisplayTwoHands(false, 1);
        RunTwoHands();   
        _theeHand = 3;  
    }
    private void MakeHands(int _bet)
    {
        _handOne.Clear();
        _handTwo.Clear();
        _handOne.Add(player._hand[0]);
        _handTwo.Add(player._hand[1]);
        _handOne = dealer.Hit(_handOne);
        _handTwo = dealer.Hit(_handTwo);
        hand0_bet = _bet;
        _handOneBet = _bet;
    }
    private void RunTwoHands()
    {
        RunDealerHandOnly();
        DisplayDealerOnly(true);
        DisplayOneHand(_handOne, hand0_bet);
        CompareHands(hand0_bet, dealer._hand, _handOne);
        Console.WriteLine("\n");
        DisplayOneHand(_handTwo, _handOneBet);
        Thread.Sleep(2000);
        CompareHands(_handOneBet, dealer._hand, _handTwo);
        Thread.Sleep(2000);
    }
    public void DisplayDealerOnly(bool _dealerDisplay)
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
            Console. WriteLine($"Value {deck._handValue}");
            Console.WriteLine("\n\n\n");
        }
    }
    public void DisplayOneHand(List<string> _displayHand, int _bet)
    {
        deck.CalculateHandValue(_displayHand);
        foreach (string _card in _displayHand)
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
    private void DisplayTwoHands(bool _dealerDisplay, int current_hand)
    {
        DisplayDealerOnly(_dealerDisplay);
        Console.WriteLine("\n\n\n");
        if(current_hand == 0)
        {
            Console.WriteLine("CURRENT HAND");
        }
        else if(current_hand == 1)
        {
            Console.WriteLine("Hand 1");
        }
        DisplayOneHand(_handOne,hand0_bet);
        Console.WriteLine("\n");
        if(current_hand == 1)
        {
            Console.WriteLine("CURRENT HAND");
        }
        else if (current_hand == 0)
        {
            Console.WriteLine("Hand 2");
        }
        DisplayOneHand(_handTwo, _handOneBet);
    }
    public void RunDealerHandOnly()
    {
        DisplayTwoHands(true, -1);
        Thread.Sleep(1000);
        deck.CalculateHandValue(dealer._hand);
        while (deck._handValue < 17)
        {
            dealer._hand = dealer.Hit(dealer._hand);
            DisplayTwoHands(true, -1);
            Thread.Sleep(1000);
            deck.CalculateHandValue(dealer._hand);
        }
    }
    public void CompareHands(int _bet, List<string> _dealerrHand, List<string> _playerHand)
    {
        deck.CalculateHandValue(_dealerrHand);
        int _dealerValue = deck._handValue;
        deck.CalculateHandValue(_playerHand);
        int _playerValue = 0;
        if (deck._optionalHandValue > deck._handValue && deck._optionalHandValue <= 21)
        {
            _playerValue = deck._optionalHandValue;
        }
        else
        {
            _playerValue = deck._handValue;
        }
        if (_dealerValue < _playerValue)
        {
            if (_playerValue == 21)
            {
                bank.BlackJackWin(_bet);
            }
            else if (_playerValue > 21)
            {
                bank.PlayerBust(_bet);
            }
            else
            {
                bank.PlayerWin(_bet);
            }
        }
        else if(_dealerValue == _playerValue)
        {
            if (_playerValue > 21)
            {
                bank.PlayerBust(_bet);
            }
            else
            {
                bank.PlayerPush();
            }
            
        }
        else if(_dealerValue > _playerValue)
        {
            if (_playerValue == 21)
            {
                bank.BlackJackWin(_bet);
            }
            else if (_playerValue > 21)
            {
                bank.PlayerBust(_bet);
            }
            else if (_dealerValue > 21)
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