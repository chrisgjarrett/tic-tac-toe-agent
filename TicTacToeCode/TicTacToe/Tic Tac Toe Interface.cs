using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;
using Player;

namespace TicTacToe
{
    public partial class GameInterface : Form
    {

        //Create instances of two players
        PlayerObject player1Obj = new PlayerObject();
        PlayerObject player2Obj = new PlayerObject();

        //Timer
        DispatcherTimer gameTimer = new System.Windows.Threading.DispatcherTimer();
        int turn = 1;
        int click0 = 0;
        int click1 = 0;
        int click2 = 0;
        int click3 = 0;
        int click4 = 0;
        int click5 = 0;
        int click6 = 0;
        int click7 = 0;
        int click8 = 0;

        //Game Matrix - stores current values. 2=x, 1=o, 0=unplayed
        int[] gameMatrix = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        int[] newGameMatrix = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        int player1 = 0;
        int player2 = 0;

        //Variables to store the states and the moves
        List<int[]> gameStates = new List<int[]>();
        List<int> gameMoves = new List<int>();
        int nextMove;

        //Constructor
        public GameInterface()
        {
            //Get human status of players
            assignHumanStatus();
            InitializeComponent();

            //Start Game Timer
            gameTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            gameTimer.Tick += new EventHandler(gameEngine);

            //gameTimer.Enabled=true;
            gameTimer.Start();
            gameTimer.IsEnabled = true;

            //Update Display
            display();
        }

        //This is the function called when the timer fires
        private void gameEngine(Object sender, EventArgs e)
        {
            //Stop Game Timer until next move
            gameTimer.Stop();

            if (turn == 1)
            {
                if (player1Obj.getHumanStatus() == true)
                {
                    //If Human, wait for mouse click
                    gameTimer.Stop();
                }

                else
                {
                    //Get next move from robot
                    nextMove = player1Obj.decideNextMove(ref gameMatrix);
                    gameTimer.Stop();
                    updateGame();
                }

                
            }

            else
            {
                if (player2Obj.getHumanStatus() == true)
                {
                    //If Human, wait for mouse click
                    gameTimer.Stop();

                }
                else
                {
                    //Get next move from robot
                    gameStates.Add(gameMatrix);
                    nextMove = player2Obj.decideNextMove(ref gameMatrix);
                    gameMoves.Add(nextMove);
                    gameTimer.Stop();
                    updateGame();

                }

            }
        }

        //Updates the gameplay
        public void updateGame()
        {

            if (gameMatrix[0] == 2)
            {
                button0.Text = "O";
                click0 = 1;

            }
            else if (gameMatrix[0] == 1)
            {
                button0.Text = "X";
                click0 = 1;
            }
            else
            {
                button0.Text = "";
            }

            if (gameMatrix[1] == 2)
            {
                button1.Text = "O";
                click1 = 1;
            }
            else if (gameMatrix[1] == 1)
            {
                button1.Text = "X";
                click1 = 1;

            }
            else
            {
                button1.Text = "";
            }

            if (gameMatrix[2] == 2)
            {
                button2.Text = "O";
                click2 = 1;


            }
            else if (gameMatrix[2] == 1)
            {
                button2.Text = "X";
                click2 = 1;
            }
            else
            {
                button2.Text = "";
            }

            if (gameMatrix[3] == 2)
            {
                button3.Text = "O";
                click3 = 1;
            }
            else if (gameMatrix[3] == 1)
            {
                button3.Text = "X";
                click3 = 1;
            }
            else
            {
                button3.Text = "";
            }

            if (gameMatrix[4] == 2)
            {
                button4.Text = "O";
                click4 = 1;
            }
            else if (gameMatrix[4] == 1)
            {
                button4.Text = "X";
                click4 = 1;
            }
            else
            {
                button4.Text = "";
            }

            if (gameMatrix[5] == 2)
            {
                button5.Text = "O";
                click5 = 1;
            }
            else if (gameMatrix[5] == 1)
            {
                button5.Text = "X";
                click5 = 1;
            }
            else
            {
                button5.Text = "";
            }

            if (gameMatrix[6] == 2)
            {
                button6.Text = "O";
                click6 = 1;
            }
            else if (gameMatrix[6] == 1)
            {
                button6.Text = "X";
                click6 = 1;
            }
            else
            {
                button6.Text = "";
            }

            if (gameMatrix[7] == 2)
            {
                button7.Text = "O";
                click7 = 1;
            }
            else if (gameMatrix[7] == 1)
            {
                button7.Text = "X";
                click7 = 1;
            }
            else
            {
                button7.Text = "";
            }

            if (gameMatrix[8] == 2)
            {
                button8.Text = "O";
                click8 = 1;
            }
            else if (gameMatrix[8] == 1)
            {
                button8.Text = "X";
                click8 = 1;
            }
            else
            {
                button8.Text = "";
            }

            //Check if there is a winner
            checkit();



        }

        //THis displays whose turn it is
        public void display()
        {
            if (turn == 1)
            {
                displayTurn.Text = "Player 1";
            }
            else
            {
                displayTurn.Text = "Player 2";
            }
        }

        //This function checks the current game status to see if there is a winner
        public void checkit()
        {
            bool gameOver = false;
            //Start with BUtton 1 and check 123, 159, 147
            if (gameMatrix[0] == gameMatrix[1] && gameMatrix[0] == gameMatrix[2] && gameMatrix[0] != 0)
            {
                button0.BackColor = Color.Green;
                button0.ForeColor = Color.White;
                button1.BackColor = Color.Green;
                button1.ForeColor = Color.White;
                button2.BackColor = Color.Green;
                button2.ForeColor = Color.White;

                if (gameMatrix[0] == 1)
                {
                    MessageBox.Show("Player 1 Wins!");
                    player1++;
                    player1Score.Text = player1.ToString();
                }
                else
                {
                    MessageBox.Show("Player 2 Wins!");
                    player2++;
                    player2Score.Text = player2.ToString();
                }
                gameOver = true;
            }

            else if (gameMatrix[0] == gameMatrix[4] && gameMatrix[0] == gameMatrix[8] && gameMatrix[0] != 0)
            {
                button0.BackColor = Color.Green;
                button0.ForeColor = Color.White;
                button4.BackColor = Color.Green;
                button4.ForeColor = Color.White;
                button8.BackColor = Color.Green;
                button8.ForeColor = Color.White;

                if (gameMatrix[0] == 1)
                {
                    MessageBox.Show("Player 1 Wins!");
                    player1++;
                    player1Score.Text = player1.ToString();
                }
                else
                {
                    MessageBox.Show("Player 2 Wins!");
                    player2++;
                    player2Score.Text = player2.ToString();
                }
                gameOver = true;
            }



            else if (gameMatrix[0] == gameMatrix[3] && gameMatrix[0] == gameMatrix[6] && gameMatrix[0] != 0)
            {
                button0.BackColor = Color.Green;
                button0.ForeColor = Color.White;
                button3.BackColor = Color.Green;
                button3.ForeColor = Color.White;
                button6.BackColor = Color.Green;
                button6.ForeColor = Color.White;
                if (gameMatrix[0] == 1)
                {
                    MessageBox.Show("Player 1 Wins!");
                    player1++;
                    player1Score.Text = player1.ToString();
                }
                else
                {
                    MessageBox.Show("Player 2 Wins!");
                    player2++;
                    player2Score.Text = player2.ToString();
                }
                gameOver = true;
            }



            else if (gameMatrix[1] == gameMatrix[4] && gameMatrix[1] == gameMatrix[7] && gameMatrix[1] != 0)
            {
                button1.BackColor = Color.Green;
                button1.ForeColor = Color.White;
                button4.BackColor = Color.Green;
                button4.ForeColor = Color.White;
                button7.BackColor = Color.Green;
                button7.ForeColor = Color.White;
                if (gameMatrix[1] == 1)
                {
                    MessageBox.Show("Player 1 Wins!");
                    player1++;
                    player1Score.Text = player1.ToString();
                }
                else
                {
                    MessageBox.Show("Player 2 Wins!");
                    player2++;
                    player2Score.Text = player2.ToString();
                }
                gameOver = true;
            }

            else if (gameMatrix[2] == gameMatrix[5] && gameMatrix[2] == gameMatrix[8] && gameMatrix[2] != 0)
            {
                button2.BackColor = Color.Green;
                button2.ForeColor = Color.White;
                button5.BackColor = Color.Green;
                button5.ForeColor = Color.White;
                button8.BackColor = Color.Green;
                button8.ForeColor = Color.White;
                if (gameMatrix[2] == 1)
                {
                    MessageBox.Show("Player 1 Wins!");
                    player1++;
                    player1Score.Text = player1.ToString();
                }
                else
                {
                    MessageBox.Show("Player 2 Wins!");
                    player2++;
                    player2Score.Text = player2.ToString();
                }
                gameOver = true;
            }


            else if (gameMatrix[2] == gameMatrix[4] && gameMatrix[2] == gameMatrix[6] && gameMatrix[2] != 0)
            {
                button2.BackColor = Color.Green;
                button2.ForeColor = Color.White;
                button4.BackColor = Color.Green;
                button4.ForeColor = Color.White;
                button6.BackColor = Color.Green;
                button6.ForeColor = Color.White;
                if (gameMatrix[2] == 1)
                {
                    MessageBox.Show("Player 1 Wins!");
                    player1++;
                    player1Score.Text = player1.ToString();
                }
                else
                {
                    MessageBox.Show("Player 2 Wins!");
                    player2++;
                    player2Score.Text = player2.ToString();
                }
                gameOver = true;
            }



            else if (gameMatrix[3] == gameMatrix[4] && gameMatrix[3] == gameMatrix[5] && gameMatrix[3] != 0)
            {
                button3.BackColor = Color.Green;
                button3.ForeColor = Color.White;
                button4.BackColor = Color.Green;
                button4.ForeColor = Color.White;
                button5.BackColor = Color.Green;
                button5.ForeColor = Color.White;
                if (gameMatrix[3] == 1)
                {
                    MessageBox.Show("Player 1 Wins!");
                    player1++;
                    player1Score.Text = player1.ToString();
                }
                else
                {
                    MessageBox.Show("Player 2 Wins!");
                    player2++;
                    player2Score.Text = player2.ToString();
                }
                gameOver = true;
            }


            else if (gameMatrix[6] == gameMatrix[7] && gameMatrix[6] == gameMatrix[8] && gameMatrix[6] != 0)
            {
                button6.BackColor = Color.Green;
                button6.ForeColor = Color.White;
                button7.BackColor = Color.Green;
                button7.ForeColor = Color.White;
                button8.BackColor = Color.Green;
                button8.ForeColor = Color.White;
                if (gameMatrix[7] == 1)
                {
                    MessageBox.Show("Player 1 Wins!");
                    player1++;
                    player1Score.Text = player1.ToString();
                }
                else
                {
                    MessageBox.Show("Player 2 Wins!");
                    player2++;
                    player2Score.Text = player2.ToString();
                }
                gameOver = true;
            }
            //Case of a draw
            else if (gameMatrix[0] != 0 && gameMatrix[1] != 0 && gameMatrix[2] != 0 && gameMatrix[3] != 0 && gameMatrix[4] != 0 && gameMatrix[5] != 0 && gameMatrix[6] != 0 && gameMatrix[7] != 0 && gameMatrix[8] != 0)
            {
                MessageBox.Show("Draw!");
                //player1++;
                //player2++;
                gameOver = true;
            }

            if (gameOver == true)
            {
                if (turn == 1)
                    player2Obj.updateQMatrix(gameStates, gameMoves, false, ref gameMatrix);
                else
                    player2Obj.updateQMatrix(gameStates, gameMoves, true, ref gameMatrix);
                cleargame();
            }
            else
            {
                //Update the turn
                if (turn == 1)
                {
                    turn = 2;
                }

                else
                {
                    turn = 1;
                }

                display();
                gameTimer.Start();
            }
        }




        //Resets the game
        public void resetgame()

        {
            //            displayTurn.Text = "Player 1";
            player1Score.Text = "0";
            player2Score.Text = "0";
            player1 = 0;
            player2 = 0;
            turn = 1;
            click0 = 0;
            click1 = 0;
            click2 = 0;
            click3 = 0;
            click4 = 0;
            click5 = 0;
            click6 = 0;
            click7 = 0;
            click8 = 0;

            button0.BackColor = Color.LightGray;
            button0.ForeColor = Color.Black;
            button0.Text = "";

            button1.BackColor = Color.LightGray;
            button1.ForeColor = Color.Black;
            button1.Text = "";

            button2.BackColor = Color.LightGray;
            button2.ForeColor = Color.Black;
            button2.Text = "";

            button3.BackColor = Color.LightGray;
            button3.ForeColor = Color.Black;
            button3.Text = "";

            button4.BackColor = Color.LightGray;
            button4.ForeColor = Color.Black;
            button4.Text = "";

            button5.BackColor = Color.LightGray;
            button5.ForeColor = Color.Black;
            button5.Text = "";

            button6.BackColor = Color.LightGray;
            button6.ForeColor = Color.Black;
            button6.Text = "";

            button7.BackColor = Color.LightGray;
            button7.ForeColor = Color.Black;
            button7.Text = "";

            button8.BackColor = Color.LightGray;
            button8.ForeColor = Color.Black;
            button8.Text = "";

            Array.Clear(gameMatrix, 0, gameMatrix.Length);
            display();
            gameTimer.Start();
        }

        //Clears the game
        public void cleargame()

        {
            //            player1Score.Text = "0";
            //            player2Score.Text = "0";
            //            player1 = 0;
            //            player2 = 0;
            turn = 1;
            click0 = 0;
            click1 = 0;
            click2 = 0;
            click3 = 0;
            click4 = 0;
            click5 = 0;
            click6 = 0;
            click7 = 0;
            click8 = 0;

            button0.BackColor = Color.LightGray;
            button0.ForeColor = Color.Black;
            button0.Text = "";

            button1.BackColor = Color.LightGray;
            button1.ForeColor = Color.Black;
            button1.Text = "";

            button2.BackColor = Color.LightGray;
            button2.ForeColor = Color.Black;
            button2.Text = "";

            button3.BackColor = Color.LightGray;
            button3.ForeColor = Color.Black;
            button3.Text = "";

            button4.BackColor = Color.LightGray;
            button4.ForeColor = Color.Black;
            button4.Text = "";

            button5.BackColor = Color.LightGray;
            button5.ForeColor = Color.Black;
            button5.Text = "";

            button6.BackColor = Color.LightGray;
            button6.ForeColor = Color.Black;
            button6.Text = "";

            button7.BackColor = Color.LightGray;
            button7.ForeColor = Color.Black;
            button7.Text = "";

            button8.BackColor = Color.LightGray;
            button8.ForeColor = Color.Black;
            button8.Text = "";

            Array.Clear(gameMatrix, 0, gameMatrix.Length);
            display();
            gameTimer.Start();
        }

        //This function calls a dialog box to determine which players are human and which are robots
        private void assignHumanStatus()
        {

            Form form1 = new Form();
            // Create two buttons to use as the accept and cancel buttons.
            Button button1 = new Button();

            CheckBox checkPlayer1 = new CheckBox();
            CheckBox checkPlayer2 = new CheckBox();

            // Set the text of button1 to "OK".
            button1.Text = "Done";

            // Set the position of the button on the form.
            button1.Location = new Point(100, 50);

            //Text of Check boxes
            checkPlayer1.Text = "Player 1 Human ?";
            checkPlayer2.Text = "Player 2 Human ?";

            checkPlayer1.Location = new Point(5, 20);
            checkPlayer2.Location = new Point(checkPlayer1.Left, checkPlayer1.Height + checkPlayer1.Top + 10);

            // Set the caption bar text of the form.   
            form1.Text = "My Dialog Box";

            // Display a help button on the form.
            form1.HelpButton = true;

            // Define the border style of the form to a dialog box.
            form1.FormBorderStyle = FormBorderStyle.FixedDialog;

            // Set the MaximizeBox to false to remove the maximize box.
            form1.MaximizeBox = false;

            // Set the MinimizeBox to false to remove the minimize box.
            form1.MinimizeBox = false;

            // Set the accept button of the form to button1.
            form1.AcceptButton = button1;
            form1.AcceptButton.DialogResult = DialogResult.OK;

            // Set the start position of the form to the center of the screen.
            form1.StartPosition = FormStartPosition.CenterScreen;

            // Add button1 to the form.
            form1.Controls.Add(button1);

            //Add checkboxes to the form
            form1.Controls.Add(checkPlayer1);
            form1.Controls.Add(checkPlayer2);

            // Display the form as a modal dialog box.
            form1.ShowDialog();

            //Assign human status to players 
            if (checkPlayer1.Checked)
                player1Obj.assignHumanStatus(true);
            else
                player1Obj.assignHumanStatus(false);

            if (checkPlayer2.Checked)
                player2Obj.assignHumanStatus(true);
            else
                player2Obj.assignHumanStatus(false);

            //Assign Player 1 and Player 2 to human/robot - need to add smart code for this
            player1Obj.setPlayerNumber(1);
            player2Obj.setPlayerNumber(2);


        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        //These functions wait for mouse clicks - only applicable if human player
        private void button0_Click(object sender, EventArgs e)
        {
            if (click0 == 0)
            {
                gameMatrix[0] = turn;
                updateGame();

            }



        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (click1 == 0)
            {
                gameMatrix[1] = turn;               //turn
                updateGame();

            }

            else
            {
                button1.Text = button1.Text;
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (click2 == 0)
            {
                gameMatrix[2] = turn;
                updateGame();
                //turn++;
            }
            else
            {
                //button2.Text = button2.Text;
            }
            //display();
            //checkit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (click3 == 0)
            {
                gameMatrix[3] = turn;
                updateGame();
                //turn++;
            }
            else
            {
                button3.Text = button3.Text;
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (click4 == 0)
            {
                gameMatrix[4] = turn;
                updateGame();
                //turn++;
            }
            else
            {
                //button4.Text = button4.Text;
            }


        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (click5 == 0)
            {
                gameMatrix[5] = turn;
                updateGame();
                //turn++;
            }
            else
            {
                //button5.Text = button5.Text;
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (click6 == 0)
            {
                gameMatrix[6] = turn;
                updateGame();
                //turn++;
            }
            else
            {
                //      button6.Text = button6.Text;
            }


        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (click7 == 0)
            {
                gameMatrix[7] = turn;
                updateGame();
            }
            else
            {
                //button7.Text = button7.Text;
            }

        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (click8 == 0)
            {
                gameMatrix[8] = turn;
                updateGame();
                //turn++;
            }
            else
            {
                //button8.Text = button8.Text;
            }
        }

        //Clears the game
        private void ClearGameButton_Click(object sender, EventArgs e)
        {
            cleargame();
        }

        //Resets game
        private void resetButton_Click(object sender, EventArgs e)
        {
            resetgame();
        }

        private void GameInterface_Load(object sender, EventArgs e)
        {

        }


    }
}
