using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess
{
    class NetworkHandler
    {
        private StreamReader _sr;
        private StreamWriter _sw;
        private bool _firstRequest = true;

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
            var sw = new Stopwatch();
            sw.Start();
            while (_firstRequest)
            {
                if (sw.Elapsed.Seconds >= 2)
                {
                    _sw.WriteLine(Game.GetInstance().GetPlayerColor());
                    sw.Restart();
                }
            }
            sw.Stop();
        }

        public void SetFirstRequestFalse()
        {
            while (_firstRequest)
            {
                _sr.ReadLine();
                _firstRequest = false;
            }
        }

        public void GetRequest()
        {
            string request = _sr.ReadLine();
            while (_firstRequest)
            {
                if (request == Piece.COLOR.White.ToString())
                {
                    Game.GetInstance().SetPlayerColor(Piece.COLOR.Black);
                    _firstRequest = false;
                    Game.GetInstance().Start();
                }
                else
                {
                    Game.GetInstance().SetPlayerColor(Piece.COLOR.White);
                    _firstRequest = false;
                    Game.GetInstance().Start();
                }
            }
        }

        public void SendRequest()
        {
            
        }
    }
}
