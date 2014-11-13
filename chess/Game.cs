using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace chess
{
    class Game
    {
        public static Game Instance = null;
        private readonly Board _board = Board.Instance;
        public readonly List<Piece> PieceList = new List<Piece>();
        private Piece _selectedPiece;
        private bool _firstClick = true;

        private Game()
        {
            
        }

        public static Game GetInstance()
        {
            return Instance ?? (Instance = new Game());
        }

        public void Start()
        {
            for (int i = 0; i < 8; i++)
            {
                PieceList.Add(new Pawn(Piece.COLOR.White, _board.MyGrids[i, 6]));
                PieceList.Add(new Pawn(Piece.COLOR.Black, _board.MyGrids[i, 1]));
            }
            PieceList.Add(new Pawn(Piece.COLOR.Black, _board.MyGrids[3, 5]));
            PieceList.Add(new Rook(Piece.COLOR.White, _board.MyGrids[0, 7]));
            PieceList.Add(new Knight(Piece.COLOR.White, _board.MyGrids[1, 7]));
            PieceList.Add(new Bishop(Piece.COLOR.White, _board.MyGrids[2, 7]));
            PieceList.Add(new Queen(Piece.COLOR.White, _board.MyGrids[3, 7]));
            PieceList.Add(new King(Piece.COLOR.White, _board.MyGrids[4, 7]));
            PieceList.Add(new Bishop(Piece.COLOR.White, _board.MyGrids[5, 7]));
            PieceList.Add(new Knight(Piece.COLOR.White, _board.MyGrids[6, 7]));
            PieceList.Add(new Rook(Piece.COLOR.White, _board.MyGrids[7, 7]));
            PieceList.Add(new Rook(Piece.COLOR.Black, _board.MyGrids[0, 0]));
            PieceList.Add(new Knight(Piece.COLOR.Black, _board.MyGrids[1, 0]));
            PieceList.Add(new Bishop(Piece.COLOR.Black, _board.MyGrids[2, 0]));
            PieceList.Add(new Queen(Piece.COLOR.Black, _board.MyGrids[3, 0]));
            PieceList.Add(new King(Piece.COLOR.Black, _board.MyGrids[4, 0]));
            PieceList.Add(new Bishop(Piece.COLOR.Black, _board.MyGrids[5, 0]));
            PieceList.Add(new Knight(Piece.COLOR.Black, _board.MyGrids[6, 0]));
            PieceList.Add(new Rook(Piece.COLOR.Black, _board.MyGrids[7, 0]));

            foreach (var p in PieceList)
            {
                Grid grid = p.GetPosition();
                Canvas image = p.GetPieceImage();
                grid.Children.Add(image);
            }
        }

        public void HandelClick(Grid grid)
        {
            if (_firstClick)
            {
                SelectPiece(grid);
            }
            else
            {
                if (!Equals(grid, _selectedPiece.GetPosition()))
                {
                }
                else
                {
                    foreach (var move in _selectedPiece.GetMoveSet())
                    {
                        var coordinates = _board.GetArrayPosition(_selectedPiece.GetPosition());
                        int newY = coordinates.Item2 + move.GetY();
                        int newX = coordinates.Item1 + move.GetX();
                        if (newY < 8 && newX < 8 && newY > -1 && newX > -1 && move.GetIsActiv())
                        {
                            _board.MyGrids[newX, newY].Children.RemoveAt(_board.MyGrids[newX, newY].Children.Count == 1
                                ? 0
                                : 1);
                        }
                    }
                    _selectedPiece.GetPosition().Children.Clear();
                    _selectedPiece.GetPosition().Children.Add(_selectedPiece.GetPieceImage());
                    _selectedPiece = null;
                    _firstClick = true;
                }
            }
        }

        private void SelectPiece(Grid grid)
        {
            foreach (var piece in PieceList.Where(piece => Equals(piece.GetPosition(), grid)))
            {
                _selectedPiece = piece;
                piece.SetMoveSet();
                foreach (var move in piece.GetMoveSet())
                {
                    var coordinates = _board.GetArrayPosition(_selectedPiece.GetPosition());
                    int newY = coordinates.Item2 + move.GetY();
                    int newX = coordinates.Item1 + move.GetX();

                    if (newY < 8 && newX < 8 && newY > -1 && newX > -1 && move.GetIsActiv())
                    {
                        var borderMoves = new Border { BorderThickness = new Thickness(6), BorderBrush = Brushes.LimeGreen };
                        _board.MyGrids[newX, newY].Children.Add(borderMoves);
                    }
                }
                var border = new Border { BorderThickness = new Thickness(6), BorderBrush = Brushes.Yellow };
                _selectedPiece.GetPosition().Children.Add(border);
            }
            if (_selectedPiece != null)
            {
                _firstClick = false;
            }
        }
    }
}
