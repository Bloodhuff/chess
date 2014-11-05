using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace chess
{
    class GetCanvas
    {
        private BitmapImage _theImage;

        public Canvas Canvas(string color, string pieceType)
        {
            _theImage = new BitmapImage(new Uri("c:\\brikker\\" + color + "\\" + pieceType + ".png", UriKind.Relative));
            var myImageBrush = new ImageBrush(_theImage);
            var myCanvas = new Canvas { Width = 90, Height = 90, Background = myImageBrush };
            return myCanvas;
        }
    }
}
