public class Hand : Game
{
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