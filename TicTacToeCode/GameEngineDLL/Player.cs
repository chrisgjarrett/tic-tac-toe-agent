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
        bool humanStatus=false;
        int playerNumber = 0;

        public void setPlayerNumber(int playerNumberToSet)
        {
            playerNumber = playerNumberToSet;

        }

        public int getPlayerNumber()
        {
            return playerNumber;

        }

        //This decides if the player is human
        public void assignHumanStatus(bool isHuman) {

            if (isHuman == true)
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
        public int[] decideNextMove(int[] gameArray)
        {
            //Figure which squares are playable
            bool[] playableSquares= {false};


            for (int i=0; i==9; i++)
            {
                if (gameArray[i] == 0)
                {
                    playableSquares[i] = true;
                }

                else
                {
                    playableSquares[i] = false;

                }
            }

            //Play the first available square
            for (int i=0; i==8; i++)
            {
                if (playableSquares[i] == true)
                {
                    gameArray[i] = getPlayerNumber();
                    break;
                }
                
            }
                return gameArray;

        }

    }
}