using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CubeSummation
{
    class Solution
    {
        static void Main(string[] args)
        {
            var t = Convert.ToInt32(Console.ReadLine());

            var a = new Algorithm();

            //var fs = new FileStream("output.txt", FileMode.Create);
            //var sw = new StreamWriter(fs);
            //Console.SetOut(sw);

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

            //sw.Flush();
        }

        private class Algorithm {

            private Slot[][][] matrix;

            public void Setup(int n)
            {
                matrix = new Slot[n][][];

                for (int i = 0; i < n; i++)
                {
                    matrix[i] = new Slot[n][];

                    for (int j = 0; j < n; j++)
                    {
                        matrix[i][j] = new Slot[n];

                        for (int k = 0; k < n; k++)
                        {
                            matrix[i][j][k] = new Slot();
                        }
                    }
                }
            }

            public void Process(string command, int[] args)
            {
                if (command.Equals("UPDATE"))
                {
                    var slotArray = matrix[args[0] - 1][args[1] - 1];
                    var slot = slotArray[args[2] - 1];
                    var diff = slot.Value - args[3];
                    slot.Value -= diff;
                    slot.Sum -= diff;
                    for (int i = args[2]; i < matrix.Length; i++)
                    {
                        var nextSlot = slotArray[i];
                        nextSlot.Sum -= diff;
                    }
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

            private long CalculateSum(int x1, int x2, int y1, int y2, int z1, int z2)
            {
                var sum = 0L;

                for (int i = x1; i <= x2; i++)
                {
                    for (int j = y1; j <= y2; j++)
                    {
                        var result = matrix[i][j][z2].Sum;

                        if(z1 > 0)
                        {
                            result -= matrix[i][j][z1 - 1].Sum;
                        }

                        sum += result;
                    }
                }

                return sum;
            }

            private class Slot {
                public long Value;
                public long Sum;
            }
        }
    }
}
