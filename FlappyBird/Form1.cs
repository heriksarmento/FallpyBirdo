using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Deployment.Application;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlappyBird
{
    public partial class Form1 : Form
    {
        private Game game;
        private Timer timer;
        

        public Form1()
        {
            BackColor = Color.Black;
            InitializeComponent();
            game = new Game();
            timer = new Timer();
            timer.Interval = 10;
            timer.Tick += Timer_Tick;
            int formHeight = this.Height;
            int formWidth = this.Width;
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            //Image flappy = Image.FromFile("flappyG.png");
            Rectangle f = new Rectangle(game.flappy.posx, game.flappy.posy, game.flappy.tamanho, game.flappy.tamanho);
            g.DrawRectangle(Pens.Yellow, f);
            for (int i = 0; i < 10; i++)
            {
                int width = game.width;
                Rectangle r  = new Rectangle(game.pipes[0, i], game.pipes[1, i], width, 500);
                Rectangle r2 = r;
                r2.Offset(0, -650);
                g.DrawRectangle(Pens.Red, r);
                g.DrawRectangle(Pens.Red, r2);
            }
            Font fontNumber = new Font("04b_19"     , 30, FontStyle.Regular, GraphicsUnit.Pixel);
            Font fontLetter = new Font("FlappyBirdy", 50, FontStyle.Regular, GraphicsUnit.Pixel);
            g.DrawString("Fallpy Birdo", fontLetter, Brushes.White, new Point(0, 0));
            g.DrawString(game.score.ToString(), fontNumber, Brushes.White, new Point(398, 100));

            if (game.gameOver)
            {
                g.DrawString("Game Over", fontNumber, Brushes.White, new Point(322, 65));
                g.DrawString("Press R to restart!", fontNumber, Brushes.White, new Point(250, 135));
            }
        }
         
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (game.gameOver)
            {
                timer.Stop();
            }
            else
            {
                if (game.flappy.posy <= 6)
                {
                    game.flappy.posy = 6;
                }
                else if (game.flappy.posy >= 435)
                {
                    game.flappy.posy = 435;
                }




                //Pulo do flappy
                if (game.flappy.jump != 0)
                {
                    game.flappy.jump -= 5;
                    game.flappy.posy -= 10;
                }


                //Cria um novo cano e destroi o primeiro dps de 2 sairem da tela
                if (game.flappy.posx - game.pipes[0, 0] == 340)
                {
                    game.newPipe();
                }

                if (game.score == 0)
                {
                    if ((game.pipes[0, 0] - (game.flappy.posx + game.flappy.tamanho)) == 0)
                    {
                        game.checkColision();
                    }
                    else if ((game.flappy.posx >= game.pipes[0, 0]) && (game.flappy.posx <= game.pipes[0, 0] + game.width))
                    {
                        game.checkColision();
                    }

                }
                else
                {
                    if ((game.pipes[0, 1] - (game.flappy.posx + game.flappy.tamanho)) == 0)
                    {
                        game.checkColision();
                    }
                    else if ((game.flappy.posx >= game.pipes[0, 1]) && (game.flappy.posx <= game.pipes[0, 1] + game.width))
                    {
                        game.checkColision();
                    }
                }
                Invalidate();

                if (!game.gameOver)
                {
                    game.flappy.posy += game.gravity;
                    game.pos -= game.speed;
                    game.progress();
                } 
            }
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            if (!timer.Enabled)
            {
                timer.Start();
            }
            game.gameOver = false;

        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {       
            if (e.KeyValue==' ')
            {
                game.flappy.jump = 50;
            }else if (e.KeyValue == 'R')
            {
                game = new Game();
                timer.Start();
            }

        }

    }
}
