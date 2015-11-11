﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConwaysGameOfLife
{
    public class GameOfLife : Board
    {
        private Cell[,] gameBoard;
        private int height;
        private int width;
        public GameOfLife(int height, int width)
        {
            Random rand = new Random();
            this.height = height;
            this.width = width;
            gameBoard = new Cell[height, width];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    double check = rand.NextDouble();
                    gameBoard[y, x] = new Cell(check >= 0.5);
                }
            }
        }

        public GameOfLife(int height, int width, string[] liveCells)
        {
            BoardSetup(height, width, liveCells);
        }

        public GameOfLife(int height, int width, string design)
        {
            string[] liveCells = Pattern(design);
            BoardSetup(height, width, liveCells);
        }

        private void BoardSetup(int height, int width, string[] liveCells)
        {
            this.height = height;
            this.width = width;
            gameBoard = new Cell[height, width];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
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
                    "5,10",
                    "6,10",
                    "7,9", "7,11",
                    "8,10",
                    "9,10",
                    "10,10",
                    "11,10",
                    "12,9", "12,11",
                    "13,10",
                    "14,10"
                };
            }
            if (check == "glider")
            {
                return new string[]
                {
                    "0,1",
                    "1,2",
                    "2,0", "2,1", "2,2"
                };
            }
            if (check == "lwss")
            {
                return new string[]
                {
                    "10,0", "10,3",
                    "11,4",
                    "12,0", "12,4",
                    "13,1", "13,2", "13,3", "13,4"
                };
            }
            if (check == "pulsar")
            {
                return new string[]
                {
                    "3,5", "3,6", "3,7", "3,11", "3,12", "3,13",
                    "5,3", "5,8", "5,10", "5,15",
                    "6,3", "6,8", "6,10", "6,15",
                    "7,3", "7,8", "7,10", "7,15",
                    "8,5", "8,6", "8,7", "8,11", "8,12", "8,13",
                    "10,5", "10,6", "10,7", "10,11", "10,12", "10,13",
                    "11,3", "11,8", "11,10", "11,15",
                    "12,3", "12,8", "12,10", "12,15",
                    "13,3", "13,8", "13,10", "13,15",
                    "15,5", "15,6", "15,7", "15,11", "15,12", "15,13",
                };
            }
            if (check == "r-pentomino")
            {
                return new string[]
                {
                    "11,11", "11,12",
                    "12,10", "12,11",
                    "13,11"
                };
            }
            if (check == "minimal engine")
            {
                return new string[]
                {
                    "10,15",
                    "11,13", "11,15", "11,16",
                    "12,13", "12,15",
                    "13,13",
                    "14,11",
                    "15,9", "15,11"
                };
            }
            if (check == "gosper")
            {
                return new string[]
                {
                    "1,25",
                    "2,23", "2,25",
                    "3,13", "3,14", "3,21", "3,22", "3,35", "3,36",
                    "4,12", "4,16", "4,21", "4,22", "4,35", "4,36",
                    "5,1", "5,2", "5,11", "5,17", "5,21", "5,22",
                    "6,1", "6,2", "6,11", "6,15", "6,17", "6,18", "6,23", "6,25",
                    "7,11", "7,17", "7,25",
                    "8,12", "8,16",
                    "9,13", "9,14"
                };
            }
            if (check == "line engine")
            {
                return new string[]
                {
                    "24,4", "24,5", "24,6", "24,7", "24,8", "24,9", "24,10", "24,11",
                    "24,13", "24,14", "24,15", "24,16", "24,17",
                    "24,21", "24,22", "24,23",
                    "24,30", "24,31", "24,32", "24,33", "24,34", "24,35", "24,36",
                    "24,38", "24,39", "24,40", "24,41", "24,42",
                };
            }
            return new string[] { };
        }

        public List<List<bool>> ToList()
        {
            List<List<bool>> cellList = new List<List<bool>> { };
            for (int y = 0; y < height; y++)
            {
                List<bool> row = new List<bool> { };
                for (int x = 0; x < width; x++)
                {
                    row.Add(gameBoard[y, x].Living);
                }
                cellList.Add(row);
            }
            return cellList;
        }

        public void Tick()
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (gameBoard[y,x].Living)
                    {
                        TellLiving(y, x);
                    }
                }
            }
            Update();
        }

        public void Update()
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
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

        private void TellLiving(int y, int x)
        {
            int y_p = y + 1;
            int x_p = x + 1;
            int y_m = y - 1;
            int x_m = x - 1;
            if (y_p > height - 1) y_p = 0;
            if (x_p > width - 1) x_p = 0;
            if (y_m < 0) y_m = height - 1;
            if (x_m < 0) x_m = width - 1;
            gameBoard[y_p, x_p].LiveNeighbors++;
            gameBoard[y_m, x_m].LiveNeighbors++;
            gameBoard[y_p, x_m].LiveNeighbors++;
            gameBoard[y_m, x_p].LiveNeighbors++;
            gameBoard[y_p, x].LiveNeighbors++;
            gameBoard[y_m, x].LiveNeighbors++;
            gameBoard[y, x_p].LiveNeighbors++;
            gameBoard[y, x_m].LiveNeighbors++;
        }
    }
}
