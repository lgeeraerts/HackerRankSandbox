using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalancedParentheses
{
    class Solution
    {
        static void Main(string[] args)
        {
            var t = Convert.ToInt32(Console.ReadLine());

            var algorithm = new Algorithm();

            for (int i = 0; i < t; i++)
            {
                var parenthesesSequence = Console.ReadLine();

                algorithm.Process(parenthesesSequence);
            }            
        }

        private class Algorithm
        {
            private Stack<char> stack = new Stack<char>();

            public void Process(string parenthesesSequence)
            {
                stack.Clear();

                Console.WriteLine(IsBalanced(parenthesesSequence) ? "YES" : "NO");
            }

            private bool IsBalanced(string parenthesesSequence)
            {
                for (int i = 0; i < parenthesesSequence.Length; i++)
                {
                    var ch = parenthesesSequence[i];

                    if (ch == '{' || ch == '[' || ch == '(')
                    {
                        stack.Push(ch);
                    }
                    else
                    {
                        if (stack.Count == 0)
                        {
                            return false;
                        }

                        var currentChar = stack.Peek();

                        switch (ch)
                        {
                            case '}':
                                if (currentChar != '{') return false;
                                break;
                            case ']':
                                if (currentChar != '[') return false;
                                break;
                            case ')':
                                if (currentChar != '(') return false;
                                break;
                        }

                        stack.Pop();
                    }
                }

                return stack.Count == 0;
            }
        }
    }
}
