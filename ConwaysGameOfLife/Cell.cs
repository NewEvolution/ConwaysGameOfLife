using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConwaysGameOfLife
{
    public class Cell
    {
        public int Y { get; set; }
        public int X { get; set; }
        public bool Living { get; set; }
        public int LiveNeighbors { get; set; }

        public Cell(bool living)
        {
            Living = living;
        }
    }
}
