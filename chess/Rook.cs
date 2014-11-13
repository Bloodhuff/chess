using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace chess
{
    class Rook : Piece
    {
        private bool hasMoved = false;
        public Rook(COLOR color, Grid position)
        {
            Piecetype = PIECETYPE.Rook;
            Color = color;
            Position = position;
            PieceImage = MyCanvasGenerator.GetCanvas(Color, Piecetype);
        }
    }
}
