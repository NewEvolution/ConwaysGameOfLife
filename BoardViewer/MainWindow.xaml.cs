using System.Windows;
using ConwaysGameOfLife;
using System.Windows.Threading;
using System;
using Xceed.Wpf.Toolkit;
using System.Collections.Generic;

namespace BoardViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Board currentBoard;
        private DispatcherTimer dispatcherTimer;

        public MainWindow()
        {
            //List<int> cell1 = new List<int> { 5, 5 }; //-------Beacon-.
            //List<int> cell2 = new List<int> { 5, 6 }; //               \
            //List<int> cell3 = new List<int> { 6, 5 }; //                \
            //List<int> cell4 = new List<int> { 7, 8 }; //                 \
            //List<int> cell5 = new List<int> { 8, 7 }; //                  \
            //List<int> cell6 = new List<int> { 8, 8 }; //                   \
            //List<List<int>> liveCells = new List<List<int>> { cell1, cell2, cell3, cell4, cell5, cell6 };
            //List<int> cell1 = new List<int> { 5, 5 }; //------Glider--.
            //List<int> cell2 = new List<int> { 5, 6 }; //               \
            //List<int> cell3 = new List<int> { 5, 7 }; //                \
            //List<int> cell4 = new List<int> { 4, 7 }; //                 \
            //List<int> cell5 = new List<int> { 3, 6 }; //                  \
            //List<List<int>> liveCells = new List<List<int>> { cell1, cell2, cell3, cell4, cell5 };
            List<List<int>> liveCells = new List<List<int>> { }; // empty grid
            currentBoard = new GameOfLife(25, false, liveCells);
            currentBoard.randomFill();
            dispatcherTimer = new DispatcherTimer();

            InitializeComponent();

            TheListView.ItemsSource = currentBoard.ToList();
            dispatcherTimer.Tick += dispatcherTimerClick;
            dispatcherTimer.Interval = TimeSpan.FromSeconds((double)RunSpeed.Value);
        }

        private void dispatcherTimerClick(object sender, EventArgs e)
        {
            InitiateTick();
        }

        private void InitiateTick()
        {
            currentBoard.Tick();
            TheListView.ItemsSource = currentBoard.ToList();
        }

        private void Run_Button_Click(object sender, RoutedEventArgs e)
        {
            InitiateTick(); // To make it clear that clicking the button worked
            dispatcherTimer.Start();
            TickButton.IsEnabled = false;
            RunButton.IsEnabled = false;
            RunSpeed.IsEnabled = true;
            StopButton.IsEnabled = true;
        }

        private void Stop_Button_Click(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Stop();
            TickButton.IsEnabled = true;
            RunButton.IsEnabled = true;
            RunSpeed.IsEnabled = false;
            StopButton.IsEnabled = false;
        }

        private void Tick_Button_Click(object sender, RoutedEventArgs e)
        {
            InitiateTick();
        }

        private void RunSpeed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            dispatcherTimer.Interval = TimeSpan.FromSeconds((double)e.NewValue);
        }
    }
}
