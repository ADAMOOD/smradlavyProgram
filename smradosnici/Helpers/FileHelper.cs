using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace smradlavyProgram
{
    internal class FileHelper
    { 
        public static bool ensureFileExists()
        {
            return File.Exists(Constants.path);
        }
    }
}
