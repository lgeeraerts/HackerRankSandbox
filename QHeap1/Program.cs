using System;
using System.Collections.Generic;
using System.IO;
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

            //Console.SetOut(new StreamWriter(new FileStream("./output.txt", FileMode.OpenOrCreate)));

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
                    if (N == elements.Length) Grow();
                    elements[N] = key;
                    Swim(N);
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
                        Sink(position);
                        return true;
                    }

                    var leftChildPosition = position * 2 + 1;
                    var rightChildPosition = leftChildPosition + 1;

                    var isRemovedInLeft = RemoveKeyStep(key, leftChildPosition);
                    var isRemovedInRight = isRemovedInLeft ? false : RemoveKeyStep(key, rightChildPosition);

                    return isRemovedInLeft || isRemovedInRight;
                }

                private void Sink(int position)
                {
                    var leftChildPosition = 2 * position + 1;
                    var rightChildPosition = leftChildPosition + 1;
                    var positionOfSmallest = 0;

                    if (leftChildPosition < N && elements[leftChildPosition] < elements[position]) positionOfSmallest = leftChildPosition;
                    else positionOfSmallest = position;

                    if (rightChildPosition < N && elements[rightChildPosition] < elements[positionOfSmallest]) positionOfSmallest = rightChildPosition;
                    if (positionOfSmallest != position)
                    {
                        SwitchElements(position, positionOfSmallest);
                        Sink(positionOfSmallest);
                    }
                }

                private void Swim(int position)
                {
                    if (position > 0)
                    {
                        var parentPosition = position / 2;

                        if (elements[parentPosition] > elements[position])
                        {
                            SwitchElements(position, parentPosition);
                            Swim(parentPosition);
                        }
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
