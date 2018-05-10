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

        private string Line { get; set; }

        private Thread[] TThread { get; set; }

        public Parser(FileReader fileReader, Episode episode)
        {
            this.FileReader = fileReader;
            this.Episode = episode;
        }

        public void Launch(string keywords)
        {
            int nbThread = this.FileReader.NbLines / 50;
            nbThread = 1;
            Utils.Log(nbThread.ToString() + " " + this.FileReader.NbLines.ToString());
            Utils.Log(keywords);
            this.TThread = new Thread[nbThread];
            for(int i = 0; i < nbThread; i++)
            {
                this.TThread[i] = new Thread(() =>
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
                                if (this.MyStrPos(line, keywords))
                                {
                                    Utils.Log("found");
                                    this.Line = line;
                                    return;
                                }
                            }
                        }catch(Exception e)
                        {
                            Utils.Log(e.Message);
                        }
                    }
                });
                Utils.Log("Thread launch");
                this.TThread[i].Start();
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

        public void getDownloadLink()
        {
            this.Launch("<td width=\"21%\" class=\"language\">French");
            foreach(Thread thread in this.TThread)
            {
                thread.Join();
            }
            Utils.Log(this.Line);
        }
    }
}
