using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConwaysGameOfLife
{
    public class GameOfLife : Board
    {
        private int boardSize;
        private Cell[,] gameBoard;

        public GameOfLife(int size, bool random)
        {
            Random rand = new Random();
            boardSize = size;
            gameBoard = new Cell[size, size];
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    double check = rand.NextDouble();
                    if (check >= 0.5 && random)
                    {
                        gameBoard[y, x] = new Cell(true);
                    }
                    else
                    {
                        gameBoard[y, x] = new Cell();
                    }
                }
            }
        }

        public GameOfLife(int size, string[] liveCells)
        {
            boardSize = size;
            gameBoard = new Cell[size, size];
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    string index = y.ToString() + "," + x.ToString();
                    if (liveCells.Contains(index))
                    {
                        gameBoard[y, x] = new Cell(true);
                    }
                    else
                    {
                        gameBoard[y, x] = new Cell();
                    }
                }
            }
        }

        public void Tick()
        {
            check();
            update();
        }

        public List<List<bool>> ToList()
        {
            List<List<bool>> table = new List<List<bool>> { };
            for (int y = 0; y < boardSize; y++)
            {
                List<bool> row = new List<bool> { };
                for (int x = 0; x < boardSize; x++)
                {
                    row.Add(gameBoard[y, x].Living);
                }
                table.Add(row);
            }
            return table;
        }

        public Cell cellAt(int y, int x)
        {
            return gameBoard[y, x];
        }

        public void switchState(int y, int x)
        {
            gameBoard[y, x].Living = !gameBoard[y, x].Living;
        }

        public void check()
        {
            for (int y = 0; y < boardSize; y++)
            {
                for (int x = 0; x < boardSize; x++)
                {
                    if (gameBoard[y,x].Living)
                    {
                        tellLiving(x, y);
                    }
                }
            }
        }

        public void update()
        {
            for (int y = 0; y < boardSize; y++)
            {
                for (int x = 0; x < boardSize; x++)
                {
                    Cell testCell = gameBoard[y, x];
                    if (testCell.Living)
                    {
                        if (testCell.LiveNeighbors < 2 || testCell.LiveNeighbors > 3) switchState(y, x);
                    }
                    else
                    {
                        if (testCell.LiveNeighbors == 3) switchState(y, x);
                    }
                    testCell.LiveNeighbors = 0;
                }
            }
        }

        private void tellLiving(int y, int x)
        {
            int x_p = x + 1;
            int y_p = y + 1;
            int x_m = x - 1;
            int y_m = y - 1;
            if (x_p > boardSize - 1) x_p = 0;
            if (y_p > boardSize - 1) y_p = 0;
            if (x_m < 0) x_m = boardSize - 1;
            if (y_m < 0) y_m = boardSize - 1;
            gameBoard[x_p, y_p].LiveNeighbors++;
            gameBoard[x_m, y_m].LiveNeighbors++;
            gameBoard[x_p, y_m].LiveNeighbors++;
            gameBoard[x_m, y_p].LiveNeighbors++;
            gameBoard[x_p, y].LiveNeighbors++;
            gameBoard[x_m, y].LiveNeighbors++;
            gameBoard[x, y_p].LiveNeighbors++;
            gameBoard[x, y_m].LiveNeighbors++;
        }
    }
}
