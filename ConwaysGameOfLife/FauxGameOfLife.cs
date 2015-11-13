using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConwaysGameOfLife
{
    public class FauxGameOfLife : Board
    {
        private ObservableCollection<ObservableCollection<bool>> cells = new ObservableCollection<ObservableCollection<bool>>();
        private Dictionary<char, ObservableCollection<ObservableCollection<bool>>> font = new Dictionary<char, ObservableCollection<ObservableCollection<bool>>>();
        private string phrase = "HELLO WORLD ";
        private int index = 0;

        public FauxGameOfLife()
        {
            ObservableCollection<ObservableCollection<bool>> letter = new ObservableCollection<ObservableCollection<bool>>();
            letter.Add(new ObservableCollection<bool>(new bool[] { true, true, true, true, true, true }));
            letter.Add(new ObservableCollection<bool>(new bool[] { true, false, true, true, false, true }));
            letter.Add(new ObservableCollection<bool>(new bool[] { true, false, true, true, false, true }));
            letter.Add(new ObservableCollection<bool>(new bool[] { true, false, false, false, false, true }));
            letter.Add(new ObservableCollection<bool>(new bool[] { true, false, true, true, false, true }));
            letter.Add(new ObservableCollection<bool>(new bool[] { true, false, true, true, false, true }));
            letter.Add(new ObservableCollection<bool>(new bool[] { true, true, true, true, true, true }));
            font['H'] = letter;

            letter = new ObservableCollection<ObservableCollection<bool>>();
            letter.Add(new ObservableCollection<bool>(new bool[] { true, true, true, true, true, true }));
            letter.Add(new ObservableCollection<bool>(new bool[] { true, false, false, false, false, true }));
            letter.Add(new ObservableCollection<bool>(new bool[] { true, false, true, true, true, true }));
            letter.Add(new ObservableCollection<bool>(new bool[] { true, false, false, false, true, true }));
            letter.Add(new ObservableCollection<bool>(new bool[] { true, false, true, true, true, true }));
            letter.Add(new ObservableCollection<bool>(new bool[] { true, false, false, false, false, true }));
            letter.Add(new ObservableCollection<bool>(new bool[] { true, true, true, true, true, true }));
            font['E'] = letter;

            letter = new ObservableCollection<ObservableCollection<bool>>();
            letter.Add(new ObservableCollection<bool>(new bool[] { true, true, true, true, true, true }));
            letter.Add(new ObservableCollection<bool>(new bool[] { true, false, true, true, true, true }));
            letter.Add(new ObservableCollection<bool>(new bool[] { true, false, true, true, true, true }));
            letter.Add(new ObservableCollection<bool>(new bool[] { true, false, true, true, true, true }));
            letter.Add(new ObservableCollection<bool>(new bool[] { true, false, true, true, true, true }));
            letter.Add(new ObservableCollection<bool>(new bool[] { true, false, false, false, false, true }));
            letter.Add(new ObservableCollection<bool>(new bool[] { true, true, true, true, true, true }));
            font['L'] = letter;

            letter = new ObservableCollection<ObservableCollection<bool>>();
            letter.Add(new ObservableCollection<bool>(new bool[] { true, true, true, true, true, true }));
            letter.Add(new ObservableCollection<bool>(new bool[] { true, false, false, false, false, true }));
            letter.Add(new ObservableCollection<bool>(new bool[] { true, false, true, true, false, true }));
            letter.Add(new ObservableCollection<bool>(new bool[] { true, false, true, true, false, true }));
            letter.Add(new ObservableCollection<bool>(new bool[] { true, false, true, true, false, true }));
            letter.Add(new ObservableCollection<bool>(new bool[] { true, false, false, false, false, true }));
            letter.Add(new ObservableCollection<bool>(new bool[] { true, true, true, true, true, true }));
            font['O'] = letter;

            letter = new ObservableCollection<ObservableCollection<bool>>();
            letter.Add(new ObservableCollection<bool>(new bool[] { true, true, true, true, true, true }));
            letter.Add(new ObservableCollection<bool>(new bool[] { true, false, true, true, true, false }));
            letter.Add(new ObservableCollection<bool>(new bool[] { true, false, true, true, true, false }));
            letter.Add(new ObservableCollection<bool>(new bool[] { true, false, true, false, true, false }));
            letter.Add(new ObservableCollection<bool>(new bool[] { true, false, true, false, true, false }));
            letter.Add(new ObservableCollection<bool>(new bool[] { true, false, false, false, false, false }));
            letter.Add(new ObservableCollection<bool>(new bool[] { true, true, true, true, true, true }));
            font['W'] = letter;

            letter = new ObservableCollection<ObservableCollection<bool>>();
            letter.Add(new ObservableCollection<bool>(new bool[] { true, true, true, true, true, true }));
            letter.Add(new ObservableCollection<bool>(new bool[] { true, false, false, false, true, true }));
            letter.Add(new ObservableCollection<bool>(new bool[] { true, false, true, true, false, true }));
            letter.Add(new ObservableCollection<bool>(new bool[] { true, false, false, false, true, true }));
            letter.Add(new ObservableCollection<bool>(new bool[] { true, false, true, true, false, true }));
            letter.Add(new ObservableCollection<bool>(new bool[] { true, false, true, true, false, true }));
            letter.Add(new ObservableCollection<bool>(new bool[] { true, true, true, true, true, true }));
            font['R'] = letter;

            letter = new ObservableCollection<ObservableCollection<bool>>();
            letter.Add(new ObservableCollection<bool>(new bool[] { true, true, true, true, true, true }));
            letter.Add(new ObservableCollection<bool>(new bool[] { true, false, false, false, true, true }));
            letter.Add(new ObservableCollection<bool>(new bool[] { true, false, true, true, false, true }));
            letter.Add(new ObservableCollection<bool>(new bool[] { true, false, true, true, false, true }));
            letter.Add(new ObservableCollection<bool>(new bool[] { true, false, true, true, false, true }));
            letter.Add(new ObservableCollection<bool>(new bool[] { true, false, false, false, true, true }));
            letter.Add(new ObservableCollection<bool>(new bool[] { true, true, true, true, true, true }));
            font['D'] = letter;

            letter = new ObservableCollection<ObservableCollection<bool>>();
            letter.Add(new ObservableCollection<bool>(new bool[] { true, true, true, true, true, true }));
            letter.Add(new ObservableCollection<bool>(new bool[] { true, true, true, true, true, true }));
            letter.Add(new ObservableCollection<bool>(new bool[] { true, true, true, true, true, true }));
            letter.Add(new ObservableCollection<bool>(new bool[] { true, true, true, true, true, true }));
            letter.Add(new ObservableCollection<bool>(new bool[] { true, true, true, true, true, true }));
            letter.Add(new ObservableCollection<bool>(new bool[] { true, true, true, true, true, true }));
            letter.Add(new ObservableCollection<bool>(new bool[] { true, true, true, true, true, true }));
            font[' '] = letter;

            Tick();
        }

        public ObservableCollection<ObservableCollection<bool>> ToList()
        {
            return cells;
        }

        public void Tick()
        {
            if(index >= phrase.Length)
            {
                index = 0;
            }
            char letter = phrase[index];
            cells = font[letter];
            index++;
        }
    }
}
