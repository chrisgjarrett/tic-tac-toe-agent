using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Player;

namespace TicTacToe
{
    public partial class GameInterface : Form
    {


        //Create instances of two players
        PlayerObject player1Obj = new PlayerObject();
        PlayerObject player2Obj = new PlayerObject();

        //Timer
        Timer gameTimer = new Timer();

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
        int[] gameMatrix= { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        int player1 = 0;
        int player2 = 0;
        //int turn = 1;
        bool isPlaying = true;

        

        //This function is the game engine, deciding the flow of the game
        private void gameEngine() {

            while (isPlaying == true)
            {

                if (turn == 1)
                {
                    if (player1Obj.getHumanStatus() == true)
                    {
                        //If Human, wait for mouse click
                        break;
                    }
                    else
                    {
                        //Get next move from robot
                        gameMatrix = player1Obj.decideNextMove(gameMatrix);
                    }
                }

                else
                {
                    if (player2Obj.getHumanStatus() == true)
                    {
                        //If Human, wait for mouse click
                        break;
                    }
                    else
                    {
                        //Get next move from robot
                        gameMatrix = player2Obj.decideNextMove(gameMatrix);
                    }
                }
                checkit();
            }
        }


        //Updates the gameplay
        public void updateGame(int[] interfaceArray)
        {
            gameMatrix = interfaceArray;            
        }

        //THis displays whose turn it is
        public void display()
        {
            if (turn % 2 != 0)
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
            //Start with BUtton 1 and check 123, 159, 147
                if (gameMatrix[0] == gameMatrix[1] && gameMatrix[0] == gameMatrix[2] && gameMatrix[0] != 0)
                {
                    button0.BackColor = Color.Green;
                    button0.ForeColor = Color.White;
                    button1.BackColor = Color.Green;
                    button1.ForeColor = Color.White;
                    button2.BackColor = Color.Green;
                    button2.ForeColor = Color.White;

                    if (gameMatrix[0] == 2)
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
                    cleargame();
                }
                
               else if (gameMatrix[0] == gameMatrix[4] && gameMatrix[0] == gameMatrix[8] && gameMatrix[0] != 0)
                {
                    button0.BackColor = Color.Green;
                    button0.ForeColor = Color.White;
                    button4.BackColor = Color.Green;
                    button4.ForeColor = Color.White;
                    button8.BackColor = Color.Green;
                    button8.ForeColor = Color.White;

                    if (gameMatrix[0] == 2)
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
                    cleargame();
                }

            

            else if (gameMatrix[0] == gameMatrix[3] && gameMatrix[0] == gameMatrix[6] && gameMatrix[0] != 0)
                {
                    button0.BackColor = Color.Green;
                    button0.ForeColor = Color.White;
                    button3.BackColor = Color.Green;
                    button3.ForeColor = Color.White;
                    button6.BackColor = Color.Green;
                    button6.ForeColor = Color.White;
                    if (gameMatrix[0] == 2)
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
                    cleargame();
                }

            

            else if (gameMatrix[1] == gameMatrix[4] && gameMatrix[1] == gameMatrix[7] && gameMatrix[1] != 0)
                {
                    button1.BackColor = Color.Green;
                    button1.ForeColor = Color.White;
                    button4.BackColor = Color.Green;
                    button4.ForeColor = Color.White;
                    button7.BackColor = Color.Green;
                    button7.ForeColor = Color.White;
                    if (gameMatrix[1] == 2)
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
                    cleargame();
                }

            else if (gameMatrix[2] == gameMatrix[5] && gameMatrix[2] == gameMatrix[8] && gameMatrix[2] != 0)
                {
                    button2.BackColor = Color.Green;
                    button2.ForeColor = Color.White;
                    button5.BackColor = Color.Green;
                    button5.ForeColor = Color.White;
                    button8.BackColor = Color.Green;
                    button8.ForeColor = Color.White;
                    if (gameMatrix[2] == 2)
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
                    cleargame();
                }

            
            else if (gameMatrix[2] == gameMatrix[4] && gameMatrix[2] == gameMatrix[6] && gameMatrix[2] != 0)
                {
                    button2.BackColor = Color.Green;
                    button2.ForeColor = Color.White;
                    button4.BackColor = Color.Green;
                    button4.ForeColor = Color.White;
                    button6.BackColor = Color.Green;
                    button6.ForeColor = Color.White;
                    if (gameMatrix[2] == 2)
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
                    cleargame();
                }

            

            else if (gameMatrix[3] == gameMatrix[4] && gameMatrix[3] == gameMatrix[5] && gameMatrix[3] != 0)
                {
                    button3.BackColor = Color.Green;
                    button3.ForeColor = Color.White;
                    button4.BackColor = Color.Green;
                    button4.ForeColor = Color.White;
                    button5.BackColor = Color.Green;
                    button5.ForeColor = Color.White;
                    if (gameMatrix[3] == 2)
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
                    cleargame();
                }

           
            else if (gameMatrix[6] == gameMatrix[7] && gameMatrix[6] == gameMatrix[8] && gameMatrix[6] != 0)
                {
                    button6.BackColor = Color.Green;
                    button6.ForeColor = Color.White;
                    button7.BackColor = Color.Green;
                    button7.ForeColor = Color.White;
                    button8.BackColor = Color.Green;
                    button8.ForeColor = Color.White;
                    if (gameMatrix[7] == 2)
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
                    cleargame();
                }
            //Case of a draw
                else if (gameMatrix[0] != 0 && gameMatrix[1]!=0 && gameMatrix[2] != 0 && gameMatrix[3] != 0 && gameMatrix[5] != 0 && gameMatrix[6] != 0 && gameMatrix[7] != 0 && gameMatrix[8] != 0 && gameMatrix[9] != 0)
            {
                MessageBox.Show("Draw!");
                //player1++;
                //player2++;
                cleargame();
            }

        }

        //Resets the game
        public void resetgame()

        {
            displayTurn.Text = "Player 1";
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
        }
        
        //Clears the game
        public void cleargame()

        {
            //displayTurn.Text = "Player 1";
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
        }

        //This function calls a dialog box to determine which players are human and which are robots
        private void assignHumanStatus() {

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
        
        //Constructor
        public GameInterface()
        {
            //Get human status of players
            assignHumanStatus();
            InitializeComponent();
                           
         }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        //These functions wait for mouse clicks - only applicable if human player
        private void button0_Click(object sender, EventArgs e)
        {
            if(click0 == 0)
            {
                if (turn % 2 != 0)
                {
                    button0.Text = "X";
                    gameMatrix[0] = 2;
                }
                else
                {
                    button0.Text = "O";
                    gameMatrix[0] = 1;
                }
                turn++;
                click0++;
            }
            else
            {
                button0.Text = button0.Text;
            }
            display();
            checkit();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            if (click1 == 0)
            {
                if (turn % 2 != 0)
                {
                    button1.Text = "X";
                    gameMatrix[1] = 2;
                }
                else
                {
                    button1.Text = "O";
                    gameMatrix[1] = 1;
                }
                turn++;
                click1++;
            }
            else
            {
                button1.Text = button1.Text;
            }
            display();
            checkit();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (click2 == 0)
            {
                if (turn % 2 != 0)
                {
                    button2.Text = "X";
                    gameMatrix[2] = 2;
                }
                else
                {
                    button2.Text = "O";
                    gameMatrix[2] = 1;
                }
                turn++;
                click2++;
            }
            else
            {
                button2.Text = button2.Text;
            }
            display();
            checkit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (click3 == 0)
            {
                if (turn % 2 != 0)
                {
                    button3.Text = "X";
                    gameMatrix[3] = 2;
                }
                else
                {
                    button3.Text = "O";
                    gameMatrix[3] = 1;
                }
                turn++;
                click3++;
            }
            else
            {
                button3.Text = button3.Text;
            }
            display();
            checkit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (click4 == 0)
            {
                if (turn % 2 != 0)
                {
                    button4.Text = "X";
                    gameMatrix[4] = 2;
                }
                else
                {
                    button4.Text = "O";
                    gameMatrix[4] = 1;
                }
                turn++;
                click4++;
            }
            else
            {
                button4.Text = button4.Text;
            }
            display();
            checkit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (click5 == 0)
            {
                if (turn % 2 != 0)
                {
                    button5.Text = "X";
                    gameMatrix[5] = 2;
                }
                else
                {
                    button5.Text = "O";
                    gameMatrix[5] = 1;
                }
                turn++;
                click5++;
            }
            else
            {
                button5.Text = button5.Text;
            }
            display();
            checkit();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (click6 == 0)
            {
                if (turn % 2 != 0)
                {
                    button6.Text = "X";
                    gameMatrix[6] = 2;
                }
                else
                {
                    button6.Text = "O";
                    gameMatrix[6] = 1;
                }
                turn++;
                click6++;
            }
            else
            {
                button6.Text = button6.Text;
            }
            display();
            checkit();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (click7 == 0)
            {
                if (turn % 2 != 0)
                {
                    button7.Text = "X";
                    gameMatrix[7] = 2;
                }
                else
                {
                    button7.Text = "O";
                    gameMatrix[7] = 1;
                }
                turn++;
                click7++;
            }
            else
            {
                button7.Text = button7.Text;
            }
            display();
            checkit();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (click8 == 0)
            {
                if (turn % 2 != 0)
                {
                    button8.Text = "X";
                    gameMatrix[8] = 2;
                }
                else
                {
                    button8.Text = "O";
                    gameMatrix[8] = 1;
                }
                turn++;
                click8++;
            }
            else
            {
                button8.Text = button8.Text;
            }
            display();
            checkit();
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

        private void start(object sender, EventArgs e)
        {
            gameEngine();
        }
    }
}
