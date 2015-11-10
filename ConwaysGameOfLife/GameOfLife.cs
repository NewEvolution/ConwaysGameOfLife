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
        public GameOfLife(int size)
        {
            boardSize = size;
            gameBoard = new Cell[size, size];
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    gameBoard[y, x] = new Cell();
                }
            }
        }

        public void Tick()
        {
            throw new NotImplementedException();
        }

        public List<List<bool>> ToList()
        {
            throw new NotImplementedException();
        }

        public Cell cellAt(int y, int x)
        {
            return gameBoard[y, x];
        }
    }
}
