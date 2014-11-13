using System.Windows.Controls;

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