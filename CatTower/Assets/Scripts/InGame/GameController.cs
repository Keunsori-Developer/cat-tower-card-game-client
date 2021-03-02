﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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



        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        void Awake()
        {
            currentRound = 0;
            currentOrder = 0;
            webSocket = WebSocketManager.Instance;
            webSocket.Connect("/ingame", () =>
            {
                AlertRoundStart();
                webSocket.ReceiveEvent<IngameStatus>("/ingame", "status", (response) =>
                {
                    gameState.InFinish(response);
                });
                webSocket.ReceiveEvent<IngameCardGive>("/ingame", "cardgive", ShowCardDeck);
                webSocket.ReceiveEvent<IngamePlayerOrder>("/ingame", "playerorder", ShowInitialPlayersInfo);
                /*WebSocketManager.ReceiveEvent<InGameEndRound>("/ingame", "endround", (response) =>
                {
                    FinishRound(response)
                });*/
            });
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
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
                        roomId = "RKH6E", // TODO: 추후 민호가 구현한거에서 받아와야함
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
                    //RKH6E {"mid" : "GWCSE1622", "nickname" : "김창렬"}
                    roomId = "RKH6E", // TODO: 추후 민호가 구현한거에서 받아와야함
                    round = currentRound,
                    user = new UserInfo
                    {
                        mid = "GWCSE1622",
                        nickname = "김창렬"
                    }
                });
        }

        public void ShowCardDeck(IngameCardGive response)
        {
            Debug.Log(response.cards);
            List<string> cards = new List<string>();
            cards = response.cards;
            GetComponent<CardDB>().GetCard(response.cards);
            while (myCards.transform.GetChild(0)) //없을 때 까지 삭제
            {
                Destroy(myCards.transform.GetChild(0));
            }
            Slot.GetComponent<SlotManager>().ResetSlot();
            Slot.GetComponent<SlotManager>().ResetSprite();
        }

        public void ShowInitialPlayersInfo(IngamePlayerOrder response)
        {
            playerOrder = new (UserInfo, bool)[response.playerOrder.Count];
            foreach (var player in response.playerOrder)
            {
                if (player.userInfo.mid == UserData.mid)
                {
                    myOrder = player.order;
                }
                //TODO: response 데이터를 기반으로 화면에 유저들 정보를 보여주고, 내 순서 또한 확인해서 myOrder에 값을 넣음
            }
            StateChanged();
        }

        public void UpdateBoard(IngameStatus response)
        {
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

            // TODO: response 정보를 토대로 게임판을 업데이트하고, 플레이어가 포기를 했는지 안했는지 또한 체크
            foreach (var player in response.player)
            {
                if (player.userInfo.mid != UserData.mid)
                {
                    if (player.giveup == true)
                    {
                        //TODO: 닉네임 옆에 포기! 가 뜨게 구현
                    }
                }
            }
        }

        public void StateChanged()
        {
            return; //TODO: 추후 지울 것!
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
                        roomId = "RKH6E"
                    });
                    break;
                }
            }
        }

        /*public void FinishRound(ingameEndRound response){
            foreach (var player in playerOrder)
            {
                if (player.Item1.mid == UserData.mid)
                {
                    webSocket.SendEvent<IngameFinish>("/ingame", "finish",
                    new IngameFinish
                    {
                        roomId = "RKH6E",
                        user = player.Item1,
                        round = currentRound,
                        leftCard = GetComponent<CheckUsable>().myScore
                    });
                }
            }
            currentRound++;
        }*/
    }
}
