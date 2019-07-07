using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Player
{
    public class PlayerObject
    {
        int currentScore = 0;
        bool humanStatus = false;
        int playerNumber = 0;
        int[] currentGame = {0, 0, 0, 0, 0, 0, 0, 0, 0};
        bool styleOfPlayIntelligent;
        //double[,] qMatrix = new double[19861, 9];
        Random ran;  //Random Number Generator
        Random ranEps;

        public PlayerObject()
        {
            ranEps = new Random();
            ran = new Random();
        }
            public void setPlayerStyle(bool style)
        {
            styleOfPlayIntelligent = style;

        }
        public void setPlayerNumber(int playerNumberToSet)
        {
            playerNumber = playerNumberToSet;

        }

        public int getPlayerNumber()
        {
            return playerNumber;

        }

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

            //Debug - checking playable squares
            for (int ii = 0; ii < playableSquares.Count; ii++)
            {
                //System.Console.WriteLine(playableSquares[ii]);

            }
            //System.Console.WriteLine(" ");

            double epsilon = 0.00;
            //Compute a random value epsilon 
            double randomNumber = ranEps.NextDouble();

            if (randomNumber < epsilon || styleOfPlayIntelligent == false)
            {
                //Console.WriteLine(randomNumber);
                
                int index = ran.Next(playableSquares.Count);
                chosenSquare = playableSquares[index];
                //chosenSquare = explorationMode(playableSquares, gameArray, qMatrix); //'Exploring' by choosing least picked options
                //Console.WriteLine("Player 2 Exploring"); - check exploration code
                gameArray[chosenSquare] = getPlayerNumber();
                //Console.WriteLine(chosenSquare.ToString());
            }

           else
           {
                chosenSquare = optimalNextMove(playableSquares, gameArray, qMatrix);
                gameArray[chosenSquare] = getPlayerNumber();
           }
                return chosenSquare;
        }

        //Need to code this to compute the optimal next move, based on the R matrix, or however it works
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
            //compute move that yields the maximum
            foreach (int i in playableSquares)
            {

                //tempGameArray.Equals(gameMatrix);
                //tempGameArray[playableSquares[j]] = getPlayerNumber();

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

        //Need to code this to compute the optimal next move, based on the R matrix, or however it works
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


        //Converts ternary to decimal
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


