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
    /// The board are mostly used to keep track of the board.
    /// It will put all squares in to a array for easy access
    /// from the other classes. 
    /// Also it is able to find coordinates on the board
    /// by useing the GetArrayPosition.
    /// </summary>
    public partial class Board : Window
    {
        public static Board Instance;
        public Grid[,] MyBoarderArray = new Grid[8, 8];
        public Board()
        {
            Instance = this;
            InitializeComponent();
            PutGridInArray();
        }

        private void grid_Click(object sender, EventArgs eventArgs)
        {
            var myGrid = ((Grid)sender);
            Game.GetInstance().HandelClick(myGrid);
        }

        private void PutGridInArray()
        {
            MyBoarderArray[0, 0] = A8;
            MyBoarderArray[0, 1] = A7;
            MyBoarderArray[0, 2] = A6;
            MyBoarderArray[0, 3] = A5;
            MyBoarderArray[0, 4] = A4;
            MyBoarderArray[0, 5] = A3;
            MyBoarderArray[0, 6] = A2;
            MyBoarderArray[0, 7] = A1;
            MyBoarderArray[1, 0] = B8;
            MyBoarderArray[1, 1] = B7;
            MyBoarderArray[1, 2] = B6;
            MyBoarderArray[1, 3] = B5;
            MyBoarderArray[1, 4] = B4;
            MyBoarderArray[1, 5] = B3;
            MyBoarderArray[1, 6] = B2;
            MyBoarderArray[1, 7] = B1;
            MyBoarderArray[2, 0] = C8;
            MyBoarderArray[2, 1] = C7;
            MyBoarderArray[2, 2] = C6;
            MyBoarderArray[2, 3] = C5;
            MyBoarderArray[2, 4] = C4;
            MyBoarderArray[2, 5] = C3;
            MyBoarderArray[2, 6] = C2;
            MyBoarderArray[2, 7] = C1;
            MyBoarderArray[3, 0] = D8;
            MyBoarderArray[3, 1] = D7;
            MyBoarderArray[3, 2] = D6;
            MyBoarderArray[3, 3] = D5;
            MyBoarderArray[3, 4] = D4;
            MyBoarderArray[3, 5] = D3;
            MyBoarderArray[3, 6] = D2;
            MyBoarderArray[3, 7] = D1;
            MyBoarderArray[4, 0] = E8;
            MyBoarderArray[4, 1] = E7;
            MyBoarderArray[4, 2] = E6;
            MyBoarderArray[4, 3] = E5;
            MyBoarderArray[4, 4] = E4;
            MyBoarderArray[4, 5] = E3;
            MyBoarderArray[4, 6] = E2;
            MyBoarderArray[4, 7] = E1;
            MyBoarderArray[5, 0] = F8;
            MyBoarderArray[5, 1] = F7;
            MyBoarderArray[5, 2] = F6;
            MyBoarderArray[5, 3] = F5;
            MyBoarderArray[5, 4] = F4;
            MyBoarderArray[5, 5] = F3;
            MyBoarderArray[5, 6] = F2;
            MyBoarderArray[5, 7] = F1;
            MyBoarderArray[6, 0] = G8;
            MyBoarderArray[6, 1] = G7;
            MyBoarderArray[6, 2] = G6;
            MyBoarderArray[6, 3] = G5;
            MyBoarderArray[6, 4] = G4;
            MyBoarderArray[6, 5] = G3;
            MyBoarderArray[6, 6] = G2;
            MyBoarderArray[6, 7] = G1;
            MyBoarderArray[7, 0] = H8;
            MyBoarderArray[7, 1] = H7;
            MyBoarderArray[7, 2] = H6;
            MyBoarderArray[7, 3] = H5;
            MyBoarderArray[7, 4] = H4;
            MyBoarderArray[7, 5] = H3;
            MyBoarderArray[7, 6] = H2;
            MyBoarderArray[7, 7] = H1;
        }

        public Tuple<int, int> GetArrayPosition(Grid value)
        {
            int w = MyBoarderArray.GetLength(0);
            int h = MyBoarderArray.GetLength(1);

            for (int x = 0; x < w; ++x)
            {
                for (int y = 0; y < h; ++y)
                {
                    if (MyBoarderArray[x, y].Equals(value)) { 
                    return Tuple.Create(x, y);
                    }
                }
            }
            return Tuple.Create(-1, -1);
        }

        private void HostButton_Click(object sender, RoutedEventArgs e)
        {
            ChooseRole.Visibility = Visibility.Hidden;
            ChooseColor.Visibility = Visibility.Visible;
            var yourIp = new YourIp();
            MyLabel.Content = "Your ip is: "+yourIp.GetIp();
        }

        private void WhiteButton_Click(object sender, RoutedEventArgs e)
        {
            ChooseColor.Visibility = Visibility.Hidden;
            Game.GetInstance().HostMatch(Piece.COLOR.White);
            Gameboard.Visibility = Visibility.Visible;
        }

        private void BlackButton_Click(object sender, RoutedEventArgs e)
        {
            ChooseColor.Visibility = Visibility.Hidden;
            Game.GetInstance().HostMatch(Piece.COLOR.Black);
            Gameboard.Visibility = Visibility.Visible;
        }

        private void JoinButton_Click(object sender, RoutedEventArgs e)
        {
            ChooseRole.Visibility = Visibility.Hidden;
            TypeIP.Visibility = Visibility.Visible;
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            TypeIP.Visibility = Visibility.Hidden;
            Gameboard.Visibility = Visibility.Visible;
            Game.GetInstance().JoinMatch(IpTextBox.Text);
        }
    }
}
