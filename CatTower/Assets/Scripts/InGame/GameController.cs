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
        public Dictionary<string, Breed> cardAlphabet;
        
        AudioClip catClip;
        AudioSource audioSource;

        void Awake()
        {
            catClip = Resources.Load("Sound/yatong") as AudioClip;
            
            cardAlphabet = new Dictionary<string, Breed>
            {
                {"A", Breed.Mackerel},
                {"B", Breed.Siamese},
                {"C", Breed.Persian},
                {"D", Breed.Ragdoll},
                {"E", Breed.RussianBlue},
                {"S0", Breed.Savanna},
                {"S1", Breed.ThreeColor},
                {"S2", Breed.Odd}
            };

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
                    AlertRoundStart();
                });
                webSocket.ReceiveEvent<IngameResult>("/ingame", "result", ShowGameResult);
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
            currentRound++;

            webSocket.SendEvent<IngameStart>("/ingame", "start",
                new IngameStart
                {
                    roomId = JoinedRoom.roomId,
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
            myCards.GetComponent<MyCardDeck>().GetCard(response.cards);

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
            uiController.ShowInitialPlayerList(response.player, currentRound);
            StateChanged();
        }

        public void UpdateBoard(IngameStatus response)
        {
            uiController.UpdatePlayerInfo(response.player, response.order);

            currentOrder = response.order;

            var board = response.board;
            var boardSize = board.Count;
            for (int i = 0; i < boardSize; i++)
            {
                if (Slot.GetComponent<SlotManager>().arrSlotIndex[i] == 1) continue; // 이미 확인된 슬롯이라 체크 안함

                Breed breedValue;
                if (!cardAlphabet.TryGetValue(board[i], out breedValue)) continue;
                var slotManager = Slot.GetComponent<SlotManager>();
                slotManager.arrSlotIndex[i] = 1;
                slotManager.arrSlotBreed[i] = breedValue;
                slotManager.SetSprite(i, breedValue);
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
            myCards.GetComponent<CheckUsable>().ResetBr();
            myCards.GetComponent<CheckUsable>().CheckBr();
            myCards.GetComponent<CheckUsable>().CheckCard();
            //모든유저 giveup -> 라운드 종료
            if (myOrder == currentOrder)
            {
                gameState = new PlayingGameState();
                RingingCatSound();
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
                        user = player.Item1,
                        roomId = JoinedRoom.roomId,
                        leftCard = myCards.GetComponent<CheckUsable>().myScore
                    });
                    break;
                }
            }
        }

        private void RingingCatSound()
        {
            bool isExistAudioSource = this.TryGetComponent<AudioSource>(out audioSource);
            if (!isExistAudioSource) audioSource = this.gameObject.AddComponent<AudioSource>();
            audioSource.clip = catClip;
            audioSource.Play();
        }

        private void ShowGameResult(IngameResult result)
        {
            Debug.Log("게임 끝");
            
            GameResultUIcontroller.Instance.ShowGameResult(result);
        }
    }
}
