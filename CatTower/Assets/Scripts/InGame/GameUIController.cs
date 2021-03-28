using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CatTower
{
    public class GameUIController : SingletonGameObject<GameUIController>
    {
        [SerializeField] private Text roundText;
        [SerializeField] private GameObject roundPopup;
        [SerializeField] private GameObject playerListLayout;
        [SerializeField] private Text currentPlayerText;
        [SerializeField] private GameObject warning;
        [SerializeField] private GameObject soundButton;
        bool playingBGM = true;
        Sprite playImage;
        Sprite stopImage;

        private SortedList<string, GameObject> playerInfo;
        private GameObject playerInfoPrefab;

        void Awake()
        {
            playImage = Resources.Load<Sprite>("Ingame/audioPlay");
            stopImage = Resources.Load<Sprite>("Ingame/audioStop");

            playerInfoPrefab = Resources.Load("Ingame/PlayerInfo") as GameObject;
            playerInfo = new SortedList<string, GameObject>();
            soundButton.GetComponent<Button>().onClick.AddListener(SetBgmButtonStatus);
        }

        void Start()
        {
            
        }

        /// <summary>
        /// 게임이 처음 시작되었을 때 플레이어들의 정보, 현재 라운드 수를 표시합니다.
        /// 또한 내 닉네임은 노란색으로 표시합니다.
        /// 
        /// </summary>
        /// <param name="players">표시할 플레이어들의 정보</param>
        /// <param name="currentRound">현재 라운드</param>
        public void ShowInitialPlayerList(List<PlayerOrder> players, int currentRound)
        {
            DeletePlayerInfo();
            ShowCurrentRound(currentRound);

            foreach (var player in players)
            {
                GameObject playerObject = Instantiate(playerInfoPrefab, playerListLayout.transform);
                var nickname = playerObject.transform.Find("nickname").GetComponent<Text>();
                nickname.text = player.userInfo.nickname;
                if (player.userInfo.mid == UserData.mid) nickname.color = Color.yellow;
                playerObject.transform.Find("score").GetComponent<Text>().text = player.score.ToString();
                playerObject.transform.Find("giveup").gameObject.SetActive(false);

                playerInfo.Add(player.userInfo.mid, playerObject);
            }

            HighlightCurrentPlayer(players.Find(x => x.order == 0).userInfo);
        }

        /// <summary>
        /// 화면에 현재 차례인 플레이어가 누군지 한눈에 알 수 있도록 파란색으로 강조합니다.
        /// 또한 현재 순서를 텍스트로 표시합니다.
        /// </summary>
        /// <param name="playerToHighlight">현재 차례임을 표시할 플레이어 정보</param>
        private void HighlightCurrentPlayer(UserInfo playerToHighlight)
        {
            foreach (var go in playerInfo.Values)
            {
                // 모든 색상들을 흰색으로 변경
                go.GetComponent<Image>().color = new Color(1, 1, 1, 0.2666f);
            }

            // 파란색으로 바꿀 플레이어만 찾아서 바꿈
            GameObject gameObject = null;
            playerInfo.TryGetValue(playerToHighlight.mid, out gameObject);
            if (gameObject != null) gameObject.GetComponent<Image>().color = new Color(0, 1, 1, 0.2666f);

            currentPlayerText.text = "<color=aqua>" + playerToHighlight.nickname + "</color> 차례!";
        }

        /// <summary>
        /// 매 턴이 지날때마다 플레이어 정보들을 갱신해줍니다.
        /// 플레이어가 포기 상태라면 `포기!` 문구를 보여주고, 다음 턴에 해당하는 플레이어 정보를 화면에서 강조합니다.
        /// </summary>
        /// <param name="players">플레이어들의 정보</param>
        /// <param name="nextOrder">다음 차례</param>
        public void UpdatePlayerInfo(List<PlayerOrder> players, int nextOrder)
        {
            for (int i = 0; i < players.Count; i++)
            {
                GameObject player = null;
                playerInfo.TryGetValue(players[i].userInfo.mid, out player);
                if (player != null)
                {
                    player.transform.Find("score").GetComponent<Text>().text = players[i].score.ToString();
                    if (players[i].giveup) player.transform.Find("giveup").gameObject.SetActive(true);
                }
            }

            HighlightCurrentPlayer(players[nextOrder].userInfo);
        }

        /// <summary>
        /// 현재 슬롯에 카드를 올려둘 수 없다는 경고 문구를 띄웁니다.
        /// </summary>
        public void WarningWrongPosition()
        {
            StartCoroutine(ShowWarningText());
        }

        private IEnumerator ShowWarningText()
        {
            warning.SetActive(true);
            yield return new WaitForSecondsRealtime(0.5f);
            warning.SetActive(false);
        }

        public void DeletePlayerInfo()
        {
            var count = playerListLayout.transform.childCount;

            for (int i = 0; i < count; i++)
            {
                Destroy(playerListLayout.transform.GetChild(i).gameObject);
            }
            playerInfo.Clear();
        }

        /// <summary>
        /// 버튼을 누를 때마다 BGM을 끄고 켭니다.
        /// 현재 BGM 상태에 따라 버튼 이미지도 변경합니다.
        /// </summary>
        private void SetBgmButtonStatus()
        {
            var audioSource = soundButton.GetComponent<AudioSource>();
            var image = soundButton.GetComponent<Image>();
            if (playingBGM)
            {
                playingBGM = false;
                audioSource.Stop();
                image.sprite = stopImage;
            }
            else
            {
                playingBGM = true;
                audioSource.Play();
                image.sprite = playImage;
            }
        }

        public void ShowCurrentRound(int round)
        {
            StartCoroutine(PopupCurrentRound(round));
        }

        /// <summary>
        /// 각 라운드가 시작될 때, 현재 라운드 정보를 보여줍니다.
        /// </summary>
        /// <param name="round">현재 라운드 수</param>
        /// <returns></returns>
        private IEnumerator PopupCurrentRound(int round)
        {
            roundPopup.SetActive(true);
            roundText.gameObject.SetActive(false);
            var roundtextInPopup = roundPopup.transform.Find("Text").gameObject;
            roundtextInPopup.GetComponent<Text>().text = "Round <color=yellow>" + round + "</color>";
            yield return new WaitForSecondsRealtime(1.5f);
            roundPopup.SetActive(false);
            roundText.text = "Round " + round;
            roundText.gameObject.SetActive(true);
        }
    }
}