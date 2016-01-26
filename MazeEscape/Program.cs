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

            var p1InitView = new Move();
            var p2InitView = new Move();

            while(! p1.IsDone && !p2.IsDone)
            {
                var move = new Move();
                var s = move.player == 1 ? p1 : p2;

                s.ApplyMove(move);
            }
        }

        public class GameState
        {
            public bool IsDone = false;

            private char[,] maze = new char[60, 60];
            private int pX = 30, pY = 30;
            private int d = 1;

            public void ApplyMove(Move move)
            {

            }

            public void ApplyView(char[][] view)
            {
                var left = pX - 1;
                var top = pY - 1;

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        maze[top + j, left + i] = view[j][i];
                    }
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

            public void RotateIfNeeded(int direction)
            {
                if(direction == 2)
                {
                    view = 
                }
                else if(direction == 3)
                {
                    
                }
                else if(direction == 4)
                {

                }
            }

            public int player;
            public char[][] view;
        }
    }
}
