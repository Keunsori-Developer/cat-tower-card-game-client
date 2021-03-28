using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatTower
{
    public class WaitGameState : IGameState
    {
        public void InStart()
        {
            Debug.Log("WaitGameState InStart()");
        }

        public void InFinish(IngameStatus response)
        {
            Debug.Log("WaitGameState InFinish()");
            GameController.Instance.UpdateBoard(response);
            GameController.Instance.StateChanged();
        }
    }
}
