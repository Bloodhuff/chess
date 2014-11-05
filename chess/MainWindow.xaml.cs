using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace chess
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public static MainWindow Instance;
        public readonly List<Piece> PieceList = new List<Piece>();
        public Piece SelectedPiece;
        private Piece _removePiece;
        private bool _firstClick = true;
        public Grid MyGrid;
        public StackPanel MyGridstackPanel;
        public MainWindow()
        {
            Instance = this;
            InitializeComponent();
            Setup();
        }

        private void Setup()
        {
            PieceList.Add(new Piece("White", "Pawn", A2));
            PieceList.Add(new Piece("White", "Pawn", B2));
            PieceList.Add(new Piece("White", "Pawn", C2));
            PieceList.Add(new Piece("White", "Pawn", D2));
            PieceList.Add(new Piece("White", "Pawn", E2));
            PieceList.Add(new Piece("White", "Pawn", F2));
            PieceList.Add(new Piece("White", "Pawn", G2));
            PieceList.Add(new Piece("White", "Pawn", H2));
            PieceList.Add(new Piece("White", "Rook", A1));
            PieceList.Add(new Piece("White", "Knight", B1));
            PieceList.Add(new Piece("White", "Bishop", C1));
            PieceList.Add(new Piece("White", "Queen", D1));
            PieceList.Add(new Piece("White", "King", E1));
            PieceList.Add(new Piece("White", "Bishop", F1));
            PieceList.Add(new Piece("White", "Knight", G1));
            PieceList.Add(new Piece("White", "Rook", H1));
            PieceList.Add(new Piece("Black", "Pawn", A7));
            PieceList.Add(new Piece("Black", "Pawn", B7));
            PieceList.Add(new Piece("Black", "Pawn", C7));
            PieceList.Add(new Piece("Black", "Pawn", D7));
            PieceList.Add(new Piece("Black", "Pawn", E7));
            PieceList.Add(new Piece("Black", "Pawn", F7));
            PieceList.Add(new Piece("Black", "Pawn", G7));
            PieceList.Add(new Piece("Black", "Pawn", H7));
            PieceList.Add(new Piece("Black", "Rook", A8));
            PieceList.Add(new Piece("Black", "Knight", B8));
            PieceList.Add(new Piece("Black", "Bishop", C8));
            PieceList.Add(new Piece("Black", "Queen", D8));
            PieceList.Add(new Piece("Black", "King", E8));
            PieceList.Add(new Piece("Black", "Bishop", F8));
            PieceList.Add(new Piece("Black", "Knight", G8));
            PieceList.Add(new Piece("Black", "Rook", H8));
            foreach (var p in PieceList)
            {
                p.Position.Children.Add(p.Img);
            }
        }
        private void grid_Click(object sender, EventArgs eventArgs)
        {
            MyGrid = ((Grid)sender);
            if (_firstClick)
            {
                foreach (var piece in PieceList.Where(piece => Equals(piece.Position, MyGrid)))
                {
                    SelectedPiece = piece;
                    piece.MoveSet = new MoveSet(piece.Color, piece.PieceType, piece.HasMoved, piece.Position);
                    var border = new Border {BorderThickness = new Thickness(6), BorderBrush = Brushes.Yellow};
                    SelectedPiece.Position.Children.Add(border);
                }
                if (SelectedPiece != null)
                {
                    _firstClick = false;
                }
            }
            else
            {
                if (!Equals(MyGrid, SelectedPiece.Position))
                {
                    var myGridPanel = MyGrid.Parent as StackPanel;
                    if (myGridPanel != null)
                    {
                        MyGridstackPanel = myGridPanel.Parent as StackPanel;
                        var selectedPanel = SelectedPiece.Position.Parent as StackPanel;
                        if (selectedPanel != null)
                        {
                            var selectedstackPanel = selectedPanel.Parent as StackPanel;         
                            foreach (var moves in SelectedPiece.MoveSet.MovesList)
                            {
                                if (selectedstackPanel != null && (MyGridstackPanel != null && (((SelectedPiece != null && myGridPanel.Children.IndexOf(MyGrid) == (selectedPanel.Children.IndexOf(SelectedPiece.Position) + moves.H))) && MyGridstackPanel.Children.IndexOf(myGridPanel) == (selectedstackPanel.Children.IndexOf(selectedPanel) + moves.V))))
                                {
                                    foreach (var piece in PieceList.Where(piece => Equals(piece.Position, MyGrid)))
                                    {
                                        _removePiece = piece;
                                    }
                                    if (_removePiece != null)
                                    {
                                        PieceList.Remove(_removePiece);
                                        _removePiece.Position.Children.Clear();
                                        _removePiece = null;
                                    }
                                    SelectedPiece.Position.Children.Clear();
                                    SelectedPiece.HasMoved = true;
                                    SelectedPiece.Position = MyGrid;
                                    SelectedPiece.Position.Children.Add(SelectedPiece.Img);
                                    _firstClick = true;
                                    SelectedPiece = null;
                                }     
                            }
                        }
                    }
                }
                else
                {
                    SelectedPiece.Position.Children.Clear();
                    SelectedPiece.Position.Children.Add(SelectedPiece.Img);
                    SelectedPiece = null;
                    _firstClick = true;
                }
            }
        }
    }
}
