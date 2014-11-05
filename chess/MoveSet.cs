using System.Collections.Generic;
using System.Windows.Controls;

namespace chess
{
    public class MoveSet
    {
        // ReSharper disable once ValueParameterNotUsed
        private string PieceType { set { } }
        private string Color { get; set; }
        public bool HasMoved;
        public readonly List<Moves> MovesList = new List<Moves>();
        public readonly MainWindow Main = MainWindow.Instance;
        public Grid Position;
        public MoveSet(string color, string pieceType, bool hasMoved, Grid position)
        {
            HasMoved = hasMoved;
            PieceType = pieceType;
            Color = color;
            Position = position;
            switch (pieceType)
            {
                case "Pawn":
                    Pawn();
                    break;
                case "Knight":
                    Knight();
                    break;
            }
        }

        void Pawn()
        {
            bool block = false;
            bool blockfirst = false;
            if (Color == "White")
            {
                var thisPanel = Position.Parent as StackPanel;
                foreach (var piece in Main.PieceList)
                {
                    var piecePanel = piece.Position.Parent as StackPanel;
                    if (piecePanel != null)
                    {
                        var piecePanelParent = piecePanel.Parent as StackPanel;
                        if (thisPanel != null)
                        {
                            var thisPanelParent = thisPanel.Parent as StackPanel;
                            if (piecePanelParent != null && (thisPanelParent != null && (((thisPanel.Children.IndexOf(Position) - 1) == piecePanel.Children.IndexOf(piece.Position)) && thisPanelParent.Children.IndexOf(thisPanel) == (piecePanelParent.Children.IndexOf(piecePanel)))))
                            {
                                block = true;
                            }
                            if (piecePanelParent != null && (thisPanelParent != null && (((thisPanel.Children.IndexOf(Position) - 2) == piecePanel.Children.IndexOf(piece.Position)) && thisPanelParent.Children.IndexOf(thisPanel) == (piecePanelParent.Children.IndexOf(piecePanel)))))
                            {
                                blockfirst = true;
                            }
                            if (Color != piece.Color && piecePanelParent != null && (thisPanelParent != null && (((thisPanel.Children.IndexOf(Position) - 1) == piecePanel.Children.IndexOf(piece.Position)) && (thisPanelParent.Children.IndexOf(thisPanel) - 1) == (piecePanelParent.Children.IndexOf(piecePanel)))))
                            {
                                MovesList.Add(new Moves(-1, -1));
                            }
                            if (Color != piece.Color && piecePanelParent != null && (thisPanelParent != null && (((thisPanel.Children.IndexOf(Position) - 1) == piecePanel.Children.IndexOf(piece.Position)) && (thisPanelParent.Children.IndexOf(thisPanel) + 1) == (piecePanelParent.Children.IndexOf(piecePanel)))))
                            {
                                MovesList.Add(new Moves(1, -1));
                            }
                        }
                    }
                }
                if (HasMoved == false && !block && !blockfirst)
                {
                    MovesList.Add(new Moves(0, -2));
                }
                if (!block)
                {
                    MovesList.Add(new Moves(0, -1));
                }
            }
            else
            {
                var thisPanel = Position.Parent as StackPanel;
                foreach (var piece in Main.PieceList)
                {
                    var piecePanel = piece.Position.Parent as StackPanel;
                    if (piecePanel != null)
                    {
                        var piecePanelParent = piecePanel.Parent as StackPanel;
                        if (thisPanel != null)
                        {
                            var thisPanelParent = thisPanel.Parent as StackPanel;
                            if (piecePanelParent != null && (thisPanelParent != null && (((thisPanel.Children.IndexOf(Position) + 1) == piecePanel.Children.IndexOf(piece.Position)) && thisPanelParent.Children.IndexOf(thisPanel) == (piecePanelParent.Children.IndexOf(piecePanel)))))
                            {
                                block = true;
                            }
                            if (piecePanelParent != null && (thisPanelParent != null && (((thisPanel.Children.IndexOf(Position) + 2) == piecePanel.Children.IndexOf(piece.Position)) && thisPanelParent.Children.IndexOf(thisPanel) == (piecePanelParent.Children.IndexOf(piecePanel)))))
                            {
                                blockfirst = true;
                            }
                            if (Color != piece.Color && piecePanelParent != null && (thisPanelParent != null && (((thisPanel.Children.IndexOf(Position) + 1) == piecePanel.Children.IndexOf(piece.Position)) && (thisPanelParent.Children.IndexOf(thisPanel) - 1) == (piecePanelParent.Children.IndexOf(piecePanel)))))
                            {
                                MovesList.Add(new Moves(-1, 1));
                            }
                            if (Color != piece.Color && piecePanelParent != null && (thisPanelParent != null && (((thisPanel.Children.IndexOf(Position) + 1) == piecePanel.Children.IndexOf(piece.Position)) && (thisPanelParent.Children.IndexOf(thisPanel) + 1) == (piecePanelParent.Children.IndexOf(piecePanel)))))
                            {
                                MovesList.Add(new Moves(1, 1));
                            }
                        }
                    }
                }
                if (HasMoved == false && !block && !blockfirst)
                {
                    MovesList.Add(new Moves(0, 2));
                }
                if (!block)
                {
                    MovesList.Add(new Moves(0, 1));
                }
            }
        }

        void Knight()
        {
            var move1 = new Moves(2, 1);
            var move2 = new Moves(2, -1);
            var move3 = new Moves(-2, 1);
            var move4 = new Moves(-2, -1);
            var move5 = new Moves(1, 2);
            var move6 = new Moves(1, -2);
            var move7 = new Moves(-1, 2);
            var move8 = new Moves(-1, -2);
            MovesList.Add(move1);
            MovesList.Add(move2);
            MovesList.Add(move3);
            MovesList.Add(move4);
            MovesList.Add(move5);
            MovesList.Add(move6);
            MovesList.Add(move7);
            MovesList.Add(move8);
            var thisPanel = Position.Parent as StackPanel;
            foreach (var piece in Main.PieceList)
            {
                var piecePanel = piece.Position.Parent as StackPanel;
                if (piecePanel != null)
                {
                    var piecePanelParent = piecePanel.Parent as StackPanel;
                    if (thisPanel != null)
                    {
                        var thisPanelParent = thisPanel.Parent as StackPanel;
                        if (Color == piece.Color && (thisPanel.Children.IndexOf(Position) + 1) == piecePanel.Children.IndexOf(piece.Position) && (thisPanelParent.Children.IndexOf(thisPanel) + 2== piecePanelParent.Children.IndexOf((piecePanel))))
                        {
                            MovesList.Remove(move1);
                        }
                        if (Color == piece.Color && (thisPanel.Children.IndexOf(Position) - 1) == piecePanel.Children.IndexOf(piece.Position) && (thisPanelParent.Children.IndexOf(thisPanel) + 2 == piecePanelParent.Children.IndexOf((piecePanel))))
                        {
                            MovesList.Remove(move2);
                        }
                        if (Color == piece.Color && (thisPanel.Children.IndexOf(Position) + 1) == piecePanel.Children.IndexOf(piece.Position) && (thisPanelParent.Children.IndexOf(thisPanel) - 2 == piecePanelParent.Children.IndexOf((piecePanel))))
                        {
                            MovesList.Remove(move3);
                        }
                        if (Color == piece.Color && (thisPanel.Children.IndexOf(Position) - 1) == piecePanel.Children.IndexOf(piece.Position) && (thisPanelParent.Children.IndexOf(thisPanel) - 2 == piecePanelParent.Children.IndexOf((piecePanel))))
                        {
                            MovesList.Remove(move4);
                        }
                        if (Color == piece.Color && (thisPanel.Children.IndexOf(Position) + 2) == piecePanel.Children.IndexOf(piece.Position) && (thisPanelParent.Children.IndexOf(thisPanel) + 1 == piecePanelParent.Children.IndexOf((piecePanel))))
                        {
                            MovesList.Remove(move5);
                        }
                        if (Color == piece.Color && (thisPanel.Children.IndexOf(Position) - 2) == piecePanel.Children.IndexOf(piece.Position) && (thisPanelParent.Children.IndexOf(thisPanel) + 1 == piecePanelParent.Children.IndexOf((piecePanel))))
                        {
                            MovesList.Remove(move6);
                        }
                        if (Color == piece.Color && (thisPanel.Children.IndexOf(Position) + 2) == piecePanel.Children.IndexOf(piece.Position) && (thisPanelParent.Children.IndexOf(thisPanel) - 1 == piecePanelParent.Children.IndexOf((piecePanel))))
                        {
                            MovesList.Remove(move7);
                        }
                        if (Color == piece.Color && (thisPanel.Children.IndexOf(Position) - 2) == piecePanel.Children.IndexOf(piece.Position) && (thisPanelParent.Children.IndexOf(thisPanel) - 1 == piecePanelParent.Children.IndexOf((piecePanel))))
                        {
                            MovesList.Remove(move8);
                        }
                    }
                }
            }

        }
    }
}
