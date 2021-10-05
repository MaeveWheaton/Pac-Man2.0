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
        public string direction;
        public int x, y, size, startAngle, speed, previousX, previousY;
        public Rectangle top, left, bottom, right, centre, entireCharacter;
        public SolidBrush colour;

        //for initializing the characters globally
        public Character()
        {

        }

        //for pacman
        public Character(string _direction, int _x, int _y, int _size, int _startAngle, int _speed, SolidBrush _colour)
        {
            x = _x;
            y = _y;
            size = _size;
            direction = _direction;
            startAngle = _startAngle;
            speed = _speed;
            colour = _colour;
        }

        //for ghosts
        public Character(string _direction, int _x, int _y, int _size, int _speed, SolidBrush _colour)
        {
            x = _x;
            y = _y;
            size = _size;
            direction = _direction;
            speed = _speed;
            colour = _colour;
        }

        public void Move()
        {
            switch (direction)
            {
                case "up":
                    y -= speed;
                    break;
                case "left":
                    x -= speed;
                    break;
                case "down":
                    y += speed;
                    break;
                case "right":
                    x += speed;
                    break;
            }

            previousX = x;
            previousY = y;
        }

        public void WallCollision(Rectangle r)
        {
            switch (direction)
            {
                case "up":
                    top = new Rectangle(x, y, size, 1);
                    if (top.IntersectsWith(r))
                    {
                        x = previousX;
                        y = previousY;
                        speed = 0;
                    }
                    break;
                case "left":
                    left = new Rectangle(x, y, 1, size);
                    if (left.IntersectsWith(r))
                    {
                        x = previousX;
                        y = previousY;
                        speed = 0;
                    }
                    break;
                case "down":
                    bottom = new Rectangle(x, y + 15, size, 1);
                    if (bottom.IntersectsWith(r))
                    {
                        x = previousX;
                        y = previousY;
                        speed = 0;
                    }
                    break;
                case "right":
                    right = new Rectangle(x + 15, y, 1, 20);
                    if (right.IntersectsWith(r))
                    {
                        x = previousX;
                        y = previousY;
                        speed = 0;
                    }
                    break;
            }
        }

        /// <summary>
        /// Checks for collision beteen PacMan and a pellet, adds points and removes pellet if there is a collision
        /// Also checks for collision with energizer and activates frightened mode
        /// </summary>
        public bool PelletCollision(Pellet p)
        {
            entireCharacter = new Rectangle(x, y, size, size);
            Rectangle pRec = new Rectangle(p.x, p.y, size, size);

            if (entireCharacter.IntersectsWith(pRec) && p.powerUp == true)
            {
                GameScreen.ghostFrightened = true;
                blinkyBrush.Color = Color.AliceBlue;
                score += 10;
                GameScreen.energizerTimer = 50;

                if (energizerTimer > 0)
                {
                    energizerTimer--;
                }
                else
                {
                    ghostFrightened = false;
                    blinkyBrush.Color = Color.Red;
                }
                return (true);
            }
            else if (entireCharacter.IntersectsWith(pRec))
            {
                GameScreen.score += 10;
                return (true);
            }
            else
            {
                return (false);
            }
        }

        public void FlipDirection(bool upArrowDown, bool rightArrowDown, bool downArrowDown, bool leftArrowDown)
        {
            switch (direction)
            {
                case "up":
                    if (downArrowDown == true)
                    {
                        direction = "down";
                        startAngle = 135;
                        speed = 10;
                    }
                    break;
                case "left":
                    if (rightArrowDown == true)
                    {
                        direction = "right";
                        startAngle = 45;
                        speed = 10;
                    }
                    break;
                case "down":
                    if (upArrowDown == true)
                    {
                        direction = "up";
                        startAngle = 315;
                        speed = 10;
                    }
                    break;
                case "right":
                    if (leftArrowDown == true)
                    {
                        direction = "left";
                        startAngle = 225;
                        speed = 10;
                    }
                    break;
            }
        }

        /// <summary>
        /// Checks current direction and changes direction if key is pressed while at a turn point, can always reverse, changes PacMan pie start angle to face direction of movement
        /// </summary>
        public void Turn(bool upArrowDown, bool rightArrowDown, bool downArrowDown, bool leftArrowDown, Pellet t)
        {
            centre = new Rectangle(x + 5, y + 5, 10, 10);
            Rectangle tRec = new Rectangle(t.x, t.y, t.size, t.size);

            switch (direction)
            {
                case "up":
                    if (leftArrowDown == true && centre.IntersectsWith(tRec))
                    {
                        direction = "left";
                        startAngle = 225;
                        speed = 10;
                    }
                    if (rightArrowDown == true && centre.IntersectsWith(tRec))
                    {
                        direction = "right";
                        startAngle = 45;
                        speed = 10;
                    }
                    break;
                case "left":
                    if (upArrowDown == true && centre.IntersectsWith(tRec))
                    {
                        direction = "up";
                        startAngle = 315;
                        speed = 10;
                    }
                    if (downArrowDown == true && centre.IntersectsWith(tRec))
                    {
                        direction = "down";
                        startAngle = 135;
                        speed = 10;
                    }
                    break;
                case "down":
                    if (leftArrowDown == true && centre.IntersectsWith(tRec))
                    {
                        direction = "left";
                        startAngle = 225;
                        speed = 10;
                    }
                    if (rightArrowDown == true && centre.IntersectsWith(tRec))
                    {
                        direction = "right";
                        startAngle = 45;
                        speed = 10;
                    }
                    break;
                case "right":
                    if (upArrowDown == true && centre.IntersectsWith(tRec))
                    {
                        direction = "up";
                        startAngle = 315;
                        speed = 10;
                    }
                    if (downArrowDown == true && centre.IntersectsWith(tRec))
                    {
                        direction = "down";
                        startAngle = 135;
                        speed = 10;
                    }
                    break;
            }
        }

        /// <summary>
        /// Moves pacman to the opposite side if touches the end of either tunnel
        /// </summary>
        public void TunnelTeleport(int width)
        {
            if (x == -5)
            {
                x = width - size - 5;
            }
            else if (x == width - 7)
            {
                x = 5;
            }
        }
    }
}
