using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private Piece SelectedPiece;
        private bool _firstClick = true;

        private Game()
        {
            
        }

        public static Game GetInstance()
        {
            if (Instance == null)
            {
                Instance = new Game();
            }
            return Instance;
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
                if (!Equals(grid, SelectedPiece.GetPosition()))
                {
                }
                else
                {
                    foreach (var moves in SelectedPiece.GetMoveSet())
                    {
                        var iPanel = SelectedPiece.GetPosition().Parent as StackPanel;
                        var iparent = iPanel.Parent as StackPanel;
                        int myGrid = iPanel.Children.IndexOf(SelectedPiece.GetPosition()) + moves.GetY();
                        int myStack = iparent.Children.IndexOf(iPanel) + moves.GetX();

                        if (myGrid < 8 && myStack < 8 && myGrid > -1 && myStack > -1 && moves.GetIsActiv())
                        {
                            var i = iparent.Children[myStack] as StackPanel;
                            var x = i.Children[myGrid] as Grid;
                            if (x.Children.Count == 1)
                            {
                                x.Children.RemoveAt(0);
                            }
                            else
                            {
                                x.Children.RemoveAt(1);
                            }

                        }
                    }
                    SelectedPiece.GetPosition().Children.Clear();
                    SelectedPiece.GetPosition().Children.Add(SelectedPiece.GetPieceImage());
                    SelectedPiece = null;
                    _firstClick = true;
                }
            }
        }

        private void SelectPiece(Grid grid)
        {
            foreach (var piece in PieceList.Where(piece => Equals(piece.GetPosition(), grid)))
            {
                SelectedPiece = piece;
                piece.SetMoveSet();
                foreach (var move in piece.GetMoveSet())
                {
                    var iPanel = SelectedPiece.GetPosition().Parent as StackPanel;
                    var iparent = iPanel.Parent as StackPanel;
                    int myGrid = iPanel.Children.IndexOf(SelectedPiece.GetPosition()) + move.GetY();
                    int myStack = iparent.Children.IndexOf(iPanel) + move.GetX();

                    if (myGrid < 8 && myStack < 8 && myGrid > -1 && myStack > -1 && move.GetIsActiv())
                    {
                        var i = iparent.Children[myStack] as StackPanel;
                        var x = i.Children[myGrid] as Grid;

                        var borderMoves = new Border { BorderThickness = new Thickness(6), BorderBrush = Brushes.LimeGreen };
                        x.Children.Add(borderMoves);
                    }
                }
                var border = new Border { BorderThickness = new Thickness(6), BorderBrush = Brushes.Yellow };
                SelectedPiece.GetPosition().Children.Add(border);
            }
            if (SelectedPiece != null)
            {
                _firstClick = false;
            }
        }
    }
}
