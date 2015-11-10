using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConwaysGameOfLife
{
    public class Cell
    {
        public bool Living { get; set; }
        public Cell()
        {
            Living = false;
        }

        public void switchState()
        {
            Living = !Living;
        }
    }
}
