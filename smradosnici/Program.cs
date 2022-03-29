using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
namespace smradlavyProgram
{
    internal class Program
    {
        enum Smells { none, onion, ass, feet }
        static Dictionary<string, Smells> nameSmell = new Dictionary<string, Smells>();
        const string path = "c:\\tmp\\smellyFile.txt";
        static void Main(string[] args)
        {
            bool count=true;
            mabeLoadSavedFile(nameSmell);
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
                            Console.WriteLine(Levels.none);
                        }
                        break;
                    case Smells.onion:
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine(Levels.onion);
                        }
                        break;
                    case Smells.ass:
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine(Levels.ass);
                        }
                        break;
                    case Smells.feet:
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(Levels.feet);
                        }
                        break;
                }
                Console.ResetColor();

            }
            return false;
        }
        private static bool delete(Dictionary<string, Smells> nameSmell)
        {
            if (File.Exists(path))
            {
                Console.WriteLine("opravdu chceš soubor smazat? y/n");
                string decide = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.Red;
                if (decide.Equals("y"))
                {
                    Console.WriteLine("Soubor smazán");
                    File.Delete(path);
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
            Console.WriteLine($"{Levels.exit} - Vypnutí programu\n{Levels.save} - Uloží soubor\n{Levels.delete} - vymaže uložený soubor\n{Levels.print} - vypíše tabulku smraďochů");
                return false;
        }
        private static void mabeLoadSavedFile(Dictionary<string, Smells> nameSmell)
        {
            if(File.Exists(path))
            {
                var file = File.ReadAllLines(path);
                if (file.Length == 0)
                {
                    return;
                }
                foreach (var smell in file)
                {
                    string name = smell.Split(';')[0];
                    Smells vallue =(Smells)Enum.Parse(typeof(Smells),(smell.Split(';')[1]));
                    nameSmell.Add(name, vallue);
                }
            }
        }
        private static int getNumberOf(Dictionary<string, Smells> nameSmell,bool count)
        {
            int number=0;
            string word;
            bool request;
            do
            {
                request=true;
                Console.ResetColor();
                Console.WriteLine("zadej kolik máš smraďochů na otestování anebo ulož soubor (můžeš použít manual)");
                word = Console.ReadLine();
                if(Regex.IsMatch(word, @"^\d+$"))
                {
                    number = Convert.ToInt32(word);
                }
                if (word.Equals(Levels.print, StringComparison.InvariantCultureIgnoreCase)&&(number!=0))
                {
                    request = printTable(number);
                }
                if (word.Equals(Levels.delete, StringComparison.InvariantCultureIgnoreCase))
                {
                   request = delete(nameSmell);
                }
                if (word.Equals(Levels.manual, StringComparison.InvariantCultureIgnoreCase))
                {
                    request = manual();
                }
                if (word.Equals(Levels.exit, StringComparison.InvariantCultureIgnoreCase))
                {
                    request = reallyExit(word);
                }
                if (word.Equals(Levels.save, StringComparison.InvariantCultureIgnoreCase))
                {
                    count = request = saveFile(nameSmell, count);
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
                    requestExit=true;
                    Console.WriteLine($"zadej smraďocha číslo {i + 1}");
                     name = Console.ReadLine();
                    if (name.Equals(Levels.exit, StringComparison.InvariantCultureIgnoreCase))
                    {
                        requestExit=reallyExit(name);
                    }
                } while (requestExit==false);
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
            double avg ;
            int sum = 0;
            for (int i = 0; i < name.Length; i++)
            {
                if (name[i] == ' ')
                {
                    continue;
                }
                sum += (int)name[i];
            }
            avg = (double)sum / name.Length;
            Math.Round(avg);
            if (Math.Round(avg) % 7 == 0)
                return Smells.feet;
            if (Math.Round(avg) % 5 == 0)
                return Smells.ass;
            if (Math.Round(avg) % 3 == 0)
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
        private static bool saveFile(Dictionary<string, Smells> nameSmell,bool count)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            if (count)
            {
                File.Delete(path);
                foreach (var player in nameSmell)
                {
                    File.AppendAllText(path, $"{player.Key};{player.Value}\n");
                }
                Console.WriteLine("ulozeno");
                return false;
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Žádné změny se neukládají");
            return false;
        }
    }
}