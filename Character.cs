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
        public Rectangle top, left, bottom, right, centre, entireCharacter, cRec, tRec;
        public SolidBrush colour;

        Color frightenedColour = Color.AliceBlue;
        Color origionalColour;
        Random randGen = new Random();
        int newGhostDirection;

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
            origionalColour = colour.Color;
        }

        /// <summary>
        /// Moves character in current direction
        /// </summary>
        public void Move()
        {
            //track previous position in case of reset
            previousX = x;
            previousY = y;

            //move in current direction
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
        }

        /// <summary>
        /// Checks if character collides with a rectangle in current direction, if so sets speed = 0
        /// </summary>
        /// <param name="r"></param>rectangle
        public void WallCollision(Rectangle r)
        {
            switch (direction)
            {
                case "up":
                    top = new Rectangle(x, y, size, 1);
                    if (top.IntersectsWith(r))
                    {
                        PositionReset();
                    }
                    break;
                case "left":
                    left = new Rectangle(x, y, 1, size);
                    if (left.IntersectsWith(r))
                    {
                        PositionReset();
                    }
                    break;
                case "down":
                    bottom = new Rectangle(x, y + 15, size, 1);
                    if (bottom.IntersectsWith(r))
                    {
                        PositionReset();
                    }
                    break;
                case "right":
                    right = new Rectangle(x + 15, y, 1, 20);
                    if (right.IntersectsWith(r))
                    {
                        PositionReset();
                    }
                    break;
            }
        }

        /// <summary>
        /// Resets character to their previous position and set speed = 0
        /// </summary>
        public void PositionReset()
        {
            x = previousX;
            y = previousY;
            speed = 0;
        }

        /// <summary>
        /// Checks for collision beteen PacMan and a pellet, adds points and removes pellet if there is a collision
        /// Also checks for collision with energizer and activates frightened mode
        /// </summary>
        public bool PelletCollision(Pellet p)
        {
            //rectangles for collision
            entireCharacter = new Rectangle(x, y, size, size);
            Rectangle pRec = new Rectangle(p.x, p.y, p.size, p.size);

            //check collision
            if (entireCharacter.IntersectsWith(pRec) && p.powerUp == true) //collision with energizer
            {
                GameScreen.ghostFrightened = true;
                GameScreen.score += 10;
                GameScreen.energizerTimer = 100;
                return (true);
            }
            else if (entireCharacter.IntersectsWith(pRec)) //collision with normal pellet
            {
                GameScreen.score += 10;
                return (true);
            }
            else //no collision
            {
                return (false);
            }
        }

        /// <summary>
        /// Change colour of ghost while frightened
        /// </summary>
        public void GhostFrightened(bool frightened)
        {
            //change the colour of the ghosts while frightened
            if(frightened)
            {
                colour.Color = frightenedColour;
            }
            else
            {
                colour.Color = origionalColour;
            }
            
        }

        /// <summary>
        /// Changes to opposite direction of movement when key is pressed, does not allow turns
        /// </summary>
        /// <param name="upArrowDown"></param>bool of keyDown
        /// <param name="rightArrowDown"></param>bool of keyDown
        /// <param name="downArrowDown"></param>bool of keyDown
        /// <param name="leftArrowDown"></param>bool of keyDown
        public void FlipDirection(bool upArrowDown, bool rightArrowDown, bool downArrowDown, bool leftArrowDown)
        {
            //switch to opposite direction if key pressed
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
        /// Checks current direction and changes direction if key is pressed while at a turn point, changes PacMan pie start angle to face direction of movement
        /// </summary>
        public void Turn(bool upArrowDown, bool rightArrowDown, bool downArrowDown, bool leftArrowDown, Pellet t)
        {
            centre = new Rectangle(x + 5, y + 5, 10, 10);
            tRec = new Rectangle(t.x, t.y, t.size, t.size);

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
            if (x <= -5)
            {
                x = width - size - 5;
            }
            else if (x >= width - 7)
            {
                x = 5;
            }
        }

        /// <summary>
        /// Randomly decides new ghost direction
        /// </summary>
        /// <param name="t"></param>turnpoint
        public void AtonumousDirectionChange(Pellet t)
        {
            centre = new Rectangle(x + 5, y + 5, 10, 10);
            tRec = new Rectangle(t.x, t.y, t.size, t.size);

            //1 = up, 2 = left, 3 = down, 4 = right
            if (centre.IntersectsWith(tRec))
            {
                if (direction == "up")
                {
                    newGhostDirection = randGen.Next(2, 5);
                }
                else if (direction == "left")
                {
                    newGhostDirection = randGen.Next(1, 5);
                    while (newGhostDirection == 2)
                    {
                        newGhostDirection = randGen.Next(1, 5);
                    }
                }
                else if (direction == "down")
                {
                    newGhostDirection = randGen.Next(1, 4);
                }
                else if (direction == "right")
                {
                    newGhostDirection = randGen.Next(1, 5);
                    while (newGhostDirection == 4)
                    {
                        newGhostDirection = randGen.Next(1, 5);
                    }
                }

                SwitchGhostDirection();
            }
        }

        /// <summary>
        /// Changes ghost direction
        /// </summary>
        public void SwitchGhostDirection()
        {
            switch (newGhostDirection)
            {
                case 1:
                    direction = "up";
                    break;
                case 2:
                    direction = "left";
                    break;
                case 3:
                    direction = "down";
                    break;
                case 4:
                    direction = "right";
                    break;
            }

            speed = 10;
        }

        /// <summary>
        /// Checks if Pac-Man collides with a ghost in frightened mode, resets ghost and adds points
        /// </summary>
        public void PacManCollision(Character c)
        {
            entireCharacter = new Rectangle(x, y, size, size);
            cRec = new Rectangle(c.x, c.y, c.size, c.size);

            if (GameScreen.ghostFrightened == true && entireCharacter.IntersectsWith(cRec))
            {
                x = 205;
                y = 175;
                direction = "right";
                speed = 10;
                GameScreen.score += 400;
            }
        }

        /// <summary>
        /// Check if one character intersects with another character
        /// </summary>
        /// <param name="c"></param>character intersecting
        /// <returns></returns>true or false
        public bool IntersectsWith(Character c)
        {
            entireCharacter = new Rectangle(x, y, size, size);
            cRec = new Rectangle(c.x, c.y, c.size, c.size);

            if (entireCharacter.IntersectsWith(cRec))
            {
                return (true);
            }
            else
            {
                return (false);
            }
        }
    }
}
