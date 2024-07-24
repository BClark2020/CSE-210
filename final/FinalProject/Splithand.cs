public class SplitHand : Hand
{
	public List<string> _handOne = new List<string>();
	public List<string> _handTwo = new List<string>();
	public int hand0_bet;
	public int _handOneBet;
	public override void Main(int _bet, List<string> _splittingHand)
	{
		game._splitHandOne = false;
		game._splitHandTwo = true;
		int _round = 0;
		Console.Clear();
		CreateHands(_splittingHand, _bet);
		DisplayHands(false, 0);
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
				DisplayHands(false, 0);
			}
		}
		_round = 0;
		DisplayHands(false, 1);
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
				DisplayHands(false, 1);
			}
		}
		DisplayHands(false, 1);
		RunHands();
		_theeHand = 3;
	}
	private void CreateHands(List<string> _splittingHand, int _bet)
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
	private void RunHands()
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
	private void DisplayHands(bool _dealerDisplay, int current_hand)
	{
		DisplayDealerOnly(_dealerDisplay);
		Console.WriteLine("\n\n\n");
		if (current_hand == 0)
		{
			Console.WriteLine("CURRENT HAND");
		}
		else if (current_hand == 1)
		{
			Console.WriteLine("Hand 1");
		}
		DisplayOneHand(_handOne, hand0_bet);
		Console.WriteLine("\n");
		if (current_hand == 1)
		{
			Console.WriteLine("CURRENT HAND");
		}
		else if (current_hand == 0)
		{
			Console.WriteLine("Hand 2");
		}
		DisplayOneHand(_handTwo, _handOneBet);
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