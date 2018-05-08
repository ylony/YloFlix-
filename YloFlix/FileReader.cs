using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YloFlix
{
    public class FileReader
    {

        public string Path { get; set; }
        public int NbLines { get; set; }
        public Stack<string> Stack { get; set; }

        public FileReader(string path)
        {
            this.Path = path;
        }

        public FileReader PutFileInMemory()
        {
            this.Stack = new Stack<string>(File.ReadAllLines(this.Path));
            this.NbLines = Stack.Count;
            return this;
        }

        public Stack<string> Pop(int nbElement)
        {
            Stack<string> stack = new Stack<string>();
            for(int i = 0; i < nbElement; i++)
            {
                try
                {
                    stack.Push(this.Stack.Pop());
                }
                catch (InvalidOperationException)
                {
                    return stack;
                }
            }
            return stack;
        }
    }
}
