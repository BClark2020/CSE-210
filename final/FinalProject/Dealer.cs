public class Dealer : Game
{
    public List<string> _hand = new List<string>();
    public List<string> Hit(List<string> _tempHand)
    {   
        bool _boolVar = false;
        do
        {
            try
            {
                _tempHand.Add(deck.ShuffledDeck[0]);
                deck.ShuffledDeck.RemoveAt(0);
                _boolVar = false;
            }
            catch (ArgumentOutOfRangeException)
            {
                deck.ShuffleDeck();
                _boolVar = true;
            }
        }while(_boolVar);
        return _tempHand;
    }
    public string GetAction(int _round, int _bet, List<string> _playerHand)
    {
        string _action = " ";
        bool _boolVar = false;
        do
        {
            if (_round == 1)
            {
                _action = round_one_options(_bet, _playerHand);
                return _action;
            }
            else
            {
            
                {
                    bool _boolVarTwo = false;
                    do
                    {
                        Console.Write("Hit, Stand  ");
                        _action = Console.ReadLine();
                        _action = _action.ToLower();
                        if (_action == "hit" || _action == "stand"|| _action == "h")
                        {
                            _boolVarTwo = false;
                            return _action;
                        }
                        else
                        {
                            _boolVarTwo = true;
                        }
                    }while(_boolVarTwo);
                }
            }
        }while (_boolVar);
        return _action;
    }
    private string round_one_options(int _bet, List<string> _playerHand)
    {
       string _action = " ";
        if (deck._handValues[_playerHand[0]] == deck._handValues[_playerHand[1]] && game._splitHandBool == true && _bet <= bank._tempBank)//////////////////////////////////////////
        {
            
            bool _boolVar = false;
            do
            {   
    
               Console.Write("Hit, Stand, Split, Double: ");
               _action = Console.ReadLine();
               _action = _action.ToLower();
               if (_action == "hit" || _action == "stand" || _action == "split" || _action == "double" || _action == "2x" || _action == "2" || _action == "h" || _action == "stay")
               {
                   _boolVar = false;
                   if (_action == "split" || _action == "double" || _action == "2x" || _action == "2")
                   {
                       bank._tempBank -=_bet;
                   }
               
                   return _action;
               }
               else
               {
                   _boolVar = true;
               }
            }while(_boolVar);
            
        }
        else if (_bet <= bank._tempBank)
        {
            bool _boolVar = false;
            do
            {
                Console.Write("Hit, Stand, Double: ");
                _action = Console.ReadLine();
                _action = _action.ToLower();
                if (_action == "hit" || _action == "stand"|| _action == "double" || _action == "x2" || _action == "2" || _action == "h" || _action == "stay")
                {
                    _boolVar = false;
                    if (_action == "split" || _action == "double" || _action == "2x" || _action == "2")
                    {
                        bank._tempBank -=_bet;
                    }
                    return _action;
                }
                else
                {
                    _boolVar = true;
                }
            }while(_boolVar);
        }
        else
        {
            bool _boolVar = false;
            do
            {
                Console.Write("Hit, Stand: ");
                _action = Console.ReadLine();
                _action = _action.ToLower();
                if (_action == "hit" || _action == "stand"|| _action == "h")
                {
                    _boolVar = false;
                    return _action;
                }
                else
                {
                    _boolVar = true;
                }
            }while(_boolVar);
        }
        return _action;
    }
    public void run_hand(int _bet, List<string> _playerHand)
    {
        player.CardView(true, _bet);
        Thread.Sleep(1000);
        deck.CalculateHandValue(_hand);
        while (deck._handValue < 17)
        {
            _hand = Hit(_hand);
            player.CardView(true, _bet);
            Thread.Sleep(1000);
            deck.CalculateHandValue(_hand);
        }
        if(deck._handValue > 21)
        {
            deck.CalculateHandValue(player._hand);
            if (deck._handValue == 21 || deck._optionalHandValue == 21)
            {
                bank.BlackJackWin(_bet);
            }
            else
            {
                bank.DealerBust(_bet);
            }
            
        }
        else
        {
            CompareHands(_bet, dealer._hand, _playerHand);
        }
    }
    private void CompareHands(int _bet, List<string> _dealerrHand, List<string> _playerHand)
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
            else
            {
                bank.PlayerWin(_bet);
            }
        }
        else if(_dealerValue == _playerValue)
        {
            bank.PlayerPush();
        }
        else if(_dealerValue > _playerValue)
        {
            bank.PLayerLoss(_bet);
        }
    }
}