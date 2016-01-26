using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeEscape
{
    class Solution
    {
        static void Main(string[] args)
        {
            var p1 = new GameState();
            var p2 = new GameState();

            p1.Init(new Move());
            p2.Init(new Move());

            while (!p1.IsDone && !p2.IsDone)
            {
                var move = new Move();
                var s = move.player == 1 ? p1 : p2;

                s.Move(move);
            }
        }

        public class GameState
        {
            public bool IsDone = false;

            private char[,] maze = new char[60, 60];
            private int pX = 30, pY = 30;
            private int d = 1;

            public void Move(Move move)
            {
                ApplyMove(move);


            }

            public void Init(Move move)
            {
                var left = pX - 1;
                var top = pY - 1;

                var view = move.view;

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        maze[top + j, left + i] = view[j][i];
                    }
                }
            }

            private void ApplyMove(Move move)
            {
                switch (d)
                {
                    case 1:
                        pY--;
                        maze[pY - 1, pX - 1] = move.view[0][0];
                        maze[pY - 1, pX] = move.view[0][1];
                        maze[pY - 1, pX + 1] = move.view[0][2];
                        break;
                    case 2:
                        pX++;
                        maze[pY - 1, pX + 1] = move.view[0][0];
                        maze[pY, pX + 1] = move.view[0][1];
                        maze[pY + 1, pX + 1] = move.view[0][2];
                        break;
                    case 3:
                        pY++;
                        maze[pY + 1, pX - 1] = move.view[0][2];
                        maze[pY + 1, pX] = move.view[0][1];
                        maze[pY + 1, pX + 1] = move.view[0][0];
                        break;
                    case 4:
                        pX--;
                        maze[pY - 1, pX - 1] = move.view[0][2];
                        maze[pY, pX - 1] = move.view[0][1];
                        maze[pY + 1, pX - 1] = move.view[0][1];
                        break;
                }
            }
        }

        public class Move {

            public Move()
            {
                player = Convert.ToInt32(Console.ReadLine());
                var top = Console.ReadLine();
                var mid = Console.ReadLine();
                var bot = Console.ReadLine();

                view = new char[][] { top.ToCharArray(), mid.ToCharArray(), bot.ToCharArray() };
            }

            public int player;
            public char[][] view;
        }
    }
}
