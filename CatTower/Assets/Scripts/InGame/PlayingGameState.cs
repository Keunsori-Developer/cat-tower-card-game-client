using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatTower
{
    public class PlayingGameState : IGameState
    {
        
        public void InStart()
        {

            GameController.Instance.controllAble = true;
            // TODO: 포기를 했으면 InFinish() 호출
        }

        public void InFinish()
        {
            GameController.Instance.controllAble = false;
            GameController.Instance.StateChanged();
        }
    }
}
