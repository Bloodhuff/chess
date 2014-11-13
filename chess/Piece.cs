using System.Collections.Generic;
using System.Windows.Controls;

namespace chess
{
    public abstract class Piece
    {
        public enum COLOR
        {
            Black,
            White
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
        private MoveSet _myMoveSet;

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
            _myMoveSet = new MoveSet(Color, Piecetype, Position);
        }

        public COLOR GetColor()
        {
            return Color;
        }

        public List<Move> GetMoveSet()
        {
            return _myMoveSet.GetMoves();
        } 
    }
}
