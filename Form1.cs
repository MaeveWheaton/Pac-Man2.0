/*
 * Maeve Wheaton
 * Mr. T
 * October 4, 2021
 * Pac-Man game, player must collect all the pellets on the screen without being caught by a ghost. 
 * Collecting any of the four energizers, one in each corner, will allow the player to catch the ghosts for extra points for a couple of seconds.
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace Pac_Man2._0
{
    public partial class Form1 : Form
    {
        public static SolidBrush pacManBrush = new SolidBrush(Color.Yellow);
        public static SolidBrush pelletsBrush = new SolidBrush(Color.PapayaWhip);
        public static SoundPlayer backgroundMusic = new SoundPlayer(Properties.Resources.background_music);
        public static SoundPlayer menuMusic = new SoundPlayer(Properties.Resources.menu_music);
        public static SoundPlayer gameOverSound = new SoundPlayer(Properties.Resources.game_over);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Program goes directly to the GameScreen method on start
            MainScreen gs = new MainScreen();
            this.Controls.Add(gs);

            //centre
            gs.Location = new Point((this.Width - gs.Width) / 2, (this.Height - gs.Height) / 2);
        }
    }
}
