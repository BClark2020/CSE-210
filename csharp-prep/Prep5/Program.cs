using System;
using System.Globalization;

class Program
{
    static string get_user_name()
        {
            Console.Write("What is your name: ");
            string username = Console.ReadLine();
            return username;

        }

    static int get_user_number()
    {
        Console.Write("What is your favorite number: ");
        string input = Console.ReadLine();
        int number = int.Parse(input);
        return number;
    }

    static int calculate_square(int number)
    {
        int square = number * number;
        return square;
    }

    static void DisplayMessage(string name, int square)
    {
        Console.WriteLine($"Hello {name},\nThe square of your favorite number is {square}.");
    }

    static void Main(string[] args)
    {
        string name = get_user_name();
        int square = calculate_square(get_user_number());
        DisplayMessage(name, square);

    }
}