using System;
using System.IO;

namespace GroceryStore.Commons
{
    public class FileHandler
    {
        public static void PrintFile(string text, string path)
        {
            var file = new FileStream(path, FileMode.Create);
            var streamwriter = new StreamWriter(file);
            streamwriter.WriteLine(text);
            streamwriter.Close();
            file.Close();
        }
    }
}
