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
        List<Rectangle> walls = new List<Rectangle>();
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
            Form1.backgroundMusic.Play();
            SetWalls();
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            SetWalls();
            Refresh();
        }

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < walls.Count(); i++)
            {
                e.Graphics.FillRectangle(wallBrush, walls[i]);
            }
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

        public void SetPellets()
        {

        }
    }
}
