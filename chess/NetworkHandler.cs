using System;
using System.IO;

namespace chess
{
    class NetworkHandler
    {
        private readonly StreamReader _sr;
        private readonly StreamWriter _sw;
        private readonly Board _board = Board.Instance;

        public NetworkHandler(Stream ns)
        {
            _sr = new StreamReader(ns);
            _sw = new StreamWriter(ns);
        }

        public void GetPlayerColor()
        {
            string request = _sr.ReadLine();
            if (request == Piece.COLOR.White.ToString())
            {
                Game.GetInstance().SetPlayerColor(Piece.COLOR.Black);
            }
            else
            {
                Game.GetInstance().SetPlayerColor(Piece.COLOR.White);
            }
        }

        public void SendPlayerColor()
        {
            _sw.WriteLine(Game.GetInstance().GetPlayerColor().ToString());
            _sw.Flush();
        }

        public void GetRequest()
        {
            while (true)
            {
                var readLine = _sr.ReadLine();
                if (readLine != null)
                {
                    string[] request = readLine.Split(',');
                    int pieceX = Convert.ToInt32(request[0]);
                    int pieceY = Convert.ToInt32(request[1]);
                    int moveToX = Convert.ToInt32(request[2]);
                    int moveToY = Convert.ToInt32(request[3]);
                    Game.GetInstance().SelectPiece(_board.MyBoarderArray[pieceX, pieceY]);
                    Game.GetInstance().MakeMove(_board.MyBoarderArray[moveToX, moveToY]);
                }
            }
        }

        public void SendRequest(string request)
        {
            _sw.WriteLine(request);
            _sw.Flush();
        }
    }
}
