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

            WebSocketManager.Instance.ReceiveEvent<IngameStatus>("/ingame", "status", (response) =>
            {
                GameController.Instance.UpdateBoard(response);
                InFinish();
            });
        }

        public void InFinish()
        {
            WebSocketManager.Instance.CancelToReceiveEvent("/ingame", "status");
            GameController.Instance.StateChanged();
        }
    }
}
