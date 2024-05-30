using System;
using System.Reflection.Metadata.Ecma335;



class Fraction
{

    private int _numerator;
    private int _denominator;
    public void set_bottom(int Denominator = 1)
    {
        _denominator = Denominator;
    }
    public void set_top(int Numerator = 1)
    {
        _numerator = Numerator;
    }
}


class Program
{

    public int get_top()
    {
        int _BCNumerator = '0';
        bool _BCBool_var = true;

        do
        {
            try
            {
                Console.Write("Numerator: ");
                _BCNumerator = int.Parse(Console.ReadLine());
                _BCBool_var = true;
                
            }
            catch (FormatException)
            {
                Console.WriteLine("PLease enter an integer");
                _BCBool_var = false;
            }

        } while (_BCBool_var == false);

        return _BCNumerator;
    }  
    public int get_bottom()
    {
        int _BCDenominator = '1';
        bool _BCBool_var = true;

        do
        {
            try
            {
                Console.Write("Denominator: ");
                _BCDenominator = int.Parse(Console.ReadLine());
                _BCBool_var = true;
                
            }
            catch (FormatException)
            {
                Console.WriteLine("PLease enter an integer");
                _BCBool_var = false;
            }

        } while (_BCBool_var == false);

        return _BCDenominator;
    }
    static void Main(string[] args)
    {
        Fraction frac = new Fraction();
        frac.set_bottom();
        frac.set_top();
        
        Console.WriteLine($"{}");



    }
}