using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckTour
{
    class Solution
    {
        static void Main(string[] args)
        {
            var t = Convert.ToInt32(Console.ReadLine());
            var petrolPumps = new int[t][];

            for (int i = 0; i < t; i++)
            {
                var line = Console.ReadLine();
                var segments = line.Split(' ');

                petrolPumps[i] = new int[] { Convert.ToInt32(segments[0]), Convert.ToInt32(segments[1])};
            }

            var algorithm = new Algorithm();
            algorithm.Process(petrolPumps);
        }

        private class Algorithm
        {
            private Queue<int> truckTour = new Queue<int>();

            public void Process(int [][] petrolPumps)
            {
                truckTour.Clear();

                InitQueue(petrolPumps);

                CalculateAndShiftTillResult(petrolPumps);
            }

            private void InitQueue(int[][] petrolPumps)
            {
                for (int i = 0; i < petrolPumps.Length; i++)
                {
                    truckTour.Enqueue(i);
                }
            }

            private void CalculateAndShiftTillResult(int[][] petrolPumps)
            {
                var tankLevel = 0;
                var isStart = true;                

                while (isStart || tankLevel < 0) {
                    tankLevel = 0;
                    var enumerator = truckTour.GetEnumerator();

                    while (enumerator.MoveNext())
                    {
                        var petrolPumpIndex = enumerator.Current;
                        tankLevel = tankLevel + petrolPumps[petrolPumpIndex][0] - petrolPumps[petrolPumpIndex][1];

                        if (tankLevel < 0) break;
                    }

                    isStart = false;
                    if (tankLevel < 0)
                    {
                        truckTour.Enqueue(truckTour.Dequeue());
                    }
                }

                Console.WriteLine(truckTour.Peek());
            }
        }
    }
}
