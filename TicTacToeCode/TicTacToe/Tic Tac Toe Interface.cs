//The states have no memory - you somehow need to penalise a move based on the rest of the board....

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
        double[,] qMatrixPlayer2 = new double[19861, 9];
        double[,] qMatrixPlayer1 = new double[19861, 9];

        int player1 = 0;
        int player2 = 0;
        bool humanPlaying = false;

        bool trainPlayer1 = false;
        bool trainPlayer2 = false;
        bool useHumanFeedback = false;

        //Variables to store the states and the moves
        List<int> gameStatesPlayer2 = new List<int>();
        List<int> gameMovesPlayer2 = new List<int>();
        List<int> gameStatesPlayer1 = new List<int>();
        List<int> gameMovesPlayer1 = new List<int>();
        int nextMove;
        bool player2Wins;
        bool isDraw = false;

        //Constructor
        public GameInterface()
        {
            //Get human status of players
            assignHumanStatus();
            InitializeComponent();


            if (trainPlayer1Checkbox.Checked == true)
            {
                trainPlayer1 = true;
            }
            else
            {
                trainPlayer1 = false;
            }

            if (trainPlayer2Checkbox.Checked == true)
            {
                trainPlayer2 = true;
            }

            else
            {
                trainPlayer2 = false;
            }
            //Start Game Timer
            gameTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            gameTimer.Tick += new EventHandler(gameEngine);

            //gameTimer.Enabled=true;
            gameTimer.Start();
            gameTimer.IsEnabled = true;

            if (System.IO.File.Exists(@"qMatrixPlayer1.txt"))
            {
                loadQMatrixPlayer1();
            }

            if (System.IO.File.Exists(@"qMatrixPlayer2.txt"))
            {
                loadQMatrixPlayer2();
            }

            //Update Display
            display();



        }

        //This is the function called when the timer fires. It controls the game and is in charge of updating the agents
        private void gameEngine(Object sender, EventArgs e)
        {
            //Updates whether the agents are smart or dumb - not sure of the best place for this code
            if (player1Obj.getHumanStatus() == true || player2Obj.getHumanStatus() == true)
            {
                if (player1Obj.getHumanStatus() == true)
                {
                    player2Obj.setPlayerStyle(true);
                }
                if (player2Obj.getHumanStatus() == true)
                {
                    player1Obj.setPlayerStyle(true);
                }


            }
            //No humans playing, train the agents
            else
            {
                if (trainPlayer1 == true && trainPlayer2 == false)
                {
                    player1Obj.setPlayerStyle(true);
                    player2Obj.setPlayerStyle(false);
                }
                else if (trainPlayer1 == false && trainPlayer2 == true)
                {
                    player1Obj.setPlayerStyle(false);
                    player2Obj.setPlayerStyle(true);
                }
                //If both are training, make them smart
                else
                {
                    player1Obj.setPlayerStyle(true);
                    player2Obj.setPlayerStyle(true);
                }
            }

            //Stop Game Timer until next move
            gameTimer.Stop();

            //Player 1's turn
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
                    int gameMatrixString = concatenateGameMatrix(gameMatrix);
                    int gameMatrixTernIndex = tri2dec(gameMatrixString);
                    gameStatesPlayer1.Add(gameMatrixTernIndex);

                    //This function computes the next optimal move, based on player intelligence and the current game board.
                    nextMove = player1Obj.decideNextMove(ref gameMatrix, ref qMatrixPlayer1);
                    gameMovesPlayer1.Add(nextMove);
                    gameTimer.Stop();
                    updateGame();
                }


            }

            //Player 2's turn
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
                    int gameMatrixString = concatenateGameMatrix(gameMatrix);
                    int gameMatrixTernIndex = tri2dec(gameMatrixString);
                    gameStatesPlayer2.Add(gameMatrixTernIndex);
                    nextMove = player2Obj.decideNextMove(ref gameMatrix, ref qMatrixPlayer2); //Optimal move based on player 2's learning and the game board
                    gameMovesPlayer2.Add(nextMove);
                    gameTimer.Stop();
                    updateGame();

                }
            }
        }

        //Updates the UI with a nought or a cross, depending on who's turn it is
        public void updateGame()
        {
            //A variable determining if the game is over
            int gameOver = 0;

            //Update UI based on the run boolean and which button was clicked
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
            gameOver = checkit();

            //If it is game over, train the agents
            if (gameOver != 0)
            {
                trainAgents(gameOver);
            }

            //Else keep playing
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

        //This displays whose turn it is
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

        //This function checks the current game status to see if there is a winner. It currently iterates through every possible
        //combination, checking if there is a 'three in a row'. Messages are only displayed if one player is human
        public int checkit()
        {
            //0 = no result yet, 1=player 1 wins, 2=player 2 wins, 3=draw
            int gameOver = 0;

            //Start with Button 1 and check 123, 159, 147
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
                    if (humanPlaying == true)
                    {
                        MessageBox.Show("Player 1 Wins!");
                    }
                    player1++;
                    player1Score.Text = player1.ToString();
                    player2Wins = false;
                    gameOver = 1;
                }
                else
                {
                    if (humanPlaying == true)
                    {
                        MessageBox.Show("Player 2 Wins!");
                    }
                    player2++;
                    player2Score.Text = player2.ToString();
                    player2Wins = true;
                    gameOver = 2;
                }
                isDraw = false;
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
                    if (humanPlaying == true)
                    {
                        MessageBox.Show("Player 1 Wins!");
                    }
                    player1++;
                    player1Score.Text = player1.ToString();
                    player2Wins = false;
                    gameOver = 1;
                }
                else
                {
                    if (humanPlaying == true)
                    {
                        MessageBox.Show("Player 2 Wins!");
                    }
                    player2++;
                    player2Score.Text = player2.ToString();
                    player2Wins = true;
                    gameOver = 2;
                }
                isDraw = false;
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
                    if (humanPlaying == true)
                    {
                        MessageBox.Show("Player 1 Wins!");
                    }
                    player1++;
                    player1Score.Text = player1.ToString();
                    player2Wins = false;
                    gameOver = 1;
                }
                else
                {
                    if (humanPlaying == true)
                    {
                        MessageBox.Show("Player 2 Wins!");
                    }
                    player2++;
                    player2Score.Text = player2.ToString();
                    player2Wins = true;
                    gameOver = 2;
                }
                isDraw = false;
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
                    if (humanPlaying == true)
                    {
                        MessageBox.Show("Player 1 Wins!");
                    }
                    player1++;
                    player1Score.Text = player1.ToString();
                    player2Wins = false;
                    gameOver = 1;
                }
                else
                {
                    if (humanPlaying == true)
                    {
                        MessageBox.Show("Player 2 Wins!");
                    }
                    player2++;
                    player2Score.Text = player2.ToString();
                    player2Wins = true;
                    gameOver = 2;
                }
                isDraw = false;
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
                    if (humanPlaying == true)
                    {
                        MessageBox.Show("Player 1 Wins!");
                    }
                    player1++;
                    player1Score.Text = player1.ToString();
                    player2Wins = false;
                    gameOver = 1;
                }
                else
                {
                    if (humanPlaying == true)
                    {
                        MessageBox.Show("Player 2 Wins!");
                    }
                    player2++;
                    player2Score.Text = player2.ToString();
                    player2Wins = true;
                    gameOver = 2;
                }
                isDraw = false;
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
                    if (humanPlaying == true)
                    {
                        MessageBox.Show("Player 1 Wins!");
                    }
                    player1++;
                    player1Score.Text = player1.ToString();
                    player2Wins = false;
                    gameOver = 1;
                }
                else
                {
                    if (humanPlaying == true)
                    {
                        MessageBox.Show("Player 2 Wins!");
                    }
                    player2++;
                    player2Score.Text = player2.ToString();
                    player2Wins = true;
                    gameOver = 2;
                }

                isDraw = false;
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
                    if (humanPlaying == true)
                    {
                        MessageBox.Show("Player 1 Wins!");
                    }
                    player1++;
                    player1Score.Text = player1.ToString();
                    gameOver = 1;
                }
                else
                {
                    if (humanPlaying == true)
                    {
                        MessageBox.Show("Player 2 Wins!");
                    }
                    player2++;
                    player2Score.Text = player2.ToString();
                    player2Wins = true;
                    gameOver = 2;
                }

                isDraw = false;
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
                    if (humanPlaying == true)
                    {
                        MessageBox.Show("Player 1 Wins!");
                    }

                    player1++;
                    player1Score.Text = player1.ToString();
                    player2Wins = false;
                    gameOver = 1;
                }
                else
                {
                    if (humanPlaying == true)
                    {
                        MessageBox.Show("Player 2 Wins!");
                    }
                    player2++;
                    player2Score.Text = player2.ToString();
                    player2Wins = true;
                    gameOver = 2;
                }
                isDraw = false;
            }

            //Case of a draw
            else if (gameMatrix[0] != 0 && gameMatrix[1] != 0 && gameMatrix[2] != 0 && gameMatrix[3] != 0 && gameMatrix[4] != 0 && gameMatrix[5] != 0 && gameMatrix[6] != 0 && gameMatrix[7] != 0 && gameMatrix[8] != 0)
            {
                if (humanPlaying == true)
                {
                    MessageBox.Show("Draw!");
                }
                gameOver = 3;
                isDraw = true;
            }

            //Still playing
            else
            {
                gameOver = 0;
            }

            return gameOver;
        }

        //Rewards the agents
        public void trainAgents(int gameOver)
        {
            //Get some human feedback
            int humanFeedbackReward = 0;

            //Obtain the human feedback - they score the agent's performance. Could potentially speed up training.
            if (useHumanFeedback == true)
            {
                if ((player1Obj.getHumanStatus() == true && player2Obj.getHumanStatus() == false) || (player1Obj.getHumanStatus() == false && player2Obj.getHumanStatus() == true))
                {
                    humanFeedbackReward = getHumanFeedback();

                }
                else
                {
                    humanFeedbackReward = 0;
                }
            }

            //Only update if robot is playing and we are training them
            double reward = 1;
            switch (gameOver)
            {
                //It's a draw
                case 3:     //Don't know if Player 1 should be penalised for a draw or not.......-could stop penalising after a certain time????

                    //Console.WriteLine("Draw!");
                    if (player2Obj.getHumanStatus() == false && trainPlayer2 == true)
                    {
                        //Player 2 is rewarded for a draw - best possible outcome for 2 expert players
                        updateQMatrix(gameStatesPlayer2, gameMovesPlayer2, true, ref gameMatrix, reward + humanFeedbackReward, ref qMatrixPlayer2);

                        //Player 1 is slightly penalised for a draw - otherwise they play for a draw when they could win
                        updateQMatrix(gameStatesPlayer1, gameMovesPlayer1, true, ref gameMatrix, -10 * reward - humanFeedbackReward, ref qMatrixPlayer1);
                    }
                    break;

                //Player 1 won
                case 1:

                    if (player2Obj.getHumanStatus() == false && trainPlayer2 == true)
                    {
                        //Penalise player 2 for the loss - an expert player should draw
                        updateQMatrix(gameStatesPlayer2, gameMovesPlayer2, true, ref gameMatrix, -50 * reward - humanFeedbackReward, ref qMatrixPlayer2);
                    }

                    if (player1Obj.getHumanStatus() == false && trainPlayer1 == true)
                    {
                        //Reward Player 1 for the win
                        updateQMatrix(gameStatesPlayer1, gameMovesPlayer1, true, ref gameMatrix, reward + humanFeedbackReward, ref qMatrixPlayer1);
                    }

                    break;
                //Player 2 won
                case 2:

                    //Console.WriteLine("PLayer 2 Wins");
                    if (player2Obj.getHumanStatus() == false && trainPlayer2 == true)
                    {
                        //Reward Player 2
                        updateQMatrix(gameStatesPlayer2, gameMovesPlayer2, true, ref gameMatrix, reward + humanFeedbackReward, ref qMatrixPlayer2);
                    }

                    if (player1Obj.getHumanStatus() == false && trainPlayer1 == true)
                    {
                        //Penalise Player 1 heavily - a loss is not acceptable
                        updateQMatrix(gameStatesPlayer1, gameMovesPlayer1, false, ref gameMatrix, -50 * reward - humanFeedbackReward, ref qMatrixPlayer1);
                    }
                    break;
            }

            //If we are training the agents, save their QMatrices
            if (trainPlayer1 == true)
            {
                saveQMatrixPlayer1();
            }
            if (trainPlayer2 == true)
            {
                saveQMatrixPlayer2();
            }


            cleargame();





            //No winner yet, keep playing and don't change Q matrices
            /*  else
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
              }*/
        }

        //Turns a number into a string of digits
        public int concatenateGameMatrix(int[] currentGame)
        {
            int listIndex = 0;
            listIndex = int.Parse(currentGame[0].ToString() + currentGame[1].ToString() + currentGame[2].ToString() + currentGame[3].ToString() + currentGame[4].ToString() + currentGame[5].ToString() + currentGame[6].ToString() + currentGame[7].ToString() + currentGame[8].ToString());
            return listIndex;
        }

        //This function updates the Q Matrix for a given player. It needs to be run at the end of each game, storing information about the episode.
        public void updateQMatrix(List<int> states, List<int> moves, bool wonGame, ref int[] gameArray, double reward, ref double[,] qMatrix)
        {
            double alpha = 1;
            double gamma = 0.5;
            List<int> playable_Squares = new List<int>();
            int j = 0;

            //Cycle through states, adding the states and moves from the previous episode to the q matrix. This will be done at the end of the game, so 
            //the states and moves need to be saved in the main code.
            double delayFactor = 1;
            int maxStatesToAdd = states.Count();
            for (int i = 0; i < states.Count(); i++)
            {
                qMatrix[states[i], moves[i]] = qMatrix[states[i], moves[i]] + (reward) * delayFactor;
                delayFactor = delayFactor + 0;
            }
        }


        //Converts ternary to decimal. This is used as part of an encoding system for reducing the size of the game matrix file.
        //The game matrix is a size-9 vector with either 0,1,2. Assuming it is a ternary number and converting to decimal for file storage is more
        //efficient
        public int tri2dec(int decNumber)
        {
            int b, k, n;
            int len, sum = 0;
            string decNumberString = decNumber.ToString();

            len = decNumberString.Length - 1;
            b = 1;
            for (k = len; k >= 0; k--)
            {

                n = (decNumberString[k] - '0');
                sum = sum + n * b;
                b = b * 3;
            }
            return (sum);
        }

        //Resets the game - clears UI and sets scores to 0
        public void resetgame()

        {
            //Clears UI
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

        //Clears the game - same as UI but scores are not set to 0
        public void cleargame()
        {

            //Resets the turn to player 1
            turn = 1;

            //Resets all boolean indicators - so no squares have been clicked
            click0 = 0;
            click1 = 0;
            click2 = 0;
            click3 = 0;
            click4 = 0;
            click5 = 0;
            click6 = 0;
            click7 = 0;
            click8 = 0;

            //Clears the states and moves for each player
            gameStatesPlayer1.Clear();
            gameMovesPlayer1.Clear();
            gameStatesPlayer2.Clear();
            gameMovesPlayer2.Clear();

            //Clears the UI
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

            //SaveQ MAtrix
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

            //Assign human status to players - determines if the agent plays or not 
            humanPlaying = false;
            if (checkPlayer1.Checked)
            {
                player1Obj.assignHumanStatus(true);
                humanPlaying = true;
            }
            else
                player1Obj.assignHumanStatus(false);

            if (checkPlayer2.Checked)
            {
                player2Obj.assignHumanStatus(true);
                humanPlaying = true;
            }
            else
                player2Obj.assignHumanStatus(false);

            //Assign Player 1 and Player 2 to human/robot - need to add smart code for this
            player1Obj.setPlayerNumber(1);
            player2Obj.setPlayerNumber(2);

            

        }

        //This function calls a dialog box to determine which players are human and which are robots
        private int getHumanFeedback()
        {
            Form form1 = new Form();
            // Create two buttons to use as the accept and cancel buttons.
            Button button1 = new Button();

            CheckBox checkPlayer1 = new CheckBox();
            CheckBox checkPlayer2 = new CheckBox();
            CheckBox checkPlayer3 = new CheckBox();

            int reward = 0;
            // Set the text of button1 to "OK".
            button1.Text = "Done";

            // Set the position of the button on the form.
            button1.Location = new Point(100, 50);

            //Text of Check boxes
            checkPlayer1.Text = "Yes";
            checkPlayer2.Text = "Mostly";
            checkPlayer3.Text = "No";

            checkPlayer1.Location = new Point(5, 20);
            checkPlayer2.Location = new Point(checkPlayer1.Left, checkPlayer1.Height + checkPlayer1.Top + 10);
            checkPlayer3.Location = new Point(checkPlayer2.Left, checkPlayer2.Height + checkPlayer2.Top + 10);

            // Set the caption bar text of the form.   
            form1.Text = "Did I Play Well?";

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
            form1.Controls.Add(checkPlayer3);

            // Display the form as a modal dialog box.
            form1.ShowDialog();

            //Assign human status to players 
            humanPlaying = false;
            if (checkPlayer1.Checked)
            {
                reward = 10;
            }

            else if (checkPlayer2.Checked)
            {
                reward = 5;
            }

            else if (checkPlayer3.Checked)
            {
                reward = -5;
            }

            else
            {
                reward = 0;
            }
            return reward;
        }

        //These functions wait for mouse clicks - only applicable if human player. There is one per square on the tic tac toe board.
        //When a button is clicked, updateGame updates scores, and rewards the agents based on their moves. Turn is a value of either 1 or 2, 
        //and game matrix is a vector that records who has taken which squares

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
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (click2 == 0)
            {
                gameMatrix[2] = turn;
                updateGame();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (click3 == 0)
            {
                gameMatrix[3] = turn;
                updateGame();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (click4 == 0)
            {
                gameMatrix[4] = turn;
                updateGame();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (click5 == 0)
            {
                gameMatrix[5] = turn;
                updateGame();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (click6 == 0)
            {
                gameMatrix[6] = turn;
                updateGame();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (click7 == 0)
            {
                gameMatrix[7] = turn;
                updateGame();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (click8 == 0)
            {
                gameMatrix[8] = turn;
                updateGame();
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


        //This saves the update matrix to file
        public void saveQMatrixPlayer2()
        {

            System.IO.StreamWriter sw = new System.IO.StreamWriter("qMatrixPlayer2.txt");

            //Loop through each of the scores for each game configuration
            for (int i = 0; i < qMatrixPlayer2.GetLength(0); i++)
            {
                for (int j = 0; j < qMatrixPlayer2.GetLength(1); j++)
                {
                    sw.WriteLine(qMatrixPlayer2[i, j].ToString());
                }
            }

            sw.Flush();
            sw.Close();
        }

        //Reads the update matrix
        public void loadQMatrixPlayer2()
        {
            int counter = 0;
            int k = 0;
            string line;
            double[] temp = new double[178749];

            // Read the file and display it line by line.
            System.IO.StreamReader file = new System.IO.StreamReader("qMatrixPlayer2.txt");
            while ((line = file.ReadLine()) != null)
            {

                temp[counter] = Convert.ToDouble(line);
                counter++;
            }

            //Loop through each of the scores for each game configuration
            for (int i = 0; i < 19861; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    qMatrixPlayer2[i, j] = temp[k];
                    k = k + 1;
                }
            }

            // Console.WriteLine(qMatrixPlayer2[2,6]);
            file.Close();

        }

        //Saves the update matrix for player 1
        public void saveQMatrixPlayer1()
        {

            System.IO.StreamWriter sw = new System.IO.StreamWriter("qMatrixPlayer1.txt");

            //Loop through each of the scores for each game configuration
            for (int i = 0; i < qMatrixPlayer1.GetLength(0); i++)
            {
                for (int j = 0; j < qMatrixPlayer1.GetLength(1); j++)
                {
                    sw.WriteLine(qMatrixPlayer1[i, j].ToString());
                }
            }

            sw.Flush();
            sw.Close();
        }

        //Read the update matrix for Player 1
        public void loadQMatrixPlayer1()
        {
            int counter = 0;
            int k = 0;
            string line;
            double[] temp = new double[178749];

            // Read the file and display it line by line.
            System.IO.StreamReader file = new System.IO.StreamReader("qMatrixPlayer1.txt");
            while ((line = file.ReadLine()) != null)
            {

                temp[counter] = Convert.ToDouble(line);
                counter++;
            }

            //Loop through each of the scores for each game configuration
            for (int i = 0; i < 19861; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    qMatrixPlayer1[i, j] = temp[k];
                    k = k + 1;
                }

            }
            // Console.WriteLine(qMatrixPlayer2[2,6]);
            file.Close();

        }

        //Reads in whether the player 1 agent is being trained or not
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (trainPlayer1Checkbox.Checked == true)
            {
                trainPlayer1 = true;
            }

            else
            {
                trainPlayer1 = false;
            }
        }

        //Reads in whether the player 2 agent is being trained or not
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (trainPlayer2Checkbox.Checked == true)
            {
                trainPlayer2 = true;
            }

            else
            {
                trainPlayer2 = false;
            }
        }

        //Reads in whether we are using human feedback to train the agent
        private void usingHumanFeedbackCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (usingHumanFeedbackCheckBox.Checked == true)
            {
                useHumanFeedback = true;
            }

            else
            {
                useHumanFeedback = false;
            }
        }
    }
}
