using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatTower
{
    public class GameController : MonoBehaviour
    {
        private static GameController _instance;
        public bool controllAble;
        public GameObject myCards;
        public GameObject Slot;
        public static GameController Instance
        {
            get
            {
                if (_instance == null) _instance = new GameController();
                return _instance;
            }
        }

        private WebSocketManager webSocket;
        private int currentRound; // 현재 진행되고 있는 라운드 수
        private int currentOrder; // 현재 라운드 내에서 진행되고 있는 순서
        private (Userinfo, bool)[] playerOrder; // Tuple 형식으로 첫번째에는 유저 정보를, 두번째에는 유저의 포기 여부를 저장
        private int myOrder;
        private IGameState gameState;



        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        void Awake()
        {
            currentRound = 0;
            currentOrder = -1;
            webSocket = WebSocketManager.Instance;
            webSocket.Connect("/ingame", () =>
            {
                AlertRoundStart();
                webSocket.ReceiveEvent<IngameCardGive>("/ingame", "cardgive", ShowCardDeck);
                webSocket.ReceiveEvent<IngamePlayerOrder>("/ingame", "playerorder", ShowInitialPlayersInfo);
            });
            StateChanged();
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void throwInfo(string s, int i)
        {
            CardInfo myCardinfo = new CardInfo();
            myCardinfo.breed = s;
            myCardinfo.index = i;
            webSocket.SendEvent<IngameThrow>("/ingame", "throw",
                new IngameThrow
                {
                    //RKH6E {"mid" : "GWCSE1622", "nickname" : "김창렬"}
                    roomId = "RKH6E", // TODO: 추후 민호가 구현한거에서 받아와야함
                    user = playerOrder[0].Item1,
                    card = myCardinfo                   
                });
        }

        public void AlertRoundStart()
        {
            webSocket.SendEvent<IngameStart>("/ingame", "start",
                new IngameStart
                {
                    //RKH6E {"mid" : "GWCSE1622", "nickname" : "김창렬"}
                    roomId = "RKH6E", // TODO: 추후 민호가 구현한거에서 받아와야함
                    round = currentRound,
                    user = new Userinfo
                    {
                        mid = "GWCSE1622",
                        nickname = "김창렬"
                    }

                });
        }

        public void ShowCardDeck(IngameCardGive response)
        {
            Debug.Log(response.cards);
            Debug.Log("Asfasf");
            List<string> cards = new List<string>();
            cards = response.cards;
            GetComponent<CardDB>().GetCard(response.cards);
            //TODO: response.cards 를 이용해서 화면에 카드 보여주는거 구현
        }

        public void ShowInitialPlayersInfo(IngamePlayerOrder response)
        {
            playerOrder = new (Userinfo, bool)[response.playerOrder.Count];
            /*for (int i = 0; i < response.playerOrder.Count; i++)
            {
                if (response.playerOrder[i].userInfo.mid == UserData.mid)
                {
                    myOrder = response.playerOrder[i].order;
                }
            }*/
            // 위 아래는 같은 로직
            foreach (var player in response.playerOrder)
            {
                if (player.userInfo.mid == UserData.mid)
                {
                    myOrder = player.order;
                }
                //TODO: response 데이터를 기반으로 화면에 유저들 정보를 보여주고, 내 순서 또한 확인해서 myOrder에 값을 넣음
            }
        }

        public void UpdateBoard(IngameStatus response)
        {
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
            }
            if (response.order == myOrder) // 이제 내 순서인거 , 누군가의 차례가 끝났을 때
            {
                webSocket.SendEvent<IngameStatus>("/ingame", "status",
                new IngameStatus
                {
                    order = (myOrder + 1) % playerOrder.Length
                });
            }
            // TODO: response 정보를 토대로 게임판을 업데이트하고, 플레이어가 포기를 했는지 안했는지 또한 체크
            foreach (var player in response.player)
            {

            }
        }

        public void StateChanged()
        {
            currentOrder = (currentOrder + 1) % playerOrder.Length;
            //모든유저 giveup -> 라운드 종료
            if (myOrder == currentOrder)
            {
                if(GetComponent<CheckUsable>().usableCard == true)
                {
                    gameState = new PlayingGameState();
                }
                else
                {
                    //give 정보를 보냄
                }
            }
            else
            {
                gameState = new WaitGameState();
            }
            // TODO: 내 순서이면 gameState = new PlayingGameState() , 내 순서 아니면 gameState = new WaitGameState().
            gameState.InStart();
        }

        public void TurnEnd()
        {
            gameState.InFinish();
        }
    }
}
