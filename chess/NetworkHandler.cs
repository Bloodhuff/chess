using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace chess
{
    class NetworkHandler
    {
        private readonly StreamReader _sr;
        private readonly StreamWriter _sw;
        public bool FirstRequest = true;

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

        public void SetFirstRequestFalse()
        {
            while (FirstRequest)
            {
                _sr.ReadLine();
                FirstRequest = false;
            }
        }

        public string GetRequest()
        {
            return _sr.ReadLine();
        }

        public void SendRequest()
        {
            _sw.WriteLine("test");        
            _sw.Flush();
        }
    }
}
