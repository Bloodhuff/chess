using System.Collections.Generic;
using System.Windows.Controls;

namespace chess
{
    public abstract class Piece
    {
        public enum COLOR
        {
            White,
            Black
        }

        public enum PIECETYPE
        {
            Pawn,
            Knight,
            Bishop,
            Rook,
            Queen,
            King
        }

        protected CanvasGenerator MyCanvasGenerator = new CanvasGenerator();
        protected PIECETYPE Piecetype;
        protected COLOR Color;
        protected Grid Position;
        protected Canvas PieceImage;
        protected MoveSet MyMoveSet;

        public Grid GetPosition()
        {
            return Position;
        }

        public Canvas GetPieceImage()
        {
            return PieceImage;
        }

        public void SetMoveSet()
        {
            MyMoveSet = new MoveSet(Color, Piecetype, Position);
        }

        public COLOR GetColor()
        {
            return Color;
        }

        public List<Move> GetMoveSet()
        {
            return MyMoveSet.GetMoves();
        }

        public void SetPosition(Grid grid)
        {
            Position = grid;
        }
    }
}
