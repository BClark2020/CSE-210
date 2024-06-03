/// Brenden Clark
/// Programming with Classes
/// 6/03/2024
/// 
/// reasources used:
///     --Scripture CSV file: https://scriptures.nephi.org/
///     --ChatGPT: Speciffically for parsing information from the CSV file
///                and helping me detect puncuation. Also helped me find a
///                way to make key strokes inputs.
///
/// 
/// 
using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Reflection.PortableExecutable;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic.FileIO;
using System.Threading;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;

class Search
{ 
    private List<int> _BCscripture_range = new List<int>{1}; 
    private bool _BC_verse_range = false;
    private string _BCbook;
    private int _BCverse_int;
    public string BC_get_scripture_cite()
    {   
        bool _BCend = false;
        string _BCscripture_location;
        _BCscripture_range.Clear();

        do 
        {
            Console.Clear();
            Console.WriteLine("Welcome to scripture memorizer!");
            Console.Write("What is the scripture (example: Genesis 1:1): ");
            _BCscripture_location = Console.ReadLine();
            _BCscripture_location = _BCscripture_location.Trim();
            _BCend = false;


            
    
                // Regular expression to extract the book, chapter, and verse/range
                Regex regex = new Regex(@"^(?<BookChapter>[^\d]*\d*[\w\s]* \d+):(?<VerseRange>\d+(-\d+)?)$");
                Match match = regex.Match( _BCscripture_location);

                if (match.Success)
                {
                    _BCbook = match.Groups["BookChapter"].Value.Trim();
                    string verseRange = match.Groups["VerseRange"].Value;

                    // Check if it's a range of verses
                    if (verseRange.Contains("-"))
                    {
                    

                        string[] rangeParts = verseRange.Split('-');
                        if (rangeParts.Length == 2 && int.TryParse(rangeParts[0], out int startVerse) && int.TryParse(rangeParts[1], out int endVerse))
                        {
                            

                            if (startVerse > endVerse)
                            {
                                Console.WriteLine("Invalid verse range. Please enter a valid range.");
                                Thread.Sleep(2500);
                                _BCend = true;
                            }
                            else
                            {
                                _BCscripture_range = GenerateRange(startVerse, endVerse);
                                _BC_verse_range = true;
                                _BCend = false; 
                            }
                            
                            
                        }
                        else
                        {
                            Console.WriteLine("Invalid verse range. Please enter a valid range.");
                            _BCend = true;
                        }
                    }
                    else if (int.TryParse(verseRange, out int verse))
                    {
                        _BCverse_int = verse;
                        _BC_verse_range = false;
                        _BCend = false;
                    }
                    else
                    {
                        Console.WriteLine("Invalid verse. Please enter a valid verse.");
                        _BCend = true;
                        Thread.Sleep(2500);
                    }
                }
                else
                {
                
                    Console.WriteLine("Invalid input format. Please enter a reference in the format 'Book Chapter:Verse' or 'Book Chapter:StartVerse-EndVerse'.");
                    Thread.Sleep(2500);
                    _BCend = true;
                }
            
        } while (_BCend);
     
        return _BCscripture_location;
    }  
    static List<int> GenerateRange(int start, int end)
    {
        List<int> numbers = new List<int>();
        for (int i = start; i <= end; i++)
        {
            numbers.Add(i);
        }
        return numbers;
    }

    public string BC_get_verses()
    {
        string _BCverse = "";
        int _BC_last_verse;
        int _BC_first_verse; 
        bool _BCresults = false;
        bool _BCindex_18 =  false;
        Console.Clear();
        Console.WriteLine($"Loading:");
        int _BC_verse_location;
        

        if (_BC_verse_range == true)
        {
            _BC_last_verse = _BCscripture_range[_BCscripture_range.Count() - 1];
            _BC_first_verse = _BCscripture_range[0];
        }
        else
        {
            _BC_first_verse = _BCverse_int;
            _BC_last_verse = _BCverse_int;
        }

        for (int i = 0; i < _BC_last_verse - _BC_first_verse + 1; i++)
        {
            if (_BC_verse_range == true)
            {
                _BC_verse_location =  _BCscripture_range[i];
            }
            else
            {
                _BC_verse_location = _BCverse_int;
            }

            string _BCscripture_location = _BCbook + ":"+ _BC_verse_location;
            
            Console.WriteLine($"{_BCscripture_location}");


         
            if (_BCindex_18 == false)
                {
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
                                _BCverse += " " + fields[16];
                                _BCresults = true;

                            }

                        }
                        if (_BCresults == false)
                        {  
                            _BCindex_18 = true;
                        }

                    }
                }



            if (_BCindex_18 == true)
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
                            _BCverse += " " + fields[16];
                            _BCresults = true;

                        }

                    }


                }

            }

            if (_BCresults == false)
            {  
                Console.WriteLine("Invalid input or scripture doesn't exist, please try again!");
                _BCverse = "false";
                Thread.Sleep(2500);
                return _BCverse;
            }
        }


        return _BCverse;
    }
}
class Word
{
    public List<string> _BCremoved_words = new List<string>();
    public List<int> _BCremoved_indexes = new List<int>();
    public List<string> BC_word_parse(string verse)
    {
        string[] _BCarray = verse.Split(new char[] {' '} , StringSplitOptions.RemoveEmptyEntries);
        List<string> passage = new List<string>(_BCarray);
        return passage;
    }
    public List<string> BC_restore_word(List<string> _BCpassage, int _BCword_drop)
    {
        

        for (int i = 0; i < _BCword_drop; i++)
        {  
            try
            {
            int _BClast_index = _BCremoved_words.Count() - 1;
            _BCpassage[_BCremoved_indexes[_BClast_index]] = _BCremoved_words[_BClast_index];
            _BCremoved_words.RemoveAt(_BClast_index);
            _BCremoved_indexes.RemoveAt(_BClast_index);
            }
            catch (ArgumentOutOfRangeException)
            {
                return _BCpassage;
            }
        }
        return _BCpassage;
    }
    public List<string> BC_word_removal(List<string> _BCpassage, int _BCword_drop)
    {
        Random random = new Random();
        int _BCrange = _BCpassage.Count; 
        bool _BCend = true;
        string _BCword;
        int _BCrandom_word;
        List<int> _BCindexed_list = new List<int>();
        
        
        for ( int i = 0; i < _BCword_drop; i++)
           
           { do
            {
                _BCrandom_word = random.Next(0,_BCrange);
                _BCword = _BCpassage[_BCrandom_word];
                _BCend = true;

                if (_BCword.Contains("_"))
                {
                    _BCindexed_list.Add(_BCrandom_word);
                    _BCend = false;
                    if (_BCindexed_list.Count() == _BCpassage.Count())
                    {
                        return _BCpassage;
                    }
                }

                else
                {
                    _BCremoved_indexes.Add(_BCrandom_word);
                    _BCremoved_words.Add(_BCword);
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
            

            _BCpassage[_BCrandom_word] = _BCblank;
        }}
        
        return _BCpassage;
    }
    public void BC_display(List<string> _BCpassage, string _BCscripture_location)
    {
        Console.Clear();
        
        Console.WriteLine("Press space to leave.");
        Console.WriteLine("Press backspace to reveal words.");
        Console.WriteLine("Press enter to remove a word.\n");
        Console.WriteLine($"{_BCscripture_location}:");
        foreach (string word in _BCpassage)
        {
            Console.Write($"{word} ");
        }
    }   
    public int BC_get_word_drop_rate()
    {
        Console.Clear();
        bool _BCboolVar = true;
        int _BCword_drop = 1;

        do
        {
            try
            {
                Console.Write("How many words would you like to drop at a time: ");
                string input = Console.ReadLine();
                _BCword_drop = int.Parse(input);
                _BCboolVar = true;
            }
        
            catch (FormatException)
            {
                Console.WriteLine("Invalid input entered, please enter an integer.");
                _BCboolVar = false;
            }
        } while (_BCboolVar != true);
        
        return _BCword_drop;
    }
}
class Program
{
    static void Main()
    {
        Search _BCsearch = new Search();
        Word _BCword = new Word();
        Program _BCprogram = new Program();
        
        string _BCscripture_location;
        string _BCverse;
        int _BCword_drop = 1; 
        bool _BCend = false;
        bool _BCrepeat = false;

        do
        {
            do
               {
                    _BCscripture_location = _BCsearch.BC_get_scripture_cite(); 

                    _BCverse = _BCsearch.BC_get_verses();
                
                    if (_BCverse != "false")
                    { 
                        _BCend = true;
                        _BCword_drop = _BCword.BC_get_word_drop_rate();
                    }


                    else
                    {
                        _BCend = false;
                    }

               } while(_BCend == false);

               
               _BCprogram.BC_run_word_removal(_BCword_drop, _BCscripture_location, _BCverse);
               
               _BCrepeat = _BCprogram.BC_get_repeat();

        } while( _BCrepeat == true);

        Console.Clear();
        Console.WriteLine("Thank you for using scripture memorizer app today!");
       
    }
    public bool BC_get_repeat()
    {
        bool _BCrepeat = false;
        Console.Clear();
        Console.WriteLine("Can you recite it all?");

        List<string> _BCaccepted_responses = new List<string>{"Yes", "yes", "y", "Y", "No", "no", "N","n"};
            bool _BCbool = false;
            string _BCresponse = "";
            do
            {
            try
            {
                Console.Write("Would you like to memorize another scripture? ");
                _BCresponse = Console.ReadLine();
                if (!_BCaccepted_responses.Contains(_BCresponse))
                {
                    throw new ArgumentException("Sorry that is an invalid input");
                }
                else
                {
                    _BCbool = false;
                }
            }
            
            catch (ArgumentException ex)
            {
                Console.WriteLine("Error" + ex.Message);
                _BCbool = true;
            }
        } while (_BCbool == true);

        if (_BCresponse == "Yes" || _BCresponse == "yes" || _BCresponse == "y" || _BCresponse == "Y")
            
            {
                _BCrepeat = true;
            }

        return _BCrepeat;
    }
    public void BC_run_word_removal(int _BCword_drop, string _BCscripture_location, string _BCverse)
    {
        Program _BCprogram = new Program();
        Word _BCword = new Word();
        string _BCpause_code = "";
        List<string> _BCpassage = new List<string>(_BCword.BC_word_parse(_BCverse));
        int _BCrange = _BCpassage.Count();
        bool _BCend = false;

        while (_BCword._BCremoved_indexes.Count() < _BCpassage.Count() && _BCend != true)
            {
    
                if (_BCpause_code == "end")
                {
                    _BCend = true;
                }

                else
                {
                    _BCword.BC_display(_BCpassage, _BCscripture_location);
                    _BCpause_code = _BCprogram.BC_pause_button_functions();
                }
                

                if (_BCpause_code == "reveal")
                {
                   _BCpassage = _BCword.BC_restore_word(_BCpassage, _BCword_drop);
                }

                else
                {
                    _BCword.BC_word_removal(_BCpassage, _BCword_drop);
                }
            }
    }
    public string BC_pause_button_functions()
    {
        string _BCpause = "";
      
        ConsoleKeyInfo keyInfo = Console.ReadKey(true);

        if (keyInfo.Key == ConsoleKey.Enter)
        {
                _BCpause = "yes";
        }
        else if (keyInfo.Key == ConsoleKey.Backspace)
        {
            _BCpause = "reveal";
        }
        else if (keyInfo.Key == ConsoleKey.Spacebar)
        {
            _BCpause = "end";
        }
        
        return _BCpause;
    }
}