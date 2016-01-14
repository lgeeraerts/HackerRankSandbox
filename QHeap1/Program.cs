using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QHeap1
{
    class Solution
    {
        static void Main(string[] args)
        {
            var t = Convert.ToInt32(Console.ReadLine());
            var commands = new int[t][];

            for (int i = 0; i < t; i++)
            {
                var segments = Console.ReadLine().Split(' ');
                var command = Convert.ToInt32(segments[0]);
                var argument = command < 3 ? Convert.ToInt32(segments[1]) : -1;

                commands[i] = new int[] { command, argument };
            }

            var algorithm = new Algorithm();
            algorithm.Process(commands);
        }

        public class Algorithm
        {
            private MinHeap minHeap = new MinHeap(100);

            public void Process(int[][] commands)
            {
                foreach (var command in commands)
                {
                    if(command[0] == 1) {
                        minHeap.Insert(command[1]);
                    }
                    else if(command[0] == 2)
                    {
                        minHeap.RemoveKey(command[1]);
                    }
                    else if (command[0] == 3)
                    {
                        Console.WriteLine(minHeap.Min());
                    }
                }
            }

            private class MinHeap
            {
                private int[] elements;
                private int N = 0;

                public MinHeap(int initialSize)
                {
                    elements = new int[initialSize];
                }

                public void Insert(int key)
                {
                    elements[N] = key;
                    Heapify(N);
                    N++;                    
                }

                public int Min()
                {
                    return elements[0];
                }

                public void RemoveKey(int key)
                {
                    RemoveKeyStep(key, 0);
                }

                private bool RemoveKeyStep(int key, int position)
                {
                    if(position >= N || elements[position] > key)
                    {
                        return false;
                    }

                    if (elements[position] == key)
                    {
                        elements[position] = elements[N - 1];
                        N--;
                        Heapify(position);
                        return true;
                    }

                    var leftChildPosition = position * 2;
                    var rightChildPosition = leftChildPosition++;

                    var isRemovedInLeft = RemoveKeyStep(key, leftChildPosition);
                    var isRemovedInRight = isRemovedInLeft ? false : RemoveKeyStep(key, rightChildPosition);

                    return isRemovedInLeft || isRemovedInRight;
                }

                private void Heapify(int position)
                {
                    if (position > 0)
                    {
                        var parentPosition = position / 2;

                        if (elements[parentPosition] > elements[position])
                        {
                            SwitchElements(position, parentPosition);
                            Heapify(parentPosition);
                            return;
                        }
                    }

                    var leftChildPosition = 2 * position;

                    if(leftChildPosition < N && elements[leftChildPosition] < elements[position])
                    {
                        SwitchElements(position, leftChildPosition);
                        Heapify(leftChildPosition);
                        return;
                    }

                    var rightChildPosition = leftChildPosition + 1;

                    if(rightChildPosition < N && elements[rightChildPosition] < elements[position])
                    {
                        SwitchElements(position, rightChildPosition);
                        Heapify(rightChildPosition);
                    }
                }

                private void SwitchElements(int fromPos, int toPos)
                {
                    var tmpPosition = elements[toPos];
                    elements[toPos] = elements[fromPos];
                    elements[fromPos] = tmpPosition;                    
                }

                private void Grow()
                {
                    var newSize = elements.Length * 2;
                    var newArray = new int[newSize];

                    for (int i = 0; i < N; i++)
                    {
                        newArray[i] = elements[i];
                    }

                    elements = newArray;
                }
            }
        }
    }
}
