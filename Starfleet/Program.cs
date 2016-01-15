using System;
using System.Collections.Generic;
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
                starFighters[i] = new StarFighter(Convert.ToInt32(segments[0]), Convert.ToInt32(segments[1]), Convert.ToInt32(segments[2]));
            }

            var queries = new Query[N];

            for (int i = 0; i < Q; i++)
            {
                segments = (Console.ReadLine()).Split(' ');
                queries[i] = new Query(Convert.ToInt32(segments[0]), Convert.ToInt32(segments[1]), Convert.ToInt32(segments[2]));
            }

            var algorithm = new Algorithm(starFighters);

            foreach (var query in queries)
            {
                algorithm.Process(query);
            }
        }

        private class Algorithm
        {
            private StarFighter[] starFighters;
            private Dictionary<int, int> frequencyCount;
            
            public Algorithm(StarFighter[] starFighters)
            {
                this.starFighters = starFighters;
                this.frequencyCount = new Dictionary<int, int>();
            }

            public void Process(Query query)
            {
                this.frequencyCount.Clear();

                this.FilterStarFightersAndCountFrequencies(query);

                Console.WriteLine(FindHighestFrequencyCount());
            }

            private void FilterStarFightersAndCountFrequencies(Query query)
            {
                foreach (var sf in starFighters)
                {
                    if (query.Contains(sf)) {
                        if (frequencyCount.ContainsKey(sf.f))
                        {
                            frequencyCount[sf.f] = frequencyCount[sf.f] + 1;
                        }
                        else
                        {
                            frequencyCount.Add(sf.f, 1);
                        }
                    }
                }
            }

            private int FindHighestFrequencyCount()
            {
                return frequencyCount.Values.Max();
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
    }
}
