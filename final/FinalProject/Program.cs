using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Security.AccessControl;
using System.Threading;
using System.Transactions;

class Game
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
    private int _bet;
    private bool _split_hand1 = false;
    private bool _split_hand2 = false;
    private bool _split_hand3 = false;
    private bool _split_hand_able = true;
    private bool _continue;

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

    private List<string> DoAction(string _action, List<string> player_hand)
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

    public class Dealer
    {
        public List<string> Hand = new List<string>();
        private int TOP_CARD = 0;


        public List<string> Hit(List<string> temp_hand)
        {   
            bool bool_var = false;
            do
            {
                try
                {
                    temp_hand.Add(deck.ShuffledDeck[TOP_CARD]);
                    deck.ShuffledDeck.RemoveAt(TOP_CARD);
                    bool_var = false;
                }
                catch (ArgumentOutOfRangeException)
                {
                    deck.ShuffleDeck();
                    bool_var = true;
                }
            }while(bool_var);
            return temp_hand;
        }

        public string GetAction(int round, int _bet, List<string> player_hand)
        {

            string _action = " ";
            bool bool_var = false;
            do
            {
                if (round == 1)
                {
                    _action = round_one_options(_bet, player_hand);
                    return _action;
                }
                else
                {
                
                    {
                        bool bool_var_2 = false;
                        do
                        {
                            Console.Write("Hit, Stand  ");
                            _action = Console.ReadLine();
                            _action = _action.ToLower();
                            if (_action == "hit" || _action == "stand"|| _action == "h")
                            {
                                bool_var_2 = false;
                                return _action;
                            }
                            else
                            {
                                bool_var_2 = true;
                            }
                        }while(bool_var_2);
                    }

                }


            }while (bool_var);
            return _action;
        }

        private string round_one_options(int _bet, List<string> player_hand)
        {
           string _action = " ";
            if (deck.HandValues[player_hand[0]] == deck.HandValues[player_hand[1]] && game._split_hand_able == true && _bet <= bank.temp_bank)//////////////////////////////////////////
            {
                
                bool bool_var = false;
                do
                {   
        
                   Console.Write("Hit, Stand, Split, Double: ");
                   _action = Console.ReadLine();
                   _action = _action.ToLower();
                   if (_action == "hit" || _action == "stand" || _action == "split" || _action == "double" || _action == "2x" || _action == "2" || _action == "h" || _action == "stay")
                   {
                       bool_var = false;
                       if (_action == "split" || _action == "double" || _action == "2x" || _action == "2")
                       {
                           bank.temp_bank -=_bet;
                       }
                   
                       return _action;
                   }
                   else
                   {
                       bool_var = true;
                   }
                }while(bool_var);
                

            }
            else if (_bet <= bank.temp_bank)
            {
                bool bool_var = false;
                do
                {
                    Console.Write("Hit, Stand, Double: ");
                    _action = Console.ReadLine();
                    _action = _action.ToLower();
                    if (_action == "hit" || _action == "stand"|| _action == "double" || _action == "x2" || _action == "2" || _action == "h" || _action == "stay")
                    {
                        bool_var = false;
                        if (_action == "split" || _action == "double" || _action == "2x" || _action == "2")
                        {
                            bank.temp_bank -=_bet;
                        }
                        return _action;
                    }
                    else
                    {
                        bool_var = true;
                    }
                }while(bool_var);
            }
            else
            {
                bool bool_var = false;
                do
                {
                    Console.Write("Hit, Stand: ");
                    _action = Console.ReadLine();
                    _action = _action.ToLower();
                    if (_action == "hit" || _action == "stand"|| _action == "h")
                    {
                        bool_var = false;
                        return _action;
                    }
                    else
                    {
                        bool_var = true;
                    }
                }while(bool_var);
            }
            return _action;
        }

        public void run_hand(int _bet, List<string> player_hand)
        {
            player.CardView(true, _bet);
            Thread.Sleep(1000);
            deck.CalculateHandValue(Hand);
            while (deck.HandValue < 17)
            {
                Hand = Hit(Hand);
                player.CardView(true, _bet);
                Thread.Sleep(1000);
                deck.CalculateHandValue(Hand);
            }

            if(deck.HandValue > 21)
            {
                deck.CalculateHandValue(player.Hand);
                if (deck.HandValue == 21 || deck.OptionalHandValue == 21)
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
                CompareHands(_bet, dealer.Hand, player_hand);
            }

        }

        private void CompareHands(int _bet, List<string> dealer_hand, List<string> player_hand)
        {
            deck.CalculateHandValue(dealer_hand);
            int dealer_value = deck.HandValue;
            deck.CalculateHandValue(player_hand);
            int player_value = 0;
            if (deck.OptionalHandValue > deck.HandValue && deck.OptionalHandValue <= 21)
            {
                player_value = deck.OptionalHandValue;
            }
            else
            {
                player_value = deck.HandValue;
            }

            if (dealer_value < player_value)
            {
                if (player_value == 21)
                {
                    bank.BlackJackWin(_bet);
                }
                else
                {
                    bank.PlayerWin(_bet);
                }
            }
            else if(dealer_value == player_value)
            {
                bank.PlayerPush();
            }
            else if(dealer_value > player_value)
            {
                bank.PLayerLoss(_bet);
            }
        }

    }

    public class Bank
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

    public class Deck
    {
        public int HandValue { get; private set; }
        public int OptionalHandValue { get; private set; }
        public bool Ace { get; private set; }
        public List<string> ShuffledDeck { get; private set; }
        private List<string> deck;

        public Deck()
        {
            deck = new List<string>
            {
                "A♠", "2♠", "3♠", "4♠", "5♠", "6♠", "7♠", "8♠", "9♠", "10♠", "J♠", "Q♠", "K♠",
                "A♦", "2♦", "3♦", "4♦", "5♦", "6♦", "7♦", "8♦", "9♦", "10♦", "J♦", "Q♦", "K♦",
                "A♥", "2♥", "3♥", "4♥", "5♥", "6♥", "7♥", "8♥", "9♥", "10♥", "J♥", "Q♥", "K♥",
                "A♣", "2♣", "3♣", "4♣", "5♣", "6♣", "7♣", "8♣", "9♣", "10♣", "J♣", "Q♣", "K♣",
                "A♠", "2♠", "3♠", "4♠", "5♠", "6♠", "7♠", "8♠", "9♠", "10♠", "J♠", "Q♠", "K♠",
                "A♦", "2♦", "3♦", "4♦", "5♦", "6♦", "7♦", "8♦", "9♦", "10♦", "J♦", "Q♦", "K♦",
                "A♥", "2♥", "3♥", "4♥", "5♥", "6♥", "7♥", "8♥", "9♥", "10♥", "J♥", "Q♥", "K♥",
                "A♣", "2♣", "3♣", "4♣", "5♣", "6♣", "7♣", "8♣", "9♣", "10♣", "J♣", "Q♣", "K♣",
                "A♠", "2♠", "3♠", "4♠", "5♠", "6♠", "7♠", "8♠", "9♠", "10♠", "J♠", "Q♠", "K♠",
                "A♦", "2♦", "3♦", "4♦", "5♦", "6♦", "7♦", "8♦", "9♦", "10♦", "J♦", "Q♦", "K♦",
                "A♥", "2♥", "3♥", "4♥", "5♥", "6♥", "7♥", "8♥", "9♥", "10♥", "J♥", "Q♥", "K♥",
                "A♣", "2♣", "3♣", "4♣", "5♣", "6♣", "7♣", "8♣", "9♣", "10♣", "J♣", "Q♣", "K♣",
                "A♠", "2♠", "3♠", "4♠", "5♠", "6♠", "7♠", "8♠", "9♠", "10♠", "J♠", "Q♠", "K♠",
                "A♦", "2♦", "3♦", "4♦", "5♦", "6♦", "7♦", "8♦", "9♦", "10♦", "J♦", "Q♦", "K♦",
                "A♥", "2♥", "3♥", "4♥", "5♥", "6♥", "7♥", "8♥", "9♥", "10♥", "J♥", "Q♥", "K♥",
                "A♣", "2♣", "3♣", "4♣", "5♣", "6♣", "7♣", "8♣", "9♣", "10♣", "J♣", "Q♣", "K♣",
                "A♠", "2♠", "3♠", "4♠", "5♠", "6♠", "7♠", "8♠", "9♠", "10♠", "J♠", "Q♠", "K♠",
                "A♦", "2♦", "3♦", "4♦", "5♦", "6♦", "7♦", "8♦", "9♦", "10♦", "J♦", "Q♦", "K♦",
                "A♥", "2♥", "3♥", "4♥", "5♥", "6♥", "7♥", "8♥", "9♥", "10♥", "J♥", "Q♥", "K♥",
                "A♣", "2♣", "3♣", "4♣", "5♣", "6♣", "7♣", "8♣", "9♣", "10♣", "J♣", "Q♣", "K♣",
                "A♠", "2♠", "3♠", "4♠", "5♠", "6♠", "7♠", "8♠", "9♠", "10♠", "J♠", "Q♠", "K♠",
                "A♦", "2♦", "3♦", "4♦", "5♦", "6♦", "7♦", "8♦", "9♦", "10♦", "J♦", "Q♦", "K♦",
                "A♥", "2♥", "3♥", "4♥", "5♥", "6♥", "7♥", "8♥", "9♥", "10♥", "J♥", "Q♥", "K♥",
                "A♣", "2♣", "3♣", "4♣", "5♣", "6♣", "7♣", "8♣", "9♣", "10♣", "J♣", "Q♣", "K♣",
                "A♠", "2♠", "3♠", "4♠", "5♠", "6♠", "7♠", "8♠", "9♠", "10♠", "J♠", "Q♠", "K♠",
                "A♦", "2♦", "3♦", "4♦", "5♦", "6♦", "7♦", "8♦", "9♦", "10♦", "J♦", "Q♦", "K♦",
                "A♥", "2♥", "3♥", "4♥", "5♥", "6♥", "7♥", "8♥", "9♥", "10♥", "J♥", "Q♥", "K♥",
                "A♣", "2♣", "3♣", "4♣", "5♣", "6♣", "7♣", "8♣", "9♣", "10♣", "J♣", "Q♣", "K♣",
                "A♠", "2♠", "3♠", "4♠", "5♠", "6♠", "7♠", "8♠", "9♠", "10♠", "J♠", "Q♠", "K♠",
                "A♦", "2♦", "3♦", "4♦", "5♦", "6♦", "7♦", "8♦", "9♦", "10♦", "J♦", "Q♦", "K♦",
                "A♥", "2♥", "3♥", "4♥", "5♥", "6♥", "7♥", "8♥", "9♥", "10♥", "J♥", "Q♥", "K♥",
                "A♣", "2♣", "3♣", "4♣", "5♣", "6♣", "7♣", "8♣", "9♣", "10♣", "J♣", "Q♣", "K♣",
                "A♠", "2♠", "3♠", "4♠", "5♠", "6♠", "7♠", "8♠", "9♠", "10♠", "J♠", "Q♠", "K♠",
                "A♦", "2♦", "3♦", "4♦", "5♦", "6♦", "7♦", "8♦", "9♦", "10♦", "J♦", "Q♦", "K♦",
                "A♥", "2♥", "3♥", "4♥", "5♥", "6♥", "7♥", "8♥", "9♥", "10♥", "J♥", "Q♥", "K♥",
                "A♣", "2♣", "3♣", "4♣", "5♣", "6♣", "7♣", "8♣", "9♣", "10♣", "J♣", "Q♣", "K♣"
            };
            ShuffledDeck = new List<string>(deck);
            ShuffleDeck();
        }

        public void ShuffleDeck()
        {
            ShuffledDeck = new List<string>(deck);
            Shuffle(ShuffledDeck);
            Shuffle(ShuffledDeck);
            ShuffledDeck = Cut(ShuffledDeck);
            Shuffle(ShuffledDeck);
        }

        private static void Shuffle(List<string> deck)
        {
            Random rand = new Random();
            int n = deck.Count;

            for (int i = n - 1; i > 0; i--)
            {
                int j = rand.Next(i + 1);
                string temp = deck[i];
                deck[i] = deck[j];
                deck[j] = temp;
            }
        }

        private static List<string> Cut(List<string> deck)
        {
            int cutPoint = deck.Count / 2;
            List<string> topHalf = deck.Take(cutPoint).ToList();
            List<string> bottomHalf = deck.Skip(cutPoint).ToList();
            List<string> newDeck = new List<string>();

            newDeck.AddRange(bottomHalf);
            newDeck.AddRange(topHalf);

            return newDeck;
        }

        public void CalculateHandValue(List<string> hand)
        {
            HandValue = 0;
            OptionalHandValue = 0;
            Ace = false;
            int aces = 0;

            foreach (string card in hand)
            {
                if ((card == "A♠" || card == "A♦" || card == "A♥" || card == "A♣") && aces == 0 )
                {
                    Ace = true;
                    aces ++;
                }
                else if(card == "A♠" || card == "A♦" || card == "A♥" || card == "A♣" && aces >= 1)
                {
                    HandValue += 1;
                    OptionalHandValue += 1;
                }
                else
                {
                    HandValue += HandValues[card];
                    OptionalHandValue += HandValues[card];
                }
            }
            if (Ace == true)
            {
                if (HandValue + 11 > 21)
                {
                    HandValue += 1;
                    Ace = false;
                }
                else
                {   
                    OptionalHandValue += 11;
                    HandValue += 1;
                    Ace = true;
                }
            }
        }

        public readonly Dictionary<string, int> HandValues = new Dictionary<string, int>
        {
            {"A♠", 1}, { "2♠", 2 }, { "3♠", 3 }, { "4♠", 4 }, { "5♠", 5 }, { "6♠", 6 }, { "7♠", 7 }, { "8♠", 8 }, { "9♠", 9 }, { "10♠", 10 }, { "J♠", 10 }, { "Q♠", 10 }, { "K♠", 10 },
            {"A♦", 1}, { "2♦", 2 }, { "3♦", 3 }, { "4♦", 4 }, { "5♦", 5 }, { "6♦", 6 }, { "7♦", 7 }, { "8♦", 8 }, { "9♦", 9 }, { "10♦", 10 }, { "J♦", 10 }, { "Q♦", 10 }, { "K♦", 10 },
            {"A♥", 1}, { "2♥", 2 }, { "3♥", 3 }, { "4♥", 4 }, { "5♥", 5 }, { "6♥", 6 }, { "7♥", 7 }, { "8♥", 8 }, { "9♥", 9 }, { "10♥", 10 }, { "J♥", 10 }, { "Q♥", 10 }, { "K♥", 10 },
            {"A♣", 1}, { "2♣", 2 }, { "3♣", 3 }, { "4♣", 4 }, { "5♣", 5 }, { "6♣", 6 }, { "7♣", 7 }, { "8♣", 8 }, { "9♣", 9 }, { "10♣", 10 }, { "J♣", 10 }, { "Q♣", 10 }, { "K♣", 10 }
        };
    }

    public class Player
    {
        
        public List<string> Hand = new List<string>();

        public void CardView(bool dealer_display, int _bet)
        {
            Console.Clear();
            if (dealer_display == false)
            {
                int counter = 0;
                foreach (string card in dealer.Hand)
                {
                    counter += 1;
                    if (counter == 1)
                    {
                        Console.Write(card + " ");
                    }
                    else
                    {
                        Console.Write("XX ");
                    }
                }
            }
            else
            {
                foreach (string card in dealer.Hand)
                {
                    Console.Write(card + " ");
                }
                deck.CalculateHandValue(dealer.Hand);
                Console. WriteLine($"Value {deck.HandValue}");
            }

            Console.WriteLine("\n\n\n");
            deck.CalculateHandValue(Hand);
            foreach (string card in Hand)
            {
                Console.Write(card + " ");
            }

            if (deck.Ace)
            {
                Console.Write($"    Value: {deck.HandValue}/{deck.OptionalHandValue}");
            }
            else
            {
                Console.Write($"    Value: {deck.HandValue}");
            }
            Console.WriteLine($"\nBet: {_bet}");
        }
    }

    public class SplitHand
    {
        public List<string> Split_hand0 = new List<string>();
        public List<string> Split_hand1 = new List<string>();

        public int hand0_bet;
        public int hand1_bet;
        public void Main(int _bet)
        {
            game._split_hand2 = true;
            int round = 0;
            Console.Clear();
            MakeHands(_bet);
            DisplayTwoHands(false, 0);
            Thread.Sleep(1000);
            game._continue = true;
            while (game._continue)
            {
                round += 1;
                string _action = dealer.GetAction(round, _bet, Split_hand0);
                if (_action == "double" || _action == "2x" || _action == "2")
                {
                    hand0_bet += hand0_bet;
                }
                Thee_hand = 1; 
                Split_hand0 = game.DoAction(_action, Split_hand0);
                if (_action == "split")
                {
                    return;
                }
                Thee_hand = 2;
                if (game._continue == true)
                {
                    DisplayTwoHands(false, 0);
                }
            }

            round = 0;
            DisplayTwoHands(false, 1);
            game._continue = true;
            while (game._continue)
            {
                round += 1;
                string _action = dealer.GetAction(round, _bet, Split_hand1);
                if (_action == "double" || _action == "2x" || _action == "2")
                {
                    hand1_bet += hand1_bet;
                }
                Thee_hand = 2; 
                Split_hand1 = game.DoAction(_action, Split_hand1);
                if (_action == "split")
                {
                    return;
                }
                Thee_hand = 3;
                if (game._continue == true)
                {
                    DisplayTwoHands(false, 1);
                }
            }
            DisplayTwoHands(false, 1);
            RunTwoHands();   
            Thee_hand = 3;  
        }
        public void MakeHands(int _bet)
        {
            Split_hand0.Clear();
            Split_hand1.Clear();
            Split_hand0.Add(player.Hand[0]);
            Split_hand1.Add(player.Hand[1]);
            Split_hand0 = dealer.Hit(Split_hand0);
            Split_hand1 = dealer.Hit(Split_hand1);
            hand0_bet = _bet;
            hand1_bet = _bet;
        }
        public void RunTwoHands()
        {
            RunDealerHandOnly();
            DisplayDealerOnly(true);
            DisplayOneHand(Split_hand0, hand0_bet);
            CompareHands(hand0_bet, dealer.Hand, Split_hand0);
            Console.WriteLine("\n");
            DisplayOneHand(Split_hand1, hand1_bet);
            Thread.Sleep(2000);
            CompareHands(hand1_bet, dealer.Hand, Split_hand1);
            Thread.Sleep(2000);
        }
        public void DisplayDealerOnly(bool dealer_display)
        {
            Console.Clear();
            if (dealer_display == false)
            {
                int counter = 0;
                foreach (string card in dealer.Hand)
                {
                    counter += 1;
                    if (counter == 1)
                    {
                        Console.Write(card + " ");
                    }
                    else
                    {
                        Console.Write("XX ");
                    }
                }
            }
            else
            {
                foreach (string card in dealer.Hand)
                {
                    Console.Write(card + " ");
                }
                deck.CalculateHandValue(dealer.Hand);
                Console. WriteLine($"Value {deck.HandValue}");
                Console.WriteLine("\n\n\n");
            }
        }
        public void DisplayOneHand(List<string> display_hand, int _bet)
        {
            deck.CalculateHandValue(display_hand);
            foreach (string card in display_hand)
            {
                Console.Write(card + " ");
            }

            if (deck.Ace)
            {
                Console.Write($"    Value: {deck.HandValue}/{deck.OptionalHandValue}");
            }
            else
            {
                Console.Write($"    Value: {deck.HandValue}");
            }
            Console.WriteLine($"\nBet: {_bet}");
        }
        public void DisplayTwoHands(bool dealer_display, int current_hand)
        {
            DisplayDealerOnly(dealer_display);
            Console.WriteLine("\n\n\n");
            if(current_hand == 0)
            {
                Console.WriteLine("CURRENT HAND");
            }
            else if(current_hand == 1)
            {
                Console.WriteLine("Hand 1");
            }
            DisplayOneHand(Split_hand0,hand0_bet);
            Console.WriteLine("\n");
            if(current_hand == 1)
            {
                Console.WriteLine("CURRENT HAND");
            }
            else if (current_hand == 0)
            {
                Console.WriteLine("Hand 2");
            }
            DisplayOneHand(Split_hand1, hand1_bet);
        }
        public void RunDealerHandOnly()
        {
            DisplayTwoHands(true, -1);
            Thread.Sleep(1000);
            deck.CalculateHandValue(dealer.Hand);
            while (deck.HandValue < 17)
            {
                dealer.Hand = dealer.Hit(dealer.Hand);
                DisplayTwoHands(true, -1);
                Thread.Sleep(1000);
                deck.CalculateHandValue(dealer.Hand);
            }

        }
        public void CompareHands(int _bet, List<string> dealer_hand, List<string> player_hand)
        {
            deck.CalculateHandValue(dealer_hand);
            int dealer_value = deck.HandValue;
            deck.CalculateHandValue(player_hand);
            int player_value = 0;
            if (deck.OptionalHandValue > deck.HandValue && deck.OptionalHandValue <= 21)
            {
                player_value = deck.OptionalHandValue;
            }
            else
            {
                player_value = deck.HandValue;
            }

            if (dealer_value < player_value)
            {
                if (player_value == 21)
                {
                    bank.BlackJackWin(_bet);
                }
                else if (player_value > 21)
                {
                    bank.PlayerBust(_bet);
                }
                else
                {
                    bank.PlayerWin(_bet);
                }
            }
            else if(dealer_value == player_value)
            {
                if (player_value > 21)
                {
                    bank.PlayerBust(_bet);
                }
                else
                {
                    bank.PlayerPush();
                }
                
            }
            else if(dealer_value > player_value)
            {
                if (player_value == 21)
                {
                    bank.BlackJackWin(_bet);
                }
                else if (player_value > 21)
                {
                    bank.PlayerBust(_bet);
                }
                else if (dealer_value > 21)
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

    public class SplitHandTwo
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

    public class SplitHandThree
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

}