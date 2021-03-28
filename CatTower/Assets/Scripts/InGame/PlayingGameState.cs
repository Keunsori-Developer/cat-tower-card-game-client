using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatTower
{
    public class PlayingGameState : IGameState
    {
        public void InStart()
        {
            Debug.Log("PlayingGameState InStart()");
            if (GameController.Instance.myCards.GetComponent<CheckUsable>().usableCard == true)
            {
                GameController.Instance.controllAble = true;
            }
            else
            {
                GameController.Instance.FindMyGiveUp();
            }
        }

        public void InFinish(IngameStatus response)
        {
            Debug.Log("PlayingGameState InFinish()");
            GameController.Instance.controllAble = false;
            GameController.Instance.UpdateBoard(response);
            GameController.Instance.StateChanged();
        }
    }
}
