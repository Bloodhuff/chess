using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace chess
{
    class Bishop : Piece
    {
        public Bishop(COLOR color, Grid position)
        {
            Piecetype = PIECETYPE.Bishop;
            Color = color;
            Position = position;
            PieceImage = MyCanvasGenerator.GetCanvas(Color, Piecetype);
        }
    }
}
