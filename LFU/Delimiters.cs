using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFU
{
    public class Delimiter
    {

        public Delimiter(char charvalue)
        {
            CharValue = charvalue;
        }

        public char CharValue { get; private set; }

        public byte AsciiNumber
        {
            get
            {
                return (byte)CharValue;
            }
        }

        public string Display
        {
            get
            {
                return string.Format(
                    "{0,3} {1}",
                    AsciiNumber.ToString(),
                    char.IsControl(CharValue)
                        ? "x" + AsciiNumber.ToString()
                        : CharValue.ToString()
                    );
            }
        }
    }

    public static class DelimiterCharacters
    {
        static DelimiterCharacters() { }

        private static Delimiter[] _DelimiterCharacters;

        /// <summary>
        /// Returns the list of delimiter characters as an array
        /// </summary>
        public static Delimiter[] Get
        {
            get
            {
                return _DelimiterCharacters;
            }
        }

        /// <summary>
        /// Build the array of delimiter characters.
        /// Excludes numbers and regular english alphabet characters
        /// </summary>
        public static void Create()
        {
            // create delimiters characters
            List<Delimiter> temp = new List<Delimiter>();

            for (byte i = 1; i < byte.MaxValue; i++) // skip null
            {
                // skip numbers, uppercase english alphabet letters, and lowercase english alphabet letters
                if ((i > 47 && i < 58) || (i > 64 && i < 91) || (i > 96 && i < 123) || (i > 126 && i < 161))
                {
                    continue;
                }
                temp.Add(new Delimiter((char)i));
            }

            _DelimiterCharacters = temp.ToArray<Delimiter>();
        }
            
    }

}
