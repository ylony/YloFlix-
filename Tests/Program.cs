using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YloFlix;

namespace Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
                string str = "Coucou je suis Dimitri";
                if (myStrPos(str, "Coucou je suis Dimitri"))
                {
                    Utils.Log("Contient :)");
                }
                else
                {
                    Utils.Log("Contient pas");
                }
                testSpeed();
            */
            //testGetCpu();
            //Console.ReadKey(true);
            testUseFromPHP();
        }

        public static void testSpeed()
        {
            string str = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas a suscipit sem. Suspendisse eget nisl a justo tempus aliquam. Mauris congue sapien id sapien sagittis, eu posuere odio eleifend. In molestie congue fermentum. Sed sed nulla scelerisque, molestie urna ac, condimentum leo. Pellentesque eu tristique nibh, sit amet lacinia lacus. Praesent dictum non nisl malesuada viverra. Sed nec leo efficitur, faucibus nisl id, venenatis purus. ";
            var now = ((DateTimeOffset)DateTime.Now).ToUnixTimeMilliseconds();
            if(testContains(str, "purus"))
            {
                Utils.Log("TestConstain found");
            }
            var timeContains = ((DateTimeOffset)DateTime.Now).ToUnixTimeMilliseconds() - now;
            now = ((DateTimeOffset)DateTime.Now).ToUnixTimeMilliseconds();
            if(myStrPos(str, "purus"))
            {
                Utils.Log("MyStrPos found");
            }
            var timeMyStrPos = ((DateTimeOffset)DateTime.Now).ToUnixTimeMilliseconds() - now;
            if(timeContains < timeMyStrPos)
            {
                Utils.Log("TimeContains plus rapide : " + timeContains + " Time MyStrPos : " + timeMyStrPos);
            }
            else
            {
                Utils.Log("MyStrPos plus rapide : " + timeMyStrPos + " Time Contains : " + timeContains);
            }
        }

        public static bool testContains(string str, string keyword)
        {
            return str.Contains(keyword);
        }

        public static bool myStrPos(string str, string keyword)
        {
            int x = 0;
            int j = str.Length;
            foreach(char c in str)
            {
                if (c == keyword[x])
                {
                    x += 1;
                }
                else
                {
                    x = 0;
                }
                if (x == keyword.Length)
                {
                    return true;
                }
            }
            return false;
        }

        public static void testGetCpu()
        {
            Utils.Log(Environment.ProcessorCount.ToString());
        }

        public static void testUseFromPHP()
        {
            Utils.Log("Hello World !");
        }

        public static void testRemote()
        {
            /*
            var rIO = new RemoteIO("http://www.addic7ed.com/");
            var fileReader = new FileReader(rIO.Cache());
            fileReader.PutFileInMemory();
            Utils.Log(fileReader.NbLines.ToString());
            var parser = new Parser(fileReader, null);
            parser.Launch();
            var episode = new Episode(4, 4, "Arrow");
            Utils.Log(episode.toAddictedUrl());
            */
        }
    }
}
