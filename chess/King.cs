using System.Windows.Controls;

namespace chess
{
    internal class King : Piece
    {
        private bool _hasMoved;

        public King(COLOR color, Grid position)
        {
            Piecetype = PIECETYPE.King;
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
