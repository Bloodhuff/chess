using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace chess
{
    public class MoveSet
    {
        private readonly Piece.COLOR _color;
        private readonly bool _hasMoved;
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
                case Piece.PIECETYPE.Knight:
                    Knight();
                    break;
                case Piece.PIECETYPE.Bishop:
                    Bishop();
                    break;
                case Piece.PIECETYPE.Queen:
                    Queen();
                    break;
            }
        }

        public MoveSet(Piece.COLOR color, Piece.PIECETYPE piecetype, Grid position, bool hasmoved)
        {
            _color = color;
            _position = position;
            _hasMoved = hasmoved;
            switch (piecetype)
            {
                case Piece.PIECETYPE.Pawn:
                    Pawn();
                    break;
                case Piece.PIECETYPE.Rook:
                    Rook();
                    break;
                case Piece.PIECETYPE.King:
                    King();
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
                    if (piece is Pawn)
                    {
                        var pawn = (Pawn) piece;
                        if (_color != piece.GetColor() &&
                            (((_positionTuple.Item2) == tempTuple.Item2) &&
                             (_positionTuple.Item1 + 1) == tempTuple.Item1) && pawn.GetPassantAble())
                        {
                            _movesList.Add(new Move(1, -1));
                        }
                        if (_color != piece.GetColor() &&
                            (((_positionTuple.Item2) == tempTuple.Item2) &&
                             (_positionTuple.Item1 - 1) == tempTuple.Item1) && pawn.GetPassantAble())
                        {
                            _movesList.Add(new Move(-1, -1));
                        }
                    }

                }
                if (_hasMoved == false && !block && !blockfirst)
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
                    if (piece is Pawn)
                    {
                        var pawn = (Pawn)piece;
                        if (_color != piece.GetColor() &&
                            (((_positionTuple.Item2) == tempTuple.Item2) &&
                             (_positionTuple.Item1 + 1) == tempTuple.Item1) && pawn.GetPassantAble())
                        {
                            _movesList.Add(new Move(1, 1));
                        }
                        if (_color != piece.GetColor() &&
                            (((_positionTuple.Item2) == tempTuple.Item2) &&
                             (_positionTuple.Item1 - 1) == tempTuple.Item1) && pawn.GetPassantAble())
                        {
                            _movesList.Add(new Move(-1, 1));
                        }
                    }
                }
                if (_hasMoved == false && !block && !blockfirst)
                {
                    _movesList.Add(new Move(0, 2));
                }
                if (!block)
                {
                    _movesList.Add(new Move(0, 1));
                }
            }
        }
        void Knight()
        {
            _movesList.Add(new Move(2, 1));
            _movesList.Add(new Move(2, -1));
            _movesList.Add(new Move(-2, 1));
            _movesList.Add(new Move(-2, -1));
            _movesList.Add(new Move(1, 2));
            _movesList.Add(new Move(1, -2));
            _movesList.Add(new Move(-1, 2));
            _movesList.Add(new Move(-1, -2));
            _positionTuple = _board.GetArrayPosition(_position);
            foreach (var piece in Game.GetInstance().PieceList)
            {
                var tempTuple = _board.GetArrayPosition(piece.GetPosition());
                foreach (var move in _movesList)
                {
                    if (_color == piece.GetColor() && (_positionTuple.Item1 + move.GetX()) == tempTuple.Item1 && (_positionTuple.Item2 + move.GetY()) == tempTuple.Item2)
                    {
                        move.SetIsActiv(false);
                    }
                }
            }
        }

        void Rook()
        {
            for (int i = 0; i < 8; i++)
            {
                _movesList.Add(new Move(0, i));
            }
            for (int i = 0; i < 8; i++)
            {
                _movesList.Add(new Move(i, 0));
            }
            for (int i = 0; i > -8; i--)
            {
                _movesList.Add(new Move(i, 0));
            }
            for (int i = 0; i > -8; i--)
            {
                _movesList.Add(new Move(0, i));
            }
            _positionTuple = _board.GetArrayPosition(_position);
            foreach (var piece in Game.GetInstance().PieceList)
            {
                var tempTuple = _board.GetArrayPosition(piece.GetPosition());
                bool blockedHPositiv = false;
                bool blockedHNegativ = false;
                bool blockedVPositiv = false;
                bool blockedVNegativ = false;

                foreach (var move in _movesList)
                {
                    if (move.GetY() > 0)
                    {
                        if ((_positionTuple.Item1 + move.GetX()) == tempTuple.Item1 && (_positionTuple.Item2 + move.GetY()) == tempTuple.Item2 || blockedHPositiv)
                        {
                            if (_color == piece.GetColor() || blockedHPositiv)
                            {
                                move.SetIsActiv(false);
                            }
                            blockedHPositiv = true;
                        }
                    }
                    if (move.GetX() > 0)
                    {
                        if ((_positionTuple.Item1 + move.GetX()) == tempTuple.Item1 && (_positionTuple.Item2 + move.GetY()) == tempTuple.Item2 || blockedVPositiv)
                        {
                            if (_color == piece.GetColor() || blockedVPositiv)
                            {
                                move.SetIsActiv(false);
                            }
                            blockedVPositiv = true;
                        }
                    }
                    if (move.GetY() < 0)
                    {
                        if ((_positionTuple.Item1 + move.GetX()) == tempTuple.Item1 && (_positionTuple.Item2 + move.GetY()) == tempTuple.Item2 || blockedHNegativ)
                        {
                            if (_color == piece.GetColor() || blockedHNegativ)
                            {
                                move.SetIsActiv(false);
                            }
                            blockedHNegativ = true;
                        }
                    }
                    if (move.GetX() < 0)
                    {
                        if ((_positionTuple.Item1 + move.GetX()) == tempTuple.Item1 && (_positionTuple.Item2 + move.GetY()) == tempTuple.Item2 || blockedVNegativ)
                        {
                            if (_color == piece.GetColor() || blockedVNegativ)
                            {
                                move.SetIsActiv(false);
                            }
                            blockedVNegativ = true;
                        }
                    }
                }
            }
        }

        void Bishop()
        {
            for (int i = 0; i < 8; i++)
            {
                _movesList.Add(new Move(i, i));
            }
            for (int i = 0; i > -8; i--)
            {
                _movesList.Add(new Move(i, i));
            }
            for (int i = 0; i > -8; i--)
            {
                _movesList.Add(new Move(Math.Abs(i), i));
            }
            for (int i = 0; i > -8; i--)
            {
                _movesList.Add(new Move(i, Math.Abs(i)));
            }
            _positionTuple = _board.GetArrayPosition(_position);
            foreach (var piece in Game.GetInstance().PieceList)
            {
                var tempTuple = _board.GetArrayPosition(piece.GetPosition());
                bool blockedVPositivHPositiv = false;
                bool blockedVPositivHNegativ = false;
                bool blockedVNegativHPositiv = false;
                bool blockedVNegativHNegativ = false;
                foreach (var move in _movesList)
                {
                    if (move.GetX() > 0 && move.GetY() > 0)
                    {
                        if ((_positionTuple.Item1 + move.GetX()) == tempTuple.Item1 && (_positionTuple.Item2 + move.GetY()) == tempTuple.Item2 || blockedVPositivHPositiv)
                        {
                            if (_color == piece.GetColor() || blockedVPositivHPositiv)
                            {
                                move.SetIsActiv(false);
                            }
                            blockedVPositivHPositiv = true;
                        }
                    }
                    if (move.GetX() > 0 && move.GetY() < 0)
                    {
                        if ((_positionTuple.Item1 + move.GetX()) == tempTuple.Item1 && (_positionTuple.Item2 + move.GetY()) == tempTuple.Item2 || blockedVPositivHNegativ)
                        {
                            if (_color == piece.GetColor() || blockedVPositivHNegativ)
                            {
                                move.SetIsActiv(false);
                            }
                            blockedVPositivHNegativ = true;
                        }
                    }
                    if (move.GetX() < 0 && move.GetY() > 0)
                    {
                        if ((_positionTuple.Item1 + move.GetX()) == tempTuple.Item1 && (_positionTuple.Item2 + move.GetY()) == tempTuple.Item2 || blockedVNegativHPositiv)
                        {
                            if (_color == piece.GetColor() || blockedVNegativHPositiv)
                            {
                                move.SetIsActiv(false);
                            }
                            blockedVNegativHPositiv = true;
                        }
                    }
                    if (move.GetX() < 0 && move.GetY() < 0)
                    {
                        if ((_positionTuple.Item1 + move.GetX()) == tempTuple.Item1 && (_positionTuple.Item2 + move.GetY()) == tempTuple.Item2 || blockedVNegativHNegativ)
                        {
                            if (_color == piece.GetColor() || blockedVNegativHNegativ)
                            {
                                move.SetIsActiv(false);
                            }
                            blockedVNegativHNegativ = true;
                        }
                    }
                }
            }
        }

        void Queen()
        {
            for (int i = 0; i < 8; i++)
            {
                _movesList.Add(new Move(i, i));
            }
            for (int i = 0; i > -8; i--)
            {
                _movesList.Add(new Move(i, i));
            }
            for (int i = 0; i > -8; i--)
            {
                _movesList.Add(new Move(Math.Abs(i), i));
            }
            for (int i = 0; i > -8; i--)
            {
                _movesList.Add(new Move(i, Math.Abs(i)));
            }
            for (int i = 0; i < 8; i++)
            {
                _movesList.Add(new Move(0, i));
            }
            for (int i = 0; i < 8; i++)
            {
                _movesList.Add(new Move(i, 0));
            }
            for (int i = 0; i > -8; i--)
            {
                _movesList.Add(new Move(i, 0));
            }
            for (int i = 0; i > -8; i--)
            {
                _movesList.Add(new Move(0, i));
            }
            _positionTuple = _board.GetArrayPosition(_position);
            foreach (var piece in Game.GetInstance().PieceList)
            {
                var tempTuple = _board.GetArrayPosition(piece.GetPosition());
                bool blockedHPositiv = false;
                bool blockedHNegativ = false;
                bool blockedVPositiv = false;
                bool blockedVNegativ = false;
                bool blockedVPositivHPositiv = false;
                bool blockedVPositivHNegativ = false;
                bool blockedVNegativHPositiv = false;
                bool blockedVNegativHNegativ = false;

                foreach (var move in _movesList)
                {
                    if (move.GetX() > 0 && move.GetY() > 0)
                    {
                        if ((_positionTuple.Item1 + move.GetX()) == tempTuple.Item1 && (_positionTuple.Item2 + move.GetY()) == tempTuple.Item2 || blockedVPositivHPositiv)
                        {
                            if (_color == piece.GetColor() || blockedVPositivHPositiv)
                            {
                                move.SetIsActiv(false);
                            }
                            blockedVPositivHPositiv = true;
                        }
                    }
                    if (move.GetX() > 0 && move.GetY() < 0)
                    {
                        if ((_positionTuple.Item1 + move.GetX()) == tempTuple.Item1 && (_positionTuple.Item2 + move.GetY()) == tempTuple.Item2 || blockedVPositivHNegativ)
                        {
                            if (_color == piece.GetColor() || blockedVPositivHNegativ)
                            {
                                move.SetIsActiv(false);
                            }
                            blockedVPositivHNegativ = true;
                        }
                    }
                    if (move.GetX() < 0 && move.GetY() > 0)
                    {
                        if ((_positionTuple.Item1 + move.GetX()) == tempTuple.Item1 && (_positionTuple.Item2 + move.GetY()) == tempTuple.Item2 || blockedVNegativHPositiv)
                        {
                            if (_color == piece.GetColor() || blockedVNegativHPositiv)
                            {
                                move.SetIsActiv(false);
                            }
                            blockedVNegativHPositiv = true;
                        }
                    }
                    if (move.GetX() < 0 && move.GetY() < 0)
                    {
                        if ((_positionTuple.Item1 + move.GetX()) == tempTuple.Item1 && (_positionTuple.Item2 + move.GetY()) == tempTuple.Item2 || blockedVNegativHNegativ)
                        {
                            if (_color == piece.GetColor() || blockedVNegativHNegativ)
                            {
                                move.SetIsActiv(false);
                            }
                            blockedVNegativHNegativ = true;
                        }
                    }
                }
                foreach (var move in _movesList)
                {
                    if (move.GetY() > 0 && move.GetX() == 0)
                    {
                        if ((_positionTuple.Item1 + move.GetX()) == tempTuple.Item1 && (_positionTuple.Item2 + move.GetY()) == tempTuple.Item2 || blockedHPositiv)
                        {
                            if (_color == piece.GetColor() || blockedHPositiv)
                            {
                                move.SetIsActiv(false);
                            }
                            blockedHPositiv = true;
                        }
                    }
                    if (move.GetX() > 0 && move.GetY() == 0)
                    {
                        if ((_positionTuple.Item1 + move.GetX()) == tempTuple.Item1 && (_positionTuple.Item2 + move.GetY()) == tempTuple.Item2 || blockedVPositiv)
                        {
                            if (_color == piece.GetColor() || blockedVPositiv)
                            {
                                move.SetIsActiv(false);
                            }
                            blockedVPositiv = true;
                        }
                    }
                    if (move.GetY() < 0 && move.GetX() == 0)
                    {
                        if ((_positionTuple.Item1 + move.GetX()) == tempTuple.Item1 && (_positionTuple.Item2 + move.GetY()) == tempTuple.Item2 || blockedHNegativ)
                        {
                            if (_color == piece.GetColor() || blockedHNegativ)
                            {
                                move.SetIsActiv(false);
                            }
                            blockedHNegativ = true;
                        }
                    }
                    if (move.GetX() < 0 && move.GetY() == 0)
                    {
                        if ((_positionTuple.Item1 + move.GetX()) == tempTuple.Item1 && (_positionTuple.Item2 + move.GetY()) == tempTuple.Item2 || blockedVNegativ)
                        {
                            if (_color == piece.GetColor() || blockedVNegativ)
                            {
                                move.SetIsActiv(false);
                            }
                            blockedVNegativ = true;
                        }
                    }
                }
            }
        }

        void King()
        {
            for (int i = 0; i < 2; i++)
            {
                _movesList.Add(new Move(i, i));
            }
            for (int i = 0; i > -2; i--)
            {
                _movesList.Add(new Move(i, i));
            }
            for (int i = 0; i > -2; i--)
            {
                _movesList.Add(new Move(Math.Abs(i), i));
            }
            for (int i = 0; i > -2; i--)
            {
                _movesList.Add(new Move(i, Math.Abs(i)));
            }
            for (int i = 0; i < 2; i++)
            {
                _movesList.Add(new Move(0, i));
            }
            for (int i = 0; i < 2; i++)
            {
                _movesList.Add(new Move(i, 0));
            }
            for (int i = 0; i > -2; i--)
            {
                _movesList.Add(new Move(i, 0));
            }
            for (int i = 0; i > -2; i--)
            {
                _movesList.Add(new Move(0, i));
            }
            _positionTuple = _board.GetArrayPosition(_position);
            foreach (var piece in Game.GetInstance().PieceList)
            {
                var tempTuple = _board.GetArrayPosition(piece.GetPosition());
                bool blockedHPositiv = false;
                bool blockedHNegativ = false;
                bool blockedVPositiv = false;
                bool blockedVNegativ = false;
                bool blockedVPositivHPositiv = false;
                bool blockedVPositivHNegativ = false;
                bool blockedVNegativHPositiv = false;
                bool blockedVNegativHNegativ = false;

                foreach (var move in _movesList)
                {
                    if (move.GetX() > 0 && move.GetY() > 0)
                    {
                        if ((_positionTuple.Item1 + move.GetX()) == tempTuple.Item1 && (_positionTuple.Item2 + move.GetY()) == tempTuple.Item2 || blockedVPositivHPositiv)
                        {
                            if (_color == piece.GetColor() || blockedVPositivHPositiv)
                            {
                                move.SetIsActiv(false);
                            }
                            blockedVPositivHPositiv = true;
                        }
                    }
                    if (move.GetX() > 0 && move.GetY() < 0)
                    {
                        if ((_positionTuple.Item1 + move.GetX()) == tempTuple.Item1 && (_positionTuple.Item2 + move.GetY()) == tempTuple.Item2 || blockedVPositivHNegativ)
                        {
                            if (_color == piece.GetColor() || blockedVPositivHNegativ)
                            {
                                move.SetIsActiv(false);
                            }
                            blockedVPositivHNegativ = true;
                        }
                    }
                    if (move.GetX() < 0 && move.GetY() > 0)
                    {
                        if ((_positionTuple.Item1 + move.GetX()) == tempTuple.Item1 && (_positionTuple.Item2 + move.GetY()) == tempTuple.Item2 || blockedVNegativHPositiv)
                        {
                            if (_color == piece.GetColor() || blockedVNegativHPositiv)
                            {
                                move.SetIsActiv(false);
                            }
                            blockedVNegativHPositiv = true;
                        }
                    }
                    if (move.GetX() < 0 && move.GetY() < 0)
                    {
                        if ((_positionTuple.Item1 + move.GetX()) == tempTuple.Item1 && (_positionTuple.Item2 + move.GetY()) == tempTuple.Item2 || blockedVNegativHNegativ)
                        {
                            if (_color == piece.GetColor() || blockedVNegativHNegativ)
                            {
                                move.SetIsActiv(false);
                            }
                            blockedVNegativHNegativ = true;
                        }
                    }
                }
                foreach (var move in _movesList)
                {
                    if (move.GetY() > 0 && move.GetX() == 0)
                    {
                        if ((_positionTuple.Item1 + move.GetX()) == tempTuple.Item1 && (_positionTuple.Item2 + move.GetY()) == tempTuple.Item2 || blockedHPositiv)
                        {
                            if (_color == piece.GetColor() || blockedHPositiv)
                            {
                                move.SetIsActiv(false);
                            }
                            blockedHPositiv = true;
                        }
                    }
                    if (move.GetX() > 0 && move.GetY() == 0)
                    {
                        if ((_positionTuple.Item1 + move.GetX()) == tempTuple.Item1 && (_positionTuple.Item2 + move.GetY()) == tempTuple.Item2 || blockedVPositiv)
                        {
                            if (_color == piece.GetColor() || blockedVPositiv)
                            {
                                move.SetIsActiv(false);
                            }
                            blockedVPositiv = true;
                        }
                    }
                    if (move.GetY() < 0 && move.GetX() == 0)
                    {
                        if ((_positionTuple.Item1 + move.GetX()) == tempTuple.Item1 && (_positionTuple.Item2 + move.GetY()) == tempTuple.Item2 || blockedHNegativ)
                        {
                            if (_color == piece.GetColor() || blockedHNegativ)
                            {
                                move.SetIsActiv(false);
                            }
                            blockedHNegativ = true;
                        }
                    }
                    if (move.GetX() < 0 && move.GetY() == 0)
                    {
                        if ((_positionTuple.Item1 + move.GetX()) == tempTuple.Item1 && (_positionTuple.Item2 + move.GetY()) == tempTuple.Item2 || blockedVNegativ)
                        {
                            if (_color == piece.GetColor() || blockedVNegativ)
                            {
                                move.SetIsActiv(false);
                            }
                            blockedVNegativ = true;
                        }
                    }
                }
            }
        }
        public List<Move> GetMoves()
        {
            return _movesList;
        }
    }
}
