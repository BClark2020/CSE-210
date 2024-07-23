public class Bank : Game
{
    public int _bank;
    public int temp_bank;
    public int _largest_win = 0;
    public int _largest_bank;
    public int _largest_loss;
    public int _losses;
    public int _wins;
    public int _starting_bank;
    public void GetBank()
    {
        Console.Clear();
        Console.WriteLine("Welcome to the table,\nMinimum bet is 10 and maximum bet is 500");
        bool bool_var = false;
        do
       { try
        {
            Console.Write("How much are you buying in for: ");
            _bank = int.Parse(Console.ReadLine());
            _starting_bank = _bank;
            if (_bank < 10)
            {
                Console.WriteLine("You need to buy in for more than 10 dollars.");
                bool_var = true;
            }
            else
            {
                bool_var = false;
            }
        }
        catch (Exception)
        {
            Console.WriteLine("Please enter an integer");
            bool_var = true;
        }
        }while(bool_var);
    }
    public int GetBet()
    {
        int _bet = 0;
        Console.Clear();
        Console.WriteLine("Enter \"leave\" to end the game.");
        bool bool_var = false;
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
                bool_var = true;
            }
            else if (_bet > _bank)
            {
                Console.WriteLine("You dont have enough money to place this bet.");
                bool_var = true;
            }
            else
            {
                bool_var = false;
            }
        }
        catch (Exception)
        {
            Console.WriteLine("Please enter an integer");
            bool_var = true;
        }
        }while(bool_var);
        temp_bank -= _bet;
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
        game._times_pushed += 1;
        
    }
    public void BlackJackWin(int _bet)
    {
        Console.WriteLine("BLACKJACK!!!!");
        int extra = _bet + (_bet/2);
        WinLoss(extra, true);
    }
    private void WinLoss(int _bet, bool win)
    {
        if(win == true)
        {
            game._times_won+=1;
            Console.WriteLine($"+${_bet}");
            _bank += _bet;
            _wins += _bet;
            if(_bet > _largest_win)
            {
                _largest_win = _bet;
            }
            if (_bank > _largest_bank)
            {
                _largest_bank = _bank;
            }
        }
        else if (win == false)
        {
            game._times_loss+=1;
            Console.WriteLine($"-${_bet}");
            _bank -= _bet;
            _losses -= _bet;
            if(_bet > _largest_loss)
            {
                _largest_loss = _bet;
            }
        }
    }
}