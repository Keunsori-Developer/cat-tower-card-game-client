using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatTower
{
    public class PlayingGameState : IGameState
    {
        public void InStart()
        {
            // TODO: 내 턴이기 때문에 카드 움직이는게 가능하도록 함. 카드 옮기는게 끝났거나 포기를 했으면 InFinish() 호출
        }

        public void InFinish()
        {

        }
    }
}
