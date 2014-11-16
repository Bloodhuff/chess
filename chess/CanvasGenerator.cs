using System;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace chess
{
    public class CanvasGenerator
    {
        public Canvas GetCanvas(Piece.COLOR color, Piece.PIECETYPE piecetype)
        {
            var theImage = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "\\Pieces\\"+Game.GetInstance().GetPlayerColor() +"\\" + color + "\\" + piecetype + ".png", UriKind.Relative));
            var myImageBrush = new ImageBrush(theImage);
            var myCanvas = new Canvas { Width = 90, Height = 90, Background = myImageBrush };
            return myCanvas;
        }
    }
}
