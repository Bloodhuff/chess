using System.Windows.Controls;

namespace chess
{
    class Pawn : Piece
    {
        private bool _hasMoved;
        private bool _passantAble;
        public Pawn(COLOR color, Grid position)
        {
            Piecetype = PIECETYPE.Pawn;
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

        public bool GetPassantAble()
        {
            return _passantAble;
        }
        public void SetPassantAble(bool status)
        {
            _passantAble = status;
        }
    }
}