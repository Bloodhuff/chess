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
                case "Rook":
                    Rook();
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
            MovesList.Add(new Moves(2, 1));
            MovesList.Add(new Moves(2, -1));
            MovesList.Add(new Moves(-2, 1));
            MovesList.Add(new Moves(-2, -1));
            MovesList.Add(new Moves(1, 2));
            MovesList.Add(new Moves(1, -2));
            MovesList.Add(new Moves(-1, 2));
            MovesList.Add(new Moves(-1, -2));
            var thisPanel = Position.Parent as StackPanel;
            foreach (var piece in Main.PieceList)
            {
                var piecePanel = piece.Position.Parent as StackPanel;
                if (piecePanel != null)
                {
                    var piecePanelParent = piecePanel.Parent as StackPanel;
                    if (thisPanel != null)
                    {
                        foreach (var movese in MovesList)
                        {
                            var thisPanelParent = thisPanel.Parent as StackPanel;
                            if (thisPanelParent != null && (piecePanelParent != null && (Color == piece.Color && (thisPanel.Children.IndexOf(Position) + movese.H) == piecePanel.Children.IndexOf(piece.Position) && (thisPanelParent.Children.IndexOf(thisPanel) + movese.V == piecePanelParent.Children.IndexOf((piecePanel))))))
                            {
                                movese.Activ = false;
                            }
                        }
                    }
                }
            }
        }

        void Rook()
        {
            for (int i = 0; i < 8; i++)
            {
                MovesList.Add(new Moves(0, i));
            }
            for (int i = 0; i < 8; i++)
            {
                MovesList.Add(new Moves(i, 0));
            }
            for (int i = 0; i > -8; i--)
            {
                MovesList.Add(new Moves(i, 0));
            }
            for (int i = 0; i > -8; i--)
            {
                MovesList.Add(new Moves(0, i));
            }
            var thisPanel = Position.Parent as StackPanel;
            foreach (var piece in Main.PieceList)
            {
                bool blockedHPositiv = false;
                bool blockedHNegativ = false;
                bool blockedVPositiv = false;
                bool blockedVNegativ = false;
                var piecePanel = piece.Position.Parent as StackPanel;
                if (piecePanel != null)
                {
                    var piecePanelParent = piecePanel.Parent as StackPanel;
                    if (thisPanel != null)
                    {
                        foreach (var movese in MovesList)
                        {
                            var thisPanelParent = thisPanel.Parent as StackPanel;
                            if (movese.H > 0)
                            {
                                if (thisPanelParent != null && (piecePanelParent != null && ((thisPanel.Children.IndexOf(Position) + movese.H) == piecePanel.Children.IndexOf(piece.Position) && (thisPanelParent.Children.IndexOf(thisPanel) + movese.V == piecePanelParent.Children.IndexOf((piecePanel))))) || blockedHPositiv)
                                {
                                    if (Color == piece.Color || blockedHPositiv)
                                    {
                                        movese.Activ = false;     
                                    }
                                    blockedHPositiv = true;
                                }   
                            }
                            if (movese.V > 0)
                            {
                                if (thisPanelParent != null && (piecePanelParent != null && ((thisPanel.Children.IndexOf(Position) + movese.H) == piecePanel.Children.IndexOf(piece.Position) && (thisPanelParent.Children.IndexOf(thisPanel) + movese.V == piecePanelParent.Children.IndexOf((piecePanel))))) || blockedVPositiv)
                                {
                                    if (Color == piece.Color || blockedVPositiv)
                                    {
                                        movese.Activ = false;
                                    }
                                    blockedVPositiv = true;
                                }
                            }
                            if (movese.H < 0)
                            {
                                if (thisPanelParent != null && (piecePanelParent != null && ((thisPanel.Children.IndexOf(Position) + movese.H) == piecePanel.Children.IndexOf(piece.Position) && (thisPanelParent.Children.IndexOf(thisPanel) + movese.V == piecePanelParent.Children.IndexOf((piecePanel))))) || blockedHNegativ)
                                {
                                    if (Color == piece.Color || blockedHNegativ)
                                    {
                                        movese.Activ = false;
                                    }
                                    blockedHNegativ = true;
                                }
                            }
                            if (movese.V < 0)
                            {
                                if (thisPanelParent != null && (piecePanelParent != null && ((thisPanel.Children.IndexOf(Position) + movese.H) == piecePanel.Children.IndexOf(piece.Position) && (thisPanelParent.Children.IndexOf(thisPanel) + movese.V == piecePanelParent.Children.IndexOf((piecePanel))))) || blockedVNegativ)
                                {
                                    if (Color == piece.Color || blockedVNegativ)
                                    {
                                        movese.Activ = false;
                                    }
                                    blockedVNegativ = true;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
