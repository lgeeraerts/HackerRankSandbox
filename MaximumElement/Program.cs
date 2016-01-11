using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaximumElement
{
    class Solution
    {
        static void Main(string[] args)
        {
            var algorithm = new Algorithm();

            var t = Convert.ToInt32(Console.ReadLine());

            for(var i = 0; i < t; i++)
            {
                var line = Console.ReadLine();
                var elements = line.Split(' ');
                var command = Convert.ToInt32(elements[0]);
                var value = command == 1 ? Convert.ToInt32(elements[1]) : -1;

                algorithm.ProcessCommand(command, value);
            }
        }

        public class Algorithm
        {
            private Stack<int> mainStack = new Stack<int>();
            private Stack<int> maxStack = new Stack<int>();
            
            public void ProcessCommand(int command, int value)
            {
                int maxTop;
                int mainTop;
                switch (command)
                {
                    case 1:
                        maxTop = maxStack.Count == 0 ? 0 : maxStack.Peek();

                        if(value >= maxTop)
                        {
                            maxStack.Push(value);
                        }

                        mainStack.Push(value);
                        break;
                    case 2:
                        if (mainStack.Count == 0) break;

                        mainTop = mainStack.Peek();

                        if (maxStack.Count > 0)
                        {
                            maxTop = maxStack.Peek();

                            if (mainTop == maxTop)
                            {
                                maxStack.Pop();
                            }
                        }

                        mainStack.Pop();
                        break;
                    default:
                        if (maxStack.Count > 0)
                        {
                            Console.WriteLine(maxStack.Peek());
                        }
                        break;
                }
            }
        }
    }
}
