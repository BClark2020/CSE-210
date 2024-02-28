using System;

class Program
{
    static void Main(string[] args)
    {
        int range;
        do
        {
            Console.Write("What is the range? ");
            string userinput = Console.ReadLine();
            range = int.Parse(userinput);
        
        } while (range == 0);
       
        
        Random randomGenerator = new Random();
        int number = randomGenerator.Next(1, range);
        int count = 0;
        
        string number_found = "false";
        while (number_found != "true")
        
        {
            count += 1;
            Console.Write("What is your guess? ");
            string userinput_1 = Console.ReadLine();
            int guess = int.Parse(userinput_1);

            if (number == guess)
            {
                number_found = "true";
            }

            else if (number > guess)
            {
                Console.WriteLine("Guess Higher");
            }

            else if (number < guess)
            {
                Console.WriteLine("Guess Lower");
            }

        }

        Console.WriteLine($"You guessed the number {number} in {count} tries!!");

    }
}