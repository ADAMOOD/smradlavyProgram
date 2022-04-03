using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace smradlavyProgram
{
    internal class CSVProvider
    {
        private const string Separator = ";";
        public static void saveFile ( Dictionary<string, Program.Smells> nameSmell)
        {   
            StringBuilder sb = new StringBuilder ();
            if (FileHelper.ensureFileExists())
            {
                File.Delete(Constants.path);
            }
            sb.AppendLine($"\"Name\"{Separator}\"Smell Level\"");
            foreach (var player in nameSmell)
            {
                sb.AppendLine($"\"{player.Key}\"{Separator}\"{player.Value}\"");
            }
            File.AppendAllText(Constants.path,sb.ToString());
        }
        public static void mabeLoadSavedFile(Dictionary<string, Program.Smells> nameSmell)
        {
            if (FileHelper.ensureFileExists())
            {
                var file = File.ReadAllLines(Constants.path);
                if (file.Length == 0)
                {
                    return;
                }

                int controll = 0;
                foreach (var smell in file)
                {
                    if (controll == 0)
                    {
                        controll++;
                        continue;
                    }
                    var parts=smell.Split(Convert.ToChar(Separator));
                    string savedName = parts[0].Trim('"');
                    Program.Smells savedVallue = (Program.Smells)Enum.Parse(typeof(Program.Smells), parts[1].Trim('"'));
                    nameSmell.Add(savedName, savedVallue);
                }
                }
        }

        public static void delete(Dictionary<string, Program.Smells> nameSmell)
        {
            File.Delete(Constants.path);
            nameSmell.Clear();
        }
    }
}
