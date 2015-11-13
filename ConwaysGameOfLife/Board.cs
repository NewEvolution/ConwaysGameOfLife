using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConwaysGameOfLife
{
    public interface Board
    {
        ObservableCollection<ObservableCollection<bool>> ToList();
        void Tick();
    }
}
