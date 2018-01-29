using System;

namespace corefeatures
{
    public static partial class Utils
    {
        public static int GetStrLenth(this string str) => string.IsNullOrEmpty(str) ? 0 : str.Length;

        public static void ConsoleStar(object o)
        {
            if(o is null) return;
            if(!(o is int i)) return;
            Console.WriteLine(new string('*',i));
        }
        public static (double,bool) TryParseNumber(string ParseStr)
        {
            if(double.TryParse(ParseStr,out var reulst))
            {
                return (reulst,true);
            }
            else
                return (double.NaN,false);
        }
    }

}