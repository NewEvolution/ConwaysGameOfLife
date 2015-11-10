using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConwaysGameOfLife
{
    public class GameOfLife : Board
    {
        private bool[,] currentBoard;
        private bool[,] modifiedBoard;
        private int boardSize;
        private bool isBounded;
        public GameOfLife(int size, bool bounded, List<List<int>> liveCells)
        {
            boardSize = size;
            isBounded = bounded;
            currentBoard = new bool[size, size];
            modifiedBoard = new bool[size,size];
            for (int i = 0; i < liveCells.Count; i++)
            {
                changeStatus(liveCells[i][0], liveCells[i][1]);
            }
        }

        public bool cellStatus(int x, int y)
        {
            return currentBoard[x, y];
        }

        public void changeStatus(int x, int y)
        {
            currentBoard[x, y] = !currentBoard[x, y];
        }

        private void changeModStatus(int x, int y)
        {
            modifiedBoard[x, y] = !modifiedBoard[x, y];
        }

        public int checkNeighbors(int x, int y) // bounded
        {
            int liveNeighbors = 0;
            if (insideArray(x + 1, y + 1) && currentBoard[x + 1, y + 1]) liveNeighbors++;
            if (insideArray(x + 1, y - 1) && currentBoard[x + 1, y - 1]) liveNeighbors++;
            if (insideArray(x - 1, y + 1) && currentBoard[x - 1, y + 1]) liveNeighbors++;
            if (insideArray(x - 1, y - 1) && currentBoard[x - 1, y - 1]) liveNeighbors++;
            if (insideArray(x, y + 1) && currentBoard[x, y + 1]) liveNeighbors++;
            if (insideArray(x, y - 1) && currentBoard[x, y - 1]) liveNeighbors++;
            if (insideArray(x + 1, y) && currentBoard[x + 1, y]) liveNeighbors++;
            if (insideArray(x - 1, y) && currentBoard[x - 1, y]) liveNeighbors++;
            return liveNeighbors;
        }

        public int checkNeighborsUnbounded(int x, int y)
        {
            int liveNeighbors = 0;
            int x_plus = x + 1;
            int x_minus = x - 1;
            int y_plus = y + 1;
            int y_minus = y - 1;
            if (x_plus > boardSize - 1) x_plus = 0;
            if (x_minus < 0) x_minus = boardSize - 1;
            if (y_plus > boardSize - 1) y_plus = 0;
            if (y_minus < 0) y_minus = boardSize - 1;
            if (currentBoard[x_plus, y_plus]) liveNeighbors++;
            if (currentBoard[x_plus, y_minus]) liveNeighbors++;
            if (currentBoard[x_minus, y_plus]) liveNeighbors++;
            if (currentBoard[x_minus, y_minus]) liveNeighbors++;
            if (currentBoard[x, y_plus]) liveNeighbors++;
            if (currentBoard[x, y_minus]) liveNeighbors++;
            if (currentBoard[x_plus, y]) liveNeighbors++;
            if (currentBoard[x_minus, y]) liveNeighbors++;
            return liveNeighbors;
        }

        public bool insideArray(int x, int y)
        {
            if (x < 0 || y < 0 || x > boardSize - 1 || y > boardSize - 1)
            {
                return false;
            }
            return true;
        }

        public void randomFill()
        {
            Random random = new Random();
            for (int x = 0; x < boardSize; x++)
            {
                for (int y = 0; y < boardSize; y++)
                {
                    double test = random.NextDouble();
                    if (test >= 0.5)
                    {
                        currentBoard[x, y] = true;
                    } 
                }
            }
        }

        public List<List<bool>> ToList()
        {
            List<List<bool>> list = new List<List<bool>> ();
            for (int x = 0; x < boardSize; x++)
            {
                List<bool> row = new List<bool> ();
                for (int y = 0; y < boardSize; y++)
                {
                    row.Add(currentBoard[x, y]);
                }
                list.Add(row);
            }
            return list;
        }

        public void Tick()
        {
            modifiedBoard = (bool[,])currentBoard.Clone();
            for (int x = 0; x < currentBoard.GetLength(0); x++)
            {
                for (int y = 0; y < currentBoard.GetLength(1); y++)
                {
                    int livingNeighbors;
                    if (isBounded)
                    {
                        livingNeighbors = checkNeighbors(x, y);
                    }
                    else
                    {
                        livingNeighbors = checkNeighborsUnbounded(x, y);
                    }
                    if (livingNeighbors < 2 && currentBoard[x, y]) changeModStatus(x, y);
                    if (livingNeighbors == 3 && !currentBoard[x, y]) changeModStatus(x, y);
                    if (livingNeighbors > 3 && currentBoard[x, y]) changeModStatus(x, y);
                }
            }
            currentBoard = (bool[,])modifiedBoard.Clone();
        }
    }
}
