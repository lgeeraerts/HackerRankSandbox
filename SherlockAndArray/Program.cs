using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SherlockAndArray
{
    class Solution
    {
        static void Main(string[] args)
        {
            var t = Convert.ToInt32(Console.ReadLine());

            var algorithm = new Algorithm();

            for (int i = 0; i < t; i++)
            {
                var n = Convert.ToInt32(Console.ReadLine());
                var segments = Console.ReadLine().Split(' ');
                var array = new int[segments.Length];

                for (int j = 0; j < segments.Length; j++)
                {
                    array[j] = Convert.ToInt32(segments[j]);
                }

                algorithm.Process(array);
            }
        }

        public class Algorithm
        {
            public void Process(int[] array)
            {
                var total = array.Sum();

                var remaining = total - array[0] - array[1];
                var counted = array[0];

                var containsEqualParts = ProcessStep(array, 1, counted, remaining);

                Console.WriteLine(containsEqualParts ? "YES" : "NO");
            }
            
            private bool ProcessStep(int[] array, int position, int counted, int remaining)
            {
                if(counted == remaining)
                {
                    return true;
                }

                if(counted > remaining)
                {
                    return false;
                }

                counted += array[position];
                position++;
                remaining -= array[position];

                return ProcessStep(array, position, counted, remaining);
            }
        }
    }
}
