using System.Windows.Controls;

namespace chess
{
    internal class King : Piece
    {
        private bool hasMoved = false;

        public King(COLOR color, Grid position)
        {
            Piecetype = PIECETYPE.King;
            Color = color;
            Position = position;
            PieceImage = MyCanvasGenerator.GetCanvas(Color, Piecetype);
        }
    }
}
