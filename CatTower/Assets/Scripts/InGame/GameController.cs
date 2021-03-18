using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace CatTower
{
    public class GameController : SingletonGameObject<GameController>
    {
        public bool controllAble;
        public GameObject myCards;
        public GameObject Slot;

        private WebSocketManager webSocket;
        private int currentRound; // 현재 진행되고 있는 라운드 수
        private int currentOrder; // 현재 라운드 내에서 진행되고 있는 순서
        private (UserInfo, bool)[] playerOrder; // Tuple 형식으로 첫번째에는 유저 정보를, 두번째에는 유저의 포기 여부를 저장
        private int myOrder;
        private IGameState gameState;
        [SerializeField] private GameUIController uiController;

        void Awake()
        {
            currentRound = 0;
            currentOrder = 0;
            webSocket = WebSocketManager.Instance;
            webSocket.Connect("/ingame", () =>
            {
                AlertRoundStart();
                webSocket.ReceiveEvent<IngameStatus>("/ingame", "status", StatusEventReceived);
                webSocket.ReceiveEvent<IngameCardGive>("/ingame", "cardgive", ShowCardDeck);
                webSocket.ReceiveEvent<IngamePlayerOrder>("/ingame", "playerorder", ShowInitialPlayersInfo);
                webSocket.ReceiveEvent<IngameEndRound>("/ingame", "endround", (response) =>
                {
                    EndRound(response);
                });
            });
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Ray2D ray = new Ray2D(wp, Vector2.zero);
                RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction);

                foreach (var h in hits)
                {
                    Debug.Log(h.collider.name);
                }
            }
        }

        public void throwInfo(string s, int i)
        {
            CardInfo myCardinfo = new CardInfo();
            myCardinfo.breed = s;
            myCardinfo.index = i;
            foreach (var player in playerOrder)
            {
                if (player.Item1.mid == UserData.mid)
                {
                    webSocket.SendEvent<IngameThrow>("/ingame", "throw",
                    new IngameThrow
                    {
                        roomId = JoinedRoom.roomId,
                        user = player.Item1,
                        card = myCardinfo
                    });
                }
            }
        }

        public void AlertRoundStart()
        {
            webSocket.SendEvent<IngameStart>("/ingame", "start",
                new IngameStart
                {
                    roomId = JoinedRoom.roomId,
                    round = currentRound,
                    user = new UserInfo
                    {
                        mid = UserData.mid,
                        nickname = UserData.nickName
                    }
                });
        }

        public void ShowCardDeck(IngameCardGive response)
        {
            var layout = myCards.transform.Find("Layout");
            for (int i = 0; i < layout.childCount; i++)
            {
                Destroy(layout.GetChild(i).gameObject);
            }

            Debug.Log(response.cards);
            myCards.GetComponent<CardDB>().GetCard(response.cards);

            Slot.GetComponent<SlotManager>().ResetSlot();
            Slot.GetComponent<SlotManager>().ResetSprite();
        }

        public void ShowInitialPlayersInfo(IngamePlayerOrder response)
        {
            playerOrder = new (UserInfo, bool)[response.player.Count];

            for (int i = 0; i < response.player.Count; i++)
            {
                playerOrder[i] = (response.player[i].userInfo, response.player[i].giveup);

                if (response.player[i].userInfo.mid == UserData.mid)
                {
                    myOrder = response.player[i].order;
                }
            }
            uiController.ShowInitialPlayerList(response.player);
            StateChanged();
        }



        public void UpdateBoard(IngameStatus response)
        {
            uiController.UpdatePlayerInfo(response.player, response.order);

            currentOrder = response.order;

            for (int i = 0; i < 57; i++)
            {
                if (response.board[i] != "X" && Slot.GetComponent<SlotManager>().arrSlotIndex[i] == 0) // 확인되지 않은 것만 업데이트
                {
                    Slot.GetComponent<SlotManager>().arrSlotIndex[i] = 1;
                    if (response.board[i] == "A")
                    {
                        Slot.GetComponent<SlotManager>().arrSlotBreed[i] = Breed.Mackerel;
                    }
                    else if (response.board[i] == "B")
                    {
                        Slot.GetComponent<SlotManager>().arrSlotBreed[i] = Breed.Siamese;
                    }
                    else if (response.board[i] == "C")
                    {
                        Slot.GetComponent<SlotManager>().arrSlotBreed[i] = Breed.Persian;
                    }
                    else if (response.board[i] == "D")
                    {
                        Slot.GetComponent<SlotManager>().arrSlotBreed[i] = Breed.Ragdoll;
                    }
                    else if (response.board[i] == "E")
                    {
                        Slot.GetComponent<SlotManager>().arrSlotBreed[i] = Breed.RussianBlue;
                    }
                    else if (response.board[i] == "S1")
                    {
                        Slot.GetComponent<SlotManager>().arrSlotBreed[i] = Breed.ThreeColor;
                    }
                    else if (response.board[i] == "S2")
                    {
                        Slot.GetComponent<SlotManager>().arrSlotBreed[i] = Breed.Odd;
                    }
                }
                GetComponent<Drag>().SetSprite();
            }

            if (!response.giveup) return;

            for (int i = 0; i < playerOrder.Length; i++)
            {
                if (playerOrder[i].Item1.mid == response.user.mid) 
                {
                    playerOrder[i].Item2 = response.giveup;
                    return;
                }
            }
        }

        public void StatusEventReceived(IngameStatus response)
        {
            gameState.InFinish(response);
        }

        public void StateChanged()
        {
            //모든유저 giveup -> 라운드 종료
            if (myOrder == currentOrder)
            {
                gameState = new PlayingGameState();
            }
            else
            {
                gameState = new WaitGameState();
            }
            gameState.InStart();
        }

        public void FindMyGiveUp()
        {
            foreach (var player in playerOrder) // 사용할 수 있는 카드가 없을 때 giveup
            {
                if (player.Item1.mid == UserData.mid)
                {
                    webSocket.SendEvent<IngameGiveUp>("/ingame", "giveup",
                    new IngameGiveUp
                    {
                        userInfo = player.Item1,
                        roomId = JoinedRoom.roomId
                    });
                    break;
                }
            }
        }

        public void EndRound(IngameEndRound response)
        {
            foreach (var player in playerOrder)
            {
                if (player.Item1.mid == UserData.mid)
                {
                    webSocket.SendEvent<IngameFinish>("/ingame", "finish",
                    new IngameFinish
                    {
                        roomId = JoinedRoom.roomId,
                        user = player.Item1,
                        round = currentRound,
                        leftCard = GetComponent<CheckUsable>().myScore
                    });
                }
            }
            currentRound++;
        }
    }
}
