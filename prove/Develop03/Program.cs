/// Brenden Clark
/// Programming with Classes
/// 3/14/2024
/// 
/// reasources used:
///     --Scripture CSV file: https://scriptures.nephi.org/
///     --ChatGPT: Speciffically for parsing information from the CSV file
///                and helping me detect puncuation.
///     -- Braydon: gave men the "console.clear()" and underscore idea
/// 
/// whats up
using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Reflection.PortableExecutable;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic.FileIO;
class Search
{
 
    public string BC_get_scripture_cite()
    {   
        Console.WriteLine("Welcome to scripture memorizer!");
        Console.Write("What is the scripture (example: Genesis 1:1): ");
        string _BCscripture_location = Console.ReadLine();
        return _BCscripture_location;
    }

    
    
    public string BC_get_verse(string _BCscripture_location)
    {
        bool _BCresults = false;
        string _BCverse = "";
        
        
        using (TextFieldParser _BCparser = new TextFieldParser("lds-scriptures.csv"))
        {
            _BCparser.TextFieldType = FieldType.Delimited;
            _BCparser.SetDelimiters(",");
            _BCparser.HasFieldsEnclosedInQuotes = true;
           
            while (!_BCparser.EndOfData)
            {   
            
                string[] fields = _BCparser.ReadFields();

                if (fields[17] == _BCscripture_location)
                {     
                    _BCverse = fields[16];
                    _BCresults = true;

                }
         
            }
            if (_BCresults == false)
            {  
                Console.WriteLine("Loading...\n\n");
                
            }
       
        }

        

        
        if (_BCresults == false)
        {
            using (TextFieldParser _BCparser = new TextFieldParser("lds-scriptures.csv"))
            {
                _BCparser.TextFieldType = FieldType.Delimited;
                _BCparser.SetDelimiters(",");
                _BCparser.HasFieldsEnclosedInQuotes = true;
                
                while (!_BCparser.EndOfData)
                {   
                
                    string[] fields = _BCparser.ReadFields();

                    if (fields[18] == _BCscripture_location)
                    {     
                        _BCverse = fields[16];
                        _BCresults = true;

                    }
            
                }
                
            
            }
        }
        
        
        if (_BCresults == false)
        {  
            Console.WriteLine("Invalid input or scripture doesn't exist, please try again!\n\n");
             _BCverse = "false";
        }
        return _BCverse;
    }
}
class Word
{
    
    
    public List<string> BC_word_parse(string verse)
    {
        string[] _BCarray = verse.Split(new char[] {' '} , StringSplitOptions.RemoveEmptyEntries);
        List<string> passage = new List<string>(_BCarray);
        return passage;
    }

    public List<string> BC_word_removal(List<string> _BCpassage)
    {
        Random random = new Random();
        int _BCrange = _BCpassage.Count; 
        bool _BCend = true;
        string _BCword;
        int _BCrandom_word;
        
        do
        {
            _BCrandom_word = random.Next(0,_BCrange);
            _BCword = _BCpassage[_BCrandom_word];
            _BCend = true;

            if (_BCword.Contains("_"))
            {
                _BCend = false;
            }
 
        } while(_BCend != true);
        
    
        
        string _BCblank = "";
        foreach (char letter in _BCword)  
        {      
            if (char.IsPunctuation(letter))
            {
                _BCblank += letter;
            }
           else
            {
                _BCblank += "_";
            }
        }

        _BCpassage[_BCrandom_word] = _BCblank;
        return _BCpassage;
    }

    
    
    
    
    public void BC_display(List<string> _BCpassage, string _BCscripture_location)
    {
        Console.Clear();
        
        Console.WriteLine("Press enter to remove a word.");
        Console.WriteLine($"{_BCscripture_location}:");
        foreach (string word in _BCpassage)
        {
            Console.Write($"{word} ");
        }
    }

    
    
    
    public int BC_get_word_drop_rate()
    {
        Console.Write("How many words would you like to drop at a time: ");
        string input = Console.ReadLine();
        int _BCword_drop = int.Parse(input);
        return _BCword_drop;
    }


}
class Program
{
    static void Main()
    {
        Search _BCsearch = new Search();
        Word _BCword = new Word();
        
        
        string _BCscripture_location = "";
        string _BCverse = "";
        bool _BCend = false;
        
        
        Console.Clear();
        
       
       do
        {
            _BCscripture_location = _BCsearch.BC_get_scripture_cite(); 
        
            _BCverse = _BCsearch.BC_get_verse(_BCscripture_location);
            
            if (_BCverse != "false")
            { 
                _BCend = true;
            }
        } while(_BCend == false);
        
        
        List<string> _BCpassage = new List<string>(_BCword.BC_word_parse(_BCverse));


        int _BCrange = _BCpassage.Count();
        for (int i = 0; i < _BCrange; i++)
        {
            _BCword.BC_display(_BCpassage, _BCscripture_location);
            _BCword.BC_word_removal(_BCpassage);
            Console.Write("");
            string _BCpause_code = Console.ReadLine();
        }

       Console.Clear();
       Console.WriteLine("Can you recite it all?");
    }
}