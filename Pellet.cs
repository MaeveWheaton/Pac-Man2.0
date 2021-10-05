using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pac_Man2._0
{
    class Pellet
    {
        public int x, y, size;
        public bool powerUp;

        public Pellet(int _x, int _y, int _size, bool _powerUp)
        {
            x = _x;
            y = _y;
            size = _size;
            powerUp = _powerUp;
        }
    }
}
