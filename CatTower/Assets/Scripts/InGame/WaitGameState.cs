using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatTower
{
    public class WaitGameState : IGameState
    {
        public void InStart()
        {
            // TODO: 다른 사람의 플레이를 기다리는 것이기 때문에, 그동안은 카드 움직이는게 불가능하도록 구현해야 함
        }

        public void InFinish(IngameStatus response)
        {
            GameController.Instance.UpdateBoard(response);
            GameController.Instance.StateChanged();
        }
    }
}
