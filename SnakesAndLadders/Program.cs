using System;
using System.Collections.Generic;
using System.IO;
class Solution
{
    static void Main(String[] args)
    {
        var t = Convert.ToInt32(Console.ReadLine());

        for (var a0 = 0; a0 < t; a0++)
        {
            var board = ConstructBoard();

            var numberOfLadders = Convert.ToInt32(Console.ReadLine());
            for (var l0 = 0; l0 < numberOfLadders; l0++)
            {
                var ladder = Console.ReadLine().Split(' ');
                var ladderSource = Convert.ToInt32(ladder[0]);
                var ladderTarget = Convert.ToInt32(ladder[1]);

                board[ladderSource].JumpToPosition = ladderTarget;
            }
            var numberOfSnakes = Convert.ToInt32(Console.ReadLine());
            for (var s0 = 0; s0 < numberOfSnakes; s0++)
            {
                var snake = Console.ReadLine().Split(' ');
                var snakeSource = Convert.ToInt32(snake[0]);
                var snakeTarget = Convert.ToInt32(snake[1]);

                board[snakeSource].JumpToPosition = snakeTarget;
                board[snakeSource].IsSnake = true;
            }

            ProcessBoard(board, 1);
        }
    }

    static void ProcessBoard(BoardPosition[] board, int startPosition)
    {
        var position = startPosition;
        
        while (position < 100)
        {
            var currentPosition = board[position];
            var nextPosition = board[position + 1];
            var nextThrowsAndDice = currentPosition.ThrowsAndDice.Advance();

            nextPosition.SetThrowsAndDice(nextThrowsAndDice);

            if (currentPosition.JumpToPosition.HasValue)
            {
                var targetPosition = board[currentPosition.JumpToPosition.Value];
                var isUpdated = targetPosition.SetThrowsAndDice(currentPosition.ThrowsAndDice.Reset());

                if (currentPosition.IsSnake)
                {
                    if (isUpdated)
                    {
                        position = currentPosition.JumpToPosition.Value;
                        continue;
                    }
                    else if(currentPosition.ThrowsAndDice.DiceEyes == 6)
                    {
                        var nextSetPosition = FindNextNoSnakeSetPosition(board, position + 1);
                        if (nextSetPosition == -1)
                        {
                            if (board[100].ThrowsAndDice == null)
                            {
                                board[100].ThrowsAndDice = new ThrowsAndDice();
                            }
                            board[100].ThrowsAndDice.Throws = -1;
                            break;
                        }
                        else
                        {
                            position = nextSetPosition;
                            continue;
                        }
                    }
                }
            }

            position++;
        }

        Console.WriteLine(board[100].ThrowsAndDice.Throws);
    }

    private static int FindNextNoSnakeSetPosition(BoardPosition[] board, int position)
    {
        for(var i = position; i < board.Length; i++)
        {
            if (board[i].IsVisited && !board[i].IsSnake) return i;
        }

        return -1;
    }
    
    private static BoardPosition[] ConstructBoard()
    {
        var board = new BoardPosition[101];

        for(var i = 1; i < board.Length; i++)
        {
            board[i] = new BoardPosition();
        }

        board[1].ThrowsAndDice = new ThrowsAndDice();
        board[1].IsVisited = true;

        return board;
    }

    private class BoardPosition
    {
        public bool IsVisited = false;
        public ThrowsAndDice ThrowsAndDice;
        public int? JumpToPosition;
        public bool IsSnake = false;

        public bool SetThrowsAndDice(ThrowsAndDice newThrowsAndDice) {
            if (!IsVisited)
            {
                ThrowsAndDice = newThrowsAndDice;
                IsVisited = true;
                return true;
            }
            else
            {
                return ThrowsAndDice.UpdateIfBetter(newThrowsAndDice);
            }
        }
    }

    private class ThrowsAndDice
    {
        public int Throws = 0;
        public int DiceEyes = 0;

        public ThrowsAndDice Advance()
        {
            var newThrows = Throws;
            if (DiceEyes == 0) newThrows++;
            var newDiceEyes = DiceEyes + 1;
            if(newDiceEyes == 7)
            {
                newDiceEyes = 1;
                newThrows++;
            }

            return new ThrowsAndDice() { Throws = newThrows, DiceEyes = newDiceEyes };
        }

        public ThrowsAndDice Reset() {
            return new ThrowsAndDice() { Throws = Throws, DiceEyes = 0 };
        }

        public bool UpdateIfBetter(ThrowsAndDice otherThrowsAndDice)
        {
            var thisThrows = this.DiceEyes == 0 ? this.Throws + 1 : this.Throws;
            var otherThrows = otherThrowsAndDice.DiceEyes == 0 ? otherThrowsAndDice.Throws + 1 : otherThrowsAndDice.Throws;
            
            if (thisThrows > otherThrows)
            {
                this.Throws = otherThrowsAndDice.Throws;
                this.DiceEyes = otherThrowsAndDice.DiceEyes;
                return true;
            }
            else if (thisThrows == otherThrows && this.DiceEyes > otherThrowsAndDice.DiceEyes && otherThrowsAndDice.DiceEyes > 0)
            {
                this.DiceEyes = otherThrowsAndDice.DiceEyes;
                return true;
            }

            return false;
        }
    }
}