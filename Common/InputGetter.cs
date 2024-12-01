using System;
using System.IO;

namespace AdventOfCode.Common
{
    public class InputGetter
    {
        public string Filename = string.Empty;

        public InputGetter(string filename)
        {
            if (string.IsNullOrEmpty(filename))
                throw new ArgumentException("filename cannot be empty");

            if (!File.Exists(filename))
                throw new ArgumentException($"{filename} does not exist");

            Filename = filename;
        }

        public string[] GetStringsFromInput()
        {
            return File.ReadAllLines(Filename);
        }
    }
}