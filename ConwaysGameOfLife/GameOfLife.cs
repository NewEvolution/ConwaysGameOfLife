using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConwaysGameOfLife
{
    public class GameOfLife : Board
    {
        private Cell[,] gameBoard;
        private int height;
        private int width;
        private int[] stayAlive;
        private int[] born;
        private List<Cell> activeCells = new List<Cell> { };

        // Constructor with just height, width & algorithm rules creates random board
        public GameOfLife(int height, int width, string rules)
        {
            RulesParser(rules);
            Random rand = new Random();
            this.height = height;
            this.width = width;
            gameBoard = new Cell[height, width];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    double check = rand.NextDouble();
                    bool cellAlive = check >= 0.5;
                    gameBoard[y, x] = new Cell(cellAlive) { Y = y, X = x };
                    if (cellAlive) activeCells.Add(gameBoard[y, x]);
                }
            }
        }

        // Constructor with height, width & an array of CSV strings creates board with those coords live
        public GameOfLife(int height, int width, string rules, string[] liveCells)
        {
            BoardSetup(height, width, rules, liveCells);
        }

        // Constructor with height, width & string value creates board with that design, or empty if design does not exist
        public GameOfLife(int height, int width, string rules, string design)
        {
            string[] liveCells = Pattern(design);
            BoardSetup(height, width, rules, liveCells);
        }

        private void BoardSetup(int height, int width, string rules, string[] liveCells)
        {
            RulesParser(rules);
            this.height = height;
            this.width = width;
            gameBoard = new Cell[height, width];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    string index = y.ToString() + "," + x.ToString();
                    bool cellAlive = liveCells.Contains(index);
                    gameBoard[y, x] = new Cell(cellAlive) { Y = y, X = x };
                    if (cellAlive) activeCells.Add(gameBoard[y, x]);
                }
            }
        }

        private void RulesParser(string rules)
        {
            string[] splitAt = new string[] { "B", "/S" };
            string[] splitRules = rules.Split(splitAt, StringSplitOptions.None);
            char[] bornRules = splitRules[1].ToCharArray();
            char[] stayRules = splitRules[2].ToCharArray();
            int[] bornInts = new int[bornRules.Length];
            int[] stayInts = new int[stayRules.Length];
            for (int i = 0; i < bornRules.Length; i++)
            {
                bornInts[i] = int.Parse(bornRules[i].ToString());
            }
            for (int i = 0; i < stayRules.Length; i++)
            {
                stayInts[i] = int.Parse(stayRules[i].ToString());
            }
            stayAlive = stayInts;
            born = bornInts;
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
            List<Cell> liveCells = new List<Cell>(activeCells);
            foreach (Cell cell in liveCells)
            {
                TellLiving(cell.Y, cell.X);
            }
            Update();
        }

        public void Update()
        {
            List<Cell> toAdd = new List<Cell> { };
            List<Cell> toRemove = new List<Cell> { };
            foreach (Cell testCell in activeCells)
            {
                if (testCell.Living && stayAlive.Contains(testCell.LiveNeighbors)) { }
                else
                {
                    testCell.Living = false;
                    if (!toRemove.Contains(testCell)) toRemove.Add(testCell);
                }
                if (!testCell.Living && born.Contains(testCell.LiveNeighbors))
                {
                    testCell.Living = true;
                    if (!toAdd.Contains(testCell)) toAdd.Add(testCell);
                }
                testCell.LiveNeighbors = 0;
            }
            foreach (Cell killed in toRemove)
            {
                activeCells.Remove(killed);
            }
            foreach (Cell born in toAdd)
            {
                activeCells.Add(born);
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
            int[][] cases = new int[][]
            { 
                new int[] {y_p, x_p }, new int[] { y_m, x_m },
                new int[] { y_p, x_m }, new int[] { y_m, x_p },
                new int[] { y_p, x }, new int[] { y_m, x },
                new int[] { y, x_p }, new int[] { y, x_m }
            };
            foreach (int[] pair in cases)
            {
                Cell targetCell = gameBoard[pair[0], pair[1]];
                targetCell.LiveNeighbors++;
                if (!activeCells.Contains(targetCell)) activeCells.Add(targetCell);
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
                    "6,8", "6,9", "6,10", "6,14", "6,15", "6,16",
                    "8,6", "8,11", "8,13", "8,18",
                    "9,6", "9,11", "9,13", "9,18",
                    "10,6", "10,11", "10,13", "10,18",
                    "11,8", "11,9", "11,10", "11,14", "11,15", "11,16",
                    "13,8", "13,9", "13,10", "13,14", "13,15", "13,16",
                    "14,6", "14,11", "14,13", "14,18",
                    "15,6", "15,11", "15,13", "15,18",
                    "16,6", "16,11", "16,13", "16,18",
                    "18,8", "18,9", "18,10", "18,14", "18,15", "18,16",
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
            if (check == "seeds expander")
            {
                return new string[] 
                {
                    "31,35", "32,35", "33,35", "34,35",
                    "36,35", "37,35", "38,35", "39,35"
                };
            }
            return new string[] { };
        }
    }
}
