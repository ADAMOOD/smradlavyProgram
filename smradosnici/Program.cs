using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using System.Runtime.CompilerServices;

namespace smradlavyProgram
{
    internal class Program
    {
       static Dictionary<string, string> nameSmell = new Dictionary<string, string>();
        static void Main(string[] args)
        {
            while (true)
            {
                int num = getNumberOf();
                
                foreach (var guy in getNames(num, nameSmell))
                {
                    Console.WriteLine($"{guy.Key} \t {guy.Value}");
                }
            }

        }
        private static int getNumberOf()
        {
            Console.WriteLine("zadej kolik máš smraďochů na otestování");
            int number = Convert.ToInt32(Console.ReadLine());
            return number;
        }
        private static Dictionary<string, string> getNames(int num, Dictionary<string, string> nameSmell)
        {
            string smell;
           

            for (int i = 0; i < num; i++)
            {
                Console.WriteLine($"zadej smraďocha číslo {i + 1}");
                string name = Console.ReadLine();
                smell=countSmell(name);
                nameSmell.Add(name,smell );
            }
            return nameSmell;
        }
        private static string countSmell(string name)
        {
            string smrad = "";
            double avg = 0;
            int sum = 0;
            for (int i = 0; i < name.Length; i++)
            {
                sum += (int)name[i];
            }
            avg = (double)sum / name.Length;
        
        if(Math.Round(avg)%7==0) smrad = "feet";
        else if(Math.Round(avg)%5==0) smrad = "ass";
        else if(Math.Round(avg)%3==0) smrad = "onion";
        else smrad = "none";
        return smrad;
        }
    }

}
