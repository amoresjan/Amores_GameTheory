using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Amores_GameTheory
{
    public partial class Form1 : Form
    { 
        int rounds = 1;
        int opponent = 0;
        int lastMove = 0; //determines your last move, 1 if dealed, 0 if ditched
        bool ditched = false;
        int yourScore = 0;
        int opponentScore = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) //button for dealing
        {
            if (opponent == 0) //if player did not choose opponent
            {
                MessageBox.Show("Please select an opponent!");
            }
            else
            {
                determinePoints(true, opponentMove(opponent));

                lastMove = 1;
                rounds++;


                //outputs and labels
                changeLabels();

                if (rounds == 6)
                {
                    determineWinner();
                    resetGame();
                }

            }
            
        }

        private void button2_Click(object sender, EventArgs e) //button for ditching
        {
            if (opponent == 0) //if player did not choose opponent
            {
                MessageBox.Show("Please select an opponent!");
            }
            else
            {
                determinePoints(false, opponentMove(opponent));

                lastMove = 0;
                ditched = true;
                rounds++;

                //outputs and labels
                changeLabels();

                if (rounds == 6)
                {
                    determineWinner();
                    resetGame();
                }
            }
        }

        private void determinePoints(bool yourMove, bool opponentMove)
        {
            if (yourMove)
            {
                if (opponentMove) //both dealed
                {
                    yourScore += 10;
                    opponentScore += 10;
                    changeColors(pictureBox1, label1a, label1b, Color.Yellow);
                }
                else //you dealed, opponent ditched
                {
                    yourScore -= 5;
                    opponentScore += 15;
                    changeColors(pictureBox2, label2a, label2b, Color.Red);
                }
            }
            else
            {
                if (opponentMove) //you ditched, opponent dealed
                {
                    yourScore += 15;
                    opponentScore -= 5;
                    changeColors(pictureBox3, label3a, label3b, Color.Green);
                }
                else //both ditched
                {
                    
                    changeColors(pictureBox4, label4a, label4b, Color.White);
                }
            }

            scoreChecker();
        }

        private bool opponentMove(int opponent)
        {
            //determines the strategies of the opponents
            //true = deal
            //false = ditch
            if (opponent == 1)
            {
                //cooperates at first round, then copies your last move
                if (rounds == 1)
                {
                    return true;
                }
                else
                {
                    if(lastMove == 1)
                    {
                        return true;
                    }
                }

                
            }
            else if (opponent == 2)
            {
                //deals until the last 
                return true;
            }
            else if (opponent == 3)
            {
                //dtiches until the last round
            }
            else if (opponent == 4)
            {
                //if you dtich, the grudger will cheat in the remaining rounds
                if (!ditched)
                {
                    return true;
                }
            }
            else if (opponent == 5)
            {
                //randomize movement
                var random = new Random();
                if (random.Next(2) == 1)
                {
                    return true;
                }
            }

            return false;

        }


        //for opponents
        private void copycatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rounds == 1)
            {
                opponent = 1;
                label7.Text = "Your opponent is: Copycat";
            }
            else
            {
                MessageBox.Show("Changing of opponents not allowed!");
            }
        }

        private void cooperativeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rounds == 1)
            {
                opponent = 2;
                label7.Text = "Your opponent is: Real Dealer"; 
            }
            else
            {
                MessageBox.Show("Changing of opponents not allowed!");
            }
        }

        private void cheaterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rounds == 1)
            {
                opponent = 3;
                label7.Text = "Your opponent is: Scammer";
            }
            else
            {
                MessageBox.Show("Changing of opponents not allowed!");
            }
        }

        private void grudgerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rounds == 1)
            {
                opponent = 4;
                label7.Text = "Your opponent is: Grudger";
            }
            else
            {
                MessageBox.Show("Changing of opponents not allowed!");
            }
        }

        private void randomizerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rounds == 1)
            {
                opponent = 5;
                label7.Text = "Your opponent is: Rich man, I don't care money";
            }
            else
            {
                MessageBox.Show("Changing of opponents not allowed!");
            }
        }

        private void selectRandomlyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rounds == 1)
            {
                var random = new Random();
                opponent = random.Next(5);
                if(opponent == 1)
                {
                    label7.Text = "Your opponent is: Copycat";
                }
                else if(opponent == 2)
                {
                    label7.Text = "Your opponent is: Real Dealer";
                }
                else if(opponent == 3)
                {
                    label7.Text = "Your opponent is: Scammer";
                }
                else if(opponent == 4)
                {
                    label7.Text = "Your opponent is: Grudger";
                }
                else if(opponent == 5)
                {
                    label7.Text = "Your opponent is: Rich man, I don't care money";
                }
            }
            else
            {
                MessageBox.Show("Changing of opponents not allowed!");
            }
        }

        private void restartGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            resetGame();
        }

        //resets the game
        private void resetGame()
        {
            opponent = 0;
            rounds = 1;
            textBox1.Text = "0";
            textBox2.Text = "0";
            textBox1.BackColor = Color.White;
            textBox2.BackColor = Color.White;
            yourScore = 0;
            opponentScore = 0;
            ditched = false;
            label7.Text = "Your opponent is: ";
            resetColor();
            changeLabels();
            
        }


        private void determineWinner()
        {
            if(yourScore > opponentScore)
            {
                MessageBox.Show("You win!" + Environment.NewLine + Environment.NewLine + "Your Score: "
                    + yourScore + Environment.NewLine + "Opponent Score: " + opponentScore);
            }
            else if(yourScore < opponentScore)
            {
                MessageBox.Show("Opponent wins!" + Environment.NewLine + Environment.NewLine + "Your Score: "
                    + yourScore + Environment.NewLine + "Opponent Score: " + opponentScore);
            }
            else
            {
                MessageBox.Show("Draw!" + Environment.NewLine + Environment.NewLine + "Your Score: "
                    + yourScore + Environment.NewLine + "Opponent Score: " + opponentScore);
            }
        }

        //ui methods
        private void resetColor()
        {
            pictureBox1.BackColor = Color.Transparent;
            pictureBox2.BackColor = Color.Transparent;
            pictureBox3.BackColor = Color.Transparent;
            pictureBox4.BackColor = Color.Transparent;
            label1a.BackColor = Color.Transparent;
            label1b.BackColor = Color.Transparent;
            label2a.BackColor = Color.Transparent;
            label2b.BackColor = Color.Transparent;
            label3a.BackColor = Color.Transparent;
            label3b.BackColor = Color.Transparent;
            label4a.BackColor = Color.Transparent;
            label4b.BackColor = Color.Transparent;
        }

        private void changeColors(PictureBox pictureBox,Label label1, Label label2, Color color)
        {
            resetColor();
            pictureBox.BackColor = color;
            label1.BackColor = color;
            label2.BackColor = color;
        }

        private void changeLabels()
        {
            textBox1.Text = yourScore.ToString();
            textBox2.Text = opponentScore.ToString();
            label8.Text = "Round: " + rounds;

        }

        private void scoreChecker()
        {
            if(yourScore > opponentScore)
            {
                textBox1.BackColor = Color.Yellow;
                textBox2.BackColor = Color.White;
            }
            else if (yourScore < opponentScore)
            {
                textBox1.BackColor = Color.White;
                textBox2.BackColor = Color.Yellow;
            }
            else
            {
                if (yourScore > opponentScore)
                {
                    textBox1.BackColor = Color.White;
                    textBox2.BackColor = Color.White;
                }
            }
        }

    }
}
