using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace chess
{
    class Queen : Piece
    {
        public Queen(COLOR color, Grid position)
        {
            Piecetype = PIECETYPE.Queen;
            Color = color;
            Position = position;
            PieceImage = MyCanvasGenerator.GetCanvas(Color, Piecetype);
        }
    }
}
