using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace YloFlix
{
    public class Parser
    {
        private FileReader FileReader { get; set; }

        private Episode Episode { get; set; }

        public Parser(FileReader fileReader, Episode episode)
        {
            this.FileReader = fileReader;
            this.Episode = episode;
        }

        public void Launch()
        {
            int nbThread = this.FileReader.NbLines / 50;
            Thread[] tThread = new Thread[nbThread];
            for(int i = 0; i < nbThread; i++)
            {
                tThread[i] = new Thread(() =>
                {
                    while (true)
                    {
                        try
                        {
                            var workingQ = this.FileReader.Pop(50);
                            if (workingQ.Count == 0)
                            {
                                return;
                            }
                            while (workingQ.Count > 0)
                            {
                                string line = workingQ.Pop();
                                if (this.MyStrPos(line, "<div id=\"container95\">"))
                                {
                                    return;
                                }
                            }
                        }catch(Exception e)
                        {
                            Utils.Log(e.Message);
                        }
                    }
                });
                tThread[i].Start();
            }
        }

        public bool MyStrPos(string str, string keyword)
        {
            int x = 0;
            int j = str.Length;
            foreach (char c in str)
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
    }
}
