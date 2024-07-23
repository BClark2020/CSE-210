using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Security.AccessControl;
using System.Threading;
using System.Transactions;

public class Game
{ 
    public static Dealer dealer = new Dealer();
    public static Deck deck =  new Deck();
    public static Bank bank = new Bank();
    public static Player player = new Player();
    public static Game game = new Game();
    public static SplitHand split = new SplitHand(); 
    public static SplitHandTwo split2 = new SplitHandTwo();
    public static int Thee_hand = 1;
    public int _times_loss = 0;
    public int _times_won = 0;
    public int _times_pushed = 0;
    public int _bet;
    public bool _split_hand1 = false;
    public bool _split_hand2 = false;
    public bool _split_hand3 = false;
    public bool _split_hand_able = true;
    public bool _continue;
    private void StartGame()
    {
        dealer.Hand = dealer.Hit(dealer.Hand);
        player.Hand = dealer.Hit(player.Hand);
        dealer.Hand = dealer.Hit(dealer.Hand);
        player.Hand = dealer.Hit(player.Hand);  

    }
    private void Reset()
    {
       dealer.Hand.Clear();
       player.Hand.Clear(); 
    }
    public List<string> DoAction(string _action, List<string> player_hand)
    {
        _continue = false;

        if (_action == "hit" || _action == "h")
        {
            player_hand = dealer.Hit(player_hand);
            deck.CalculateHandValue(player_hand);
            if (deck.HandValue > 21)
            {
                _continue = false;
                return player_hand;
            }
            else
            {
                _continue = true;
                return player_hand;
            }
        }
        else if (_action == "stand")
        {
            _continue = false;
            return player_hand;
        }
        else if (_action == "double" || _action == "2x" || _action == "2")
        {
            player_hand = dealer.Hit(player_hand);
            _bet = _bet*2;
            _continue = false;
            return player_hand;
        }
        else
        {   
            

            _split_hand1 = true;
            if(_split_hand1 == true && _split_hand2 != true)
            {
                
                split.Main(_bet);
                _split_hand2 = true;
                _continue = false;
            }
            else if(_split_hand2 == true && _split_hand3 != true)
            {
                split2.Main(player_hand);
                _split_hand3 = true;
                _continue = false;
            }
            else
            {
                SplitHandThree split3 = new SplitHandThree();
                split3.Main(player_hand);
                _continue = false;
            }
        }
        return player_hand;

    }
    private void MainGame()
    {
        bool end = true;
        deck.ShuffleDeck();
        bank.GetBank();
        while (end)
        { 
            _split_hand1 = false;
            _split_hand2 =  false;
            _split_hand3 = false;
            bank.temp_bank = bank._bank;
            if (bank._bank == 0)
            {
                return;
            }
            int round = 0;
            _bet = bank.GetBet();
            if (_bet == 0)
            {
                return; 
            }
            Console.Clear();
            StartGame();
            player.CardView(false, _bet);
            deck.CalculateHandValue(player.Hand);
            Thread.Sleep(1000);
            if(deck.OptionalHandValue == 21)
            {
                dealer.run_hand(_bet, player.Hand);
                Thread.Sleep(3000);
                Reset();
            }
            else
            {
                _continue = true;
                while (_continue)
                {
                    round += 1;
                    string _action = dealer.GetAction(round, _bet, player.Hand);
                    player.Hand = DoAction(_action, player.Hand);
                    if(_split_hand1 != true)
                    {
                       player.CardView(false, _bet); 
                    }
                }
                if (_split_hand1 != true)
                {
                    deck.CalculateHandValue(player.Hand);
                    if (deck.HandValue > 21)
                    {
                        player.CardView(true, _bet);
                        bank.PlayerBust(_bet);
                    }
                    else
                    {
                        dealer.run_hand(_bet, player.Hand); 
                    }
                }
                Reset();
                Thread.Sleep(3000);
            }
            
        }
    }
    public static void Main()
    {

        Console.Clear();
        Game game = new Game();
        game.MainGame();
        Console.Clear();
        game.GamesAssessment();
        Console.WriteLine("Thank you for playing!!");
    }
    public void GamesAssessment()
    {
        Console.WriteLine($"Starting amount: ${bank._starting_bank}");
            Console.WriteLine($"Ending amount: ${bank._bank}");
            int even = bank._bank - bank._starting_bank;
            if(even < 0)
            {
                Console.WriteLine($"You are ${-1*even} under even");
            }
            else if(even>0)
            {
                Console.WriteLine($"You are ${even} over even.");
            }
            else if(even == 0)
            {
                Console.WriteLine($"You are even.");
            }
            Console.WriteLine("\n___________");
            Console.WriteLine($"Total wins: ${bank._wins}");
            Console.WriteLine($"Total losses: ${bank._losses}");
            Console.WriteLine($"Your largest bank: {bank._largest_bank}");
            Console.WriteLine($"Your Largest win: {bank._largest_win}");
            Console.WriteLine($"Your largest loss: {bank._largest_loss}");
            Console.WriteLine($"Total games played: {game._times_won +game. _times_loss + game._times_pushed}");
            Console.WriteLine($"Total times won {game._times_won}");
            Console.WriteLine($"Total times lost: {game._times_loss}");
            Console.WriteLine($"Total times pushed: {game._times_pushed}");
            if (game._times_won != 0)
            {
                Console.WriteLine($"Win/loss ratio: {game._times_won/game._times_won}/{game._times_loss/game._times_won}");
            }
    }
}
