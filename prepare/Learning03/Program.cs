using System;
using System.Reflection.Metadata.Ecma335;



public class Fraction
{

    private int _numerator;
    private int _denominator;

    public Fraction()
    {
        _numerator = 1;
        _denominator = 1;
    }
    public Fraction(int _BCwholenumber)
    {
        _numerator = _BCwholenumber;
        _denominator = 1;

    }
    public Fraction(int Numerator, int Denominator)
    {
        _numerator = Numerator;
        _denominator = Denominator;
    }

    public string get_fraction()
    {
        string _BCfraction = $"{_numerator}/{_denominator}";
        return _BCfraction;
    }

    public double get_decimal()
    {
        return (double)_numerator/(double)_denominator;
    }
}


class Program
{

 static void Main()
 {
    Fraction f1 = new Fraction();
        Console.WriteLine(f1.get_fraction());
        Console.WriteLine(f1.get_decimal());

        Fraction f2 = new Fraction(3);
        Console.WriteLine(f2.get_fraction());
        Console.WriteLine(f2.get_decimal());

        Fraction f3 = new Fraction(1, 4);
        Console.WriteLine(f3.get_fraction());
        Console.WriteLine(f3.get_decimal());

        Fraction f4 = new Fraction(22, 7);
        Console.WriteLine(f4.get_fraction());
        Console.WriteLine(f4.get_decimal());
 }
}