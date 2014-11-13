using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess
{
    public class Move
    {
        private readonly int _x;
        private readonly int _y;
        private bool _isActiv = true;

        public Move(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public int GetX()
        {
            return _x;
        }

        public int GetY()
        {
            return _y;
        }

        public void SetIsActiv(bool status)
        {
            _isActiv = status;
        }

        public bool GetIsActiv()
        {
            return _isActiv;
        }
    }
}
