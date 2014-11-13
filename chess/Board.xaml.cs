using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace chess
{
    /// <summary>
    /// Interaction logic for Board.xaml
    /// </summary>
    public partial class Board : Window
    {
        public static Board Instance;
        public Grid[,] MyGrids = new Grid[8, 8];
        public Board()
        {
            Instance = this;
            InitializeComponent();
            PutGridInArray();
            Game.GetInstance().Start();
        }

        private void grid_Click(object sender, EventArgs eventArgs)
        {
            var myGrid = ((Grid)sender);
            Game.GetInstance().HandelClick(myGrid);
        }

        private void PutGridInArray()
        {
            MyGrids[0, 0] = A8;
            MyGrids[0, 1] = A7;
            MyGrids[0, 2] = A6;
            MyGrids[0, 3] = A5;
            MyGrids[0, 4] = A4;
            MyGrids[0, 5] = A3;
            MyGrids[0, 6] = A2;
            MyGrids[0, 7] = A1;
            MyGrids[1, 0] = B8;
            MyGrids[1, 1] = B7;
            MyGrids[1, 2] = B6;
            MyGrids[1, 3] = B5;
            MyGrids[1, 4] = B4;
            MyGrids[1, 5] = B3;
            MyGrids[1, 6] = B2;
            MyGrids[1, 7] = B1;
            MyGrids[2, 0] = C8;
            MyGrids[2, 1] = C7;
            MyGrids[2, 2] = C6;
            MyGrids[2, 3] = C5;
            MyGrids[2, 4] = C4;
            MyGrids[2, 5] = C3;
            MyGrids[2, 6] = C2;
            MyGrids[2, 7] = C1;
            MyGrids[3, 0] = D8;
            MyGrids[3, 1] = D7;
            MyGrids[3, 2] = D6;
            MyGrids[3, 3] = D5;
            MyGrids[3, 4] = D4;
            MyGrids[3, 5] = D3;
            MyGrids[3, 6] = D2;
            MyGrids[3, 7] = D1;
            MyGrids[4, 0] = E8;
            MyGrids[4, 1] = E7;
            MyGrids[4, 2] = E6;
            MyGrids[4, 3] = E5;
            MyGrids[4, 4] = E4;
            MyGrids[4, 5] = E3;
            MyGrids[4, 6] = E2;
            MyGrids[4, 7] = E1;
            MyGrids[5, 0] = F8;
            MyGrids[5, 1] = F7;
            MyGrids[5, 2] = F6;
            MyGrids[5, 3] = F5;
            MyGrids[5, 4] = F4;
            MyGrids[5, 5] = F3;
            MyGrids[5, 6] = F2;
            MyGrids[5, 7] = F1;
            MyGrids[6, 0] = G8;
            MyGrids[6, 1] = G7;
            MyGrids[6, 2] = G6;
            MyGrids[6, 3] = G5;
            MyGrids[6, 4] = G4;
            MyGrids[6, 5] = G3;
            MyGrids[6, 6] = G2;
            MyGrids[6, 7] = G1;
            MyGrids[7, 0] = H8;
            MyGrids[7, 1] = H7;
            MyGrids[7, 2] = H6;
            MyGrids[7, 3] = H5;
            MyGrids[7, 4] = H4;
            MyGrids[7, 5] = H3;
            MyGrids[7, 6] = H2;
            MyGrids[7, 7] = H1;
        }

        public Tuple<int, int> GetArrayPosition(Grid value)
        {
            int w = MyGrids.GetLength(0);
            int h = MyGrids.GetLength(1);

            for (int x = 0; x < w; ++x)
            {
                for (int y = 0; y < h; ++y)
                {
                    if (MyGrids[x, y].Equals(value)) { 
                    return Tuple.Create(x, y);
                    }
                }
            }
            return Tuple.Create(-1, -1);
        }
    }
}
