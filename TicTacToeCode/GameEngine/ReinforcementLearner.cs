using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngineDLL
{
    class ReinforcementLearner
    {
        int[] QMatrix = { };
        int reward = 500;
        List<int> stateMapping;
        
        //This function takes a gameHistory matrix - all board states, a another list of player Moves made in response to those states, and a 
        //bool indicating if it was a win or a loss.
        public void updateMatrix(bool win, List<int[]> gameHistory, List<int[]> playerMoves) {
            
        }
    }
}
