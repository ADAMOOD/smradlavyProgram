using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using smradosnici;
namespace smradlavyProgram
{
    internal class Program
    {
       enum smells {none, onion, ass, feet}
       static Dictionary<string, smells> nameSmell = new Dictionary<string, smells>();
        static void Main(string[] args)
        {
            while (true)
            {
                int num = getNumberOf();
                foreach (var guy in getNames(num, nameSmell))
                {
                    Console.Write($"{guy.Key} ");
                    switch (guy.Value)
                    {
                       
                        case smells.none:
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine(Levels.none);
                            }
                            break;
                        case smells.onion:
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine(Levels.onion);
                            }
                            break;
                        case smells.ass:
                            {
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine(Levels.ass);
                            }
                            break;
                        case smells.feet:
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine(Levels.feet);
                            }
                            break;

                    }
                    Console.ForegroundColor = ConsoleColor.Gray;
                }  
            }
        }
       

        private static int getNumberOf()
        {
            Console.WriteLine("zadej kolik máš smraďochů na otestování");
            int number = Convert.ToInt32(Console.ReadLine());
            return number;
        }
        private static Dictionary<string, smells> getNames(int num, Dictionary<string, smells> nameSmell)
        {
            smells smell;
            for (int i = 0; i < num; i++)
            {
                Console.WriteLine($"zadej smraďocha číslo {i + 1}");
                string name = Console.ReadLine();
                smell=countSmell(name);
                nameSmell.Add(name,smell);
            }
            return nameSmell;
        }
        private static smells countSmell(string name)
        {
            double avg = 0;
            int sum = 0;
            for (int i = 0; i < name.Length; i++)
            { 
                
                if(name[i]==' ')
                {
                    continue;
                }
                sum += (int)name[i];

            }
            avg = (double)sum / name.Length;
            if (Math.Round(avg) % 7 == 0)
                return smells.feet;
            if (Math.Round(avg) % 5 == 0)
                return smells.ass;
            if (Math.Round(avg) % 3 == 0)
                return smells.onion;
                return smells.none;
        }
    }
}