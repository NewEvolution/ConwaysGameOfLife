using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConwaysGameOfLife
{
    public class GameOfLife : Board
    {
        private ObservableCollection<ObservableCollection<bool>> cellList = new ObservableCollection<ObservableCollection<bool>> { };
        private Cell[,] gameBoard;
        private int height;
        private int width;
        private int[] born;
        private int[] stayAlive;
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
                ObservableCollection<bool> row = new ObservableCollection<bool> { };
                for (int x = 0; x < width; x++)
                {
                    double check = rand.NextDouble();
                    bool cellAlive = check >= 0.5;
                    gameBoard[y, x] = new Cell(cellAlive) { Y = y, X = x };
                    if (cellAlive) activeCells.Add(gameBoard[y, x]);
                    row.Add(cellAlive);
                }
                cellList.Add(row);
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
                ObservableCollection<bool> row = new ObservableCollection<bool> { };
                for (int x = 0; x < width; x++)
                {
                    string index = y.ToString() + "," + x.ToString();
                    bool cellAlive = liveCells.Contains(index);
                    gameBoard[y, x] = new Cell(cellAlive) { Y = y, X = x };
                    if (cellAlive) activeCells.Add(gameBoard[y, x]);
                    row.Add(cellAlive);
                }
                cellList.Add(row);
            }
        }

        private void RulesParser(string rules)
        {
            string[] splitRules = rules.Split('/');
            char[] stayRules = splitRules[0].ToCharArray();
            char[] bornRules = splitRules[1].ToCharArray();
            int[] stayInts = new int[stayRules.Length];
            int[] bornInts = new int[bornRules.Length];
            for (int i = 0; i < stayRules.Length; i++)
            {
                stayInts[i] = int.Parse(stayRules[i].ToString());
            }
            for (int i = 0; i < bornRules.Length; i++)
            {
                bornInts[i] = int.Parse(bornRules[i].ToString());
            }
            born = bornInts;
            stayAlive = stayInts;
        }

        public ObservableCollection<ObservableCollection<bool>> ToList()
        {
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
            if ((stayAlive.Contains(0) || born.Contains(0)) && activeCells.Count != gameBoard.Length)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        if (!activeCells.Contains(gameBoard[y, x])) activeCells.Add(gameBoard[y, x]);
                    }
                }
            }
            List<Cell> toAdd = new List<Cell> { };
            List<Cell> toRemove = new List<Cell> { };
            foreach (Cell testCell in activeCells)
            {
                if (testCell.Living && stayAlive.Contains(testCell.LiveNeighbors)) { }
                else if (testCell.Living)
                {
                    testCell.Living = false;
                    cellList[testCell.Y][testCell.X] = false;
                    if (!toRemove.Contains(testCell)) toRemove.Add(testCell);
                }
                if (!testCell.Living && born.Contains(testCell.LiveNeighbors))
                {
                    testCell.Living = true;
                    cellList[testCell.Y][testCell.X] = true;
                    if (!toAdd.Contains(testCell)) toAdd.Add(testCell);
                }
                if (!testCell.Living && !toRemove.Contains(testCell)) toRemove.Add(testCell); 
                testCell.LiveNeighbors = 0;
            }
            foreach (Cell killed in toRemove)
            {
                activeCells.Remove(killed);
            }
            foreach (Cell born in toAdd)
            {
                if(!activeCells.Contains(born)) activeCells.Add(born);
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
                    "31,35",
                    "32,35",
                    "33,34", "33,36",
                    "34,35",
                    "35,35",
                    "36,35",
                    "37,35",
                    "38,34", "38,36",
                    "39,35",
                    "40,35"
                };
            }
            if (check == "double pentadecathlon")
            {
                return new string[]
                {
                    "26,35", "26,36",
                    "27,35", "27,36",
                    "28,35", "28,36",
                    "29,35", "29,36",
                    "30,33", "30,34", "30,37", "30,38",
                    "31,33", "31,34", "31,37", "31,38",
                    "32,35", "32,36",
                    "33,35", "33,36",
                    "34,35", "34,36",
                    "35,35", "35,36",
                    "36,35", "36,36",
                    "37,35", "37,36",
                    "38,35", "38,36",
                    "39,35", "39,36",
                    "40,33", "40,34", "40,37", "40,38",
                    "41,33", "41,34", "41,37", "41,38",
                    "42,35", "42,36",
                    "43,35", "43,36",
                    "44,35", "44,36",
                    "45,35", "45,36"
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
                    "29,31", "29,32", "29,33", "29,37", "29,38", "29,39",
                    "31,29", "31,34", "31,36", "31,41",
                    "32,29", "32,34", "32,36", "32,41",
                    "33,29", "33,34", "33,36", "33,41",
                    "34,31", "34,32", "34,33", "34,37", "34,38", "34,39",
                    "36,31", "36,32", "36,33", "36,37", "36,38", "36,39",
                    "37,29", "37,34", "37,36", "37,41",
                    "38,29", "38,34", "38,36", "38,41",
                    "39,29", "39,34", "39,36", "39,41",
                    "41,31", "41,32", "41,33", "41,37", "41,38", "41,39"
                };
            }
            if (check == "r-pentomino")
            {
                return new string[]
                {
                    "34,35", "34,36",
                    "35,34", "35,35",
                    "36,35"
                };
            }
            if (check == "minimal engine")
            {
                return new string[]
                {
                    "33,38",
                    "34,36", "34,38", "34,39",
                    "35,36", "35,38",
                    "36,36",
                    "37,34",
                    "38,32", "38,34"
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
                    "35,21", "35,22", "35,23", "35,24", "35,25", "35,26", "35,27", "35,28",
                    "35,30", "35,31", "35,32", "35,33", "35,34",
                    "35,38", "35,39", "35,40",
                    "35,47", "35,48", "35,49", "35,50", "35,51", "35,52", "35,53",
                    "35,55", "35,56", "35,57", "35,58", "35,59"
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
            if (check == "center cross")
            {
                return new string[]
                {
                    "34,35",
                    "35,34", "35,36",
                    "36,35"
                };
            }
            return new string[] { };
        }
    }
}
