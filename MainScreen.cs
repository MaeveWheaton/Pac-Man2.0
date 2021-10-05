using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Pac_Man2._0
{
    public partial class MainScreen : UserControl
    {
        int pelletX = 135;
        int pelletY = 185;

        public MainScreen()
        {
            InitializeComponent();
        }
        
        private void MainScreen_Paint(object sender, PaintEventArgs e)
        {
            titleLabel.Text = "PAC-MAN";
            instructionLabel.Text = "Press Green to Start\nPress Black to Exit";

            //draw pacman and dots
            e.Graphics.FillPie(Form1.pacManBrush, 80, 170, 40, 40, 45, 270);
            e.Graphics.FillEllipse(Form1.pelletsBrush, pelletX, pelletY, 10, 10);
            e.Graphics.FillEllipse(Form1.pelletsBrush, pelletX + 30, pelletY, 10, 10);
            e.Graphics.FillEllipse(Form1.pelletsBrush, pelletX + 60, pelletY, 10, 10);
            e.Graphics.FillEllipse(Form1.pelletsBrush, pelletX + 90, pelletY, 10, 10);
        }

        private void MainScreen_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    //change to game screen
                    Form f = this.FindForm();
                    f.Controls.Remove(this);

                    GameScreen gs = new GameScreen();
                    f.Controls.Add(gs);

                    //centre screen on the form
                    gs.Location = new Point((f.Width - gs.Width) / 2, (f.Height - gs.Height) / 2);
                    break;
                case Keys.Escape:
                    //close application
                    Application.Exit();
                    break;
            }
        }
    }
}
