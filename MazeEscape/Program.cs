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
            private bool[,] mazeVisited = new bool[60, 60];
            private int pX = 30, pY = 30;
            private int d = 1;
            private int eX, eY;

            private PathFinder pathFinder;
            private List<PointAndDirection> currentPath = null;

            public GameState()
            {
                pathFinder = new PathFinder(maze, mazeVisited);
            }

            public void Move(Move move)
            {
                ApplyMove(move);

                if(maze[pY, pX] == 'e')
                {
                    IsDone = true;
                    return;
                }

                if(currentPath == null || currentPath.Count == 0)
                {
                    currentPath = pathFinder.FindPath(new Point(pY, pX));
                }

                var p = currentPath.First();
                d = p.direction;
                switch (d)
                {
                    case 1:
                        Console.WriteLine("UP");
                        break;
                    case 2:
                        Console.WriteLine("RIGHT");
                        break;
                    case 3:
                        Console.WriteLine("DOWN");
                        break;
                    case 4:
                        Console.WriteLine("LEFT");
                        break;
                }
                currentPath.Remove(p);
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

        private struct Point
        {
            public Point(int x, int y)
            {
                this.x = x;
                this.y = y;
            }

            public PointAndDirection Up()
            {
                return new PointAndDirection(new Point(this.x, this.y - 1), 1);
            }

            public PointAndDirection Right()
            {
                return new PointAndDirection(new Point(this.x + 1, this.y), 2);
            }

            public PointAndDirection Down()
            {
                return new PointAndDirection(new Point(this.x, this.y + 1), 3);
            }

            public PointAndDirection Left()
            {
                return new PointAndDirection(new Point(this.x - 1, this.y), 4);
            }

            public int x;
            public int y;
        }

        private struct PointAndDirection
        {
            public PointAndDirection(Point point, int direction)
            {
                this.point = point;
                this.direction = direction;
            }

            public Point point;
            public int direction;
        }

        private class PathFinder {

            public PathFinder(char[,] maze, bool[,] mazeVisits)
            {
                this.maze = maze;
                this.mazeVisits = mazeVisits;
            }

            private Queue<List<PointAndDirection>> paths = new Queue<List<PointAndDirection>>();
            private char[,] maze = new char[60, 60];
            private bool[,] mazeVisits = new bool[60, 60];
            private bool[,] mazeEnqueued;

            public List<PointAndDirection> FindPath(Point currentPoint)
            {
                paths.Clear();

                mazeEnqueued = new bool[60, 60];
                mazeEnqueued[currentPoint.y, currentPoint.x] = true;

                EnqueueInAllDirections(new List<PointAndDirection>());

                return FindShortestPath();
            }

            private List<PointAndDirection> FindShortestPath()
            {
                while(paths.Count > 0)
                {
                    var cp = paths.Dequeue();
                    var p = cp.Last().point;

                    if (IsTargetPoint(p)) return cp;
                    else if (IsValidPoint(p)) {
                        EnqueueInAllDirections(cp);
                    }
                }

                return null;
            }

            private void EnqueueInAllDirections(List<PointAndDirection> path)
            {
                var currentPoint = path.Last().point;

                var up = currentPoint.Up();
                var right = currentPoint.Right();
                var down = currentPoint.Down();
                var left = currentPoint.Left();

                if (IsEnqueuable(up.point)) paths.Enqueue(CloneListAndAddElement(path, up));
                if (IsEnqueuable(right.point)) paths.Enqueue(CloneListAndAddElement(path, right));
                if (IsEnqueuable(down.point)) paths.Enqueue(CloneListAndAddElement(path, down));
                if (IsEnqueuable(left.point)) paths.Enqueue(CloneListAndAddElement(path, left));
            }

            private List<PointAndDirection> CloneListAndAddElement(List<PointAndDirection> list, PointAndDirection el)
            {
                var result = list.Select(e => e).ToList();
                result.Add(el);
                mazeEnqueued[el.point.y, el.point.x] = true;
                return result;
            }

            private bool IsEnqueuable(Point p)
            {
                return !mazeEnqueued[p.y - 1, p.x] && (maze[p.y, p.x] == '-' || maze[p.y, p.x] == 'e');
            }

            private bool IsTargetPoint(Point p)
            {
                return maze[p.y, p.x] != '#' && !mazeVisits[p.y, p.x];
            }

            private bool IsValidPoint(Point p)
            {
                return maze[p.y, p.x] != '#' && mazeVisits[p.y, p.x];
            }
        }
    }
}
