using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starfleet
{
    class Solution
    {
        static void Main(string[] args)
        {
            var segments = (Console.ReadLine()).Split(' ');
            var N = Convert.ToInt32(segments[0]);
            var Q = Convert.ToInt32(segments[1]);
            var V = Convert.ToInt32(segments[2]);

            var starFighters = new StarFighter[N];

            for (int i = 0; i < N; i++)
            {
                segments = (Console.ReadLine()).Split(' ');
                var frequency = Convert.ToInt32(segments[2]);
                var sf = new StarFighter(Convert.ToInt32(segments[0]), Convert.ToInt32(segments[1]), frequency);
                starFighters[i] = sf;
            }

            var sfByFrequency = starFighters.GroupBy(sf => sf.f, sf => sf).Select(g => g.OrderBy(sf => sf.y).ToArray()).ToArray();
            
            var queries = new int[Q][];

            for (int i = 0; i < Q; i++)
            {
                segments = (Console.ReadLine()).Split(' ');
                queries[i] = new int[] { Convert.ToInt32(segments[0]), Convert.ToInt32(segments[1]), Convert.ToInt32(segments[2]), 0 };
            }

            var algorithm = new Algorithm(sfByFrequency);

            var tasks = new Task[Environment.ProcessorCount];
            var interval = queries.Length / Environment.ProcessorCount;
            for (int i = 0; i < Environment.ProcessorCount; i++)
            {                
                var start = i * interval;
                var end = i == Environment.ProcessorCount - 1 ? queries.Length :  start + interval;
                tasks[i] = new Task(() => {
                    for (int j = start; j < end; j++)
                    {
                        algorithm.Process(queries[j]);
                    }
                });

                tasks[i].Start();                
            }

            Task.WaitAll(tasks);

            foreach (var query in queries)
            {
                Console.WriteLine(query[3]);
            }
        }

        private class Algorithm
        {
            private StarFighter[][] starFighters;
            
            public Algorithm(StarFighter[][] starFighters)
            {
                this.starFighters = starFighters;
            }

            public void Process(int[] query)
            {
                this.FilterStarFightersAndCountFrequencies(query);
            }

            private int FilterStarFightersAndCountFrequencies(int[] query)
            {
                var max = 0;

                foreach (var frSfList in starFighters)
                {
                    var size = 0;
                    if (frSfList.Length == 1 && frSfList[0].y >= query[1] && frSfList[0].y <= query[0])
                    {
                        size = 1;
                    }
                    else
                    {
                        var beginPosition = FindPositionOrFirstBeforeOrAfter(0, frSfList.Length - 1, query[1], true, frSfList);
                        var endPosition = FindPositionOrFirstBeforeOrAfter(beginPosition, frSfList.Length - 1, query[0], false, frSfList);

                        size = endPosition < beginPosition ? 0 : endPosition == beginPosition ? 1 : endPosition - beginPosition + 1;
                    }

                    max = Math.Max(max, size);
                }

                query[3] = max;

                return max;
            }
            
            private int FindPositionOrFirstBeforeOrAfter(int start, int end, int key, bool firstAfter, StarFighter[] list)
            {
                while (start < end) {
                    var mid = (start + end) / 2;
                    var midKey = list[mid].y;

                    if (midKey == key) return SearchUpOrDown(mid, key, firstAfter, list);
                    else if (key < midKey) end = mid - 1;
                    else start = mid + 1;
                }
                
                var sf = list[start];
                if (sf.y == key) return start;
                else
                {
                    var pos = 0;
                    if (firstAfter)
                    {
                        pos = sf.y > key ? start : start + 1;
                        if (pos >= list.Length) pos = list.Length - 1;
                    }
                    else
                    {
                        pos = sf.y < key ? start : start - 1;
                        if (pos < 0) pos = 0;
                    }
                    
                    return pos;
                }
            }

            private int SearchUpOrDown(int position, int key, bool down, StarFighter[] list)
            {
                var pos = position;
                var nextpos = down ? pos - 1 : pos + 1;
                while (nextpos >= 0 && nextpos < list.Length && list[nextpos].y == key)
                {
                    pos = nextpos;
                    if (down) nextpos--; else nextpos++;
                }

                return pos;
            }
        }

        private class StarFighter {
            
            public StarFighter(int x, int y, int f)
            {
                this.x = x;
                this.y = y;
                this.f = f;
            }

            public int x;
            public int y;
            public int f;
        }
    }
}
