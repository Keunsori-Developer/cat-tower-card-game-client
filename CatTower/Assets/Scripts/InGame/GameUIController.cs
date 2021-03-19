using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CatTower
{
    public class GameUIController : SingletonGameObject<GameUIController>
    {
        [SerializeField] private Text roundText;
        [SerializeField] private GameObject playerListLayout;
        [SerializeField] private Text currentPlayerText;
        private SortedList<string, GameObject> playerInfo;
        private GameObject playerInfoPrefab;

        void Awake()
        {
            playerInfoPrefab = Resources.Load("Ingame/PlayerInfo") as GameObject;
            playerInfo = new SortedList<string, GameObject>();
        }

        void Start()
        {
            //TODO: 이거는 테스트하려고 추가한거니까 제거 필요
            // ShowInitialPlayerList(new List<PlayerOrder>{
            //     new PlayerOrder{
            //         score = 0, userInfo = new UserInfo{mid = "ASDFg", nickname = "승곰이"}
            //     }
            // });
            // HighlightCurrentPlayer(new UserInfo { mid = "ASDFg", nickname = "승곰이" });
            // 여기까지 제거
        }

        /// <summary>
        /// 게임이 처음 시작되었을 때 플레이어들의 정보를 표시합니다.
        /// 또한 내 닉네임은 노란색으로 표시합니다.
        /// </summary>
        /// <param name="players">표시할 플레이어들의 정보</param>
        public void ShowInitialPlayerList(List<PlayerOrder> players)
        {
            foreach (var player in players)
            {
                GameObject playerObject = Instantiate(playerInfoPrefab, playerListLayout.transform);
                var nickname = playerObject.transform.Find("nickname").GetComponent<Text>();
                nickname.text = player.userInfo.nickname;
                if (player.userInfo.mid == UserData.mid) nickname.color = Color.yellow;
                playerObject.transform.Find("score").GetComponent<Text>().text = "0";
                playerObject.transform.Find("giveup").gameObject.SetActive(false);

                playerInfo.Add(player.userInfo.mid, playerObject);
            }

            HighlightCurrentPlayer(players.Find(x => x.order == 0).userInfo); //TODO: 수정할거임(집에서)
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

            currentPlayerText.text = "<color=aqua>" + playerToHighlight.mid + "</color> 차례!";
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
    }
}