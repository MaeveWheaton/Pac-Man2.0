using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pac_Man2._0
{
    public partial class GameScreen : UserControl
    {
        List<Pellet> pellets = new List<Pellet>();
        List<Pellet> toRemovePellets = new List<Pellet>();
        List<Pellet> turnPoints = new List<Pellet>();
        //List<Character> ghosts = new List<Character>();
        List<Rectangle> walls = new List<Rectangle>();

        //player control variables
        public static bool upArrowDown;
        public static bool leftArrowDown;
        public static bool downArrowDown;
        public static bool rightArrowDown;

        //create characters
        Character pacMan = new Character();
        Character blinky = new Character();
        Character pinky = new Character();
        Character inky = new Character();
        Character clyde = new Character();
        int pacManPreviousX, pacManPreviousY;
        public static bool ghostFrightened;

        //game variables
        public static int score;
        int time;
        bool removePellet;
        public static int energizerTimer;

        //brushes for ghosts and walls
        SolidBrush blinkyBrush = new SolidBrush(Color.Red);
        SolidBrush pinkyBrush = new SolidBrush(Color.Pink);
        SolidBrush inkyBrush = new SolidBrush(Color.LightBlue);
        SolidBrush clydeBrush = new SolidBrush(Color.Orange);
        SolidBrush wallBrush = new SolidBrush(Color.DodgerBlue);

        public GameScreen()
        {
            InitializeComponent();
            GameInit();
        }

        /// <summary>
        /// Initialize game
        /// </summary>
        public void GameInit()
        {
            //start music
            Form1.backgroundMusic.Play();

            //reset time and score
            //score = 0;
            //time = 850;

            //set up screen objects
            SetWalls();
            SetTurnPoints();
            SetPellets();
            //pellets = pelletsOrigins;

            //set up Pac-Man
            pacMan = new Character("left", 205, 335, 20, 225, 10, Form1.pacManBrush);

            //set up ghosts
            blinky = new Character("right", 205, 175, 20, 10, blinkyBrush);
            pinky = new Character("right", 205, 215, 20, 0, pinkyBrush);
            inky = new Character("right", 165, 215, 20, 0, inkyBrush);
            clyde = new Character("right", 245, 215, 20, 0, clydeBrush);

            ghostFrightened = false;

            //StartCountdown();

            gameTimer.Enabled = true;
        }

        /*/// <summary>
        /// Displays a countdown from 3 to GO
        /// </summary>
        public void StartCountdown()
        {
            countDownLabel.Visible = true;
            countDownLabel.Text = "3";
            Refresh();
            Thread.Sleep(1000);

            countDownLabel.Text = "2";
            Refresh();
            Thread.Sleep(1000);

            countDownLabel.Text = "1";
            Refresh();
            Thread.Sleep(1000);

            countDownLabel.Text = "GO!";
            Refresh();
            Thread.Sleep(1000);
            countDownLabel.Visible = false;
        }*/

        private void GameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                /*second player controls case Keys.W:
                    wDown = true;
                    break;
                case Keys.A:
                    aDown = true;
                    break;
                case Keys.S:
                    sDown = true;
                    break;
                case Keys.D:
                    dDown = true;
                    break;*/
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;
                    /*go to choose gamemode screen case Keys.B:
                        if (gameState == "waiting" || gameState == "over")
                        {
                            gameState = "waiting";
                            gameMode = "undefined";

                            //add buttons
                            p1Button.Visible = true;
                            p1Button.Enabled = true;
                            p2Button.Visible = true;
                            p2Button.Enabled = true;
                            Refresh();
                        }
                        break;*/
            }
        }

        private void GameScreen_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                /*second player controls case Keys.W:
                    wDown = false;
                    break;
                case Keys.A:
                    aDown = false;
                    break;
                case Keys.S:
                    sDown = false;
                    break;
                case Keys.D:
                    dDown = false;
                    break;*/
                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
            }
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            //move pacman in current direction
            pacMan.Move();

            //check for collision with wall in current direction
            foreach(Rectangle w in walls)
            {
                pacMan.WallCollision(w);
            }

            //flip up/down or left/right
            pacMan.FlipDirection(upArrowDown, rightArrowDown, downArrowDown, leftArrowDown);

            //check for turn at a corner
            foreach (Pellet t in turnPoints)
            {
                pacMan.Turn(upArrowDown, rightArrowDown, downArrowDown, leftArrowDown, t);
            }

            //check if pacman touches the end of a tunnel
            pacMan.TunnelTeleport(this.Width);

            //check if pacman collides with a pellet or energizer, if so remove pellet
            foreach (Pellet p in pellets)
            {
                removePellet = pacMan.PelletCollision(p);
                if(removePellet == true)
                {
                    toRemovePellets.Add(p);
                }
            }

            foreach (Pellet p in toRemovePellets)
            {
                pellets.Remove(p);
            }

            //change visuals if ghost frightened
            if(ghostFrightened == true)
            {
                blinky.GhostFrightened();
            }
            
            //move in current direction
            blinky.Move();

            //check for change in direction
            foreach(Pellet t in turnPoints)
            {
                blinky.AtonumousDirectionChange(t);
            }

            //check for collision with wall in current direction
            foreach(Rectangle w in walls)
            {
                blinky.WallCollision(w);
            }

            //reverse ghost direction if it goes down the tunnel
            if (blinky.x <= 0 || blinky.x >= this.Width - blinky.size)
            {
                if (blinky.direction == "left")
                {
                    blinky.direction = "right";
                }
                else
                {
                    blinky.direction = "left";
                }
            }

            //decrease time
            time--;

            //check for pacman & ghost collision in frightened mode
            blinky.PacManCollision(pacMan);

            //check for end game
            CheckEndConditions();

            Refresh();
        }

        /// <summary>
        /// Ends game if time is up, all the pellet are gone, or blinky catches pacman
        /// </summary>
        public void CheckEndConditions()
        {
            if (time == 0)
            {
                //outcome = "lose";
                gameTimer.Enabled = false;

                //change to game screen
                Form f = this.FindForm();
                f.Controls.Remove(this);

                MainScreen gs = new MainScreen();
                f.Controls.Add(gs);

                //centre screen on the form
                gs.Location = new Point((f.Width - gs.Width) / 2, (f.Height - gs.Height) / 2);
            }
            else if (blinky.IntersectsWith(pacMan) && ghostFrightened == false)
            {
                //outcome = "p2win";
                gameTimer.Enabled = false;

                //change to game screen
                Form f = this.FindForm();
                f.Controls.Remove(this);

                MainScreen gs = new MainScreen();
                f.Controls.Add(gs);

                //centre screen on the form
                gs.Location = new Point((f.Width - gs.Width) / 2, (f.Height - gs.Height) / 2);
            }
            else if (pellets.Count() == 0)
            {
                //outcome = "p1win";
                gameTimer.Enabled = false;

                //change to game screen
                Form f = this.FindForm();
                f.Controls.Remove(this);

                MainScreen gs = new MainScreen();
                f.Controls.Add(gs);

                //centre screen on the form
                gs.Location = new Point((f.Width - gs.Width) / 2, (f.Height - gs.Height) / 2);
            }
        }

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            //update score
            //scoreLabel.Text = $"SCORE: {score}";

            //draw walls
            for (int i = 0; i < walls.Count(); i++)
            {
                e.Graphics.FillRectangle(wallBrush, walls[i]);
            }

            //draw pellets
            foreach(Pellet p in pellets)
            {
                if(p.powerUp == false)
                {
                    e.Graphics.FillRectangle(Form1.pelletsBrush, p.x, p.y, p.size, p.size);
                }
                else
                {
                    e.Graphics.FillEllipse(Form1.pelletsBrush, p.x, p.y, p.size, p.size);
                }
            }

            //draw pacman
            e.Graphics.FillPie(pacMan.colour, pacMan.x, pacMan.y, pacMan.size, pacMan.size, pacMan.startAngle, 270);

            //draw ghosts
            e.Graphics.FillEllipse(blinkyBrush, blinky.x, blinky.y, blinky.size, blinky.size);
            e.Graphics.FillEllipse(pinkyBrush, pinky.x, pinky.y, pinky.size, pinky.size);
            e.Graphics.FillEllipse(inkyBrush, inky.x, inky.y, inky.size, inky.size);
            e.Graphics.FillEllipse(clydeBrush, clyde.x, clyde.y, clyde.size, clyde.size);

            //update time
            //timeLabel.Text = $"TIME LEFT: {time}";
        }


        /// <summary>
        /// Adds all the wall rectangles to the walls list
        /// </summary>
        public void SetWalls()
        {
            int outsideWallWeight = 5;
            //outside walls from the top counterclockwise (around in the left direction)
            walls.Add(new Rectangle(5, 35, this.Width - 10, outsideWallWeight)); //top
            walls.Add(new Rectangle(5, 35, outsideWallWeight, 130));
            walls.Add(new Rectangle(5, 160, 75, outsideWallWeight));
            walls.Add(new Rectangle(75, 160, outsideWallWeight, 45));
            walls.Add(new Rectangle(0, 205, 80, outsideWallWeight)); //left top tunnel wall
            walls.Add(new Rectangle(0, 240, 80, outsideWallWeight)); //left bottom tunnel wall
            walls.Add(new Rectangle(75, 240, outsideWallWeight, 45));
            walls.Add(new Rectangle(5, 285, 75, outsideWallWeight));
            walls.Add(new Rectangle(5, 285, outsideWallWeight, 160));
            walls.Add(new Rectangle(5, this.Height - 30, this.Width - 10, outsideWallWeight)); //bottom
            walls.Add(new Rectangle(this.Width - 10, 285, outsideWallWeight, 160));
            walls.Add(new Rectangle(this.Width - 80, 285, 75, outsideWallWeight));
            walls.Add(new Rectangle(this.Width - 80, 240, outsideWallWeight, 45));
            walls.Add(new Rectangle(this.Width - 80, 240, 80, outsideWallWeight)); //right bottom tunnel wall
            walls.Add(new Rectangle(this.Width - 80, 205, 80, outsideWallWeight)); //right top tunnel wall
            walls.Add(new Rectangle(this.Width - 80, 160, outsideWallWeight, 45));
            walls.Add(new Rectangle(this.Width - 80, 160, 75, outsideWallWeight));
            walls.Add(new Rectangle(this.Width - 10, 35, outsideWallWeight, 130));

            //large rectangles at the top
            walls.Add(new Rectangle(40, 70, 40, 20)); //left
            walls.Add(new Rectangle(110, 70, 70, 20));
            walls.Add(new Rectangle(this.Width - 80, 70, 40, 20)); //right
            walls.Add(new Rectangle(this.Width - 180, 70, 70, 20));

            int insideWallWeight = 10;
            //inside walls from top to bottom, left to right
            walls.Add(new Rectangle((this.Width / 2) - 5, 35, insideWallWeight, 55)); //vertical at very top
            walls.Add(new Rectangle(40, 120, 40, insideWallWeight)); //left horizontal individual
            walls.Add(new Rectangle(110, 120, insideWallWeight, 88)); //left horizontal T
            walls.Add(new Rectangle(110, 160, 70, insideWallWeight));
            walls.Add(new Rectangle(150, 120, 130, insideWallWeight)); //top T
            walls.Add(new Rectangle(210, 120, insideWallWeight, 45));
            walls.Add(new Rectangle(310, 120, insideWallWeight, 88)); //right horizontal T
            walls.Add(new Rectangle(250, 160, 70, insideWallWeight));
            walls.Add(new Rectangle(this.Width - 80, 120, 40, insideWallWeight)); //right horizontal individual
            walls.Add(new Rectangle(110, 242, insideWallWeight, 48)); //lone vertical lines in the middle
            walls.Add(new Rectangle(310, 242, insideWallWeight, 48));
            walls.Add(new Rectangle(150, 280, 130, insideWallWeight)); //middle T
            walls.Add(new Rectangle(210, 280, insideWallWeight, 50));
            walls.Add(new Rectangle(40, 320, 40, insideWallWeight)); //left inverted L
            walls.Add(new Rectangle(70, 320, insideWallWeight, 50));
            walls.Add(new Rectangle(110, 320, 70, insideWallWeight)); //two horizontal individual
            walls.Add(new Rectangle(250, 320, 70, insideWallWeight));
            walls.Add(new Rectangle(350, 320, 40, insideWallWeight)); //right inverted L
            walls.Add(new Rectangle(350, 320, insideWallWeight, 50));
            walls.Add(new Rectangle(10, 360, 30, insideWallWeight)); //left short horizontal
            walls.Add(new Rectangle(40, 400, 140, insideWallWeight)); //left inverted T
            walls.Add(new Rectangle(110, 360, insideWallWeight, 50));
            walls.Add(new Rectangle(150, 360, 130, insideWallWeight)); //bottom T
            walls.Add(new Rectangle(210, 360, insideWallWeight, 50));
            walls.Add(new Rectangle(250, 400, 140, insideWallWeight)); //right inverted T
            walls.Add(new Rectangle(310, 360, insideWallWeight, 50));
            walls.Add(new Rectangle(390, 360, 30, insideWallWeight)); //right short horizontal

            //ghost house
            walls.Add(new Rectangle(150, 200, 130, 48));
        }

        /// <summary>
        /// Adds all the turn point rectangles to the turnPoints list
        /// </summary>
        public void SetTurnPoints()
        {
            //intersection points from top to bottom, left to right
            int turnPointSize = 1;
            //row 1
            int turnPointY = 55;
            turnPoints.Add(new Pellet(25, turnPointY, turnPointSize, false));
            turnPoints.Add(new Pellet(95, turnPointY, turnPointSize, false));
            turnPoints.Add(new Pellet(195, turnPointY, turnPointSize, false));
            turnPoints.Add(new Pellet(235, turnPointY, turnPointSize, false));
            turnPoints.Add(new Pellet(335, turnPointY, turnPointSize, false));
            turnPoints.Add(new Pellet(405, turnPointY, turnPointSize, false));

            //row 2
            turnPointY = 105;
            TurnsRow2_X_Pattern(turnPointY, turnPointSize);

            //row 3
            turnPointY = 145;
            TurnsRow2_X_Pattern(turnPointY, turnPointSize);

            //row 4
            turnPointY = 185;
            turnPoints.Add(new Pellet(139, turnPointY, turnPointSize, false));
            turnPoints.Add(new Pellet(195, turnPointY, turnPointSize, false));
            turnPoints.Add(new Pellet(235, turnPointY, turnPointSize, false));
            turnPoints.Add(new Pellet(291, turnPointY, turnPointSize, false));

            //row 5
            turnPointY = 225;
            turnPoints.Add(new Pellet(95, turnPointY, turnPointSize, false));
            turnPoints.Add(new Pellet(137, turnPointY, turnPointSize, false));
            turnPoints.Add(new Pellet(291, turnPointY, turnPointSize, false));
            turnPoints.Add(new Pellet(335, turnPointY, turnPointSize, false));

            //row 6
            turnPointY = 260;
            turnPoints.Add(new Pellet(139, turnPointY, turnPointSize, false));
            turnPoints.Add(new Pellet(291, turnPointY, turnPointSize, false));

            //row 7
            turnPointY = 305;
            TurnsRow2_X_Pattern(turnPointY, turnPointSize);

            //row 8
            turnPointY = 345;
            TurnsRow8_X_Pattern(turnPointY, turnPointSize);

            //row 9
            turnPointY = 385;
            TurnsRow8_X_Pattern(turnPointY, turnPointSize);

            //row 10
            turnPointY = 425;
            turnPoints.Add(new Pellet(25, turnPointY, turnPointSize, false));
            turnPoints.Add(new Pellet(195, turnPointY, turnPointSize, false));
            turnPoints.Add(new Pellet(235, turnPointY, turnPointSize, false));
            turnPoints.Add(new Pellet(405, turnPointY, turnPointSize, false));
        }

        /// <summary>
        /// Adds a series of rectangles in the same row to the turn points list, pattern in multiple rows
        /// </summary>
        /// <param name="turnPointY"></param>row y value
        /// <param name="turnPointSize"></param>size of square point
        public void TurnsRow2_X_Pattern(int turnPointY, int turnPointSize)
        {
            turnPoints.Add(new Pellet(25, turnPointY, turnPointSize, false));
            turnPoints.Add(new Pellet(95, turnPointY, turnPointSize, false));
            turnPoints.Add(new Pellet(139, turnPointY, turnPointSize, false));
            turnPoints.Add(new Pellet(195, turnPointY, turnPointSize, false));
            turnPoints.Add(new Pellet(235, turnPointY, turnPointSize, false));
            turnPoints.Add(new Pellet(291, turnPointY, turnPointSize, false));
            turnPoints.Add(new Pellet(335, turnPointY, turnPointSize, false));
            turnPoints.Add(new Pellet(405, turnPointY, turnPointSize, false));
        }

        /// <summary>
        /// Adds a series of rectangles, same as row 2 + two more, in the same row to the turn points list, pattern in multiple rows
        /// </summary>
        /// <param name="turnPointY"></param>row y value
        /// <param name="turnPointSize"></param>size of square point
        public void TurnsRow8_X_Pattern(int turnPointY, int turnPointSize)
        {
            TurnsRow2_X_Pattern(turnPointY, turnPointSize);
            turnPoints.Add(new Pellet(55, turnPointY, turnPointSize, false));
            turnPoints.Add(new Pellet(375, turnPointY, turnPointSize, false));
        }

        /// <summary>
        /// Adds all the pellet rectangles to the pellets list
        /// </summary>
        public void SetPellets()
        {
            //set energizers at the beginning of the list
            //one in each corner
            int pelletSize = 10;
            pellets.Add(new Pellet(20, 50, pelletSize, true));
            pellets.Add(new Pellet(400, 50, pelletSize, true));
            pellets.Add(new Pellet(20, 420, pelletSize, true));
            pellets.Add(new Pellet(400, 420, pelletSize, true));

            pelletSize = 4;
            //pellets top to bottom, left to right
            //row 1
            Row1_X_Pattern(53, pelletSize);

            //row 2
            Row2_X_Pattern(70, pelletSize);

            //row 3
            Row2_X_Pattern(86, pelletSize);

            //row 4
            Row4_X_Pattern(103, pelletSize);

            //row 5
            Row5_X_Pattern(123, pelletSize);

            //row 6
            Row6_X_Pattern(143, pelletSize);

            // rows 7 - 15
            Row7to15(pelletSize);

            //row 16
            Row1_X_Pattern(303, pelletSize);

            //row 17
            Row17_X_Pattern(323, pelletSize);

            //row 18
            Row18_X_Pattern(343, pelletSize);

            //row 19
            Row19_X_Pattern(363, pelletSize);

            //row 20
            Row6_X_Pattern(383, pelletSize);

            //row 21
            Row21_X_Pattern(403, pelletSize);

            //row 22
            Row4_X_Pattern(423, pelletSize);
        }

        /// <summary>
        /// Adds a series of rectangles in the same row to the pellets list, pattern in multiple rows
        /// </summary>
        /// <param name="pelletY"></param>row y value
        /// <param name="pelletSize"></param>square pellet value
        public void Row1_X_Pattern(int pelletY, int pelletSize)
        {
            for (int i = 23; i <= 163; i += 14)
            {
                pellets.Add(new Pellet(i, pelletY, pelletSize, false));
            }
            pellets.Add(new Pellet(178, pelletY, pelletSize, false));
            pellets.Add(new Pellet(193, pelletY, pelletSize, false));
            for (int i = 233; i <= 303; i += 14)
            {
                pellets.Add(new Pellet(i, pelletY, pelletSize, false));
            }
            pellets.Add(new Pellet(318, pelletY, pelletSize, false));
            for (int i = 333; i <= 403; i += 14)
            {
                pellets.Add(new Pellet(i, pelletY, pelletSize, false));
            }
        }

        /// <summary>
        /// Adds a series of rectangles in the same row to the pellets list, pattern in multiple rows
        /// </summary>
        /// <param name="pelletY"></param>row y value
        /// <param name="pelletSize"></param>square pellet value
        public void Row2_X_Pattern(int pelletY, int pelletSize)
        {
            pellets.Add(new Pellet(23, pelletY, pelletSize, false));
            pellets.Add(new Pellet(93, pelletY, pelletSize, false));
            pellets.Add(new Pellet(193, pelletY, pelletSize, false));
            pellets.Add(new Pellet(233, pelletY, pelletSize, false));
            pellets.Add(new Pellet(333, pelletY, pelletSize, false));
            pellets.Add(new Pellet(403, pelletY, pelletSize, false));
        }

        /// <summary>
        /// Adds a series of rectangles in the same row to the pellets list, pattern in multiple rows
        /// </summary>
        /// <param name="pelletY"></param>row y value
        /// <param name="pelletSize"></param>square pellet value
        public void Row4_X_Pattern(int pelletY, int pelletSize)
        {
            for (int i = 23; i <= 163; i += 14)
            {
                pellets.Add(new Pellet(i, pelletY, pelletSize, false));
            }
            pellets.Add(new Pellet(178, pelletY, pelletSize, false));
            pellets.Add(new Pellet(193, pelletY, pelletSize, false));

            pellets.Add(new Pellet(207, pelletY, pelletSize, false));
            pellets.Add(new Pellet(221, pelletY, pelletSize, false));

            for (int i = 233; i <= 303; i += 14)
            {
                pellets.Add(new Pellet(i, pelletY, pelletSize, false));
            }
            pellets.Add(new Pellet(318, pelletY, pelletSize, false));
            for (int i = 333; i <= 403; i += 14)
            {
                pellets.Add(new Pellet(i, pelletY, pelletSize, false));
            }
        }

        /// <summary>
        /// Adds a series of rectangles in the same row to the pellets list, pattern in multiple rows
        /// </summary>
        /// <param name="pelletY"></param>row y value
        /// <param name="pelletSize"></param>square pellet value
        public void Row5_X_Pattern(int pelletY, int pelletSize)
        {
            pellets.Add(new Pellet(23, pelletY, pelletSize, false));
            pellets.Add(new Pellet(93, pelletY, pelletSize, false));
            pellets.Add(new Pellet(135, pelletY, pelletSize, false));
            pellets.Add(new Pellet(289, pelletY, pelletSize, false));
            pellets.Add(new Pellet(333, pelletY, pelletSize, false));
            pellets.Add(new Pellet(403, pelletY, pelletSize, false));
        }

        /// <summary>
        /// Adds a series of rectangles in the same row to the pellets list, pattern in multiple rows
        /// </summary>
        /// <param name="pelletY"></param>row y value
        /// <param name="pelletSize"></param>square pellet value
        public void Row6_X_Pattern(int pelletY, int pelletSize)
        {
            for (int i = 23; i <= 93; i += 14)
            {
                pellets.Add(new Pellet(i, pelletY, pelletSize, false));
            }
            for (int i = 135; i <= 163; i += 14)
            {
                pellets.Add(new Pellet(i, pelletY, pelletSize, false));
            }
            pellets.Add(new Pellet(178, pelletY, pelletSize, false));
            pellets.Add(new Pellet(193, pelletY, pelletSize, false));
            for (int i = 233; i <= 289; i += 14)
            {
                pellets.Add(new Pellet(i, pelletY, pelletSize, false));
            }
            for (int i = 333; i <= 403; i += 14)
            {
                pellets.Add(new Pellet(i, pelletY, pelletSize, false));
            }
        }

        /// <summary>
        /// Adds multiple rows with the same series of rectangles to the pellets list
        /// </summary>
        /// <param name="pelletSize"></param>square pellet value
        public void Row7to15(int pelletSize)
        {
            for (int i = 159; i <= 287; i += 16)
            {
                pellets.Add(new Pellet(93, i, pelletSize, false));
                pellets.Add(new Pellet(333, i, pelletSize, false));
            }
        }

        /// <summary>
        /// Adds a series of rectangles in the same row to the pellets list, pattern in multiple rows
        /// </summary>
        /// <param name="pelletY"></param>row y value
        /// <param name="pelletSize"></param>square pellet value
        public void Row17_X_Pattern(int pelletY, int pelletSize)
        {
            pellets.Add(new Pellet(23, pelletY, pelletSize, false));
            pellets.Add(new Pellet(93, pelletY, pelletSize, false));
            pellets.Add(new Pellet(193, pelletY, pelletSize, false));
            pellets.Add(new Pellet(233, pelletY, pelletSize, false));
            pellets.Add(new Pellet(333, pelletY, pelletSize, false));
            pellets.Add(new Pellet(403, pelletY, pelletSize, false));
        }

        /// <summary>
        /// Adds a series of rectangles in the same row to the pellets list, pattern in multiple rows
        /// </summary>
        /// <param name="pelletY"></param>row y value
        /// <param name="pelletSize"></param>square pellet value
        public void Row18_X_Pattern(int pelletY, int pelletSize)
        {
            for (int i = 23; i <= 51; i += 14)
            {
                pellets.Add(new Pellet(i, pelletY, pelletSize, false));
            }
            for (int i = 93; i <= 163; i += 14)
            {
                pellets.Add(new Pellet(i, pelletY, pelletSize, false));
            }
            pellets.Add(new Pellet(178, pelletY, pelletSize, false));
            pellets.Add(new Pellet(193, pelletY, pelletSize, false));
            for (int i = 233; i <= 303; i += 14)
            {
                pellets.Add(new Pellet(i, pelletY, pelletSize, false));
            }
            pellets.Add(new Pellet(318, pelletY, pelletSize, false));
            pellets.Add(new Pellet(333, pelletY, pelletSize, false));
            for (int i = 375; i <= 403; i += 14)
            {
                pellets.Add(new Pellet(i, pelletY, pelletSize, false));
            }
        }

        /// <summary>
        /// Adds a series of rectangles in the same row to the pellets list, pattern in multiple rows
        /// </summary>
        /// <param name="pelletY"></param>row y value
        /// <param name="pelletSize"></param>square pellet value
        public void Row19_X_Pattern(int pelletY, int pelletSize)
        {
            pellets.Add(new Pellet(51, pelletY, pelletSize, false));
            pellets.Add(new Pellet(93, pelletY, pelletSize, false));
            pellets.Add(new Pellet(135, pelletY, pelletSize, false));
            pellets.Add(new Pellet(289, pelletY, pelletSize, false));
            pellets.Add(new Pellet(333, pelletY, pelletSize, false));
            pellets.Add(new Pellet(375, pelletY, pelletSize, false));
        }

        /// <summary>
        /// Adds a series of rectangles in the same row to the pellets list, pattern in multiple rows
        /// </summary>
        /// <param name="pelletY"></param>row y value
        /// <param name="pelletSize"></param>square pellet value
        public void Row21_X_Pattern(int pelletY, int pelletSize)
        {
            pellets.Add(new Pellet(23, pelletY, pelletSize, false));
            pellets.Add(new Pellet(193, pelletY, pelletSize, false));
            pellets.Add(new Pellet(233, pelletY, pelletSize, false));
            pellets.Add(new Pellet(403, pelletY, pelletSize, false));
        }
    }
}
