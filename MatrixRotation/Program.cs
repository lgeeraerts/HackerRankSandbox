using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixRotation
{
    class Solution
    {
        static void Main(string[] args)
        {
            var line = Console.ReadLine();
            var segments = line.Split(' ');

            var Rows = Convert.ToInt32(segments[0]);
            var Columns = Convert.ToInt32(segments[1]);
            var rotations = Convert.ToInt32(segments[2]);

            var matrix = new int[Rows][];
            for (int i = 0; i < Rows; i++)
            {
                line = Console.ReadLine();
                segments = line.Split(' ');
                matrix[i] = new int[segments.Length];

                for (int t = 0; t < segments.Length; t++)
                {
                    matrix[i][t] = Convert.ToInt32(segments[t]);
                }
            }

            var algorithm = new Algorithm();

            algorithm.Process(matrix, rotations);
        }

        public class Algorithm
        {
            public void Process(int[][] matrix, int rotations)
            {
                var rows = matrix.Length;
                var columns = matrix[0].Length;
                var resultMatrix = BuildResultMatrix(columns, rows);

                var cycles = Math.Min(rows, columns) / 2;

                var topLeft = new Point(0, 0);
                var bottomRight = new Point(columns - 1, rows - 1);

                for (int i = 0; i < cycles; i++)
                {
                    RotateRectangleInMatrix(matrix, resultMatrix, topLeft, bottomRight, rotations);

                    topLeft.Add(1, 1);
                    bottomRight.Add(-1, -1);
                }

                PrintMatrix(resultMatrix);
            }

            private void RotateRectangleInMatrix(int[][] matrix, int[][] resultMatrix, Point topLeft, Point bottomRight, int rotations)
            {
                var width = bottomRight.x - topLeft.x + 1;
                var height = bottomRight.y - topLeft.y + 1;
                int totalPositions = 2 * width + 2 * (height - 2);

                var currentPoint = new Point(topLeft.x, topLeft.y);

                for (int i = 0; i < totalPositions; i++)
                {
                    var targetPoint = CalculateTargetPoint(currentPoint, rotations, topLeft, bottomRight);

                    resultMatrix[targetPoint.y][targetPoint.x] = matrix[currentPoint.y][currentPoint.x];

                    if(currentPoint.x == topLeft.x && currentPoint.y < bottomRight.y) {
                        // move down
                        currentPoint.y++;
                    }else if(currentPoint.y == bottomRight.y && currentPoint.x < bottomRight.x)
                    {
                        // move right
                        currentPoint.x++;
                    }else if(currentPoint.x == bottomRight.x && currentPoint.y > topLeft.y)
                    {
                        // move up
                        currentPoint.y--;
                    }
                    else
                    {
                        // move left
                        currentPoint.x--;
                    }
                }

                var adjustedRotations = rotations % totalPositions;
            }

            private Point CalculateTargetPoint(Point currentPoint, int rotations, Point topLeft, Point bottomRight)
            {
                var targetPoint = new Point(currentPoint.x, currentPoint.y);
                int remainingRotations = rotations;
                var maxMoves = 0;
                var moves = 0;

                while(remainingRotations > 0)
                {
                    if (targetPoint.x == topLeft.x && targetPoint.y < bottomRight.y)
                    {
                        // move down
                        maxMoves = bottomRight.y - targetPoint.y;
                        moves = Math.Min(maxMoves, remainingRotations);
                        targetPoint.y += moves;
                        remainingRotations -= moves;
                    }
                    else if (targetPoint.y == bottomRight.y && targetPoint.x < bottomRight.x)
                    {
                        // move right
                        maxMoves = bottomRight.x - targetPoint.x;
                        moves = Math.Min(maxMoves, remainingRotations);
                        targetPoint.x += moves;
                        remainingRotations -= moves;
                    }
                    else if (targetPoint.x == bottomRight.x && targetPoint.y > topLeft.y)
                    {
                        // move up
                        maxMoves = targetPoint.y - topLeft.y;
                        moves = Math.Min(maxMoves, remainingRotations);
                        targetPoint.y -= moves;
                        remainingRotations -= moves;
                    }
                    else
                    {
                        // move left
                        maxMoves = targetPoint.x - topLeft.x;
                        moves = Math.Min(maxMoves, remainingRotations);
                        targetPoint.x -= moves;
                        remainingRotations -= moves;
                    }
                }

                return targetPoint;
            }

            private int[][] BuildResultMatrix(int columns, int rows)
            {
                var resultMatrix = new int[rows][];

                for (int i = 0; i < rows; i++)
                {
                    resultMatrix[i] = new int[columns];
                }

                return resultMatrix;
            }

            private void PrintMatrix(int[][] matrix)
            {
                var columns = matrix[0].Length;

                for (int i = 0; i < matrix.Length; i++)
                {
                    for (int t = 0; t < columns - 1; t++)
                    {
                        Console.Write(matrix[i][t] + " ");
                    }

                    Console.WriteLine(matrix[i][columns - 1]);
                }
            }

            private class Point
            {
                public Point(int x, int y)
                {
                    this.x = x;
                    this.y = y;
                }

                public int x;
                public int y;

                public void Add(int x, int y)
                {
                    this.x += x;
                    this.y += y;
                }
            }
        }
    }
}
