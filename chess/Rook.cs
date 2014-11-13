using System.Windows.Controls;

namespace chess
{
    class Rook : Piece
    {
        private bool _hasMoved;
        public Rook(COLOR color, Grid position)
        {
            Piecetype = PIECETYPE.Rook;
            Color = color;
            Position = position;
            PieceImage = MyCanvasGenerator.GetCanvas(Color, Piecetype);
        }
        public void SethasMoved()
        {
            _hasMoved = true;
        }
        public new void SetMoveSet()
        {
            MyMoveSet = new MoveSet(Color, Piecetype, Position, _hasMoved);
        }
    }
}
