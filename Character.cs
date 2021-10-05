using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Pac_Man2._0
{
    class Character
    {
        string direction;
        int startAngle, speed, previousX, previousY;
        Rectangle top, left, bottom, right, centre;
        SolidBrush colour;

        public Character(string _direction, int _startAngle, int _speed, SolidBrush _colour)
        {
            direction = _direction;
            startAngle = _startAngle;
            speed = _speed;
            colour = _colour;
        }

        public Character(string _direction, int _speed, SolidBrush _colour)
        {
            direction = _direction;
            speed = _speed;
            colour = _colour;
        }
    }
}
