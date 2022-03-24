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
        enum smells { none, onion, ass, feet }
        static Dictionary<string, smells> nameSmell = new Dictionary<string, smells>();
        const string path = "c:\\tmp\\smellyFile.txt";


        static void Main(string[] args)
        {
            bool count=false;
            mabeLoadSavedFile(nameSmell);
            while (true)
            {

                int num = getNumberOf(nameSmell,count);
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
                count=true;
            }
        }
        private static void mabeLoadSavedFile(Dictionary<string, smells> nameSmell)
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
                    smells vallue =(smells)Enum.Parse(typeof(smells),(smell.Split(';')[1]));
                    nameSmell.Add(name, vallue);
                }
            }
        }

        private static int getNumberOf(Dictionary<string, smells> nameSmell,bool count)
        {
            string word;
            bool requestExit;
            do
            {
                requestExit=true;
                Console.WriteLine("zadej kolik máš smraďochů na otestování anebo ulož soubor (save)");
                word = Console.ReadLine();
                if (word == Levels.exit)
                {
                    requestExit = reallyExit(word);
                }
                if(word == Levels.save)
                {
                    count = requestExit = saveFile(nameSmell,count);
                }
            } while (requestExit == false);
            int number = Convert.ToInt32(word);
            return number;
        }
        private static Dictionary<string, smells> getNames(int num, Dictionary<string, smells> nameSmell)
        {
            smells smell;
            for (int i = 0; i < num; i++)
            {
                bool requestExit = true;
                string name;
                do
                {
                    requestExit=true;
                    Console.WriteLine($"zadej smraďocha číslo {i + 1}");
                     name = Console.ReadLine();
                    if (name == Levels.exit)
                    {
                        requestExit=reallyExit(name);
                    }
                } while (requestExit==false);

                smell = countSmell(name);

                nameSmell.Add(name, smell);
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("VÝPIS SMRAĎOCHŮ:\n***************************************");
            Console.ForegroundColor = ConsoleColor.Gray;
            return nameSmell;
        }
        private static smells countSmell(string name)
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
                return smells.feet;
            if (Math.Round(avg) % 5 == 0)
                return smells.ass;
            if (Math.Round(avg) % 3 == 0)
                return smells.onion;
            return smells.none;

        }
        private static bool reallyExit(string text)
        {
           
                Console.WriteLine("Opravdu chceš odejít zadej y/n");
                string controll = Console.ReadLine();
                if (controll == "y")
                {
                    Environment.Exit(0);
                    return true;
                }
                return false;

        }
        private static bool saveFile(Dictionary<string, smells> nameSmell,bool count)
        {
            if(count)
            {
                File.Delete(path);
                foreach (var player in nameSmell)
                {
                    File.AppendAllText(path, $"{player.Key};{player.Value}\n");
                }
                Console.WriteLine("ulozeno");
                return false;
            }
            Console.WriteLine("Žádné změny se neukládají");
            return false;
        }
    }
}