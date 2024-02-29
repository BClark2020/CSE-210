using System;
using System.Security.Cryptography.X509Certificates;

class Program
{
    static void Main(string[] args)
    {
        List<int> numbers = new List<int>();
        Console.WriteLine("Enter '0' when done");
        int number = 1;
        int count = -1;
        int total = 0;



        
        while (number != 0)
        {
            count += 1; 
            Console.Write("enter a number: ");
            string word = Console.ReadLine();
            number = int.Parse(word);


            total += number;
            numbers.Add(number);
        }

        int largest = numbers.Max();
        int smallest = numbers.Min();
        float average = total/count;
        
        Console.WriteLine($"The total sum of the list is {total}");
        Console.WriteLine($"The average of the list is {average}");
        Console.WriteLine($"The largest number of the list is {largest}");
        Console.WriteLine($"The smallest number of the list is {smallest}");

    }
}