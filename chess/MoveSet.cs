using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace chess
{
    public class MoveSet
    {
        private readonly Piece.COLOR _color;
        private bool HasMoved;
        private readonly List<Move> _movesList = new List<Move>();
        private readonly Board _board = Board.Instance;
        private readonly Grid _position;
        private Tuple<int, int> _positionTuple;

        public MoveSet(Piece.COLOR color, Piece.PIECETYPE piecetype, Grid position)
        {
            _color = color;
            _position = position;
            switch (piecetype)
            {
                case Piece.PIECETYPE.Pawn:
                    Pawn();
                    break;
            }
        }

        private void Pawn()
        {
            var block = false;
            var blockfirst = false;
            _positionTuple = _board.GetArrayPosition(_position);
            if (_color.ToString() == "White")
            {
                foreach (var piece in Game.GetInstance().PieceList)
                {
                    var tempTuple = _board.GetArrayPosition(piece.GetPosition());
                    if (((_positionTuple.Item2 - 1) == tempTuple.Item2) &&
                         _positionTuple.Item1 == tempTuple.Item1)
                    {
                        block = true;
                    }
                    if (((_positionTuple.Item2 - 2) == tempTuple.Item2) &&
                         _positionTuple.Item1 == tempTuple.Item1)
                    {
                        blockfirst = true;
                    }
                    if (_color != piece.GetColor() &&
                        (((_positionTuple.Item2 - 1) == tempTuple.Item2) &&
                         (_positionTuple.Item1 - 1) == tempTuple.Item1))
                    {
                        _movesList.Add(new Move(-1, -1));
                    }
                    if (_color != piece.GetColor() &&
                        (((_positionTuple.Item2 - 1) == tempTuple.Item2) &&
                         (_positionTuple.Item1 + 1) == tempTuple.Item1))
                    {
                        _movesList.Add(new Move(1, -1));
                    }
                }
                if (HasMoved == false && !block && !blockfirst)
                {
                    _movesList.Add(new Move(0, -2));
                }
                if (!block)
                {
                    _movesList.Add(new Move(0, -1));
                }
            }
            else
            {
                foreach (var piece in Game.GetInstance().PieceList)
                {
                    var tempTuple = _board.GetArrayPosition(piece.GetPosition());
                    if (((_positionTuple.Item2 + 1) == tempTuple.Item2) &&
                         _positionTuple.Item1 == tempTuple.Item1)
                    {
                        block = true;
                    }
                    if (((_positionTuple.Item2 + 2) == tempTuple.Item2) &&
                         _positionTuple.Item1 == tempTuple.Item1)
                    {
                        blockfirst = true;
                    }
                    if (_color != piece.GetColor() &&
                        (((_positionTuple.Item2 + 1) == tempTuple.Item2) &&
                         (_positionTuple.Item1 - 1) == tempTuple.Item1))
                    {
                        _movesList.Add(new Move(-1, 1));
                    }
                    if (_color != piece.GetColor() &&
                        (((_positionTuple.Item2 + 1) == tempTuple.Item2) &&
                         (_positionTuple.Item1 + 1) == tempTuple.Item1))
                    {
                        _movesList.Add(new Move(1, 1));
                    }
                }
                if (HasMoved == false && !block && !blockfirst)
                {
                    _movesList.Add(new Move(0, 2));
                }
                if (!block)
                {
                    _movesList.Add(new Move(0, 1));
                }
            }
        }

        public List<Move> GetMoves()
        {
            return _movesList;
        }
    }
}
