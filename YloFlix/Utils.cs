using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace YloFlix
{
    public class Utils
    {
        public static bool debug = true;

        public static void Log(string s)
        {
            if (Utils.debug)
            {
                Console.WriteLine(s);
            }
        }

        public static Episode ConvertStrToEpisode(string str)
        {
            string showName = "";
            int number = 0, season = 0;
            string[] words = str.Split('.');
            foreach(string word in words)
            {
                if(!Regex.IsMatch(word, "^S(\\d{2})E(\\d{2}$)"))
                {
                    showName += word + " ";
                }
                else
                {
                    string[] data = word.Split('E');
                    season = int.Parse(data[0].Substring(1));
                    number = int.Parse(data[1]);
                    return new Episode(number, season, showName.TrimEnd(' '));
                }

            }
            throw new Exception("Can't translate that string into an episode object");
        }
    }
}
