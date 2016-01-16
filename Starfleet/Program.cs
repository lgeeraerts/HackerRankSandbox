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
            var frequencyDictionary = new Dictionary<int, Counter>();
            var frequencyCounterList = new List<Counter>();

            for (int i = 0; i < N; i++)
            {
                segments = (Console.ReadLine()).Split(' ');
                var frequency = Convert.ToInt32(segments[2]);
                var sf = new StarFighter(Convert.ToInt32(segments[0]), Convert.ToInt32(segments[1]), frequency);
                starFighters[i] = sf;
                if (!frequencyDictionary.ContainsKey(frequency))
                {
                    var frequencyCounter = new Counter();
                    frequencyDictionary.Add(frequency, frequencyCounter);
                    frequencyCounterList.Add(frequencyCounter);
                    sf.frequencyCounter = frequencyCounter;
                }
                else
                {
                    sf.frequencyCounter = frequencyDictionary[frequency];
                }
            }

            starFighters = starFighters.OrderBy(sf => sf.y).ToArray();

            var queries = new Query[Q];

            for (int i = 0; i < Q; i++)
            {
                segments = (Console.ReadLine()).Split(' ');
                queries[i] = new Query(Convert.ToInt32(segments[0]), Convert.ToInt32(segments[1]), Convert.ToInt32(segments[2]));
            }

            var algorithm = new Algorithm(starFighters, frequencyCounterList);

            //Console.SetOut(new StreamWriter(new FileStream("./output.txt", FileMode.OpenOrCreate)));

            //var stopWatch = new System.Diagnostics.Stopwatch();
            //stopWatch.Start();
            foreach (var query in queries)
            {
                algorithm.Process(query);
            }
            //stopWatch.Stop();
            //Console.WriteLine("Time: " + stopWatch.ElapsedMilliseconds);
        }

        private class Algorithm
        {
            private StarFighter[] starFighters;
            private List<Counter> frequencyCount;
            
            public Algorithm(StarFighter[] starFighters, List<Counter> frequencyCounterList)
            {
                this.starFighters = starFighters;
                this.frequencyCount = frequencyCounterList;
            }

            public void Process(Query query)
            {
                foreach (var counter in frequencyCount)
                {
                    counter.Count = 0;
                }            

                Console.WriteLine(this.FilterStarFightersAndCountFrequencies(query));
            }

            private int FilterStarFightersAndCountFrequencies(Query query)
            {
                var beginPosition = FindPositionOrFirstBeforeOrAfter(0, starFighters.Length - 1, query.dy, true);
                var endPosition = FindPositionOrFirstBeforeOrAfter(beginPosition, starFighters.Length - 1, query.uy, false);

                var max = 0;

                for (int i = beginPosition; i <= endPosition; i++)
                {
                    var sf = starFighters[i];
                    sf.frequencyCounter.Count++;
                    max = Math.Max(max, sf.frequencyCounter.Count);
                }

                return max;
            }
            
            private int FindPositionOrFirstBeforeOrAfter(int start, int end, int key, bool firstAfter)
            {
                while (start < end) {
                    var mid = (start + end) / 2;
                    var midKey = starFighters[mid].y;

                    if (midKey == key) return SearchUpOrDown(mid, key, firstAfter);
                    else if (key < midKey) end = mid - 1;
                    else start = mid + 1;
                }
                
                var sf = starFighters[start];
                if (sf.y == key) return start;
                else
                {
                    var pos = firstAfter ? start + 1 : start - 1;
                    if (pos < 0) pos = 0;
                    else if (pos >= starFighters.Length) pos = starFighters.Length - 1;
                    return pos;
                }
            }

            private int SearchUpOrDown(int position, int key, bool down)
            {
                var pos = position;
                var nextpos = down ? pos - 1 : pos + 1;
                while (nextpos >= 0 && nextpos < starFighters.Length && starFighters[nextpos].y == key)
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
            public Counter frequencyCounter;
        }

        private class Query
        {
            public Query(int uy, int dy, int t)
            {
                this.uy = uy;
                this.dy = dy;
                this.t = t;
            }

            public bool Contains(StarFighter starFighter)
            {
                return starFighter.y <= this.uy && starFighter.y >= this.dy;
            }

            public int uy;
            public int dy;
            public int t;
        }

        private class Counter
        {
            public int Count = 0;
        }
    }
}
