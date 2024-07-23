public class Dealer : Game
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