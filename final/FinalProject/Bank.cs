public class Bank : Game
{
    public int _bank;
    public int _tempBank;
    public int _largestWin = 0;
    public int _largestBank;
    public int _largestLoss;
    public int _losses;
    public int _wins;
    public int _startingBank;
    public void GetBank()
    {
        Console.Clear();
        Console.WriteLine("Welcome to the table,\nMinimum bet is 10 and maximum bet is 500");
        bool _boolVar = false;
        do
       { try
        {
            Console.Write("How much are you buying in for: ");
            _bank = int.Parse(Console.ReadLine());
            _startingBank = _bank;
            if (_bank < 10)
            {
                Console.WriteLine("You need to buy in for more than 10 dollars.");
                _boolVar = true;
            }
            else
            {
                _boolVar = false;
            }
        }
        catch (Exception)
        {
            Console.WriteLine("Please enter an integer");
            _boolVar = true;
        }
        }while(_boolVar);
    }
    public int GetBet()
    {
        int _bet = 0;
        Console.Clear();
        Console.WriteLine("Enter \"leave\" to end the game.");
        bool _boolVar = false;
        do
       { try
        {
            Console.WriteLine($"Bank: {_bank}");
            Console.Write("How much do you want to bet: ");
            string _string_bet = Console.ReadLine();
            if (_string_bet == "leave")
            {
                return 0;
            }
            else
            {
                _bet = int.Parse(_string_bet);
            }
            if (_bet < 10 || _bet > 500)
            {
                Console.WriteLine("Please enter a bet between 10 and 500 dollars.");
                _boolVar = true;
            }
            else if (_bet > _bank)
            {
                Console.WriteLine("You dont have enough money to place this bet.");
                _boolVar = true;
            }
            else
            {
                _boolVar = false;
            }
        }
        catch (Exception)
        {
            Console.WriteLine("Please enter an integer");
            _boolVar = true;
        }
        }while(_boolVar);
        _tempBank -= _bet;
        return _bet;
    }
    public void PlayerBust(int _bet)
    {
        Console.WriteLine("BUST");
        WinLoss(_bet, false);
    }
    public void PLayerLoss(int _bet)
    {
        Console.WriteLine("LOSS TO DEALER");
        WinLoss(_bet, false);
    }
    public void PlayerWin(int _bet)
    {
        Console.WriteLine("WINNER");
        WinLoss(_bet, true);
    }
    public void DealerBust(int _bet)
    {
        Console.WriteLine("DEALER BUST");
        WinLoss(_bet, true);
    }
    public void PlayerPush()
    {
        Console.WriteLine("PUSH");
        game._timePushed += 1;
        
    }
    public void BlackJackWin(int _bet)
    {
        Console.WriteLine("BLACKJACK!!!!");
        int _extra = _bet + (_bet/2);
        WinLoss(_extra, true);
    }
    private void WinLoss(int _bet, bool win)
    {
        if(win == true)
        {
            game._timeWon+=1;
            Console.WriteLine($"+${_bet}");
            _bank += _bet;
            _wins += _bet;
            if(_bet > _largestWin)
            {
                _largestWin = _bet;
            }
            if (_bank > _largestBank)
            {
                _largestBank = _bank;
            }
        }
        else if (win == false)
        {
            game._timeLoss+=1;
            Console.WriteLine($"-${_bet}");
            _bank -= _bet;
            _losses -= _bet;
            if(_bet > _largestLoss)
            {
                _largestLoss = _bet;
            }
        }
    }
}