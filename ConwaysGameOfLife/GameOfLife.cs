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
                    gameBoard[y, x] = new Cell(check >= 0.5 && random);
                }
            }
        }

        public GameOfLife(int size, string[] liveCells)
        {
            BoardSetup(size, liveCells);
        }

        public GameOfLife(int size, string design)
        {
            string[] liveCells = Pattern(design);
            BoardSetup(size, liveCells);
        }

        private void BoardSetup(int size, string[] liveCells)
        {
            boardSize = size;
            gameBoard = new Cell[size, size];
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    string index = y.ToString() + "," + x.ToString();
                    gameBoard[y, x] = new Cell(liveCells.Contains(index));
                }
            }
        }

        public string[] Pattern(string design)
        {
            string check = design.ToLower();
            if (check == "pentadecathlon")
            {
                return new string[]
                {
                    "5,10", "6,10", "7,9", "7,11",
                    "8,10", "9,10", "10,10", "11,10",
                    "12,9", "12,11", "13,10", "14,10"
                };
            }
            if (check == "glider")
            {
                return new string[]
                {
                    "0,1", "1,2", "2,0", "2,1", "2,2"
                };
            }
            return new string[] { };
        }

        public List<List<bool>> ToList()
        {
            List<List<bool>> cellList = new List<List<bool>> { };
            for (int y = 0; y < boardSize; y++)
            {
                List<bool> row = new List<bool> { };
                for (int x = 0; x < boardSize; x++)
                {
                    row.Add(gameBoard[y, x].Living);
                }
                cellList.Add(row);
            }
            return cellList;
        }

        public void Tick()
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
            update();
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
                        if (testCell.LiveNeighbors < 2 || testCell.LiveNeighbors > 3) testCell.Living = false;
                    }
                    else
                    {
                        if (testCell.LiveNeighbors == 3) testCell.Living = true;
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
