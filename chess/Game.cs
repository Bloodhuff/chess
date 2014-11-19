using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace chess
{
    class Game
    {
        public static Game Instance = null;
        private SynchronizationContext _mainThread;
        private readonly Board _board = Board.Instance;
        public readonly List<Piece> PieceList = new List<Piece>();
        private Piece _selectedPiece;
        private bool _firstClick = true;
        private Piece.COLOR _playerColor;
        private Piece.COLOR _playerTurn = Piece.COLOR.White;
        private Stream _ns;
        private NetworkHandler _nh;

        private Game()
        {
        }

        public static Game GetInstance()
        {
            return Instance ?? (Instance = new Game());
        }

        public void SetMainThread(SynchronizationContext thread)
        {
            _mainThread = thread;
        }

        public void HostMatch(Piece.COLOR playerColor)
        {
            _playerColor = playerColor;
            var serverSocket = new TcpListener(IPAddress.Any, 65080);
            serverSocket.Start();
            var connectionSocket = serverSocket.AcceptSocket();
            _ns = new NetworkStream(connectionSocket);
            _nh = new NetworkHandler(_ns);
            _nh.SendPlayerColor();
            Start();
        }

        public void JoinMatch(string ip)
        {
            try
            {
                if (ip == "")
                {
                    ip = "localhost";
                }
                var clientSocket = new TcpClient(ip, 65080);
                _ns = clientSocket.GetStream();
                _nh = new NetworkHandler(_ns);
            }
            catch (Exception)
            {
                _board.TypeIP.Visibility = Visibility.Visible;
                _board.Gameboard.Visibility = Visibility.Hidden;
            }
            if (_ns != null)
            {
                _board.MyLabel.Content = "Connected to: " + ip;
                _nh.GetPlayerColor();
                Start();
            }
        }
        public void Start()
        {
            for (int i = 0; i < 8; i++)
            {
                PieceList.Add(new Pawn(Piece.COLOR.White, _board.MyBoarderArray[i, 6]));
                PieceList.Add(new Pawn(Piece.COLOR.Black, _board.MyBoarderArray[i, 1]));
            }
            PieceList.Add(new Rook(Piece.COLOR.White, _board.MyBoarderArray[0, 7]));
            PieceList.Add(new Knight(Piece.COLOR.White, _board.MyBoarderArray[1, 7]));
            PieceList.Add(new Bishop(Piece.COLOR.White, _board.MyBoarderArray[2, 7]));
            PieceList.Add(new Queen(Piece.COLOR.White, _board.MyBoarderArray[3, 7]));
            PieceList.Add(new King(Piece.COLOR.White, _board.MyBoarderArray[4, 7]));
            PieceList.Add(new Bishop(Piece.COLOR.White, _board.MyBoarderArray[5, 7]));
            PieceList.Add(new Knight(Piece.COLOR.White, _board.MyBoarderArray[6, 7]));
            PieceList.Add(new Rook(Piece.COLOR.White, _board.MyBoarderArray[7, 7]));
            PieceList.Add(new Rook(Piece.COLOR.Black, _board.MyBoarderArray[0, 0]));
            PieceList.Add(new Knight(Piece.COLOR.Black, _board.MyBoarderArray[1, 0]));
            PieceList.Add(new Bishop(Piece.COLOR.Black, _board.MyBoarderArray[2, 0]));
            PieceList.Add(new Queen(Piece.COLOR.Black, _board.MyBoarderArray[3, 0]));
            PieceList.Add(new King(Piece.COLOR.Black, _board.MyBoarderArray[4, 0]));
            PieceList.Add(new Bishop(Piece.COLOR.Black, _board.MyBoarderArray[5, 0]));
            PieceList.Add(new Knight(Piece.COLOR.Black, _board.MyBoarderArray[6, 0]));
            PieceList.Add(new Rook(Piece.COLOR.Black, _board.MyBoarderArray[7, 0]));

            foreach (var p in PieceList)
            {
                var grid = p.GetPosition();
                var image = p.GetPieceImage();
                grid.Children.Add(image);
            }
            if (_playerColor == Piece.COLOR.Black)
            {
                _board.GameBoardTransform.Angle = 180;
            }
            new Thread(_nh.GetRequest).Start();
        }

        public Piece.COLOR GetPlayerColor()
        {
            return _playerColor;
        }

        public void SetPlayerColor(Piece.COLOR playercolor)
        {
            _playerColor = playercolor;
        }

        public void HandelClick(Grid grid)
        {
            if (_playerTurn == _playerColor)
            {
                if (_firstClick)
                {
                    SelectPiece(grid);
                }
                else
                {
                    if (!Equals(grid, _selectedPiece.GetPosition()))
                    {
                        MakeMove(grid);
                    }
                    else
                    {
                        DeselectPiece();
                    }
                }
            }
        }

        public void MakeMove(Grid grid)
        {
            var gridcoordinates = _board.GetArrayPosition(grid);
            var selectedPieceCoordinates = _board.GetArrayPosition(_selectedPiece.GetPosition());
            string request = selectedPieceCoordinates.Item1 + "," + selectedPieceCoordinates.Item2 + "," + gridcoordinates.Item1 + "," + gridcoordinates.Item2;
            foreach (var move in _selectedPiece.GetMoveSet())
            {
                if (gridcoordinates.Item1 == (selectedPieceCoordinates.Item1 + move.GetX()) && gridcoordinates.Item2 == (selectedPieceCoordinates.Item2 + move.GetY()) && move.GetIsActiv())
                {
                    if (_selectedPiece != null)
                    {
                        _mainThread.Send(state => _selectedPiece.GetPosition().Children.Clear(), null);
                        foreach (var move1 in _selectedPiece.GetMoveSet())
                        {
                            var coordinates = _board.GetArrayPosition(_selectedPiece.GetPosition());
                            int newY = coordinates.Item2 + move1.GetY();
                            int newX = coordinates.Item1 + move1.GetX();
                            if (newY < 8 && newX < 8 && newY > -1 && newX > -1 && move1.GetIsActiv())
                            {
                                _mainThread.Send(state =>
                                {
                                    if (_board.MyBoarderArray[newX, newY].Children.Count != 0)
                                    {
                                        _board.MyBoarderArray[newX, newY].Children.RemoveAt(
                                            _board.MyBoarderArray[newX, newY].Children.Count == 1
                                                ? 0
                                                : 1);
                                    }
                                }, null);
                            }
                        }
                        Piece removePiece = null;
                        foreach (var piece in PieceList.Where(piece => Equals(piece.GetPosition(), grid)))
                        {
                            removePiece = piece;
                        }
                        if ((gridcoordinates.Item2 - 1) > -1 && (gridcoordinates.Item2 + 1) < 8)
                        {
                            foreach (var piece in PieceList.Where(piece => (Equals(_board.MyBoarderArray[gridcoordinates.Item1, (gridcoordinates.Item2 - 1)], piece.GetPosition()) && _selectedPiece.GetColor() == Piece.COLOR.Black) || (Equals(_board.MyBoarderArray[gridcoordinates.Item1, (gridcoordinates.Item2 + 1)], piece.GetPosition()) && _selectedPiece.GetColor() == Piece.COLOR.White)))
                            {
                                if (piece is Pawn)
                                {
                                    var pawn = (Pawn)piece;
                                    if (pawn.GetPassantAble() && _selectedPiece is Pawn)
                                    {
                                        removePiece = piece;
                                    }
                                }
                            }
                        }
                        if (removePiece != null)
                        {
                            PieceList.Remove(removePiece);
                            _mainThread.Send(state => removePiece.GetPosition().Children.Clear(), null);
                        }
                        foreach (var piece in PieceList)
                        {
                            if (piece is Pawn)
                            {
                                var pawn = (Pawn)piece;
                                pawn.SetPassantAble(false);
                            }
                        }
                        if (_selectedPiece is Pawn)
                        {
                            var pawn = (Pawn)_selectedPiece;
                            pawn.SethasMoved();
                            if ((selectedPieceCoordinates.Item2 - 2) > -1 && (selectedPieceCoordinates.Item2 + 2) < 8)
                            {
                                if (_selectedPiece.GetColor() == Piece.COLOR.White)
                                {
                                    if (Equals(_board.MyBoarderArray[selectedPieceCoordinates.Item1, (selectedPieceCoordinates.Item2 - 2)], grid))
                                    {
                                        pawn.SetPassantAble(true);
                                    }
                                }
                                else
                                {
                                    if (Equals(_board.MyBoarderArray[selectedPieceCoordinates.Item1, (selectedPieceCoordinates.Item2 + 2)], grid))
                                    {
                                        pawn.SetPassantAble(true);
                                    }
                                }
                            }
                            foreach (var grid1 in _board.BackRowList)
                            {
                                if (Equals(grid, grid1))
                                {
                                    PieceList.Remove(_selectedPiece);
                                    _mainThread.Send(state =>
                                    {
                                        _selectedPiece = new Queen(_playerTurn, grid);
                                        PieceList.Add(_selectedPiece);
                                    }, null);
                                }
                            }
                        }
                        else if (_selectedPiece is Rook)
                        {
                            var rook = (Rook)_selectedPiece;
                            rook.SethasMoved();
                        }
                        else if (_selectedPiece is King)
                        {
                            var king = (King)_selectedPiece;
                            king.SethasMoved();
                        }
                        _selectedPiece.SetPosition(grid);
                        _mainThread.Send(state => _selectedPiece.GetPosition().Children.Add(_selectedPiece.GetPieceImage()), null);
                        _firstClick = true;
                    }
                    _selectedPiece = null;
                    if (_playerTurn == _playerColor)
                    {
                        _nh.SendRequest(request);
                    }
                    _playerTurn = _playerTurn == Piece.COLOR.White ? Piece.COLOR.Black : Piece.COLOR.White;
                }
            }
        }

        private void DeselectPiece()
        {
            foreach (var move in _selectedPiece.GetMoveSet())
            {
                var coordinates = _board.GetArrayPosition(_selectedPiece.GetPosition());
                int newY = coordinates.Item2 + move.GetY();
                int newX = coordinates.Item1 + move.GetX();
                if (newY < 8 && newX < 8 && newY > -1 && newX > -1 && move.GetIsActiv())
                {
                    _mainThread.Send(
                        state =>
                            _board.MyBoarderArray[newX, newY].Children.RemoveAt(
                                _board.MyBoarderArray[newX, newY].Children.Count == 1
                                    ? 0
                                    : 1), null);
                }
            }
            _mainThread.Send(state =>
            {
                _selectedPiece.GetPosition().Children.Clear();
                _selectedPiece.GetPosition().Children.Add(_selectedPiece.GetPieceImage());
            }, null);
            _selectedPiece = null;
            _firstClick = true;
        }
        public void SelectPiece(Grid grid)
        {
            foreach (var piece in PieceList.Where(piece => Equals(piece.GetPosition(), grid)))
            {
                if (piece.GetColor() == _playerTurn)
                {
                    _selectedPiece = piece;
                    if (piece is Pawn)
                    {
                        var pawn = (Pawn)piece;
                        pawn.SetMoveSet();
                    }
                    else if (piece is Rook)
                    {
                        var pawn = (Rook)piece;
                        pawn.SetMoveSet();
                    }
                    else if (piece is King)
                    {
                        var pawn = (King)piece;
                        pawn.SetMoveSet();
                    }
                    else
                    {
                        piece.SetMoveSet();
                    }
                    foreach (var move in piece.GetMoveSet())
                    {
                        var coordinates = _board.GetArrayPosition(_selectedPiece.GetPosition());
                        int newY = coordinates.Item2 + move.GetY();
                        int newX = coordinates.Item1 + move.GetX();
                        if (newY < 8 && newX < 8 && newY > -1 && newX > -1 && move.GetIsActiv())
                        {
                            _mainThread.Send(state =>
                            {
                                var borderMoves = new Border { BorderThickness = new Thickness(6), BorderBrush = Brushes.LimeGreen };
                                _board.MyBoarderArray[newX, newY].Children.Add(borderMoves);
                            }, null);

                        }
                    }
                    _mainThread.Send(state =>
                    {
                        var border = new Border { BorderThickness = new Thickness(6), BorderBrush = Brushes.Yellow };
                        _selectedPiece.GetPosition().Children.Add(border);
                    }, null);
                }
            }
            if (_selectedPiece != null)
            {
                _firstClick = false;
            }
        }
    }
}
