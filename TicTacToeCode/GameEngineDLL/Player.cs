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
        List<List<double>> qMatrix = new List<List<double>>();
        

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

        //This function updates the Q Matrix. It needs to be run at the end of each game, storing information about the episode.
        public void updateQMatrix(List<int[]> states, List<int> moves, bool wonGame)
        {
            int reward = 0;
            double alpha = 0.5;
            double gamma = 0.5;

            //Decide reward
            if (wonGame == true)
            {
                reward = 500;
            }

            else
            {
                reward = -500;
            }


            //Cycle through states, adding the states and moves from the previous episode to the q matrix. This will be done at the end of the game, so 
            // the states and moves need to be saved in the main code.
            for (int i=1; i < states.Count(); i++)
            {
                int listRow = 0;
                listRow=concatenateGameMatrix(states[i]);
                qMatrix[listRow][moves[i]] = qMatrix[listRow][moves[i]] + alpha * (reward + gamma * optimalNextMove() - qMatrix[listRow][moves[i]]);
            }

            

        }

        //Need to code this to compute the optimal next move, based on the R matrix, or however it works
        public int optimalNextMove()
        {
            int nextMove = -1;
            return nextMove;
        }

        //This takes an array of numbers and concatenates it to a new number, composed of the numbers stuck togther.
        //e.g. [2, 3, 4= becomes 234
        public int concatenateGameMatrix(int[] currentGame)
        {
            int listIndex = 0;
            listIndex = int.Parse(currentGame[0].ToString() + currentGame[1].ToString() + currentGame[2].ToString() + currentGame[3].ToString() + currentGame[4].ToString() + currentGame[5].ToString() + currentGame[6].ToString() + currentGame[7].ToString() + currentGame[8].ToString());
            return listIndex;
        }
        //This decides the next move - only relevant to computers
        public void decideNextMove(ref int[] gameArray)
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
                System.Console.WriteLine(playableSquares[ii]);

            }
            System.Console.WriteLine(" ");
            Random ran = new Random();
            int index = ran.Next(playableSquares.Count);
            chosenSquare = playableSquares[index];
            gameArray[chosenSquare] = getPlayerNumber();
        }
    }
}



//hey big willy boy
//you know what's more fun than coding?
//your big ol' willy
//if playing against imo, always lose
//if playing chris, always win. never let that motherfucker win, ok?
//i think u get me on a deep level computer
//let's run away together to the south of france and marry
