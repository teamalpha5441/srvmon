using System;
using System.Drawing;
using System.Collections.Generic;

namespace srvmon.Text
{
    public abstract class Font
    {
        public abstract byte MaxHeight { get; }
        protected abstract Dictionary<char, Letter> Letters { get; }

        public int MeasureString(string String)
        {
            if (String.Length > 0)
            {
                int length = String.Length - 1;
                for (ushort i = 0; i < String.Length; i++)
                    length += GetLetter(String[i]).Width;
                return length;
            }
            else return 0;
        }

        public Letter GetLetter(char Char)
        {
            Letter result = null;
            if (Letters.TryGetValue(Char, out result))
                return result;
            else if (Letters.TryGetValue(char.ToUpper(Char), out result))
                return result;
            else return Letter.GenerateDefaultLetter(4, 7);
        }
    }
}
