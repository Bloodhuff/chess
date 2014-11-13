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
