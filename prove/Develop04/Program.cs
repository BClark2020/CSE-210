using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

public class Mindfullness_app
{
    public int _BCtimer; 

   public string Menu()
   {
    Console.Clear();
    string activity = "";
    List<string> _BCaccepted_values = new List<string>{"2","listing", "listing activity", "1", "reflection", "reflection activity", "3", "meditation", "meditation activity",
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
            _BCbool = false;
            if (!_BCaccepted_values.Contains(activity))
            {
                throw new Exception();
            }

        }

        catch (Exception)
        {
            Console.WriteLine("Error, input not recognized.");
            _BCbool = true; 
        }
    }while(_BCbool);


    set_timer();

    loading();

    if (activity == "listing" || activity == "listing activity" || activity == "2")
    {
        Listing List = new Listing();
        List.program(_BCtimer);
        activity = "2";
    }

    else if (activity == "reflection"||  activity == "reflection activity" || activity == "1" )
    {
       Reflection reflect = new Reflection(); 
       reflect.program(_BCtimer);
       activity = "1";
    }
    
    else if (activity == "3" || activity == "meditation" || activity == "meditation activity")
    {
        Meditation meditate = new Meditation();
        meditate.program(_BCtimer);
        activity = "3";
    }

    else if (activity == "4"|| activity == "affirmation"|| activity == "affirmations"|| activity == "affirmation activity"||activity == "affirmations activity")
    {
        Affirmations affirm = new Affirmations(); 
        affirm.program(_BCtimer);
        activity = "4";
    }
    else
    {
        activity = "5";  
    }
    return activity;
   }

    private void set_timer()
    {
        bool _BCbool = false;
        do
        {
            try
            {
            Console.WriteLine("How long would you like to do this activity(in seconds)?");
            string input = Console.ReadLine();
            _BCtimer = int.Parse(input);
            _BCbool = false;
            }
            catch (FormatException)
            {
            Console.WriteLine("Please enter an integer.");
            _BCbool = true;
            }

        }while(_BCbool);



    }
    protected void loading()
    {
        Console.Clear();
        int repetions = 8;
        for (int i = 0; i < repetions; i++)
        {
        Console.Write("\b\b\b\b\b\b\b\b\bLoading |");
        Thread.Sleep(75);
        Console.Write("\b\b\b\b\b\b\b\b\bLoading /");
        Thread.Sleep(75);
        Console.Write("\b\b\b\b\b\b\b\b\bLoading —");
        Thread.Sleep(75);
        Console.Write("\b\b\b\b\b\b\b\b\bLoading \\");
        Thread.Sleep(75);
        Console.Write("\b\b\b\b\b\b\b\b\bLoading |");
        Thread.Sleep(75);
        Console.Write("\b\b\b\b\b\b\b\b\bLoading /");
        Thread.Sleep(75);
        Console.Write("\b\b\b\b\b\b\b\b\bLoading —");
        Thread.Sleep(75);
        Console.Write("\b\b\b\b\b\b\b\b\bLoading \\");
        Console.Write("\b\b\b\b\b\b\b\b\b");
        }
    }
    protected void spinner(int repetitions = 1)
    {

        for (int i = 0; i < repetitions; i++)
        {
        Console.Write("\b|");
        Thread.Sleep(112);
        Console.Write("\b/");
        Thread.Sleep(112);
        Console.Write("\b—");
        Thread.Sleep(112);
        Console.Write("\b\\");
        Thread.Sleep(112);
        Console.Write("\b|");
        Thread.Sleep(112);
        Console.Write("\b/");
        Thread.Sleep(115);
        Console.Write("\b—");
        Thread.Sleep(115);
        Console.Write("\b\\");
        Console.Write("\b");
        }
    }

    


   protected class Reflection
   {

    Mindfullness_app mind = new Mindfullness_app();
    Random random = new Random();
    private int randomNumber = 0;

    private List<string> prompts = new List<string>()
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private List<string> follow_ups = new List<string>()
    {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different than other times when you were not as successful?",
        "What is your favorite thing about this experience?",
        "What could you learn from this experience that applies to other situations?",
        "What did you learn about yourself through this experience?",
        "How can you keep this experience in mind in the future?"
    };

    public void program(int _BCtimer)
    {
        Console.Clear();
        Console.WriteLine("This activity will help you recognize the strength and power you have by reflecting on moments you showed these atributes and help you learn how to use them.");
        mind.spinner(8);
        Console.Clear();
        Console.WriteLine(prompts[randomNumber = random.Next(0, prompts.Count())]);
        mind.spinner(7);
    
        
        
        bool _BCbool = true;
        Timer timer = new Timer(stoploop, null, _BCtimer * 1000, Timeout.Infinite);
        int index = 0;
        while (_BCbool)
        {
            try
            {
                Console.WriteLine(follow_ups[index]);
                mind.spinner(5);
                index += 1;
           }
            catch (ArgumentOutOfRangeException)
            {
                _BCbool = false;
            }
        
        }
        void stoploop(object state)
        {
            _BCbool = false;
        }
    }

   }

   protected class Listing
   {
    Mindfullness_app mind = new Mindfullness_app();
    Random random = new Random();
    private bool _BCbool = true;

    private List<string> prompts = new List<string>()
    {   
        "good attributes about yourself",
        "amazing things you have accomplished",
        "positive things about your day",
        "people that helped you today",
        "people that are there for you",
        "fun activities you can do right now",
        "things you are looking forward to",
        "future plans that you are excited for",
        "pets you would like to own",
        "places you would want to visit",
        "foods you like to eat",
        "foods you are curious to try"
    };
    public void program(int _BCtimer)
    {
        Console.WriteLine("This activity will help you when you are feeling down by asking you to list some positive ascpets of your life.");
        mind.spinner(8);
        _BCbool = true;
        Timer timer = new Timer(stoploop, null, _BCtimer * 1000, Timeout.Infinite);
        int index = 0;
        List<int> used_prompts = new List<int>();
        mind.loading();
        while (_BCbool)
        {
            bool bool_var = false;
            do
            {
                index = random.Next(0, prompts.Count());
                if (used_prompts.Contains(index) )
                {
                    bool_var = true; 
                }
                else
                {
                    bool_var = false;
                    used_prompts.Add(index);
                    if (used_prompts.Count() == prompts.Count())
                    {
                        _BCbool = false;
                    }
                }
            }while (bool_var);


            int randomNumber = random.Next(5, 10);

            Console.WriteLine($"List {randomNumber} " + prompts[index] + ".");
            Console.WriteLine("When you finish press enter to continue");
            Console.ReadLine();
            
        }
        
    }
    
    void stoploop(object state)
        {
            _BCbool = false;
        }
   }

   protected class Meditation
   {
    
    Mindfullness_app mind = new Mindfullness_app();
    Random random = new Random();  
    public void program(int _BCtimer)
    {
        Console.Clear();
        Console.WriteLine("This activity will help you achieve a calm mental state through a series of breathing excersizes.");
        mind.spinner(8);
        Console.Clear();
        Console.WriteLine("We will start in");
        Console.WriteLine("3");
        Thread.Sleep(1000);
        Console.WriteLine("2");
        Thread.Sleep(1000);
        Console.WriteLine("1");
        Thread.Sleep(1000);
        
        bool _BCbool = true;
        Timer timer = new Timer(stoploop, null, _BCtimer * 1000, Timeout.Infinite);
        
        while (_BCbool)
        {
            Console.Clear();
            Console.WriteLine("Breathe in");
            hold(4);
            Console.Clear();
            Console.WriteLine("Hold");
            hold(4);
            Console.Clear();
            Console.WriteLine("Breathe out");
            hold(4);
            Console.Clear();
            Console.WriteLine("Hold");
            hold(4);
            
        }
        void stoploop(object state)
        {
            _BCbool = false;
        }
    }

    private void hold(int repetition)
    {
        Console.WriteLine("");
        int counter = 0;
        for (int i = 0; i < repetition; i++ )
        {
            counter += 1;
            Console.Write($"{counter},");
            Thread.Sleep(1000);

        }
    }


   }

   protected class Affirmations
   {
    Mindfullness_app mind = new Mindfullness_app();
    Random random = new Random();
    private int randomNumber = 0;
    private List<string> affirmations = new List<string>()
    {
        "I am happy.",
        "I am able.",
        "I am strong.",
        "I am worth it.",
        "I am loved.",
        "I have unlimited potential.",
        "I am free to make my own decisions.",
        "I love myself and all my flaws.",
        "I am good enough.",
        "I am in control of myself and my actions.",
        "I deserve to do what makes me happy.",
        "I am fearless."

    };
    public void program(int _BCtimer)
    {
        Console.Clear();
        Console.WriteLine("This activity will help you achieve confidence in yourself through positive affirmations that are designed to boost self esteem.");
        mind.spinner(8);
        Console.Clear();
       Console.WriteLine("Repeat aloud the phrases you see on your screen.");
        mind.spinner(7);
    
      
        bool _BCbool = true;
        Timer timer = new Timer(stoploop, null, _BCtimer * 1000, Timeout.Infinite);
        int index = 0;
        while (_BCbool)
        {
            List<int> used_indexes = new List<int>(); 
            bool bool_var = false;
            do
            {
                randomNumber = random.Next(0, affirmations.Count());
                bool found = used_indexes.Contains(randomNumber);
                if (found)
                {
                    bool_var = true;
                }
                else
                {
                    bool_var = false;
                    used_indexes.Add(randomNumber);
                }
                
            }while(bool_var);
            
            try
            {
                Console.WriteLine(affirmations[randomNumber]);
                mind.spinner(4);
                index += 1;
           }
            catch (ArgumentOutOfRangeException)
            {
                _BCbool = false;
            }
        
        }
        void stoploop(object state)
        {
            _BCbool = false;
        }
    }
        
    }
    

}



public class Program
{
    static void Main()
    {
    string activity = " ";
    do
    {
        Mindfullness_app mind = new Mindfullness_app();
        activity = mind.Menu();  

    }while(activity != "5");
    
    }
}

    