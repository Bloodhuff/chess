using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace chess
{
    class Knight : Piece
    {
        public Knight(COLOR color, Grid position)
        {
            Piecetype = PIECETYPE.Knight;
            Color = color;
            Position = position;
            PieceImage = MyCanvasGenerator.GetCanvas(Color, Piecetype);
        }
    }
}
