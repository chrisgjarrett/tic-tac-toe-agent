using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//This class defines a "Player". 
namespace Player
{
    public class PlayerObject
    {
        int currentScore = 0;
        bool humanStatus = false;
        int playerNumber = 0;
        int[] currentGame = {0, 0, 0, 0, 0, 0, 0, 0, 0};
        bool styleOfPlayIntelligent;

        Random ran;  //Random Number Generator
        Random ranEps;

        //Constructor
        public PlayerObject()
        {
            ranEps = new Random();
            ran = new Random();
        }

        //This method determines if the agent plays intelligently or makes random moves
        public void setPlayerStyle(bool style)
        {
            styleOfPlayIntelligent = style;

        }

        //Assigns a 'player' to be player 1 or player 2
        public void setPlayerNumber(int playerNumberToSet)
        {
            playerNumber = playerNumberToSet;
        }

        //Returns the player number
        public int getPlayerNumber()
        {
            return playerNumber;

        }

        //Updates the player's internal game knowledge - tells them which squares have been played
        public void updateCurrentGame(ref int[] gameArray)
        {
            currentGame = gameArray;
        }

        //This decides if the player is human
        public void assignHumanStatus(bool isHuman) {

            if (isHuman)
            {
                humanStatus = true;
            }

            else
            {
                humanStatus = false;
            }
                
        }

        //This returns if the player is human
        public bool getHumanStatus()
        {
            return humanStatus;
        }


        //This decides the next move - only relevant to computers
        public int decideNextMove(ref int[] gameArray, ref double[,] qMatrix)
        {


            int j = 0;
            int chosenSquare = -1;
            List<int> playableSquares = new List<int>();

            //Figure which squares are playable
            foreach (int i in gameArray)
            {
                if (i == 0)
                {
                    playableSquares.Add(j);
                }
                j = j + 1;
            }
            /*
            //Debug - checking playable squares
            for (int ii = 0; ii < playableSquares.Count; ii++)
            {
                System.Console.WriteLine(playableSquares[ii]);
            }
            System.Console.WriteLine(" ");
            */
            double epsilon = 0.05;
            
            //Compute a random value - epsilon 
            double randomNumber = ranEps.NextDouble();

            //Make a random move 5% of the time (random number generates between 0 and 1) or every move if the player is not playing intelligently.
            if (randomNumber < epsilon || styleOfPlayIntelligent == false)
            {
                //Console.WriteLine(randomNumber);
                     
                //Generate a new random number to choose the playing square           
                int index = ran.Next(playableSquares.Count);
                chosenSquare = playableSquares[index];

                //'Exploring' by choosing least picked options
                //chosenSquare = explorationMode(playableSquares, gameArray, qMatrix); 
                //Console.WriteLine("Player 2 Exploring"); - check exploration code
                
                //Assign the player's number to the square
                gameArray[chosenSquare] = getPlayerNumber();
                //Console.WriteLine(chosenSquare.ToString());
            }

           //If not making a random move, choose the optimal move
           else
           {
                chosenSquare = optimalNextMove(playableSquares, gameArray, qMatrix);
                gameArray[chosenSquare] = getPlayerNumber();
           }
                return chosenSquare;
        }

        //Compute the optimal move
        public int optimalNextMove(List<int> playableSquares, int[] gameMatrix, double[,] qMatrix)
        {

            double bestMoveReward = -1;
            List<double> tempMoveReward = new List<double>();
            int bestMove =-1;
            int index = 0;
            int bestMoveIndex = -1;
            int indexTern = 0;
            int currentGameIndex = tri2dec(concatenateGameMatrix(gameMatrix));

            int j = 0;
            int[] tempGameArray = gameMatrix;
            //Console.WriteLine(tempGameArray);

            //Figure out which move has been most successful in the past given the current board state. Cycle through the remaining playable squares, checking each
            //possible move
            foreach (int i in playableSquares)
            {

                //tempGameArray.Equals(gameMatrix);
                //tempGameArray[playableSquares[j]] = getPlayerNumber();

                //The game array is considered a ternary number - it needs to be converted to a decimal number to be operated on 
                index = concatenateGameMatrix(tempGameArray);
                indexTern = tri2dec(index);
                tempMoveReward.Add(qMatrix[currentGameIndex, i]);
                
            }
            //Console.WriteLine(tempMoveReward.ToString());
            

            bestMoveReward = tempMoveReward.Max();
            bestMoveIndex = tempMoveReward.IndexOf(bestMoveReward);
            bestMove = playableSquares[bestMoveIndex];
           //Console.WriteLine(bestMoveReward.ToString());
            
            return bestMove;

        }

        //This code should explore the least popular option. Currently, it looks for the minimum - this is incorrect as it will just choose the worst move.
        //It should be picking the least played move - potentially the moves that have rewards closest to 0?
        public int explorationMode(List<int> playableSquares, int[] gameMatrix, double[,] qMatrix)
        {

            double bestMoveReward = -1;
            List<double> tempMoveReward = new List<double>();
            int bestMove = -1;
            int index = 0;
            int bestMoveIndex = -1;
            int indexTern = 0;
            int currentGameIndex = tri2dec(concatenateGameMatrix(gameMatrix));

            int j = 0;
            int[] tempGameArray = gameMatrix;
            //Console.WriteLine(tempGameArray);
            //compute move that yields the maximum
            foreach (int i in playableSquares)
            {

                //tempGameArray.Equals(gameMatrix);
                //tempGameArray[playableSquares[j]] = getPlayerNumber();

                index = concatenateGameMatrix(tempGameArray);
                indexTern = tri2dec(index);
                tempMoveReward.Add(Math.Abs(qMatrix[currentGameIndex, i]));

            }
            //Console.WriteLine(tempMoveReward.ToString());

            //Change this to pick the move closest to 0
            bestMoveReward = tempMoveReward.Min();
            bestMoveIndex = tempMoveReward.IndexOf(bestMoveReward);
            bestMove = playableSquares[bestMoveIndex];
            Console.WriteLine(bestMoveReward.ToString());

            return bestMove;

        }

        //Turns a number into a string of digits
        public int concatenateGameMatrix(int[] currentGame)
        {
            int listIndex = 0;
            listIndex = int.Parse(currentGame[0].ToString() + currentGame[1].ToString() + currentGame[2].ToString() + currentGame[3].ToString() + currentGame[4].ToString() + currentGame[5].ToString() + currentGame[6].ToString() + currentGame[7].ToString() + currentGame[8].ToString());
            return listIndex;
        }


        //Converts ternary to decimal for the encoding system - game array is a vector of 0,1,2 and so it can be represented as a ternary number
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

    }

}


