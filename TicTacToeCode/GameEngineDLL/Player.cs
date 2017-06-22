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