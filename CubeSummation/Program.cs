using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeSummation
{
    class Solution
    {
        static void Main(string[] args)
        {
            var t = Convert.ToInt32(Console.ReadLine());

            var a = new Algorithm();

            for (int i = 0; i < t; i++)
            {
                var segments = Console.ReadLine().Split(' ');
                var n = Convert.ToInt32(segments[0]);
                var m = Convert.ToInt32(segments[1]);

                a.Setup(n);

                for (int j = 0; j < m; j++)
                {
                    var cs = Console.ReadLine().Split(' ');
                    var c = cs[0];
                    var a1 = Convert.ToInt32(cs[1]);
                    var a2 = Convert.ToInt32(cs[2]);
                    var a3 = Convert.ToInt32(cs[3]);
                    var a4 = Convert.ToInt32(cs[4]);
                    int a5 = 0, a6 = 0;

                    if (c.Equals("QUERY"))
                    {
                        a5 = Convert.ToInt32(cs[5]);
                        a6 = Convert.ToInt32(cs[6]);
                    }

                    a.Process(c, new int[] { a1, a2, a3, a4, a5, a6 });
                }
            }
        }

        private class Algorithm {

            private int[][][] matrix;

            public void Setup(int n)
            {
                matrix = new int[n][][];

                for (int i = 0; i < n; i++)
                {
                    matrix[i] = new int[n][];

                    for (int j = 0; j < n; j++)
                    {
                        matrix[i][j] = new int[n];
                    }
                }
            }

            public void Process(string command, int[] args)
            {
                if (command.Equals("UPDATE"))
                {
                    matrix[args[0] - 1][args[1] - 1][args[2] - 1] = args[3];
                }
                else
                {
                    var x1 = args[0];
                    var x2 = args[3];
                    var y1 = args[1];
                    var y2 = args[4];
                    var z1 = args[2];
                    var z2 = args[5];

                    var sum = CalculateSum(x1 - 1, x2 - 1, y1 - 1, y2 - 1, z1 - 1, z2 - 1);
                    Console.WriteLine(sum);
                }
            }

            private int CalculateSum(int x1, int x2, int y1, int y2, int z1, int z2)
            {
                var sum = 0;

                for (int i = x1; i <= x2; i++)
                {
                    for (int j = y1; j <= y2; j++)
                    {
                        for (int k = z1; k <= z2; k++)
                        {
                            sum += matrix[i][j][k];
                        }
                    }
                }

                return sum;
            }
        }
    }
}
