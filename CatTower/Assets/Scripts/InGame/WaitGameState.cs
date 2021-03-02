using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatTower
{
    public class WaitGameState : IGameState
    {
        public void InStart()
        {
            
        }

        public void InFinish(IngameStatus response)
        {
            GameController.Instance.UpdateBoard(response);
            GameController.Instance.StateChanged();
        }
    }
}
