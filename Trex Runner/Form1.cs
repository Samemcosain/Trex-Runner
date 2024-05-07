using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trex_Runner
{
    public partial class formula : Form
    {
        bool jumping = false;
        int jumpSpeed;
        int force = 0;
        int score = 0;
        int obstaclespeed = 10;
        Random rand = new Random();
        int position;
        bool isGameOver = false;

       
        
        public formula()
        {
            InitializeComponent();

            GameReset();
        }

        private void MainGameTimerEvent(object sender, EventArgs e)
        {
            trex.Top += jumpSpeed;

            txtscore.Text = "Score: " + score;

            if (jumping == true && force < -5)
            {
                jumping = false;
            }

            if (jumping == true)
            {
                jumpSpeed = -12;
                force -= 2;
            }
            else
            {
                jumpSpeed = 12;
            }


            if (trex.Top > 297 && jumping == false)
            {
                force = 13;
                trex.Top = 298;
                jumpSpeed = 0;
            }


            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "obstacle")
                {
                    x.Left -= obstaclespeed;

                    if (x.Left < -100)
                    {
                        x.Left = ClientSize.Width + rand.Next(200, 500) + (x.Width * 15);
                        score++;
                    }

                    if (trex.Bounds.IntersectsWith(x.Bounds))
                    {
                        gamerTimer.Stop();
                        trex.Image = Properties.Resources.dead;
                        txtscore.Text += "    Press R to restart the game!";
                        isGameOver = true;
                    }
                }
            }

            if (score > 10)
            {
                obstaclespeed = 10;
            }
        }

        private void keyless(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space && jumping == false)
            {
                jumping =true;
            }
        }

        private void keyarrow(object sender, KeyEventArgs e)
        {
            if (jumping == true)
            {
                jumping = false;
            }

            if (e.KeyCode == Keys.R && isGameOver == true)
            {
                GameReset();
            }
        }

        private void GameReset()
        {
            force =13;
            jumpSpeed = 0;
            jumping = false;
            score = 0;
            obstaclespeed = 10;
            txtscore.Text = "Score: " + score;
            trex.Image = Properties.Resources.running;
            isGameOver = false;
            trex.Top = 298;

            foreach (Control x in this.Controls)
            {

                if (x is PictureBox && (string)x.Tag == "obstacle")
                {
                 position = this.ClientSize.Width + rand.Next(200, 500) + (x.Width * 10);

                    x.Left = position;
                }
            }

            gamerTimer.Start();

        }

        
    }
}
