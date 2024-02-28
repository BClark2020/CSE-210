using System;
using System.Runtime.Serialization;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Enter your grade for CSE 210: ");
        string answer = Console.ReadLine();
        int grade = int.Parse(answer);

        if (grade >= 90)
        {
            Console.WriteLine("You have an A!!");
        }

        else if (grade < 90 && grade >= 80)
        {
            Console.WriteLine("You have a B!");
        }

        else if (grade < 80 && grade >= 70)
        {
            Console.WriteLine("You have a C.");
        }

        else if (grade < 70 && grade >= 60)
        {
            Console.WriteLine("You have a D!");
        }

        else if (grade > 60)
        {
            Console.WriteLine("You have a F!!");
        }

        if (grade >= 70)
        {
            Console.WriteLine("Good job you are passing!!!!");
        }

        else
        {
            Console.WriteLine("You are currently failing, do better next time.");
        }
    }
}