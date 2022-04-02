using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
namespace smradlavyProgram
{
    internal class Program
    {
        public enum Smells
        {
            none,
            onion,
            ass,
            feet
        };
        static Dictionary<string, Smells> nameSmell = new Dictionary<string, Smells>();
        
        static void Main(string[] args)
        {
            bool count = true;
            CSVProvider.mabeLoadSavedFile(nameSmell);
            while (true)
            {
                int num = getNumberOf(nameSmell, count);
                printTable(num);
                count = true;
            }
        }
        private static bool printTable(int num)
        {
            foreach (var guy in getNames(num, nameSmell))
            {
                Console.Write($"{guy.Key} ");
                switch (guy.Value)
                {

                    case Smells.none:
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(Constants.none);
                        }
                        break;
                    case Smells.onion:
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine(Constants.onion);
                        }
                        break;
                    case Smells.ass:
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine(Constants.ass);
                        }
                        break;
                    case Smells.feet:
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(Constants.feet);
                        }
                        break;
                }
                Console.ResetColor();

            }
            return false;
        }
        private static bool delete(Dictionary<string, Smells> nameSmell)
        {
            if (FileHelper.ensureFileExists())
            {
                Console.WriteLine("opravdu chceš soubor smazat? y/n");
                string decide = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.Red;
                if (decide.Equals("y"))
                {
                    Console.WriteLine("Soubor smazán");
                    File.Delete(Constants.path);
                    nameSmell.Clear();
                    return false;
                }
                return false;
            }
            Console.WriteLine("neexistuje soubor na smazání");
            return false;
        }
        private static bool manual()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"{Constants.exit} - Vypnutí programu\n{Constants.save} - Uloží soubor\n{Constants.delete} - vymaže uložený soubor\n{Constants.print} - vypíše tabulku smraďochů");
            return false;
        }
       
        private static int getNumberOf(Dictionary<string, Smells> nameSmell, bool count)
        {
            int number = 0;
            string word;
            bool request;
            do
            {
                request = true;
                Console.ResetColor();
                Console.WriteLine("zadej kolik máš smraďochů na otestování anebo ulož soubor (můžeš použít manual)");
                word = Console.ReadLine();
                if (Regex.IsMatch(word, @"^\d+$"))
                {
                    number = Convert.ToInt32(word);
                }
                if (word.Equals(Constants.print, StringComparison.InvariantCultureIgnoreCase) && (number != 0))
                {
                    request = printTable(number);
                }
                if (word.Equals(Constants.delete, StringComparison.InvariantCultureIgnoreCase))
                {
                    request = delete(nameSmell);
                }
                if (word.Equals(Constants.manual, StringComparison.InvariantCultureIgnoreCase))
                {
                    request = manual();
                }
                if (word.Equals(Constants.exit, StringComparison.InvariantCultureIgnoreCase))
                {
                    request = reallyExit(word);
                }

                if (word.Equals(Constants.save, StringComparison.InvariantCultureIgnoreCase))
                {
                    count = request = saving(nameSmell, count);
                }
            } while (request == false);
            Console.ResetColor();
            return number;
        }
        private static Dictionary<string, Smells> getNames(int num, Dictionary<string, Smells> nameSmell)
        {
            Smells smell;
            for (int i = 0; i < num; i++)
            {
                bool requestExit = true;
                string name;
                do
                {
                    requestExit = true;
                    Console.WriteLine($"zadej smraďocha číslo {i + 1}");
                    name = Console.ReadLine();
                    if (name.Equals(Constants.exit, StringComparison.InvariantCultureIgnoreCase))
                    {
                        requestExit = reallyExit(name);
                    }
                } while (requestExit == false);
                smell = countSmell(name);
                nameSmell.Add(name, smell);
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("VÝPIS SMRAĎOCHŮ:\n************************************************************");
            Console.ResetColor();
            return nameSmell;
        }
        private static Smells countSmell(string name)
        {
            int sum = 0;
            for (int i = 0; i < name.Length; i++)
            {
                if (name[i] == ' ')
                {
                    continue;
                }
                sum += (int)name[i];
            }
            var INCavg = (double)sum / name.Length;
            double avg= (Math.Round(INCavg));
            if (avg % 7 == 0)
                return Smells.feet;
            if (avg % 5 == 0)
                return Smells.ass;
            if (avg% 3 == 0)
                return Smells.onion;
            return Smells.none;
        }
        private static bool reallyExit(string text)
        {
            Console.WriteLine("Opravdu chceš odejít zadej? y/n");
            string decide = Console.ReadLine();
            if (decide == "y")
            {
                Environment.Exit(0);
                return true;
            }
            return false;
        }
        private static bool saving(Dictionary<string, Smells> nameSmell, bool count)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            if (count)
            {
                CSVProvider.saveFile(nameSmell);
                Console.WriteLine("ulozeno");
                return false;
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Žádné změny se neukládají");
            return false;
        }
    }
}