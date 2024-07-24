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
	public static Deck deck = new Deck();
	public static Bank bank = new Bank();
	public static Player player = new Player();
	public static Game game = new Game();
	public static SplitHand split = new SplitHand();
	public static SplitHandTwo split2 = new SplitHandTwo();
	public static SplitHandThree split3 = new SplitHandThree();
	public static Hand _handClass = new Hand();
	public static int _theeHand = 1;
	public int _timeLoss = 0;
	public int _timeWon = 0;
	public int _timePushed = 0;
	public int _bet;
	public bool _splitHandOne = true;
	public bool _splitHandTwo = false;
	public bool _splitHandThree = false;
	public bool _splitHandBool = true;
	public bool _continue;
	private void StartGame()
	{
		dealer._hand = dealer.Hit(dealer._hand);
		player._hand = dealer.Hit(player._hand);
		dealer._hand = dealer.Hit(dealer._hand);
		player._hand = dealer.Hit(player._hand);

	}
	private void Reset()
	{
		dealer._hand.Clear();
		player._hand.Clear();
		_splitHandOne = true;
		_splitHandTwo = false;
		_splitHandThree = false;
		_splitHandBool = true;
	}
	
	private void SplitHands(Hand _handClass)
	{
		_handClass.Main(player._hand);
	}
		

	
	public List<string> DoAction(string _action, List<string> _playerHand)
	{
		_continue = false;

		if (_action == "hit" || _action == "h")
		{
			_playerHand = dealer.Hit(_playerHand);
			deck.CalculateHandValue(_playerHand);
			if (deck._handValue > 21)
			{
				_continue = false;
				return _playerHand;
			}
			else
			{
				_continue = true;
				return _playerHand;
			}
		}
		else if (_action == "stand")
		{
			_continue = false;
			return _playerHand;
		}
		else if (_action == "double" || _action == "2x" || _action == "2")
		{
			_playerHand = dealer.Hit(_playerHand);
			_bet = _bet * 2;
			_continue = false;
			return _playerHand;
		}
		else
		{
			if (_splitHandOne == true)
			{
				SplitHands(split);
				_splitHandOne = false;
				_splitHandTwo = true;
			}
			else if (_splitHandTwo == true)
			{
				SplitHands(split2);
				_splitHandTwo = false;
				_splitHandThree = true;
			}

			else if (_splitHandThree == true)
			{
				SplitHands(split3);
				_splitHandBool = false;
			}
		}
		return _playerHand;
	}
	private void MainGame()
	{
		bool end = true;
		deck.ShuffleDeck();
		bank.GetBank();
		while (end)
		{
			_splitHandOne = false;
			_splitHandTwo = false;
			_splitHandThree = false;
			bank._tempBank = bank._bank;
			if (bank._bank == 0)
			{
				return;
			}
			int _round = 0;
			_bet = bank.GetBet();
			if (_bet == 0)
			{
				return;
			}
			Console.Clear();
			StartGame();
			player.CardView(false, _bet);
			deck.CalculateHandValue(player._hand);
			Thread.Sleep(1000);
			if (deck._optionalHandValue == 21)
			{
				dealer.run_hand(_bet, player._hand);
				Thread.Sleep(3000);
				Reset();
			}
			else
			{
				_continue = true;
				while (_continue)
				{
					_round += 1;
					string _action = dealer.GetAction(_round, _bet, player._hand);
					player._hand = DoAction(_action, player._hand);
					if (_splitHandOne != true)
					{
						player.CardView(false, _bet);
					}
				}
				if (_splitHandOne != true)
				{
					deck.CalculateHandValue(player._hand);
					if (deck._handValue > 21)
					{
						player.CardView(true, _bet);
						bank.PlayerBust(_bet);
					}
					else
					{
						dealer.run_hand(_bet, player._hand);
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
		Console.WriteLine($"Starting amount: ${bank._startingBank}");
		Console.WriteLine($"Ending amount: ${bank._bank}");
		int _even = bank._bank - bank._startingBank;
		if (_even < 0)
		{
			Console.WriteLine($"You are ${-1 * _even} under _even");
		}
		else if (_even > 0)
		{
			Console.WriteLine($"You are ${_even} over _even.");
		}
		else if (_even == 0)
		{
			Console.WriteLine($"You are even.");
		}
		Console.WriteLine("\n___________");
		Console.WriteLine($"Total wins: ${bank._wins}");
		Console.WriteLine($"Total losses: ${bank._losses}");
		Console.WriteLine($"Your largest bank: {bank._largestBank}");
		Console.WriteLine($"Your Largest win: {bank._largestWin}");
		Console.WriteLine($"Your largest loss: {bank._largestLoss}");
		Console.WriteLine($"Total games played: {game._timeWon + game._timeLoss + game._timePushed}");
		Console.WriteLine($"Total times won {game._timeWon}");
		Console.WriteLine($"Total times lost: {game._timeLoss}");
		Console.WriteLine($"Total times pushed: {game._timePushed}");
		if (game._timeWon != 0)
		{
			float _losingRatio = game._timeLoss / game._timeWon;
			Console.WriteLine($"Win/loss ratio: {game._timeWon / game._timeWon}/{_losingRatio}");
		}
	}
}
