using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlappyBird
{


    public class Game
    {
        public Flappy flappy;
        public int score;
        public int gravity;
        public int speed;
        public int[,] pipes = new int[2, 10];       //posx e posy dos pipes
        private Random rnd = new Random();
        public int pipeSpc; // Distancia X entre pipes
        public int pos;
        public int cleaningAssistant;
        public int width;
        public bool gameOver;

        public Game()
        {
            flappy = new Flappy();
            score = 0;
            gravity = 4;
            speed = 1;
            pipeSpc = 200;
            pos = 400;
            width = 40;
            cleaningAssistant = 0;
            gameOver = false;
            for (int i = 0; i < 10; i++)
            {
                pipes[1, i] = rnd.Next(100, 400);
                if (i != 0)
                {
                    pipes[0, i] = pipes[0, i - 1] + pipeSpc;
                }
                else
                {
                    pipes[0, i] = pos;
                }
            }
        }
        // Ideia de conforme a speed aumenta o tamanho da flappy diminui para que assim tenha que se movimentar mais para fugir dos canos

        public void progressPipes()
        {
            for (int i = 0; i < 10; i++)
            {
                if (i != 0)
                {
                    pipes[0, i] = pipes[0, i - 1] + pipeSpc;
                }
                else
                {
                    pipes[0, i] = pos;
                }
            }
        }

        public void newPipe()
        {
            for (int i = 0; i < 10; i++)
            {      
                if (i == 9)
                {
                    pipes[0, i] = pipes[0, i - 1] + pipeSpc;
                    pipes[1, i] = rnd.Next(100, 350);
                }
                else
                {
                    pipes[0, i] = pipes[0, i + 1];
                    pipes[1, i] = pipes[1, i + 1];
                }
            }
            pos = pipes[0, 0];

        }

        public bool checkColision()
        {
            if (score == 0)
            {

                if ((((flappy.posy >= pipes[1, 0]) && (flappy.posy <= 500)) || ((flappy.posy <= pipes[1, 0] - 150) && (flappy.posy >= pipes[1, 0] - 650)))
                    ||
                    ((pipes[1, 0] >= flappy.posy)&&(pipes[1, 0] <= flappy.posy+flappy.tamanho))
                    ||
                    ((pipes[1, 0] <= flappy.posy) && (pipes[1, 0] >= flappy.posy - flappy.tamanho)))
                {
                   gameOver = true;
                }
            }
            else
            {
                if ((((flappy.posy >= pipes[1, 1]) && (flappy.posy <= 500)) || ((flappy.posy <= pipes[1, 1] - 150) && (flappy.posy >= pipes[1, 1] - 650)))
                    ||
                    ((pipes[1, 1] >= flappy.posy) && (pipes[1, 1] <= flappy.posy + flappy.tamanho))
                    ||
                    ((pipes[1, 1] <= flappy.posy) && (pipes[1, 1] >= flappy.posy - flappy.tamanho)))
                {
                   gameOver = true;
                }
            }
            
            return gameOver; 
        }


        public void progress()
        {
            progressPipes();
            if(score== 0)
            {
                if ((flappy.posx - pipes[0, 0]) >= 40)
                {
                    score += 1;
                }
            }
            else
            {
                if ((flappy.posx - pipes[0, 1]) == 40)
                {
                    score += 1;
                }
            }
        }


    }
}
