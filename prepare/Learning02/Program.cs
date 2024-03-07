using System;
using System.Dynamic;
using System.Reflection;
using System.Runtime.InteropServices.Marshalling;
using System.Security.Cryptography.X509Certificates;
public class Job
{
   
    public string GetCompany()
    {
        Console.Write("Company: ");
        string _BCcompany = Console.ReadLine();

        return _BCcompany;
    }

    public string GetJob()
    {
        Console.Write("Job title: ");
        string _BCjobTitle = Console.ReadLine();
        
        return _BCjobTitle;
    }

    public int get_start_year()
    {
        Console.Write("Start Year: " );
        string input = Console.ReadLine();
        int _BCstartYear = int.Parse(input);

        return _BCstartYear;
    }

    public int get_end_year()
    {
        Console.Write("End Year: ");
        string input = Console.ReadLine();
        int _BCendYear = int.Parse(input);

        return _BCendYear;
    }

    
    public string _BCcompany;
    public string _BCjobTitle;
    public int _BCstartYear;
    public int _BCendYear;

    public void Display_Resume()
    {
        Console.WriteLine($"{_BCjobTitle} ({_BCcompany}) {_BCstartYear} - {_BCendYear}");
    }
    
}

public class Resume
{
    public string _BCname;
    public List<Job> _BCjobs = new List<Job>();

    public void Display()
    {
        Console.WriteLine($"\n\n\n\nName: {_BCname}"); 
        Console.WriteLine("Jobs:");
    
        foreach (Job job in _BCjobs)
        { 
            job.Display_Resume();
        }
    }

}


class Program
{
    static void Main()
    {
        Resume resume = new Resume();
        Console.Write("Enter your name: ");
        resume._BCname = Console.ReadLine();

        int i = 0;
        while (i != 1)
        {
            Job job = new Job();
            job._BCjobTitle = job.GetJob();
            if (string.IsNullOrEmpty(job._BCjobTitle))
            {
                i = 1;
            }
            else
            {
                job._BCcompany = job.GetCompany();
                job._BCstartYear = job.get_start_year();
                job._BCendYear = job.get_end_year();
                resume._BCjobs.Add(job);
            }

            
        }

    resume.Display();
       
    }

}