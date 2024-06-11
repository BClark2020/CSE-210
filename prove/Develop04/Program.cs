using System;
using System.Collections.Immutable;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

public class Mindfullness_apps
{

   public string Menu()
   {
    Console.Clear();
    string activity = "";
    List<string> _BCaccepted_values = new List<string>{"2","listing", "listing activity", "1", "reflection", "reflection activity", "3", "breathing", "breathing activity",
    "4", "affirmation", "affirmations", "affirmation activity", "affirmations activity", "5", "quit", "q"};


    Console.WriteLine("Welcome to the mindfullness app!");
    Console.WriteLine("What would you like to do:\n1.) Reflection Activity");
    Console.WriteLine("2.) Listing Activity\n3.) Meditation Activity\n4.) Affirmation Activity");
    Console.WriteLine("5.) Quit");


    bool _BCbool = false;
    do
    {
        try
        {
            activity = Console.ReadLine().ToLower();
            if (!_BCaccepted_values.Contains(activity))
            {
                throw new Exception();
                

            }

        }

        catch (Exception)
        {

        }
    }while(_BCbool);

    return activity;
   }
   private class Reflection
   {

   }

   private class Listing
   {

   }

   private class Meditation
   {

   }

   private class Affirmations
   {

   }

}



public class Program
{
    static void Main()
    {
        
    }
}

