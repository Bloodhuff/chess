using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace chess
{
    class Pawn : Piece
    {
        private bool hasMoved = false;
        private bool Passantable = false;
        public Pawn(COLOR color, Grid position)
        {
            Piecetype = PIECETYPE.Pawn;
            Color = color;
            Position = position;
            PieceImage = MyCanvasGenerator.GetCanvas(Color, Piecetype);
        }
    }
}