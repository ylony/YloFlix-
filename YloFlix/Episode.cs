using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YloFlix
{
    public class Episode
    {
        public int Number { get; set; }
        public int Season { get; set; }
        public string ShowName { get; set; }

        public Episode(int number, int season, string showName)
        {
            this.Number = number;
            this.Season = season;
            this.ShowName = showName;
        }

        public string toAddictedUrl()
        {
            return "http://www.addic7ed.com/search.php?search=" + string.Join("+", this.ShowName.Split(' ')) + "+s" + this.Season + "e" + this.Number + "&Submit=Search";
        }

        override
        public string ToString()
        {
            return this.ShowName + " " + this.Season + " " + this.Number;
        }
    }
}
