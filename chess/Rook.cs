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
