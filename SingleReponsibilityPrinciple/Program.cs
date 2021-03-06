using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using static System.Console;

namespace SingleReponsibilityPrinciple
{
    public class Journal
    {
        private readonly List<string> entries = new List<string>();
        private static int count = 0;

        public int AddEntry(string text)
        {
            entries.Add($"{++count} : {text}");
            return count;
        }

        public void RemoveEnrty(int index)
        {
            entries.RemoveAt(index);
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, entries);
        }
    }

    public class Persistence
    {
        public void SaveToFile(Journal j, string filename, bool overwrite = false)
        {
            if (overwrite || !File.Exists(filename))
                File.WriteAllText(filename, j.ToString());
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var j = new Journal();
            j.AddEntry("I woke up 5:30 AM today.");
            j.AddEntry("Then I had breakfast.");
            WriteLine(j);

            var p = new Persistence();
            var filename = @"C:\TempTest\journal.txt";
            p.SaveToFile(j, filename, true);
            new Process { StartInfo = new ProcessStartInfo(filename) { UseShellExecute = true } }.Start();
        }

    }
}
